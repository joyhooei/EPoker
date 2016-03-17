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

    
	public class LobbyPanelController : LobbyPanelControllerBase
	{
        
		public override void InitializeLobbyPanel (LobbyPanelViewModel viewModel)
		{
			base.InitializeLobbyPanel (viewModel);
			// This is called when a LobbyPanelViewModel is created
		}

		public override void EnterRoom (LobbyPanelViewModel viewModel)
		{
			base.EnterRoom (viewModel);
		}

		public override void QuitLobby (LobbyPanelViewModel viewModel)
		{
			base.QuitLobby (viewModel);

			// 进行登出
			Publish (new NetLogout () {
				SuccessCallback = _ => {
					UnityEngine.Debug.Log ("SuccessCallback: " + _);
					OutOfGameRoot.ExecuteDoLogout ();
				},
				ErrorCallback = _ => {
					UnityEngine.Debug.Log ("ErrorCallback: " + _);
				}
			});
		}
	}
}
