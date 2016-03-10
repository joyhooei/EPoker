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
    
    
    public class LobbyPanelController : LobbyPanelControllerBase {
        
        public override void InitializeLobbyPanel(LobbyPanelViewModel viewModel) {
            base.InitializeLobbyPanel(viewModel);
            // This is called when a LobbyPanelViewModel is created
        }
    }
}
