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
			Publish (new NetLeaveRoom () {
				SuccessCallback = s => {
					OutOfGameRoot.ExecuteDoQuitRoom ();
				}
			});
		}

		public override void RefreshRoom (RoomPanelViewModel viewModel)
		{
			base.RefreshRoom (viewModel);
			Dictionary<int, Player> playerDic = Network.Client.CurrentRoom.Players;

			viewModel.Players.Clear ();
			viewModel.Players.AddRange (playerDic.Select (kv => string.Format ("id:{0}, name:{1}", kv.Value.ID, kv.Value.Name)));
		}
	}
}
