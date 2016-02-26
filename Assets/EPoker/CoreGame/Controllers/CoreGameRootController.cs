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
    }
}
