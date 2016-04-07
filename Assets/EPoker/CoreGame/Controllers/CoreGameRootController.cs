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

		public override void RootMatchBegan (CoreGameRootViewModel viewModel)
		{
			base.RootMatchBegan (viewModel);

			viewModel.Pile.Cards.Clear ();

			viewModel.PlayerCollection.ToList ().ForEach (playerVM => {
				playerVM.ExecuteMatchBegan ();
			});
		}

		public override void RootMatchOver (CoreGameRootViewModel viewModel)
		{
			base.RootMatchOver (viewModel);

			viewModel.PlayerCollection.ToList ().ForEach (playerVM => {
				playerVM.ExecuteOver ();
			});
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
			viewModel.ExecuteRefreshCoreGame ();
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
			viewModel.ExecuteRefreshCoreGame ();
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
				kv.vm.OrderIdx = kv._idx;
				kv.vm.PosId = jArrangeItem [idx_in_players].Value<string> ();
				kv.vm.PlayerRoomIdentity = kv.vm.LBPlayer.IsMasterClient ? RoomIdentity.RoomMaster : RoomIdentity.RoomGuest;
			});
		}

		public override void TurnNext (CoreGameRootViewModel viewModel)
		{
			base.TurnNext (viewModel);
			int active_actor_id = Convert.ToInt32 (Network.Client.CurrentRoom.CustomProperties ["active_actor_id"]);

			Hashtable ht1 = new Hashtable ();
			ht1.Add ("my_turn", false);
			Publish (new NetSetPlayerProperties () {
				ActorId = active_actor_id,
				PropertiesToSet = ht1
			});

			int actor_id = active_actor_id;
			int player_count = viewModel.PlayerCollection.Count;
			do {
				int orderIdx = viewModel.PlayerCollection.SingleOrDefault (playerVM => playerVM.ActorId == actor_id).OrderIdx;
				orderIdx++;
				if (orderIdx >= player_count) {
					orderIdx = 0;
				}
				actor_id = viewModel.PlayerCollection.SingleOrDefault (playerVM => playerVM.OrderIdx == orderIdx).ActorId;

				bool correct = true;
				if (Convert.ToBoolean (Network.Client.CurrentRoom.GetPlayer (actor_id).CustomProperties ["is_win"])) {
					correct = false;
				}

				if (correct) {
					break;
				}

			} while(actor_id != active_actor_id);

			if (viewModel.WinPlayersCount == Network.Client.CurrentRoom.PlayerCount - 1) {

				Hashtable ht = new Hashtable ();
				ht.Add ("active_actor_id", -1);
				Publish (new NetSetRoomProperties () {
					PropertiesToSet = ht
				});

				Publish (new NetRaiseEvent () {
					EventCode = GameService.EventCode.MatchOver
				});

				viewModel.ExecuteRootMatchOver ();

			} else {

				Hashtable ht2 = new Hashtable ();
				ht2.Add ("my_turn", true);
				Publish (new NetSetPlayerProperties () {
					ActorId = actor_id,
					PropertiesToSet = ht2
				});

				Hashtable ht = new Hashtable ();
				ht.Add ("active_actor_id", actor_id);
				Publish (new NetSetRoomProperties () {
					PropertiesToSet = ht
				});
			}

		}

	}
}
