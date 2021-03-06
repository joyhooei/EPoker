namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using yigame.epoker;
	using UniRx;
	using uFrame.MVVM;
	using uFrame.Kernel;
	using uFrame.IOC;
	using uFrame.Serialization;
	using ExitGames.Client.Photon.LoadBalancing;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using GameDataEditor;

	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WII || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS3 || UNITY_XBOX360 || UNITY_NACL  || UNITY_FLASH  || UNITY_BLACKBERRY
	using Hashtable = ExitGames.Client.Photon.Hashtable;
	#endif
    
	public class PlayerController : PlayerControllerBase
	{
		[Inject] public Network Network;
		[Inject] public GameService GameService;
		//		[Inject ("CoreGameRoot")] public CoreGameRootViewModel CoreGameRoot;

		public override void InitializePlayer (PlayerViewModel viewModel)
		{
			base.InitializePlayer (viewModel);
		}

		public override void PlayerReady (PlayerViewModel viewModel)
		{
			base.PlayerReady (viewModel);
			if (viewModel.IsSelf) {
				Hashtable ht = new Hashtable ();
				ht.Add ("is_ready", true);
				Publish (new NetSetPlayerProperties () {
					ActorId = viewModel.ActorId,
					PropertiesToSet = ht
				});
			}
		}

		public override void PlayerCancel (PlayerViewModel viewModel)
		{
			base.PlayerCancel (viewModel);
			if (viewModel.IsSelf) {
				Hashtable ht = new Hashtable ();
				ht.Add ("is_ready", false);
				Publish (new NetSetPlayerProperties () {
					ActorId = viewModel.ActorId,
					PropertiesToSet = ht
				});
			}
		}

		public override void MatchBegan (PlayerViewModel viewModel)
		{
			base.MatchBegan (viewModel);

			viewModel.HandCards.Clear ();

			Hashtable ht = new Hashtable ();
			ht.Add ("is_ready", false);
			Publish (new NetSetPlayerProperties () {
				ActorId = viewModel.ActorId,
				PropertiesToSet = ht
			});

			string hand_cards_str = Convert.ToString (viewModel.LBPlayer.CustomProperties ["hand_cards"]);
			List<CardInfo> card_info_list = JsonConvert.DeserializeObject<List<CardInfo>> (hand_cards_str);

			viewModel.Execute (new AddCardsCommand () {
				CardInfos = card_info_list
			});

		}

		public override void BeganToPlay (PlayerViewModel viewModel)
		{
			base.BeganToPlay (viewModel);
		}

		public override void BeganToWait (PlayerViewModel viewModel)
		{
			base.BeganToWait (viewModel);
		}

		public override void TurnOn (PlayerViewModel viewModel)
		{
			base.TurnOn (viewModel);
			viewModel.ExecuteRefreshButtonDealEnabled ();
		}

		public override void TurnOff (PlayerViewModel viewModel)
		{
			base.TurnOff (viewModel);
		}

		public override void Win (PlayerViewModel viewModel)
		{
			base.Win (viewModel);
		}

		public override void Over (PlayerViewModel viewModel)
		{
			base.Over (viewModel);
		}

		public override void InitOK (PlayerViewModel viewModel)
		{
			base.InitOK (viewModel);
		}

		public override void RefreshPlayer (PlayerViewModel viewModel)
		{
			base.RefreshPlayer (viewModel);

			if (viewModel.IsSelf == false) {
				// ready 按钮
				bool is_ready = false;
				if (viewModel.LBPlayer.CustomProperties.ContainsKey ("is_ready")) {
					is_ready = Convert.ToBoolean (viewModel.LBPlayer.CustomProperties ["is_ready"]);
				}
				if (is_ready && viewModel.Status is Ready == false) {
					viewModel.ExecutePlayerReady ();
				} else if (is_ready == false && viewModel.Status is Wait == false) {
					viewModel.ExecutePlayerCancel ();
				}
			}

			// turn
			object my_turn = false;
			if (viewModel.LBPlayer.CustomProperties.TryGetValue ("my_turn", out my_turn)) {
				if (viewModel.Status is MatchDeal && Convert.ToBoolean (my_turn) == false) {

					object is_win = false;
					if (viewModel.LBPlayer.CustomProperties.TryGetValue ("is_win", out is_win)) {
						if (Convert.ToBoolean (is_win)) {
							viewModel.ExecuteWin ();
							CoreGameRoot.ExecuteRefreshSummaryPlayersList ();
						} else {
							viewModel.ExecuteTurnOff ();
						}
					}

				} else if (viewModel.Status is MatchIdle && Convert.ToBoolean (my_turn)) {
					viewModel.ExecuteTurnOn ();
				}
			}

		}

		public override void ButtonReadyClicked (PlayerViewModel viewModel)
		{
			base.ButtonReadyClicked (viewModel);

			if (viewModel.Status is Wait || viewModel.Status is Init || viewModel.Status is MatchOver) {
				viewModel.ExecutePlayerReady ();	
			} else if (viewModel.Status is Ready) {
				viewModel.ExecutePlayerCancel ();
			}

			viewModel.ExecuteRefreshPlayer ();
		}

		public override void ButtonStartClicked (PlayerViewModel viewModel)
		{
			base.ButtonStartClicked (viewModel);

			if (viewModel.PlayerRoomIdentity == RoomIdentity.RoomMaster) {

				// 点击开始按钮后,生成每个人的手牌
				List<CardInfo> card_info = GameService.GetDeck (true);
				Dictionary<int, List<CardInfo>> card_info_dic = Network.Client.CurrentRoom.Players.ToDictionary (kv1 => kv1.Key, kv2 => new List<CardInfo> ());

				int first_get_actor_id = Network.Client.LocalPlayer.ID;
				try {
					first_get_actor_id = Network.Client.CurrentRoom.Players.Where (kv => {
						return kv.Value.CustomProperties.ContainsKey ("first_get")
						&& Convert.ToBoolean (kv.Value.CustomProperties ["first_get"]);
					}).Select (kv => kv.Key).Single ();
				} catch (Exception ex) {
					UnityEngine.Debug.Log ("没有指定先抓牌的 actorid, 使用 RoomMaster 的 actorid: " + ex.Message);
				}

				List<int> actor_id_list = Network.Client.CurrentRoom.Players.Select (kv => kv.Key).OrderBy (id => id).ToList ();
				bool has_find_first = false;

				int first_turn_actor_id = -1;

				try {
					first_turn_actor_id = Network.Client.CurrentRoom.Players.Where (kv => {
						return kv.Value.CustomProperties.ContainsKey ("rank");
					}).OrderBy (_ => Convert.ToInt32 (_.Value.CustomProperties ["rank"])).Select (kv2 => kv2.Value.ID).First ();
				} catch (Exception ex) {
					UnityEngine.Debug.Log ("没有指定先出牌的 actorid, 使用 先抓牌者 的 actorid: " + ex.Message);
				}

				Dictionary<int, int> teamDic = new Dictionary<int, int> ();
				foreach (int actor_id in actor_id_list) {
					// 2队为普通队;
					teamDic.Add (actor_id, 2);

					if (GameService.GameMode == GameMode.m_2p_r40c) {
						if (actor_id_list [0] == actor_id) {
							teamDic [actor_id] = 1;
						}
					}
				}

				int i = 0;
				while (i < card_info.Count) {

					foreach (int actor_id in actor_id_list) {
						if (actor_id == first_get_actor_id) {
							has_find_first = true;
						}

						if (has_find_first) {
							CardInfo ci = card_info [i];
							card_info_dic [actor_id].Add (ci);

//							if (ci.NumericalValue == NumericalValue.NV_BIG_JOKER && first_turn_actor_id == -1) {
//								first_turn_actor_id = actor_id;
//							}

							// 未测试
							if (GameService.GameMode == GameMode.m_3p_spade_ace) {
								if (CardInfo.ValueEqual (ci, new CardInfo (Suit.SPADE, NumericalValue.NV_ACE))) {
									// 1队为黑桃 ACE 队
									teamDic [actor_id] = 1;
								}
							}

							if (++i >= card_info.Count) {
								break;
							}
						}
					}
				}

				if (first_turn_actor_id == -1) {
					first_turn_actor_id = first_get_actor_id;
				}

				foreach (int actor_id in actor_id_list) {

					Hashtable ht = new Hashtable ();
					ht.Add ("hand_cards", JsonConvert.SerializeObject (card_info_dic [actor_id]));
					ht.Add ("first_get", false);
					ht.Add ("my_turn", actor_id == first_turn_actor_id);
					ht.Add ("is_win", false);
					ht.Add ("rank", -1);
					ht.Add ("team_id", teamDic [actor_id]);
					ht.Add ("is_team_win", false);
					ht.Add ("first_deal", false);

					Publish (new NetSetPlayerProperties () {
						ActorId = actor_id,
						PropertiesToSet = ht
					});
				}

				Hashtable ht2 = new Hashtable ();
				ht2.Add ("is_playing", true);
				ht2.Add ("current_cards", JsonConvert.SerializeObject (new List<CardInfo> ()));
				ht2.Add ("current_cards_actor_id", -1);
				ht2.Add ("active_actor_id", first_turn_actor_id);

				Publish (new NetSetRoomProperties () {
					PropertiesToSet = ht2
				});

				// 发出事件,开始抓牌
				Publish (new NetRaiseEvent () {
					EventCode = GameService.EventCode.MatchBegan
				});

				CoreGameRoot.ExecuteRootMatchBegan ();

			}

		}

		public override void LogInfo (PlayerViewModel viewModel)
		{
			base.LogInfo (viewModel);

			UnityEngine.Debug.Log (viewModel.LBPlayer.ToString ());
		}

		public override void AddCards (PlayerViewModel viewModel, AddCardsCommand arg)
		{
			base.AddCards (viewModel, arg);

			foreach (CardInfo ci in arg.CardInfos) {
				CardViewModel card = MVVMKernelExtensions.CreateViewModel<CardViewModel> ();
				card.Info = ci;

				if (viewModel.IsSelf) {
					card.Face = CardFace.FaceUp;
				} else {
					card.Face = CardFace.FaceDown;
				}

				card.Place = CardPlace.Floor;
				card.OwnerActorId = viewModel.ActorId;
				viewModel.HandCards.Add (card);
			}

			viewModel.ExecuteReorder ();
		}

		public override void RemoveCards (PlayerViewModel viewModel, RemoveCardsCommand arg)
		{
			base.RemoveCards (viewModel, arg);
			foreach (CardInfo ci in arg.CardInfos) {
				CardViewModel card = viewModel.HandCards.FirstOrDefault (cardVM => CardInfo.ValueEqual (cardVM.Info, ci));
				viewModel.HandCards.Remove (card);
			}

			viewModel.ExecuteReorder ();
		}

		public override void Reorder (PlayerViewModel viewModel)
		{
			base.Reorder (viewModel);

			List<CardViewModel> cards = viewModel.HandCards.ToList ();

			cards.Sort ((a, b) => {
				return CardInfo.SingleInHandCompare (a.Info, b.Info);
			});

			cards.Select ((vm, idx) => {
				return new {vm, idx};
			}).ToList ().ForEach (t => {
				t.vm.PosIdx = t.idx;
				t.vm.TotalCount = cards.Count;
			});
		}

		public override void ButtonPassClicked (PlayerViewModel viewModel)
		{
			base.ButtonPassClicked (viewModel);
			if (viewModel.Status is MatchDeal) {
				viewModel.HandCards.Where (cardVM => {
					return cardVM.IsSelected;
				}).ToList ().ForEach (_ => _.ExecuteDeselectCard ());

				CoreGameRoot.ExecuteTurnNext ();

				Publish (new NetRaiseEvent () {
					EventCode = GameService.EventCode.PassAndTurnNext
				});

				CoreGameRoot.ExecuteRefreshCoreGame ();
			}
		}

		public override void ButtonDealClicked (PlayerViewModel viewModel)
		{
			base.ButtonDealClicked (viewModel);
			if (viewModel.Status is MatchDeal) {

				List<CardInfo> cardInfoList = viewModel.CurrentSelectedCards;

				// 检查是否需要设置is_win 标志
				if (viewModel.HandCards.Count == cardInfoList.Count) {

					int rank = CoreGameRoot.WinPlayersCount + 1;

					Hashtable ht = new Hashtable ();
					ht.Add ("is_win", true);
					ht.Add ("rank", rank);
					if (rank == 1) {
						ht.Add ("first_get", true);
					}

					Publish (new NetSetPlayerProperties () {
						ActorId = viewModel.ActorId,
						PropertiesToSet = ht
					});
				}

				Hashtable ht2 = new Hashtable ();
				ht2.Add ("current_cards", JsonConvert.SerializeObject (cardInfoList));
				ht2.Add ("current_cards_actor_id", viewModel.ActorId);

				Publish (new NetSetRoomProperties () {
					PropertiesToSet = ht2
				});

				CoreGameRoot.ExecuteTurnNext ();

				Publish (new NetRaiseEvent () {
					EventCode = GameService.EventCode.ShowCardAndTurnNext
				});

				viewModel.ExecuteShowCardsToPile ();
				CoreGameRoot.ExecuteRefreshCoreGame ();

			}
		}

		public override void ShowCardsToPile (PlayerViewModel viewModel)
		{
			base.ShowCardsToPile (viewModel);

			List<CardInfo> cardInfoList = JsonConvert.DeserializeObject<List<CardInfo>> (
				                              Convert.ToString (Network.Client.CurrentRoom.CustomProperties ["current_cards"])
			                              );

			// 清除牌堆原有的牌
			CoreGameRoot.Pile.Cards.Clear ();

			foreach (CardInfo ci in cardInfoList) {
				// 1.查找手牌的这一张
				CardViewModel card = viewModel.HandCards.Where (cardVM => CardInfo.ValueEqual (cardVM.Info, ci)).FirstOrDefault ();
				if (card != null) {

					// 1.1.还原一些属性
					card.ExecuteDeselectCard ();
					card.OwnerActorId = -1;
					card.Face = CardFace.FaceUp;

					// 2.牌堆中加入相同的这一张牌
					CoreGameRoot.Pile.Cards.Add (card);

					// 3.删除手牌这一张
					viewModel.HandCards.Remove (card);
				}
			}

			viewModel.ExecuteReorder ();
			CoreGameRoot.Pile.ExecutePileCardsReorder ();

		}

		public override void RefreshButtonDealEnabled (PlayerViewModel viewModel)
		{
			base.RefreshButtonDealEnabled (viewModel);

			if (viewModel.IsSelf) {

				int current_cards_actor_id = Convert.ToInt32 (Network.Client.CurrentRoom.CustomProperties ["current_cards_actor_id"]);

				if (current_cards_actor_id == -1) {
					// 第一手牌
					List<CardInfo> current_selected_cards = viewModel.CurrentSelectedCards;
					bool is_deal = GameService.IsDealCards (current_selected_cards);
					viewModel.ButtonDealEnable = is_deal;

				} else if (current_cards_actor_id == viewModel.ActorId) {
					// 自己上轮大
					List<CardInfo> current_selected_cards = viewModel.CurrentSelectedCards;
					bool is_deal = GameService.IsDealCards (current_selected_cards);
					viewModel.ButtonDealEnable = is_deal;

				} else {
					// 需要大过当前牌
					string current_cards_str = Convert.ToString (Network.Client.CurrentRoom.CustomProperties ["current_cards"]);
					List<CardInfo> current_cards = JsonConvert.DeserializeObject<List<CardInfo>> (current_cards_str);
					List<CardInfo> current_selected_cards = viewModel.CurrentSelectedCards;

					bool is_larger = GameService.IsLargerCards (current_selected_cards, current_cards);

					GDESDebugData dd = new GDESDebugData (GDEItemKeys.SDebug_DefaultDebug);
					if (dd.FreeDealCardsRuleForTest) {
						is_larger = true;
					}

					viewModel.ButtonDealEnable = is_larger;
				}
			}
		}
	}
}
