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
	using yigame.epoker;

    
	public class PlayerView : PlayerViewBase
	{
        
		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
			// NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
			// var vm = model as PlayerViewModel;
			// This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		}

		public override void Bind ()
		{
			base.Bind ();
			// Use this.Player to access the viewmodel.
			// Use this method to subscribe to the view-model.
			// Any designer bindings are created in the base implementation.
		}

		#region Status Changed

		public override void StatusChanged (Invert.StateMachine.State arg1)
		{
			base.StatusChanged (arg1);
		}

		// 初始化
		public override void OnInit ()
		{
			base.OnInit ();

		}

		public override void OnReady ()
		{
			base.OnReady ();
		}

		public override void OnMatchPrepare ()
		{
			base.OnMatchPrepare ();
		}

		public override void OnMatchIdle ()
		{
			base.OnMatchIdle ();
		}

		public override void OnMatchDeal ()
		{
			base.OnMatchDeal ();
		}

		public override void OnMatchWin ()
		{
			base.OnMatchWin ();
		}

		public override void OnMatchOver ()
		{
			base.OnMatchOver ();
		}

		public override void OnWait ()
		{
			base.OnWait ();
		}

		#endregion

	}
}
