// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace yigame.epoker {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UniRx;
    using uFrame.IOC;
    using uFrame.MVVM;
    using yigame.epoker;
    using uFrame.Kernel;
    
    
    public class NetworkBase : uFrame.Kernel.SystemServiceMonoBehavior {
        
        /// <summary>
        /// This method is invoked whenever the kernel is loading.
        /// Since the kernel lives throughout the entire lifecycle of the game, this will only be invoked once.
        /// </summary>
        public override void Setup() {
            base.Setup();
            this.OnEvent<NetInit>().Subscribe(this.NetInitHandler);
            this.OnEvent<NetLogin>().Subscribe(this.NetLoginHandler);
            this.OnEvent<NetLogout>().Subscribe(this.NetLogoutHandler);
            this.OnEvent<NetJoinOrCreateRoom>().Subscribe(this.NetJoinOrCreateRoomHandler);
            this.OnEvent<NetLeaveRoom>().Subscribe(this.NetLeaveRoomHandler);
            this.OnEvent<NetSetRoomProperties>().Subscribe(this.NetSetRoomPropertiesHandler);
            this.OnEvent<NetSetPlayerProperties>().Subscribe(this.NetSetPlayerPropertiesHandler);
            this.OnEvent<NetRaiseEvent>().Subscribe(this.NetRaiseEventHandler);
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new NetInit())
        /// </summary>
        public virtual void NetInitHandler(NetInit data) {
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new NetLogin())
        /// </summary>
        public virtual void NetLoginHandler(NetLogin data) {
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new NetLogout())
        /// </summary>
        public virtual void NetLogoutHandler(NetLogout data) {
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new NetJoinOrCreateRoom())
        /// </summary>
        public virtual void NetJoinOrCreateRoomHandler(NetJoinOrCreateRoom data) {
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new NetLeaveRoom())
        /// </summary>
        public virtual void NetLeaveRoomHandler(NetLeaveRoom data) {
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new NetSetRoomProperties())
        /// </summary>
        public virtual void NetSetRoomPropertiesHandler(NetSetRoomProperties data) {
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new NetSetPlayerProperties())
        /// </summary>
        public virtual void NetSetPlayerPropertiesHandler(NetSetPlayerProperties data) {
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
        
        /// <summary>
        // This method is executed when using this.Publish(new NetRaiseEvent())
        /// </summary>
        public virtual void NetRaiseEventHandler(NetRaiseEvent data) {
            // Process the commands information.  Also, you can publish new events by using the line below.
            // this.Publish(new AnotherEvent())
        }
    }
}
