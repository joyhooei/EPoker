namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.MVVM;
	using uFrame.Serialization;
	using UniRx;
	using uFrame.Kernel;
	using uFrame.IOC;

    
	public class PlayerItemController : PlayerItemControllerBase
	{
        
		public override void InitializePlayerItem (PlayerItemViewModel viewModel)
		{
			base.InitializePlayerItem (viewModel);
			// This is called when a PlayerItemViewModel is created
		}

		public override void RefreshByPlayer (PlayerItemViewModel viewModel)
		{
			base.RefreshByPlayer (viewModel);
		}
	}
}
