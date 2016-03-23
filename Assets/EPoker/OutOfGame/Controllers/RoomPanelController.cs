namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UniRx;
	using uFrame.Serialization;
	using uFrame.IOC;
	using uFrame.Kernel;
	using uFrame.MVVM;
	using ExitGames.Client.Photon.LoadBalancing;
	using Newtonsoft.Json;

	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WII || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS3 || UNITY_XBOX360 || UNITY_NACL  || UNITY_FLASH  || UNITY_BLACKBERRY
	using Hashtable = ExitGames.Client.Photon.Hashtable;
	#endif

	public class RoomPanelController : RoomPanelControllerBase
	{
		[Inject] public Network Network;

		public override void InitializeRoomPanel (RoomPanelViewModel viewModel)
		{
			base.InitializeRoomPanel (viewModel);
			// This is called when a RoomPanelViewModel is created
		}

		public override void QuitRoom (RoomPanelViewModel viewModel)
		{
			base.QuitRoom (viewModel);
			Publish (new NetLeaveRoom ());
		}

		public override void RefreshRoom (RoomPanelViewModel viewModel)
		{
			base.RefreshRoom (viewModel);

			viewModel.PlayerItems.Clear ();

			Dictionary<int, Player> playerDic = Network.Client.CurrentRoom.Players;

			playerDic.OrderBy (kv => kv.Value.ID).ToList ()
				.ForEach (kv => {
				PlayerItemViewModel vm = MVVMKernelExtensions.CreateViewModel<PlayerItemViewModel> ();
				vm.Player = kv.Value;
				vm.ActerId = kv.Value.ID;
				vm.Name = kv.Value.Name;
				vm.IsLocal = kv.Value.IsLocal;
				viewModel.PlayerItems.Add (vm);
			});
		}

		public override void RefreshRoomProperties (RoomPanelViewModel viewModel)
		{
			base.RefreshRoomProperties (viewModel);
		}

		public override void SetProperties (RoomPanelViewModel viewModel)
		{
			base.SetProperties (viewModel);

			Hashtable hashtable = new Hashtable ();
			hashtable.Add ("room_property", viewModel.RoomPropertiesJson);

			Publish (new NetSetRoomProperties () {
				PropertiesToSet = hashtable
			});
		}

		public override void SendEvent (RoomPanelViewModel viewModel)
		{
			base.SendEvent (viewModel);

			Hashtable hashtable = new Hashtable ();
			hashtable.Add ("event_content", viewModel.EventParamsJson);

			Publish (new NetRaiseEvent () {
				EventCode = 19,
				EventContent = hashtable
			});
		}

		public override void RefreshPlayerProperties (RoomPanelViewModel viewModel)
		{
			base.RefreshPlayerProperties (viewModel);

			viewModel.PlayerItems.ToList ().ForEach (playerItemVM => {
				playerItemVM.Player = Network.Client.CurrentRoom.GetPlayer (playerItemVM.ActerId);
				playerItemVM.ExecuteRefreshByPlayer ();
			});
		}
	}
}
