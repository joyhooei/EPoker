namespace yigame.epoker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using yigame.epoker;
    using uFrame.MVVM;
    using uFrame.Serialization;
    using UniRx;
    using uFrame.Kernel;
    using uFrame.IOC;
    
    
    public class CardsPileController : CardsPileControllerBase {
        
        public override void InitializeCardsPile(CardsPileViewModel viewModel) {
            base.InitializeCardsPile(viewModel);
            // This is called when a CardsPileViewModel is created
        }
    }
}
