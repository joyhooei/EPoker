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
    using Invert.StateMachine;
    
    
    public class PlayerStatus : Invert.StateMachine.StateMachine {
        
        private Invert.StateMachine.StateMachineTrigger _PlayerReady;
        
        private Invert.StateMachine.StateMachineTrigger _PlayerCancel;
        
        private Invert.StateMachine.StateMachineTrigger _MatchBegan;
        
        private Invert.StateMachine.StateMachineTrigger _BeganToPlay;
        
        private Invert.StateMachine.StateMachineTrigger _BeganToWait;
        
        private Invert.StateMachine.StateMachineTrigger _TurnOn;
        
        private Invert.StateMachine.StateMachineTrigger _TurnOff;
        
        private Invert.StateMachine.StateMachineTrigger _Win;
        
        private Invert.StateMachine.StateMachineTrigger _Over;
        
        private Invert.StateMachine.StateMachineTrigger _InitOK;
        
        private Init _Init;
        
        private Ready _Ready;
        
        private MatchPrepare _MatchPrepare;
        
        private MatchIdle _MatchIdle;
        
        private MatchDeal _MatchDeal;
        
        private MatchWin _MatchWin;
        
        private MatchOver _MatchOver;
        
        private Wait _Wait;
        
        public PlayerStatus(uFrame.MVVM.ViewModel vm, string propertyName) : 
                base(vm, propertyName) {
        }
        
        public PlayerStatus() : 
                base(null, string.Empty) {
        }
        
        public override Invert.StateMachine.State StartState {
            get {
                return this.Init;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger PlayerReady {
            get {
                if (this._PlayerReady == null) {
                    this._PlayerReady = new StateMachineTrigger(this , "PlayerReady");
                }
                return _PlayerReady;
            }
            set {
                _PlayerReady = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger PlayerCancel {
            get {
                if (this._PlayerCancel == null) {
                    this._PlayerCancel = new StateMachineTrigger(this , "PlayerCancel");
                }
                return _PlayerCancel;
            }
            set {
                _PlayerCancel = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger MatchBegan {
            get {
                if (this._MatchBegan == null) {
                    this._MatchBegan = new StateMachineTrigger(this , "MatchBegan");
                }
                return _MatchBegan;
            }
            set {
                _MatchBegan = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger BeganToPlay {
            get {
                if (this._BeganToPlay == null) {
                    this._BeganToPlay = new StateMachineTrigger(this , "BeganToPlay");
                }
                return _BeganToPlay;
            }
            set {
                _BeganToPlay = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger BeganToWait {
            get {
                if (this._BeganToWait == null) {
                    this._BeganToWait = new StateMachineTrigger(this , "BeganToWait");
                }
                return _BeganToWait;
            }
            set {
                _BeganToWait = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger TurnOn {
            get {
                if (this._TurnOn == null) {
                    this._TurnOn = new StateMachineTrigger(this , "TurnOn");
                }
                return _TurnOn;
            }
            set {
                _TurnOn = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger TurnOff {
            get {
                if (this._TurnOff == null) {
                    this._TurnOff = new StateMachineTrigger(this , "TurnOff");
                }
                return _TurnOff;
            }
            set {
                _TurnOff = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger Win {
            get {
                if (this._Win == null) {
                    this._Win = new StateMachineTrigger(this , "Win");
                }
                return _Win;
            }
            set {
                _Win = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger Over {
            get {
                if (this._Over == null) {
                    this._Over = new StateMachineTrigger(this , "Over");
                }
                return _Over;
            }
            set {
                _Over = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger InitOK {
            get {
                if (this._InitOK == null) {
                    this._InitOK = new StateMachineTrigger(this , "InitOK");
                }
                return _InitOK;
            }
            set {
                _InitOK = value;
            }
        }
        
        public virtual Init Init {
            get {
                if (this._Init == null) {
                    this._Init = new Init();
                }
                return _Init;
            }
            set {
                _Init = value;
            }
        }
        
        public virtual Ready Ready {
            get {
                if (this._Ready == null) {
                    this._Ready = new Ready();
                }
                return _Ready;
            }
            set {
                _Ready = value;
            }
        }
        
        public virtual MatchPrepare MatchPrepare {
            get {
                if (this._MatchPrepare == null) {
                    this._MatchPrepare = new MatchPrepare();
                }
                return _MatchPrepare;
            }
            set {
                _MatchPrepare = value;
            }
        }
        
        public virtual MatchIdle MatchIdle {
            get {
                if (this._MatchIdle == null) {
                    this._MatchIdle = new MatchIdle();
                }
                return _MatchIdle;
            }
            set {
                _MatchIdle = value;
            }
        }
        
        public virtual MatchDeal MatchDeal {
            get {
                if (this._MatchDeal == null) {
                    this._MatchDeal = new MatchDeal();
                }
                return _MatchDeal;
            }
            set {
                _MatchDeal = value;
            }
        }
        
        public virtual MatchWin MatchWin {
            get {
                if (this._MatchWin == null) {
                    this._MatchWin = new MatchWin();
                }
                return _MatchWin;
            }
            set {
                _MatchWin = value;
            }
        }
        
        public virtual MatchOver MatchOver {
            get {
                if (this._MatchOver == null) {
                    this._MatchOver = new MatchOver();
                }
                return _MatchOver;
            }
            set {
                _MatchOver = value;
            }
        }
        
        public virtual Wait Wait {
            get {
                if (this._Wait == null) {
                    this._Wait = new Wait();
                }
                return _Wait;
            }
            set {
                _Wait = value;
            }
        }
        
        public override void Compose(System.Collections.Generic.List<Invert.StateMachine.State> states) {
            base.Compose(states);
            Init.InitOK = new StateTransition("InitOK", Init, Wait);
            Transitions.Add(Init.InitOK);
            Init.PlayerReady = new StateTransition("PlayerReady", Init, Ready);
            Transitions.Add(Init.PlayerReady);
            Init.AddTrigger(InitOK, Init.InitOK);
            Init.AddTrigger(PlayerReady, Init.PlayerReady);
            Init.StateMachine = this;
            states.Add(Init);
            Ready.PlayerCancel = new StateTransition("PlayerCancel", Ready, Wait);
            Transitions.Add(Ready.PlayerCancel);
            Ready.MatchBegan = new StateTransition("MatchBegan", Ready, MatchPrepare);
            Transitions.Add(Ready.MatchBegan);
            Ready.AddTrigger(PlayerCancel, Ready.PlayerCancel);
            Ready.AddTrigger(MatchBegan, Ready.MatchBegan);
            Ready.StateMachine = this;
            states.Add(Ready);
            MatchPrepare.BeganToPlay = new StateTransition("BeganToPlay", MatchPrepare, MatchDeal);
            Transitions.Add(MatchPrepare.BeganToPlay);
            MatchPrepare.BeganToWait = new StateTransition("BeganToWait", MatchPrepare, MatchIdle);
            Transitions.Add(MatchPrepare.BeganToWait);
            MatchPrepare.AddTrigger(BeganToPlay, MatchPrepare.BeganToPlay);
            MatchPrepare.AddTrigger(BeganToWait, MatchPrepare.BeganToWait);
            MatchPrepare.StateMachine = this;
            states.Add(MatchPrepare);
            MatchIdle.TurnOn = new StateTransition("TurnOn", MatchIdle, MatchDeal);
            Transitions.Add(MatchIdle.TurnOn);
            MatchIdle.Over = new StateTransition("Over", MatchIdle, MatchOver);
            Transitions.Add(MatchIdle.Over);
            MatchIdle.AddTrigger(TurnOn, MatchIdle.TurnOn);
            MatchIdle.AddTrigger(Over, MatchIdle.Over);
            MatchIdle.StateMachine = this;
            states.Add(MatchIdle);
            MatchDeal.Win = new StateTransition("Win", MatchDeal, MatchWin);
            Transitions.Add(MatchDeal.Win);
            MatchDeal.TurnOff = new StateTransition("TurnOff", MatchDeal, MatchIdle);
            Transitions.Add(MatchDeal.TurnOff);
            MatchDeal.AddTrigger(Win, MatchDeal.Win);
            MatchDeal.AddTrigger(TurnOff, MatchDeal.TurnOff);
            MatchDeal.StateMachine = this;
            states.Add(MatchDeal);
            MatchWin.Over = new StateTransition("Over", MatchWin, MatchOver);
            Transitions.Add(MatchWin.Over);
            MatchWin.AddTrigger(Over, MatchWin.Over);
            MatchWin.StateMachine = this;
            states.Add(MatchWin);
            MatchOver.PlayerReady = new StateTransition("PlayerReady", MatchOver, Ready);
            Transitions.Add(MatchOver.PlayerReady);
            MatchOver.AddTrigger(PlayerReady, MatchOver.PlayerReady);
            MatchOver.StateMachine = this;
            states.Add(MatchOver);
            Wait.PlayerReady = new StateTransition("PlayerReady", Wait, Ready);
            Transitions.Add(Wait.PlayerReady);
            Wait.AddTrigger(PlayerReady, Wait.PlayerReady);
            Wait.StateMachine = this;
            states.Add(Wait);
        }
    }
    
    public class Init : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _InitOK;
        
        private Invert.StateMachine.StateTransition _PlayerReady;
        
        public Invert.StateMachine.StateTransition InitOK {
            get {
                return _InitOK;
            }
            set {
                _InitOK = value;
            }
        }
        
        public Invert.StateMachine.StateTransition PlayerReady {
            get {
                return _PlayerReady;
            }
            set {
                _PlayerReady = value;
            }
        }
        
        public override string Name {
            get {
                return "Init";
            }
        }
        
        public virtual void InitOKTransition() {
            this.Transition(this.InitOK);
        }
        
        public virtual void PlayerReadyTransition() {
            this.Transition(this.PlayerReady);
        }
    }
    
    public class Ready : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _PlayerCancel;
        
        private Invert.StateMachine.StateTransition _MatchBegan;
        
        public Invert.StateMachine.StateTransition PlayerCancel {
            get {
                return _PlayerCancel;
            }
            set {
                _PlayerCancel = value;
            }
        }
        
        public Invert.StateMachine.StateTransition MatchBegan {
            get {
                return _MatchBegan;
            }
            set {
                _MatchBegan = value;
            }
        }
        
        public override string Name {
            get {
                return "Ready";
            }
        }
        
        public virtual void PlayerCancelTransition() {
            this.Transition(this.PlayerCancel);
        }
        
        public virtual void MatchBeganTransition() {
            this.Transition(this.MatchBegan);
        }
    }
    
    public class MatchPrepare : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _BeganToPlay;
        
        private Invert.StateMachine.StateTransition _BeganToWait;
        
        public Invert.StateMachine.StateTransition BeganToPlay {
            get {
                return _BeganToPlay;
            }
            set {
                _BeganToPlay = value;
            }
        }
        
        public Invert.StateMachine.StateTransition BeganToWait {
            get {
                return _BeganToWait;
            }
            set {
                _BeganToWait = value;
            }
        }
        
        public override string Name {
            get {
                return "MatchPrepare";
            }
        }
        
        public virtual void BeganToPlayTransition() {
            this.Transition(this.BeganToPlay);
        }
        
        public virtual void BeganToWaitTransition() {
            this.Transition(this.BeganToWait);
        }
    }
    
    public class MatchIdle : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _TurnOn;
        
        private Invert.StateMachine.StateTransition _Over;
        
        public Invert.StateMachine.StateTransition TurnOn {
            get {
                return _TurnOn;
            }
            set {
                _TurnOn = value;
            }
        }
        
        public Invert.StateMachine.StateTransition Over {
            get {
                return _Over;
            }
            set {
                _Over = value;
            }
        }
        
        public override string Name {
            get {
                return "MatchIdle";
            }
        }
        
        public virtual void TurnOnTransition() {
            this.Transition(this.TurnOn);
        }
        
        public virtual void OverTransition() {
            this.Transition(this.Over);
        }
    }
    
    public class MatchDeal : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _Win;
        
        private Invert.StateMachine.StateTransition _TurnOff;
        
        public Invert.StateMachine.StateTransition Win {
            get {
                return _Win;
            }
            set {
                _Win = value;
            }
        }
        
        public Invert.StateMachine.StateTransition TurnOff {
            get {
                return _TurnOff;
            }
            set {
                _TurnOff = value;
            }
        }
        
        public override string Name {
            get {
                return "MatchDeal";
            }
        }
        
        public virtual void WinTransition() {
            this.Transition(this.Win);
        }
        
        public virtual void TurnOffTransition() {
            this.Transition(this.TurnOff);
        }
    }
    
    public class MatchWin : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _Over;
        
        public Invert.StateMachine.StateTransition Over {
            get {
                return _Over;
            }
            set {
                _Over = value;
            }
        }
        
        public override string Name {
            get {
                return "MatchWin";
            }
        }
        
        public virtual void OverTransition() {
            this.Transition(this.Over);
        }
    }
    
    public class MatchOver : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _PlayerReady;
        
        public Invert.StateMachine.StateTransition PlayerReady {
            get {
                return _PlayerReady;
            }
            set {
                _PlayerReady = value;
            }
        }
        
        public override string Name {
            get {
                return "MatchOver";
            }
        }
        
        public virtual void PlayerReadyTransition() {
            this.Transition(this.PlayerReady);
        }
    }
    
    public class Wait : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _PlayerReady;
        
        public Invert.StateMachine.StateTransition PlayerReady {
            get {
                return _PlayerReady;
            }
            set {
                _PlayerReady = value;
            }
        }
        
        public override string Name {
            get {
                return "Wait";
            }
        }
        
        public virtual void PlayerReadyTransition() {
            this.Transition(this.PlayerReady);
        }
    }
    
    public class CoreGameSM : Invert.StateMachine.StateMachine {
        
        private Invert.StateMachine.StateMachineTrigger _CoreGameStart;
        
        private Invert.StateMachine.StateMachineTrigger _CoreGameOver;
        
        private Waiting _Waiting;
        
        private Playing _Playing;
        
        public CoreGameSM(uFrame.MVVM.ViewModel vm, string propertyName) : 
                base(vm, propertyName) {
        }
        
        public CoreGameSM() : 
                base(null, string.Empty) {
        }
        
        public override Invert.StateMachine.State StartState {
            get {
                return this.Waiting;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger CoreGameStart {
            get {
                if (this._CoreGameStart == null) {
                    this._CoreGameStart = new StateMachineTrigger(this , "CoreGameStart");
                }
                return _CoreGameStart;
            }
            set {
                _CoreGameStart = value;
            }
        }
        
        public virtual Invert.StateMachine.StateMachineTrigger CoreGameOver {
            get {
                if (this._CoreGameOver == null) {
                    this._CoreGameOver = new StateMachineTrigger(this , "CoreGameOver");
                }
                return _CoreGameOver;
            }
            set {
                _CoreGameOver = value;
            }
        }
        
        public virtual Waiting Waiting {
            get {
                if (this._Waiting == null) {
                    this._Waiting = new Waiting();
                }
                return _Waiting;
            }
            set {
                _Waiting = value;
            }
        }
        
        public virtual Playing Playing {
            get {
                if (this._Playing == null) {
                    this._Playing = new Playing();
                }
                return _Playing;
            }
            set {
                _Playing = value;
            }
        }
        
        public override void Compose(System.Collections.Generic.List<Invert.StateMachine.State> states) {
            base.Compose(states);
            Waiting.CoreGameStart = new StateTransition("CoreGameStart", Waiting, Playing);
            Transitions.Add(Waiting.CoreGameStart);
            Waiting.AddTrigger(CoreGameStart, Waiting.CoreGameStart);
            Waiting.StateMachine = this;
            states.Add(Waiting);
            Playing.CoreGameOver = new StateTransition("CoreGameOver", Playing, Waiting);
            Transitions.Add(Playing.CoreGameOver);
            Playing.AddTrigger(CoreGameOver, Playing.CoreGameOver);
            Playing.StateMachine = this;
            states.Add(Playing);
        }
    }
    
    public class Waiting : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _CoreGameStart;
        
        public Invert.StateMachine.StateTransition CoreGameStart {
            get {
                return _CoreGameStart;
            }
            set {
                _CoreGameStart = value;
            }
        }
        
        public override string Name {
            get {
                return "Waiting";
            }
        }
        
        public virtual void CoreGameStartTransition() {
            this.Transition(this.CoreGameStart);
        }
    }
    
    public class Playing : Invert.StateMachine.State {
        
        private Invert.StateMachine.StateTransition _CoreGameOver;
        
        public Invert.StateMachine.StateTransition CoreGameOver {
            get {
                return _CoreGameOver;
            }
            set {
                _CoreGameOver = value;
            }
        }
        
        public override string Name {
            get {
                return "Playing";
            }
        }
        
        public virtual void CoreGameOverTransition() {
            this.Transition(this.CoreGameOver);
        }
    }
}
