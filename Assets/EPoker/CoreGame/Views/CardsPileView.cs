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

	public class CardsPileView : CardsPileViewBase
	{
        
		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
		}

		public override void Bind ()
		{
			base.Bind ();
		}

		public override uFrame.MVVM.ViewBase CardsCreateView (uFrame.MVVM.ViewModel viewModel)
		{
			GameObject prefab = Resources.Load<GameObject> ("_Card_Blue_");
			return InstantiateView (prefab, viewModel);
		}

		public override void CardsAdded (uFrame.MVVM.ViewBase view)
		{
		}

		public override void CardsRemoved (uFrame.MVVM.ViewBase view)
		{
			Destroy (view.gameObject);
		}
	}
}
