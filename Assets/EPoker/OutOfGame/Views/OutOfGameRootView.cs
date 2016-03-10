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
			// NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
			// var vm = model as OutOfGameRootViewModel;
			// This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		}

		public override void Bind ()
		{
			base.Bind ();
			// Use this.OutOfGameRoot to access the viewmodel.
			// Use this method to subscribe to the view-model.
			// Any designer bindings are created in the base implementation.
		}

		public override void UIFlowStatusChanged (Invert.StateMachine.State arg1)
		{
			base.UIFlowStatusChanged (arg1);
		}

		public override void OnUILogin ()
		{
			base.OnUILogin ();
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
				OpenPanels = new List<Type> () { typeof(RoomPanelViewModel) },
				ClosePanels = new List<Type> () { typeof(LoginPanelViewModel), typeof(LobbyPanelViewModel) }
			});
		}
	}
}
