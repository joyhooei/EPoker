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
    
    
    public class PanelController : PanelControllerBase {
        
        public override void InitializePanel(PanelViewModel viewModel) {
            base.InitializePanel(viewModel);
            // This is called when a PanelViewModel is created
        }
        
        public override void PanelIn(PanelViewModel viewModel) {
            base.PanelIn(viewModel);
        }
        
        public override void PanelOut(PanelViewModel viewModel) {
            base.PanelOut(viewModel);
        }
    }
}
