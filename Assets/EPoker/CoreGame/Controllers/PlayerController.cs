namespace yigame.epoker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using yigame.epoker;
    using UniRx;
    using uFrame.MVVM;
    using uFrame.Kernel;
    using uFrame.IOC;
    using uFrame.Serialization;
    
    
    public class PlayerController : PlayerControllerBase {
        
        public override void InitializePlayer(PlayerViewModel viewModel) {
            base.InitializePlayer(viewModel);
            // This is called when a PlayerViewModel is created
        }
    }
}
