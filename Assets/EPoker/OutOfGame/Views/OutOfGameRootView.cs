namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.Kernel;
	using uFrame.MVVM;
	using uFrame.MVVM.Services;
	using uFrame.MVVM.Bindings;
	using uFrame.Serialization;
	using UniRx;
	using UnityEngine;

    
	public class OutOfGameRootView : OutOfGameRootViewBase
	{
        
		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
		}

		public override void Bind ()
		{
			base.Bind ();
		}

		public override void UIFlowStatusChanged (Invert.StateMachine.State arg1)
		{
			base.UIFlowStatusChanged (arg1);
		}

		public override void OnUILogin ()
		{
			base.OnUILogin ();

			OutOfGameRoot.ExecuteInitGame ();

			OutOfGameRoot.CanvasRoot.Execute (new OpenClosePanelCommand () {
				OpenPanels = new List<Type> () { typeof(LoginPanelViewModel) },
				ClosePanels = new List<Type> () { typeof(LobbyPanelViewModel), typeof(RoomPanelViewModel) }
			});
		}

		public override void OnUILobby ()
		{
			base.OnUILobby ();
			OutOfGameRoot.CanvasRoot.Execute (new OpenClosePanelCommand () {
				OpenPanels = new List<Type> () { typeof(LobbyPanelViewModel) },
				ClosePanels = new List<Type> () { typeof(LoginPanelViewModel), typeof(RoomPanelViewModel) }
			});
		}

		public override void OnUIRoom ()
		{
			base.OnUIRoom ();
			OutOfGameRoot.CanvasRoot.Execute (new OpenClosePanelCommand () {
				OpenPanels = new List<Type> () { /* typeof(RoomPanelViewModel) */ },
				ClosePanels = new List<Type> () { typeof(LoginPanelViewModel), typeof(LobbyPanelViewModel) }
			});
		}
	}
}
