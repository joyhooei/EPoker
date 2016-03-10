namespace yigame.epoker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UniRx;
    using uFrame.Serialization;
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.MVVM;
    
    
    public class LoginPanelController : LoginPanelControllerBase {
        
        public override void InitializeLoginPanel(LoginPanelViewModel viewModel) {
            base.InitializeLoginPanel(viewModel);
            // This is called when a LoginPanelViewModel is created
        }
    }
}
