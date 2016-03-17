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

    
	public class OutOfGameRootController : OutOfGameRootControllerBase
	{
        
		public override void InitializeOutOfGameRoot (OutOfGameRootViewModel viewModel)
		{
			base.InitializeOutOfGameRoot (viewModel);
		}

		public override void DoLogin (OutOfGameRootViewModel viewModel)
		{
			base.DoLogin (viewModel);
		}

		public override void DoEnterRoom (OutOfGameRootViewModel viewModel)
		{
			base.DoEnterRoom (viewModel);
		}

		public override void DoQuitRoom (OutOfGameRootViewModel viewModel)
		{
			base.DoQuitRoom (viewModel);
		}

		public override void InitGame (OutOfGameRootViewModel viewModel)
		{
			base.InitGame (viewModel);

			Publish (new NetInit ());
		}

		public override void DoLogout (OutOfGameRootViewModel viewModel)
		{
			base.DoLogout (viewModel);
		}
	}
}
