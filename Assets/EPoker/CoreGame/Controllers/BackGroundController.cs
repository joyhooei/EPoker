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
    
    
    public class BackGroundController : BackGroundControllerBase {
        
        public override void InitializeBackGround(BackGroundViewModel viewModel) {
            base.InitializeBackGround(viewModel);
            // This is called when a BackGroundViewModel is created
        }
    }
}
