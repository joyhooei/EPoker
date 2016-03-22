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
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

    
	public class CoreGameRootView : CoreGameRootViewBase
	{

		[Inject] public GameService GameSrv;

		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
		}

		public override void Bind ()
		{
			base.Bind ();
		}

		public override uFrame.MVVM.ViewBase PlayerCollectionCreateView (uFrame.MVVM.ViewModel viewModel)
		{
			return InstantiateView (viewModel);
		}

		public override void PlayerCollectionAdded (uFrame.MVVM.ViewBase view)
		{
			Debug.Log ("PlayerCollectionAdded");
			(view as PlayerView).SetPanelPosByPosId ();
		}

		public override void PlayerCollectionRemoved (uFrame.MVVM.ViewBase view)
		{
			Debug.Log ("PlayerCollectionRemoved");
			Destroy (view.gameObject);
		}
	}
}
