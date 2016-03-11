namespace yigame.epoker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using uFrame.IOC;
    using uFrame.MVVM;
    using UnityEngine;
    using UniRx;
    using uFrame.Kernel;
    
    
    public class Network : NetworkBase {
        
        /// <summary>
        // This method is executed when using this.Publish(new NetLogin())
        /// </summary>
        public override void NetLoginHandler(NetLogin data) {
            base.NetLoginHandler(data);
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
    }
}
