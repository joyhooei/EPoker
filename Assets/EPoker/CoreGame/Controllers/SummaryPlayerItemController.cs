namespace yigame.epoker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.MVVM;
    using uFrame.IOC;
    using uFrame.Kernel;
    using UniRx;
    using uFrame.Serialization;
    
    
    public class SummaryPlayerItemController : SummaryPlayerItemControllerBase {
        
        public override void InitializeSummaryPlayerItem(SummaryPlayerItemViewModel viewModel) {
            base.InitializeSummaryPlayerItem(viewModel);
            // This is called when a SummaryPlayerItemViewModel is created
        }
    }
}
