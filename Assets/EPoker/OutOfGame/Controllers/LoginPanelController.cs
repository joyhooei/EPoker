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

    
	public class LoginPanelController : LoginPanelControllerBase
	{
		[Inject ("OutOfGameRoot")] public OutOfGameRootViewModel OutOfGameRoot;

		public override void InitializeLoginPanel (LoginPanelViewModel viewModel)
		{
			base.InitializeLoginPanel (viewModel);
			// This is called when a LoginPanelViewModel is created
		}

		public override void Login (LoginPanelViewModel viewModel)
		{
			base.Login (viewModel);

			// 进行登录
			Publish (new NetLogin () {
				CustomID = viewModel.CustomId,
				SuccessCallback = _ => {
					UnityEngine.Debug.Log ("SuccessCallback: " + _);
					OutOfGameRoot.ExecuteDoLogin ();
				},
				ErrorCallback = _ => {
					UnityEngine.Debug.Log ("ErrorCallback: " + _);
				}
			});
		}
	}
}
