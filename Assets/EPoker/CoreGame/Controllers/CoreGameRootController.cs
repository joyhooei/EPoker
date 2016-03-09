namespace yigame.epoker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.Kernel;
    using uFrame.IOC;
    using uFrame.MVVM;
    using uFrame.Serialization;
    using UniRx;
    
    public class CoreGameRootController : CoreGameRootControllerBase {
        
        public override void InitializeCoreGameRoot(CoreGameRootViewModel viewModel) {
            base.InitializeCoreGameRoot(viewModel);
            // This is called when a CoreGameRootViewModel is created
        }
    
		public override void ResetPlayerCount (CoreGameRootViewModel viewModel, int arg)
		{
			base.ResetPlayerCount (viewModel, arg);

			viewModel.PlayerCount = arg;

			viewModel.PlayerCollection.Clear ();

			for (int i = 0; i < arg; i++) {
				PlayerViewModel player = MVVMKernelExtensions.CreateViewModel<PlayerViewModel> ();
				viewModel.PlayerCollection.Add (player);
			}
		}
    }
}
