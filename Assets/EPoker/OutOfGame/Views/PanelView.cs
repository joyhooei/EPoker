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
	using UnityEngine.UI;

    
	public class PanelView : PanelViewBase
	{
        
		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
			// NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
			// var vm = model as PanelViewModel;
			// This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		}

		public override void Bind ()
		{
			base.Bind ();
			// Use this.Panel to access the viewmodel.
			// Use this method to subscribe to the view-model.
			// Any designer bindings are created in the base implementation.
		}

		public override void PanelInExecuted (PanelInCommand command)
		{
			GetPanelRoot ().gameObject.SetActive (true);
		}

		public override void PanelOutExecuted (PanelOutCommand command)
		{
			GetPanelRoot ().gameObject.SetActive (false);
		}

		public virtual Transform GetPanelRoot ()
		{
			return null;
		}
	}
}
