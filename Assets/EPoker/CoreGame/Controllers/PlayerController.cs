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

	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WII || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS3 || UNITY_XBOX360 || UNITY_NACL  || UNITY_FLASH  || UNITY_BLACKBERRY
	using Hashtable = ExitGames.Client.Photon.Hashtable;
	#endif
    
	public class PlayerController : PlayerControllerBase
	{
		[Inject] public Network Network;
		[Inject] public GameService GameService;
		[Inject ("CoreGameRoot")] public CoreGameRootViewModel CoreGameRoot;

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
					viewModel.ExecuteTurnOff ();
				} else if (viewModel.Status is MatchIdle && Convert.ToBoolean (my_turn)) {
					viewModel.ExecuteTurnOn ();
				}
			}

		}

		public override void ButtonReadyClicked (PlayerViewModel viewModel)
		{
			base.ButtonReadyClicked (viewModel);

			if (viewModel.Status is Wait || viewModel.Status is Init) {
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

				int i = 0;
				while (i < card_info.Count) {

					foreach (int actor_id in actor_id_list) {
						if (actor_id == first_get_actor_id) {
							has_find_first = true;
						}

						if (has_find_first) {
							CardInfo ci = card_info [i];
							card_info_dic [actor_id].Add (ci);

							if (ci.NumericalValue == NumericalValue.NV_BIG_JOKER && first_turn_actor_id == -1) {
								first_turn_actor_id = actor_id;
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
					ht.Add ("first_get", actor_id == first_get_actor_id);
					ht.Add ("my_turn", actor_id == first_turn_actor_id);
					ht.Add ("is_win", false);

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

				viewModel.CoreGameRoot.PlayerCollection.ToList ().ForEach (vm => {
					vm.ExecuteMatchBegan ();
				});

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
				card.Face = CardFace.FaceUp;
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

				List<CardInfo> cardInfoList = viewModel.HandCards.Where (cardVM => cardVM.IsSelected).Select (_ => _.Info).ToList ();

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

					// 2.牌堆中加入相同的这一张牌
					CoreGameRoot.Pile.Cards.Add (card);

					// 3.删除手牌这一张
					viewModel.HandCards.Remove (card);
				}
			}

			viewModel.ExecuteReorder ();
			CoreGameRoot.Pile.ExecutePileCardsReorder ();

		}
	}
}
