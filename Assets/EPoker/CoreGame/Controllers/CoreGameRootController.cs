namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.Kernel;
	using uFrame.IOC;
	using uFrame.MVVM;
	using uFrame.Serialization;
	using UniRx;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using ExitGames.Client.Photon.LoadBalancing;

	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WII || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS3 || UNITY_XBOX360 || UNITY_NACL  || UNITY_FLASH  || UNITY_BLACKBERRY
	using Hashtable = ExitGames.Client.Photon.Hashtable;
	#endif

	public class CoreGameRootController : CoreGameRootControllerBase
	{
		[Inject] public Network Network;
		[Inject] public GameService GameService;
		[Inject ("OutOfGameRoot")] public OutOfGameRootViewModel OutOfGameRoot;

		public override void InitializeCoreGameRoot (CoreGameRootViewModel viewModel)
		{
			base.InitializeCoreGameRoot (viewModel);
		}


		public override void RefreshCoreGame (CoreGameRootViewModel viewModel)
		{
			base.RefreshCoreGame (viewModel);

			viewModel.PlayerCollection.ToList ().ForEach (vm => {
				vm.ExecuteRefreshPlayer ();
			});
		}

		//		public override void ResetPlayerCount (CoreGameRootViewModel viewModel)
		//		{
		//			base.ResetPlayerCount (viewModel);
		//
		//			int count = viewModel.InfoJson ["player_count"].Value<int> ();
		//			int idx = viewModel.InfoJson ["players"]
		//				.Select ((jt, _idx) => new {jt, _idx})
		//				.Where (kv => kv.jt ["display_name"].Value<string> () == Network.Client.PlayerName)
		//				.Select (kv => kv._idx)
		//				.Single ();
		//
		//			JObject jArrange = JObject.Parse (posid_arrange_player_count);
		//			JArray jArrangeItem = jArrange [count.ToString ()] as JArray;
		//
		//			viewModel.PlayerCount = count;
		//
		////			viewModel.PlayerCollection.Clear ();
		//			List<PlayerViewModel> vm_need_remove = new List<PlayerViewModel> ();
		//			viewModel.PlayerCollection.ToList ().ForEach (vm => {
		//				if (viewModel.InfoJson ["players"].ToList ().Exists (jt => (jt as JObject) ["actor_id"].Value<int> () == vm.ActorId) == false) {
		//					vm_need_remove.Add (vm);
		//				}
		//			});
		//
		//			vm_need_remove.ForEach (vm => viewModel.PlayerCollection.Remove (vm));
		//
		//			for (int i = 0; i < count; i++) {
		//
		//				int idx_in_players = (i + idx) % count;
		//				var jPlayer = viewModel.InfoJson ["players"] [idx_in_players];
		//
		//				PlayerViewModel player = null;
		//				bool newly_created = false;
		//				if (viewModel.PlayerCollection.ToList ().Exists (vm => (vm as PlayerViewModel).ActorId == jPlayer ["actor_id"].Value<int> ())) {
		//					player = viewModel.PlayerCollection.Where (vm => (vm as PlayerViewModel).ActorId == jPlayer ["actor_id"].Value<int> ()).Single ();
		//				} else {
		//					player = MVVMKernelExtensions.CreateViewModel<PlayerViewModel> ();
		//					newly_created = true;
		//				}
		//
		//				player.PosId = jArrangeItem [i].Value<string> ();
		//				player.Id = jPlayer ["playfab_id"].Value<string> ();
		//				player.ActorId = jPlayer ["actor_id"].Value<int> ();
		//				player.DisplayName = jPlayer ["display_name"].Value<string> ();
		//				player.PlayerRoomIdentity = (RoomIdentity)Enum.Parse (typeof(RoomIdentity), jPlayer ["player_room_identity"].Value<string> ());
		//				player.IsSelf = i == 0;
		//
		//				if (newly_created) {
		//					viewModel.PlayerCollection.Add (player);
		//				}
		//
		//				player.ExecuteRefreshPlayer ();
		//			}
		//		}

		public override void RootMatchBegan (CoreGameRootViewModel viewModel)
		{
			base.RootMatchBegan (viewModel);

			viewModel.PlayerCollection.ToList ().ForEach (playerVM => {
				playerVM.ExecuteMatchBegan ();
			});
		}

		public override void CreateDeckToPile (CoreGameRootViewModel viewModel)
		{
			base.CreateDeckToPile (viewModel);

//			List<CardInfo> card_info_list = this.GameService.GetDeck (true);
//			JObject jInfo = CoreGameRoot.InfoJson;
////			jInfo ["pile_for_show"] = JArray.Parse (JsonConvert.SerializeObject (card_info_list.Select (ci => ci.ToString ())));
//
//
//			int get_card_first_idx = jInfo ["players"]
//				.Where (jp => jp ["get_card_first"].Value<bool> ())
//				.Select (jp => jp ["idx"].Value<int> ())
//				.Single ();
//
//			int i = get_card_first_idx;
//			card_info_list.ForEach (ci => {
//				JArray j_hand_cards = jInfo ["players"] [i] ["hand_cards"] as JArray;
//				j_hand_cards.Add (ci.ToString ());
//				i = (i + 1) % viewModel.PlayerCount;
//			});
//
//			UnityEngine.Debug.Log ("jInfo: " + JsonConvert.SerializeObject (jInfo, Formatting.Indented));
//
//			Publish (new UploadInfoJson ());
		}

		public override void DealPile (CoreGameRootViewModel viewModel)
		{
			base.DealPile (viewModel);
		}

		public override void QuitCoreGame (CoreGameRootViewModel viewModel)
		{
			base.QuitCoreGame (viewModel);

			Publish (new NetLeaveRoom ());
		}

		public override void PlayerJoin (CoreGameRootViewModel viewModel)
		{
			base.PlayerJoin (viewModel);
			// 在 LBRoom 中查找 PlayerCollection 中没有的, 加入
			Network.Client.CurrentRoom.Players.OrderBy (_ => _.Key).ToList ().ForEach (kv => {
				int actorId = kv.Key;
				Player player = kv.Value;

				if (viewModel.PlayerCollection.ToList ().Exists (vm => vm.ActorId == actorId) == false) {
					PlayerViewModel playerVM = MVVMKernelExtensions.CreateViewModel<PlayerViewModel> ();
					playerVM.ActorId = player.ID;
					playerVM.PlayerName = player.Name;
					playerVM.IsSelf = player.IsLocal;
					playerVM.PlayerRoomIdentity = player.IsMasterClient ? RoomIdentity.RoomMaster : RoomIdentity.RoomGuest;
					playerVM.LBPlayer = player;

					viewModel.PlayerCollection.Add (playerVM);
				}
			});

			viewModel.ExecuteCalcPosIdAndRepos ();
		}

		public override void PlayerLeave (CoreGameRootViewModel viewModel)
		{
			base.PlayerLeave (viewModel);

			// 在 PlayerCollection 中查找 LBRoom 中没有的, 删除
			List<PlayerViewModel> player_need_remove = new List<PlayerViewModel> ();

			viewModel.PlayerCollection.ToList ().ForEach (vm => {
				if (Network.Client.CurrentRoom.Players.ToList ().Exists (kv => kv.Value.ID == vm.ActorId) == false) {
					player_need_remove.Add (vm);
				}
			});

			foreach (var vm in player_need_remove) {
				viewModel.PlayerCollection.Remove (vm);
			}

			viewModel.ExecuteCalcPosIdAndRepos ();
		}

		public static string posid_arrange_player_count = @"{
			1: [0],
			2: [0,3],
			3: [0,2,4],
			4: [0,1,3,5],
			5: [0,1,2,4,5],
			6: [0,1,2,3,4,5]
		}";

		public override void CalcPosIdAndRepos (CoreGameRootViewModel viewModel)
		{
			base.CalcPosIdAndRepos (viewModel);

			int count = Network.Client.CurrentRoom.Players.Count;

			int idx = viewModel.PlayerCollection.OrderBy (o_vm => o_vm.ActorId).ToList ()
				.Select ((vm, _idx) => new {vm, _idx})
				.Where (kv => kv.vm.PlayerName == Network.Client.PlayerName)
				.Select (kv => kv._idx)
				.Single ();

			JObject jArrange = JObject.Parse (posid_arrange_player_count);
			JArray jArrangeItem = jArrange [count.ToString ()] as JArray;

			viewModel.PlayerCollection.OrderBy (o_vm => o_vm.ActorId).ToList ()
				.Select ((vm, _idx) => new {vm, _idx})
				.ToList ()
				.ForEach (kv => {
				int idx_in_players = (kv._idx + count - idx) % count;
				kv.vm.PosId = jArrangeItem [idx_in_players].Value<string> ();
			});
		}
	}
}
