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
    
    
    public class RoomPanelController : RoomPanelControllerBase {
        
        public override void InitializeRoomPanel(RoomPanelViewModel viewModel) {
            base.InitializeRoomPanel(viewModel);
            // This is called when a RoomPanelViewModel is created
        }
    }
}
