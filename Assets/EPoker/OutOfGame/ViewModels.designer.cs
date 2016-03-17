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
    using uFrame.IOC;
    using uFrame.Kernel;
    using uFrame.MVVM;
    using uFrame.MVVM.Bindings;
    using uFrame.Serialization;
    using UnityEngine;
    using UniRx;
    using yigame.epoker;
    
    
    public partial class OutOfGameRootViewModelBase : uFrame.MVVM.ViewModel {
        
        private UIFlowSM _UIFlowStatusProperty;
        
        private P<CanvasRootViewModel> _CanvasRootProperty;
        
        private Signal<InitGameCommand> _InitGame;
        
        private Signal<DoLoginCommand> _DoLogin;
        
        private Signal<DoLogoutCommand> _DoLogout;
        
        private Signal<DoEnterRoomCommand> _DoEnterRoom;
        
        private Signal<DoQuitRoomCommand> _DoQuitRoom;
        
        private Signal<DoDisconnectCommand> _DoDisconnect;
        
        public OutOfGameRootViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual UIFlowSM UIFlowStatusProperty {
            get {
                return _UIFlowStatusProperty;
            }
            set {
                _UIFlowStatusProperty = value;
            }
        }
        
        public virtual Invert.StateMachine.State UIFlowStatus {
            get {
                return UIFlowStatusProperty.Value;
            }
            set {
                UIFlowStatusProperty.Value = value;
            }
        }
        
        public virtual P<CanvasRootViewModel> CanvasRootProperty {
            get {
                return _CanvasRootProperty;
            }
            set {
                _CanvasRootProperty = value;
            }
        }
        
        public virtual CanvasRootViewModel CanvasRoot {
            get {
                return CanvasRootProperty.Value;
            }
            set {
                CanvasRootProperty.Value = value;
            }
        }
        
        public virtual Signal<InitGameCommand> InitGame {
            get {
                return _InitGame;
            }
            set {
                _InitGame = value;
            }
        }
        
        public virtual Signal<DoLoginCommand> DoLogin {
            get {
                return _DoLogin;
            }
            set {
                _DoLogin = value;
            }
        }
        
        public virtual Signal<DoLogoutCommand> DoLogout {
            get {
                return _DoLogout;
            }
            set {
                _DoLogout = value;
            }
        }
        
        public virtual Signal<DoEnterRoomCommand> DoEnterRoom {
            get {
                return _DoEnterRoom;
            }
            set {
                _DoEnterRoom = value;
            }
        }
        
        public virtual Signal<DoQuitRoomCommand> DoQuitRoom {
            get {
                return _DoQuitRoom;
            }
            set {
                _DoQuitRoom = value;
            }
        }
        
        public virtual Signal<DoDisconnectCommand> DoDisconnect {
            get {
                return _DoDisconnect;
            }
            set {
                _DoDisconnect = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.InitGame = new Signal<InitGameCommand>(this);
            this.DoLogin = new Signal<DoLoginCommand>(this);
            this.DoLogout = new Signal<DoLogoutCommand>(this);
            this.DoEnterRoom = new Signal<DoEnterRoomCommand>(this);
            this.DoQuitRoom = new Signal<DoQuitRoomCommand>(this);
            this.DoDisconnect = new Signal<DoDisconnectCommand>(this);
            _CanvasRootProperty = new P<CanvasRootViewModel>(this, "CanvasRoot");
            _UIFlowStatusProperty = new UIFlowSM(this, "UIFlowStatus");
            DoLogin.Subscribe(_ => UIFlowStatusProperty.Login.OnNext(true));
            DoLogout.Subscribe(_ => UIFlowStatusProperty.Logout.OnNext(true));
            DoEnterRoom.Subscribe(_ => UIFlowStatusProperty.EnterRoom.OnNext(true));
            DoQuitRoom.Subscribe(_ => UIFlowStatusProperty.QuitRoom.OnNext(true));
            DoDisconnect.Subscribe(_ => UIFlowStatusProperty.Disconnect.OnNext(true));
        }
        
        public virtual void ExecuteInitGame() {
            this.InitGame.OnNext(new InitGameCommand());
        }
        
        public virtual void ExecuteDoLogin() {
            this.DoLogin.OnNext(new DoLoginCommand());
        }
        
        public virtual void ExecuteDoLogout() {
            this.DoLogout.OnNext(new DoLogoutCommand());
        }
        
        public virtual void ExecuteDoEnterRoom() {
            this.DoEnterRoom.OnNext(new DoEnterRoomCommand());
        }
        
        public virtual void ExecuteDoQuitRoom() {
            this.DoQuitRoom.OnNext(new DoQuitRoomCommand());
        }
        
        public virtual void ExecuteDoDisconnect() {
            this.DoDisconnect.OnNext(new DoDisconnectCommand());
        }
        
        public override void Read(ISerializerStream stream) {
            base.Read(stream);
            		if (stream.DeepSerialize) this.CanvasRoot = stream.DeserializeObject<CanvasRootViewModel>("CanvasRoot");;
            this._UIFlowStatusProperty.SetState(stream.DeserializeString("UIFlowStatus"));
        }
        
        public override void Write(ISerializerStream stream) {
            base.Write(stream);
            if (stream.DeepSerialize) stream.SerializeObject("CanvasRoot", this.CanvasRoot);;
            stream.SerializeString("UIFlowStatus", this.UIFlowStatus.Name);;
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("InitGame", InitGame) { ParameterType = typeof(void) });
            list.Add(new ViewModelCommandInfo("DoLogin", DoLogin) { ParameterType = typeof(void) });
            list.Add(new ViewModelCommandInfo("DoLogout", DoLogout) { ParameterType = typeof(void) });
            list.Add(new ViewModelCommandInfo("DoEnterRoom", DoEnterRoom) { ParameterType = typeof(void) });
            list.Add(new ViewModelCommandInfo("DoQuitRoom", DoQuitRoom) { ParameterType = typeof(void) });
            list.Add(new ViewModelCommandInfo("DoDisconnect", DoDisconnect) { ParameterType = typeof(void) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_CanvasRootProperty, true, false, false, false));
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_UIFlowStatusProperty, false, false, false, false));
        }
    }
    
    public partial class OutOfGameRootViewModel {
        
        public OutOfGameRootViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
    
    public partial class CanvasRootViewModelBase : uFrame.MVVM.ViewModel {
        
        private ModelCollection<PanelViewModel> _PanelCollection;
        
        private Signal<OpenClosePanelCommand> _OpenClosePanel;
        
        public CanvasRootViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual ModelCollection<PanelViewModel> PanelCollection {
            get {
                return _PanelCollection;
            }
            set {
                _PanelCollection = value;
            }
        }
        
        public virtual Signal<OpenClosePanelCommand> OpenClosePanel {
            get {
                return _OpenClosePanel;
            }
            set {
                _OpenClosePanel = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.OpenClosePanel = new Signal<OpenClosePanelCommand>(this);
            _PanelCollection = new ModelCollection<PanelViewModel>(this, "PanelCollection");
        }
        
        public virtual void Execute(OpenClosePanelCommand argument) {
            this.OpenClosePanel.OnNext(argument);
        }
        
        public override void Read(ISerializerStream stream) {
            base.Read(stream);
            if (stream.DeepSerialize) {
                this.PanelCollection.Clear();
                this.PanelCollection.AddRange(stream.DeserializeObjectArray<PanelViewModel>("PanelCollection"));
            }
        }
        
        public override void Write(ISerializerStream stream) {
            base.Write(stream);
            if (stream.DeepSerialize) stream.SerializeArray("PanelCollection", this.PanelCollection);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("OpenClosePanel", OpenClosePanel) { ParameterType = typeof(OpenClosePanelCommand) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            list.Add(new ViewModelPropertyInfo(_PanelCollection, true, true, false, false));
        }
    }
    
    public partial class CanvasRootViewModel {
        
        public CanvasRootViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
    
    public partial class PanelViewModelBase : uFrame.MVVM.ViewModel {
        
        private Signal<PanelInCommand> _PanelIn;
        
        private Signal<PanelOutCommand> _PanelOut;
        
        public PanelViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual Signal<PanelInCommand> PanelIn {
            get {
                return _PanelIn;
            }
            set {
                _PanelIn = value;
            }
        }
        
        public virtual Signal<PanelOutCommand> PanelOut {
            get {
                return _PanelOut;
            }
            set {
                _PanelOut = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.PanelIn = new Signal<PanelInCommand>(this);
            this.PanelOut = new Signal<PanelOutCommand>(this);
        }
        
        public virtual void ExecutePanelIn() {
            this.PanelIn.OnNext(new PanelInCommand());
        }
        
        public virtual void ExecutePanelOut() {
            this.PanelOut.OnNext(new PanelOutCommand());
        }
        
        public override void Read(ISerializerStream stream) {
            base.Read(stream);
        }
        
        public override void Write(ISerializerStream stream) {
            base.Write(stream);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("PanelIn", PanelIn) { ParameterType = typeof(void) });
            list.Add(new ViewModelCommandInfo("PanelOut", PanelOut) { ParameterType = typeof(void) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
        }
    }
    
    public partial class PanelViewModel {
        
        public PanelViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
    
    public partial class LoginPanelViewModelBase : PanelViewModel {
        
        private P<String> _CustomIdProperty;
        
        private Signal<LoginCommand> _Login;
        
        public LoginPanelViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual P<String> CustomIdProperty {
            get {
                return _CustomIdProperty;
            }
            set {
                _CustomIdProperty = value;
            }
        }
        
        public virtual String CustomId {
            get {
                return CustomIdProperty.Value;
            }
            set {
                CustomIdProperty.Value = value;
            }
        }
        
        public virtual Signal<LoginCommand> Login {
            get {
                return _Login;
            }
            set {
                _Login = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.Login = new Signal<LoginCommand>(this);
            _CustomIdProperty = new P<String>(this, "CustomId");
        }
        
        public virtual void ExecuteLogin() {
            this.Login.OnNext(new LoginCommand());
        }
        
        public override void Read(ISerializerStream stream) {
            base.Read(stream);
        }
        
        public override void Write(ISerializerStream stream) {
            base.Write(stream);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("Login", Login) { ParameterType = typeof(void) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_CustomIdProperty, false, false, false, false));
        }
    }
    
    public partial class LoginPanelViewModel {
        
        public LoginPanelViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
    
    public partial class LobbyPanelViewModelBase : PanelViewModel {
        
        private P<String> _RoomIdProperty;
        
        private Signal<EnterRoomCommand> _EnterRoom;
        
        private Signal<QuitLobbyCommand> _QuitLobby;
        
        public LobbyPanelViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual P<String> RoomIdProperty {
            get {
                return _RoomIdProperty;
            }
            set {
                _RoomIdProperty = value;
            }
        }
        
        public virtual String RoomId {
            get {
                return RoomIdProperty.Value;
            }
            set {
                RoomIdProperty.Value = value;
            }
        }
        
        public virtual Signal<EnterRoomCommand> EnterRoom {
            get {
                return _EnterRoom;
            }
            set {
                _EnterRoom = value;
            }
        }
        
        public virtual Signal<QuitLobbyCommand> QuitLobby {
            get {
                return _QuitLobby;
            }
            set {
                _QuitLobby = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.EnterRoom = new Signal<EnterRoomCommand>(this);
            this.QuitLobby = new Signal<QuitLobbyCommand>(this);
            _RoomIdProperty = new P<String>(this, "RoomId");
        }
        
        public virtual void ExecuteEnterRoom() {
            this.EnterRoom.OnNext(new EnterRoomCommand());
        }
        
        public virtual void ExecuteQuitLobby() {
            this.QuitLobby.OnNext(new QuitLobbyCommand());
        }
        
        public override void Read(ISerializerStream stream) {
            base.Read(stream);
        }
        
        public override void Write(ISerializerStream stream) {
            base.Write(stream);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("EnterRoom", EnterRoom) { ParameterType = typeof(void) });
            list.Add(new ViewModelCommandInfo("QuitLobby", QuitLobby) { ParameterType = typeof(void) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_RoomIdProperty, false, false, false, false));
        }
    }
    
    public partial class LobbyPanelViewModel {
        
        public LobbyPanelViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
    
    public partial class RoomPanelViewModelBase : PanelViewModel {
        
        private ModelCollection<String> _Players;
        
        private Signal<QuitRoomCommand> _QuitRoom;
        
        private Signal<RefreshRoomCommand> _RefreshRoom;
        
        public RoomPanelViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual ModelCollection<String> Players {
            get {
                return _Players;
            }
            set {
                _Players = value;
            }
        }
        
        public virtual Signal<QuitRoomCommand> QuitRoom {
            get {
                return _QuitRoom;
            }
            set {
                _QuitRoom = value;
            }
        }
        
        public virtual Signal<RefreshRoomCommand> RefreshRoom {
            get {
                return _RefreshRoom;
            }
            set {
                _RefreshRoom = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            this.QuitRoom = new Signal<QuitRoomCommand>(this);
            this.RefreshRoom = new Signal<RefreshRoomCommand>(this);
            _Players = new ModelCollection<String>(this, "Players");
        }
        
        public virtual void ExecuteQuitRoom() {
            this.QuitRoom.OnNext(new QuitRoomCommand());
        }
        
        public virtual void ExecuteRefreshRoom() {
            this.RefreshRoom.OnNext(new RefreshRoomCommand());
        }
        
        public override void Read(ISerializerStream stream) {
            base.Read(stream);
        }
        
        public override void Write(ISerializerStream stream) {
            base.Write(stream);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModelCommandInfo> list) {
            base.FillCommands(list);
            list.Add(new ViewModelCommandInfo("QuitRoom", QuitRoom) { ParameterType = typeof(void) });
            list.Add(new ViewModelCommandInfo("RefreshRoom", RefreshRoom) { ParameterType = typeof(void) });
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            list.Add(new ViewModelPropertyInfo(_Players, false, true, false, false));
        }
    }
    
    public partial class RoomPanelViewModel {
        
        public RoomPanelViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
    
    public partial class DebugInfoPanelViewModelBase : PanelViewModel {
        
        private P<String> _TextProperty;
        
        public DebugInfoPanelViewModelBase(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
        
        public virtual P<String> TextProperty {
            get {
                return _TextProperty;
            }
            set {
                _TextProperty = value;
            }
        }
        
        public virtual String Text {
            get {
                return TextProperty.Value;
            }
            set {
                TextProperty.Value = value;
            }
        }
        
        public override void Bind() {
            base.Bind();
            _TextProperty = new P<String>(this, "Text");
        }
        
        public override void Read(ISerializerStream stream) {
            base.Read(stream);
        }
        
        public override void Write(ISerializerStream stream) {
            base.Write(stream);
        }
        
        protected override void FillCommands(System.Collections.Generic.List<uFrame.MVVM.ViewModelCommandInfo> list) {
            base.FillCommands(list);
        }
        
        protected override void FillProperties(System.Collections.Generic.List<uFrame.MVVM.ViewModelPropertyInfo> list) {
            base.FillProperties(list);
            // PropertiesChildItem
            list.Add(new ViewModelPropertyInfo(_TextProperty, false, false, false, false));
        }
    }
    
    public partial class DebugInfoPanelViewModel {
        
        public DebugInfoPanelViewModel(uFrame.Kernel.IEventAggregator aggregator) : 
                base(aggregator) {
        }
    }
}
