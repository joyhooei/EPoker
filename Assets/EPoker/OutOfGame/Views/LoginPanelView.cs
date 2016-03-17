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
    
	public class LoginPanelView : LoginPanelViewBase
	{
        
		public Transform PanelTransform;

		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);

			(model as LoginPanelViewModel).CustomId = PlayerPrefs.GetString ("login_panel_custom_id", "");
		}

		public override void Bind ()
		{
			base.Bind ();
		}

		public override Transform GetPanelRoot ()
		{
			return PanelTransform;
		}

		public override void LoginExecuted (LoginCommand command)
		{
			PlayerPrefs.SetString ("login_panel_custom_id", LoginPanel.CustomId);
		}
	}
}
