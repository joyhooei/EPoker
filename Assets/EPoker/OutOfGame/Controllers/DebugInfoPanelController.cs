namespace yigame.epoker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.MVVM;
    using uFrame.Serialization;
    using UniRx;
    using uFrame.Kernel;
    using uFrame.IOC;
    
    
    public class DebugInfoPanelController : DebugInfoPanelControllerBase {
        
        public override void InitializeDebugInfoPanel(DebugInfoPanelViewModel viewModel) {
            base.InitializeDebugInfoPanel(viewModel);
            // This is called when a DebugInfoPanelViewModel is created
        }
    }
}
