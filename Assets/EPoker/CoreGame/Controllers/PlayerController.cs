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
    
    public override void PlayerReady(PlayerViewModel viewModel) {
        base.PlayerReady(viewModel);
    }
    
    public override void PlayerCancel(PlayerViewModel viewModel) {
        base.PlayerCancel(viewModel);
    }
    
    public override void MatchBegan(PlayerViewModel viewModel) {
        base.MatchBegan(viewModel);
    }
    
    public override void BeganToPlay(PlayerViewModel viewModel) {
        base.BeganToPlay(viewModel);
    }
    
    public override void BeganToWait(PlayerViewModel viewModel) {
        base.BeganToWait(viewModel);
    }
    
    public override void TurnOn(PlayerViewModel viewModel) {
        base.TurnOn(viewModel);
    }
    
    public override void TurnOff(PlayerViewModel viewModel) {
        base.TurnOff(viewModel);
    }
    
    public override void Win(PlayerViewModel viewModel) {
        base.Win(viewModel);
    }
    
    public override void Over(PlayerViewModel viewModel) {
        base.Over(viewModel);
    }
    
    public override void InitOK(PlayerViewModel viewModel) {
        base.InitOK(viewModel);
    }
    }
}
