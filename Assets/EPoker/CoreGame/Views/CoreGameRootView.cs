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
	using uFrame.IOC;
	using uFrame.Serialization;
	using UniRx;
	using UnityEngine;

    
	public class CoreGameRootView : CoreGameRootViewBase
	{

		[Inject] public GameService GameSrv;

		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
			// NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
			// var vm = model as CoreGameRootViewModel;
			// This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		}

		public override void Bind ()
		{
			base.Bind ();
			// Use this.CoreGameRoot to access the viewmodel.
			// Use this method to subscribe to the view-model.
			// Any designer bindings are created in the base implementation.
		}

		public override uFrame.MVVM.ViewBase PlayerCollectionCreateView (uFrame.MVVM.ViewModel viewModel)
		{
			return InstantiateView (viewModel);
		}

		public override void PlayerCollectionAdded (uFrame.MVVM.ViewBase view)
		{
			Debug.Log ("PlayerCollectionAdded");
		}

		public override void PlayerCollectionRemoved (uFrame.MVVM.ViewBase view)
		{
			Debug.Log ("PlayerCollectionRemoved");
			Destroy (view.gameObject);
		}
	}
}
