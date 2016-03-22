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
    using uFrame.Kernel;
    using uFrame.MVVM;
    using uFrame.MVVM.Services;
    using uFrame.MVVM.Bindings;
    using uFrame.Serialization;
    using UniRx;
    using UnityEngine;
    using yigame.epoker;
    
    
    public class OutOfGameRootViewBase : uFrame.MVVM.ViewBase {
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public uFrame.MVVM.ViewBase _CanvasRoot;
        
        [UFToggleGroup("UIFlowStatus")]
        [UnityEngine.HideInInspector()]
        public bool _BindUIFlowStatus = true;
        
        [UFGroup("UIFlowStatus")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_UIFlowStatusonlyWhenChanged")]
        protected bool _UIFlowStatusOnlyWhenChanged;
        
        public override string DefaultIdentifier {
            get {
                return "OutOfGameRoot";
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(OutOfGameRootViewModel);
            }
        }
        
        public OutOfGameRootViewModel OutOfGameRoot {
            get {
                return (OutOfGameRootViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as OutOfGameRootViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var outofgamerootview = ((OutOfGameRootViewModel)model);
            outofgamerootview.CanvasRoot = this._CanvasRoot == null ? null :  ViewService.FetchViewModel(this._CanvasRoot) as CanvasRootViewModel;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.OutOfGameRoot to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindUIFlowStatus) {
                this.BindStateProperty(this.OutOfGameRoot.UIFlowStatusProperty, this.UIFlowStatusChanged, _UIFlowStatusOnlyWhenChanged);
            }
        }
        
        public virtual void UIFlowStatusChanged(Invert.StateMachine.State arg1) {
            if (arg1 is UILogin) {
                this.OnUILogin();
            }
            if (arg1 is UILobby) {
                this.OnUILobby();
            }
            if (arg1 is UIRoom) {
                this.OnUIRoom();
            }
        }
        
        public virtual void OnUILogin() {
        }
        
        public virtual void OnUILobby() {
        }
        
        public virtual void OnUIRoom() {
        }
        
        public virtual void ExecuteInitGame() {
            OutOfGameRoot.InitGame.OnNext(new InitGameCommand() { Sender = OutOfGameRoot });
        }
        
        public virtual void ExecuteDoLogin() {
            OutOfGameRoot.DoLogin.OnNext(new DoLoginCommand() { Sender = OutOfGameRoot });
        }
        
        public virtual void ExecuteDoLogout() {
            OutOfGameRoot.DoLogout.OnNext(new DoLogoutCommand() { Sender = OutOfGameRoot });
        }
        
        public virtual void ExecuteDoEnterRoom() {
            OutOfGameRoot.DoEnterRoom.OnNext(new DoEnterRoomCommand() { Sender = OutOfGameRoot });
        }
        
        public virtual void ExecuteDoQuitRoom() {
            OutOfGameRoot.DoQuitRoom.OnNext(new DoQuitRoomCommand() { Sender = OutOfGameRoot });
        }
        
        public virtual void ExecuteDoDisconnect() {
            OutOfGameRoot.DoDisconnect.OnNext(new DoDisconnectCommand() { Sender = OutOfGameRoot });
        }
        
        public virtual void ExecuteInitGame(InitGameCommand command) {
            command.Sender = OutOfGameRoot;
            OutOfGameRoot.InitGame.OnNext(command);
        }
        
        public virtual void ExecuteDoLogin(DoLoginCommand command) {
            command.Sender = OutOfGameRoot;
            OutOfGameRoot.DoLogin.OnNext(command);
        }
        
        public virtual void ExecuteDoLogout(DoLogoutCommand command) {
            command.Sender = OutOfGameRoot;
            OutOfGameRoot.DoLogout.OnNext(command);
        }
        
        public virtual void ExecuteDoEnterRoom(DoEnterRoomCommand command) {
            command.Sender = OutOfGameRoot;
            OutOfGameRoot.DoEnterRoom.OnNext(command);
        }
        
        public virtual void ExecuteDoQuitRoom(DoQuitRoomCommand command) {
            command.Sender = OutOfGameRoot;
            OutOfGameRoot.DoQuitRoom.OnNext(command);
        }
        
        public virtual void ExecuteDoDisconnect(DoDisconnectCommand command) {
            command.Sender = OutOfGameRoot;
            OutOfGameRoot.DoDisconnect.OnNext(command);
        }
    }
    
    public class CanvasRootViewBase : uFrame.MVVM.ViewBase {
        
        [UFToggleGroup("PanelCollection")]
        [UnityEngine.HideInInspector()]
        public bool _BindPanelCollection = true;
        
        [UFGroup("PanelCollection")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_PanelCollectionparent")]
        protected UnityEngine.Transform _PanelCollectionParent;
        
        [UFGroup("PanelCollection")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_PanelCollectionviewFirst")]
        protected bool _PanelCollectionViewFirst;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(CanvasRootViewModel);
            }
        }
        
        public CanvasRootViewModel CanvasRoot {
            get {
                return (CanvasRootViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as CanvasRootViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.CanvasRoot to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindPanelCollection) {
                this.BindToViewCollection(this.CanvasRoot.PanelCollection, this.PanelCollectionCreateView, this.PanelCollectionAdded, this.PanelCollectionRemoved, _PanelCollectionParent, _PanelCollectionViewFirst);
            }
        }
        
        public virtual uFrame.MVVM.ViewBase PanelCollectionCreateView(uFrame.MVVM.ViewModel viewModel) {
            return InstantiateView(viewModel);
        }
        
        public virtual void PanelCollectionAdded(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void PanelCollectionRemoved(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void ExecuteOpenClosePanel(OpenClosePanelCommand command) {
            command.Sender = CanvasRoot;
            CanvasRoot.OpenClosePanel.OnNext(command);
        }
    }
    
    public class PanelViewBase : uFrame.MVVM.ViewBase {
        
        [UFToggleGroup("PanelIn")]
        [UnityEngine.HideInInspector()]
        public bool _BindPanelIn = true;
        
        [UFToggleGroup("PanelOut")]
        [UnityEngine.HideInInspector()]
        public bool _BindPanelOut = true;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(PanelViewModel);
            }
        }
        
        public PanelViewModel Panel {
            get {
                return (PanelViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as PanelViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Panel to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindPanelIn) {
                this.BindCommandExecuted(this.Panel.PanelIn, this.PanelInExecuted);
            }
            if (_BindPanelOut) {
                this.BindCommandExecuted(this.Panel.PanelOut, this.PanelOutExecuted);
            }
        }
        
        public virtual void PanelInExecuted(PanelInCommand command) {
        }
        
        public virtual void PanelOutExecuted(PanelOutCommand command) {
        }
        
        public virtual void ExecutePanelIn() {
            Panel.PanelIn.OnNext(new PanelInCommand() { Sender = Panel });
        }
        
        public virtual void ExecutePanelOut() {
            Panel.PanelOut.OnNext(new PanelOutCommand() { Sender = Panel });
        }
        
        public virtual void ExecutePanelIn(PanelInCommand command) {
            command.Sender = Panel;
            Panel.PanelIn.OnNext(command);
        }
        
        public virtual void ExecutePanelOut(PanelOutCommand command) {
            command.Sender = Panel;
            Panel.PanelOut.OnNext(command);
        }
    }
    
    public class LoginPanelViewBase : PanelView {
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _CustomId;
        
        [UFToggleGroup("CustomId")]
        [UnityEngine.HideInInspector()]
        public bool _BindCustomId = true;
        
        [UFGroup("CustomId")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_CustomIdinput")]
        protected UnityEngine.UI.InputField _CustomIdInput;
        
        [UFToggleGroup("Login")]
        [UnityEngine.HideInInspector()]
        public bool _BindLogin = true;
        
        [UFGroup("Login")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_Loginbutton")]
        protected UnityEngine.UI.Button _LoginButton;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(LoginPanelViewModel);
            }
        }
        
        public LoginPanelViewModel LoginPanel {
            get {
                return (LoginPanelViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as LoginPanelViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var loginpanelview = ((LoginPanelViewModel)model);
            loginpanelview.CustomId = this._CustomId;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.LoginPanel to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindCustomId) {
                this.BindInputFieldToProperty(_CustomIdInput, this.LoginPanel.CustomIdProperty);
            }
            if (_BindLogin) {
                this.BindButtonToCommand(_LoginButton, this.LoginPanel.Login);
            }
            if (_BindLogin) {
                this.BindCommandExecuted(this.LoginPanel.Login, this.LoginExecuted);
            }
        }
        
        public virtual void LoginExecuted(LoginCommand command) {
        }
        
        public virtual void ExecuteLogin() {
            LoginPanel.Login.OnNext(new LoginCommand() { Sender = LoginPanel });
        }
        
        public virtual void ExecuteLogin(LoginCommand command) {
            command.Sender = LoginPanel;
            LoginPanel.Login.OnNext(command);
        }
    }
    
    public class LobbyPanelViewBase : PanelView {
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _RoomId;
        
        [UFToggleGroup("RoomId")]
        [UnityEngine.HideInInspector()]
        public bool _BindRoomId = true;
        
        [UFGroup("RoomId")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_RoomIdinput")]
        protected UnityEngine.UI.InputField _RoomIdInput;
        
        [UFToggleGroup("EnterRoom")]
        [UnityEngine.HideInInspector()]
        public bool _BindEnterRoom = true;
        
        [UFGroup("EnterRoom")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_EnterRoombutton")]
        protected UnityEngine.UI.Button _EnterRoomButton;
        
        [UFToggleGroup("QuitLobby")]
        [UnityEngine.HideInInspector()]
        public bool _BindQuitLobby = true;
        
        [UFGroup("QuitLobby")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_QuitLobbybutton")]
        protected UnityEngine.UI.Button _QuitLobbyButton;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(LobbyPanelViewModel);
            }
        }
        
        public LobbyPanelViewModel LobbyPanel {
            get {
                return (LobbyPanelViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as LobbyPanelViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var lobbypanelview = ((LobbyPanelViewModel)model);
            lobbypanelview.RoomId = this._RoomId;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.LobbyPanel to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindRoomId) {
                this.BindInputFieldToProperty(_RoomIdInput, this.LobbyPanel.RoomIdProperty);
            }
            if (_BindEnterRoom) {
                this.BindButtonToCommand(_EnterRoomButton, this.LobbyPanel.EnterRoom);
            }
            if (_BindQuitLobby) {
                this.BindButtonToCommand(_QuitLobbyButton, this.LobbyPanel.QuitLobby);
            }
        }
        
        public virtual void ExecuteEnterRoom() {
            LobbyPanel.EnterRoom.OnNext(new EnterRoomCommand() { Sender = LobbyPanel });
        }
        
        public virtual void ExecuteQuitLobby() {
            LobbyPanel.QuitLobby.OnNext(new QuitLobbyCommand() { Sender = LobbyPanel });
        }
        
        public virtual void ExecuteEnterRoom(EnterRoomCommand command) {
            command.Sender = LobbyPanel;
            LobbyPanel.EnterRoom.OnNext(command);
        }
        
        public virtual void ExecuteQuitLobby(QuitLobbyCommand command) {
            command.Sender = LobbyPanel;
            LobbyPanel.QuitLobby.OnNext(command);
        }
    }
    
    public class RoomPanelViewBase : PanelView {
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _RoomPropertiesJson;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _EventParamsJson;
        
        [UFToggleGroup("QuitRoom")]
        [UnityEngine.HideInInspector()]
        public bool _BindQuitRoom = true;
        
        [UFGroup("QuitRoom")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_QuitRoombutton")]
        protected UnityEngine.UI.Button _QuitRoomButton;
        
        [UFToggleGroup("RefreshRoom")]
        [UnityEngine.HideInInspector()]
        public bool _BindRefreshRoom = true;
        
        [UFToggleGroup("PlayerItems")]
        [UnityEngine.HideInInspector()]
        public bool _BindPlayerItems = true;
        
        [UFGroup("PlayerItems")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_PlayerItemsparent")]
        protected UnityEngine.Transform _PlayerItemsParent;
        
        [UFGroup("PlayerItems")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_PlayerItemsviewFirst")]
        protected bool _PlayerItemsViewFirst;
        
        [UFToggleGroup("RefreshRoomProperties")]
        [UnityEngine.HideInInspector()]
        public bool _BindRefreshRoomProperties = true;
        
        [UFToggleGroup("RoomPropertiesJson")]
        [UnityEngine.HideInInspector()]
        public bool _BindRoomPropertiesJson = true;
        
        [UFGroup("RoomPropertiesJson")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_RoomPropertiesJsoninput")]
        protected UnityEngine.UI.InputField _RoomPropertiesJsonInput;
        
        [UFToggleGroup("SetProperties")]
        [UnityEngine.HideInInspector()]
        public bool _BindSetProperties = true;
        
        [UFGroup("SetProperties")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_SetPropertiesbutton")]
        protected UnityEngine.UI.Button _SetPropertiesButton;
        
        [UFToggleGroup("EventParamsJson")]
        [UnityEngine.HideInInspector()]
        public bool _BindEventParamsJson = true;
        
        [UFGroup("EventParamsJson")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_EventParamsJsoninput")]
        protected UnityEngine.UI.InputField _EventParamsJsonInput;
        
        [UFToggleGroup("SendEvent")]
        [UnityEngine.HideInInspector()]
        public bool _BindSendEvent = true;
        
        [UFGroup("SendEvent")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_SendEventbutton")]
        protected UnityEngine.UI.Button _SendEventButton;
        
        [UFToggleGroup("RefreshEvent")]
        [UnityEngine.HideInInspector()]
        public bool _BindRefreshEvent = true;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(RoomPanelViewModel);
            }
        }
        
        public RoomPanelViewModel RoomPanel {
            get {
                return (RoomPanelViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as RoomPanelViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var roompanelview = ((RoomPanelViewModel)model);
            roompanelview.RoomPropertiesJson = this._RoomPropertiesJson;
            roompanelview.EventParamsJson = this._EventParamsJson;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.RoomPanel to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindQuitRoom) {
                this.BindButtonToCommand(_QuitRoomButton, this.RoomPanel.QuitRoom);
            }
            if (_BindRefreshRoom) {
                this.BindCommandExecuted(this.RoomPanel.RefreshRoom, this.RefreshRoomExecuted);
            }
            if (_BindPlayerItems) {
                this.BindToViewCollection(this.RoomPanel.PlayerItems, this.PlayerItemsCreateView, this.PlayerItemsAdded, this.PlayerItemsRemoved, _PlayerItemsParent, _PlayerItemsViewFirst);
            }
            if (_BindRefreshRoomProperties) {
                this.BindCommandExecuted(this.RoomPanel.RefreshRoomProperties, this.RefreshRoomPropertiesExecuted);
            }
            if (_BindRoomPropertiesJson) {
                this.BindInputFieldToProperty(_RoomPropertiesJsonInput, this.RoomPanel.RoomPropertiesJsonProperty);
            }
            if (_BindSetProperties) {
                this.BindButtonToCommand(_SetPropertiesButton, this.RoomPanel.SetProperties);
            }
            if (_BindEventParamsJson) {
                this.BindInputFieldToProperty(_EventParamsJsonInput, this.RoomPanel.EventParamsJsonProperty);
            }
            if (_BindSendEvent) {
                this.BindButtonToCommand(_SendEventButton, this.RoomPanel.SendEvent);
            }
            if (_BindRefreshEvent) {
                this.BindCommandExecuted(this.RoomPanel.RefreshEvent, this.RefreshEventExecuted);
            }
        }
        
        public virtual void RefreshRoomExecuted(RefreshRoomCommand command) {
        }
        
        public virtual uFrame.MVVM.ViewBase PlayerItemsCreateView(uFrame.MVVM.ViewModel viewModel) {
            return InstantiateView(viewModel);
        }
        
        public virtual void PlayerItemsAdded(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void PlayerItemsRemoved(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void RefreshRoomPropertiesExecuted(RefreshRoomPropertiesCommand command) {
        }
        
        public virtual void RefreshEventExecuted(RefreshEventCommand command) {
        }
        
        public virtual void ExecuteQuitRoom() {
            RoomPanel.QuitRoom.OnNext(new QuitRoomCommand() { Sender = RoomPanel });
        }
        
        public virtual void ExecuteRefreshRoom() {
            RoomPanel.RefreshRoom.OnNext(new RefreshRoomCommand() { Sender = RoomPanel });
        }
        
        public virtual void ExecuteRefreshRoomProperties() {
            RoomPanel.RefreshRoomProperties.OnNext(new RefreshRoomPropertiesCommand() { Sender = RoomPanel });
        }
        
        public virtual void ExecuteRefreshPlayerProperties() {
            RoomPanel.RefreshPlayerProperties.OnNext(new RefreshPlayerPropertiesCommand() { Sender = RoomPanel });
        }
        
        public virtual void ExecuteSetProperties() {
            RoomPanel.SetProperties.OnNext(new SetPropertiesCommand() { Sender = RoomPanel });
        }
        
        public virtual void ExecuteSendEvent() {
            RoomPanel.SendEvent.OnNext(new SendEventCommand() { Sender = RoomPanel });
        }
        
        public virtual void ExecuteQuitRoom(QuitRoomCommand command) {
            command.Sender = RoomPanel;
            RoomPanel.QuitRoom.OnNext(command);
        }
        
        public virtual void ExecuteRefreshRoom(RefreshRoomCommand command) {
            command.Sender = RoomPanel;
            RoomPanel.RefreshRoom.OnNext(command);
        }
        
        public virtual void ExecuteRefreshRoomProperties(RefreshRoomPropertiesCommand command) {
            command.Sender = RoomPanel;
            RoomPanel.RefreshRoomProperties.OnNext(command);
        }
        
        public virtual void ExecuteRefreshPlayerProperties(RefreshPlayerPropertiesCommand command) {
            command.Sender = RoomPanel;
            RoomPanel.RefreshPlayerProperties.OnNext(command);
        }
        
        public virtual void ExecuteSetProperties(SetPropertiesCommand command) {
            command.Sender = RoomPanel;
            RoomPanel.SetProperties.OnNext(command);
        }
        
        public virtual void ExecuteSendEvent(SendEventCommand command) {
            command.Sender = RoomPanel;
            RoomPanel.SendEvent.OnNext(command);
        }
        
        public virtual void ExecuteRefreshEvent(RefreshEventCommand command) {
            command.Sender = RoomPanel;
            RoomPanel.RefreshEvent.OnNext(command);
        }
    }
    
    public class DebugInfoPanelViewBase : uFrame.MVVM.ViewBase {
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _Text;
        
        [UFToggleGroup("Text")]
        [UnityEngine.HideInInspector()]
        public bool _BindText = true;
        
        [UFGroup("Text")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_Textinput")]
        protected UnityEngine.UI.Text _TextInput;
        
        public override string DefaultIdentifier {
            get {
                return "DebugInfoPanel";
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(DebugInfoPanelViewModel);
            }
        }
        
        public DebugInfoPanelViewModel DebugInfoPanel {
            get {
                return (DebugInfoPanelViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as DebugInfoPanelViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var debuginfopanelview = ((DebugInfoPanelViewModel)model);
            debuginfopanelview.Text = this._Text;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.DebugInfoPanel to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindText) {
                this.BindTextToProperty(_TextInput, this.DebugInfoPanel.TextProperty);
            }
        }
        
        public virtual void ExecutePanelIn() {
            DebugInfoPanel.PanelIn.OnNext(new PanelInCommand() { Sender = DebugInfoPanel });
        }
        
        public virtual void ExecutePanelOut() {
            DebugInfoPanel.PanelOut.OnNext(new PanelOutCommand() { Sender = DebugInfoPanel });
        }
        
        public virtual void ExecutePanelIn(PanelInCommand command) {
            command.Sender = DebugInfoPanel;
            DebugInfoPanel.PanelIn.OnNext(command);
        }
        
        public virtual void ExecutePanelOut(PanelOutCommand command) {
            command.Sender = DebugInfoPanel;
            DebugInfoPanel.PanelOut.OnNext(command);
        }
    }
    
    public class PlayerItemViewBase : uFrame.MVVM.ViewBase {
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Int32 _ActerId;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _Name;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Boolean _Ready;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public ExitGames.Client.Photon.LoadBalancing.Player _Player;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Boolean _IsLocal;
        
        [UFToggleGroup("Name")]
        [UnityEngine.HideInInspector()]
        public bool _BindName = true;
        
        [UFGroup("Name")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_Nameinput")]
        protected UnityEngine.UI.Text _NameInput;
        
        [UFToggleGroup("Ready")]
        [UnityEngine.HideInInspector()]
        public bool _BindReady = true;
        
        [UFGroup("Ready")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_Readytoggle")]
        protected UnityEngine.UI.Toggle _ReadyToggle;
        
        [UFGroup("Ready")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_ReadyonlyWhenChanged")]
        protected bool _ReadyOnlyWhenChanged;
        
        [UFToggleGroup("IsLocal")]
        [UnityEngine.HideInInspector()]
        public bool _BindIsLocal = true;
        
        [UFGroup("IsLocal")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_IsLocalonlyWhenChanged")]
        protected bool _IsLocalOnlyWhenChanged;
        
        [UFToggleGroup("ActerId")]
        [UnityEngine.HideInInspector()]
        public bool _BindActerId = true;
        
        [UFGroup("ActerId")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_ActerIdonlyWhenChanged")]
        protected bool _ActerIdOnlyWhenChanged;
        
        [UFToggleGroup("RefreshByPlayer")]
        [UnityEngine.HideInInspector()]
        public bool _BindRefreshByPlayer = true;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(PlayerItemViewModel);
            }
        }
        
        public PlayerItemViewModel PlayerItem {
            get {
                return (PlayerItemViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as PlayerItemViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var playeritemview = ((PlayerItemViewModel)model);
            playeritemview.ActerId = this._ActerId;
            playeritemview.Name = this._Name;
            playeritemview.Ready = this._Ready;
            playeritemview.Player = this._Player;
            playeritemview.IsLocal = this._IsLocal;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.PlayerItem to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindName) {
                this.BindTextToProperty(_NameInput, this.PlayerItem.NameProperty);
            }
            if (_BindReady) {
                this.BindToggleToProperty(_ReadyToggle, this.PlayerItem.ReadyProperty);
            }
            if (_BindReady) {
                this.BindProperty(this.PlayerItem.ReadyProperty, this.ReadyChanged, _ReadyOnlyWhenChanged);
            }
            if (_BindIsLocal) {
                this.BindProperty(this.PlayerItem.IsLocalProperty, this.IsLocalChanged, _IsLocalOnlyWhenChanged);
            }
            if (_BindActerId) {
                this.BindProperty(this.PlayerItem.ActerIdProperty, this.ActerIdChanged, _ActerIdOnlyWhenChanged);
            }
            if (_BindRefreshByPlayer) {
                this.BindCommandExecuted(this.PlayerItem.RefreshByPlayer, this.RefreshByPlayerExecuted);
            }
        }
        
        public virtual void ReadyChanged(Boolean arg1) {
        }
        
        public virtual void IsLocalChanged(Boolean arg1) {
        }
        
        public virtual void ActerIdChanged(Int32 arg1) {
        }
        
        public virtual void RefreshByPlayerExecuted(RefreshByPlayerCommand command) {
        }
        
        public virtual void ExecuteRefreshByPlayer() {
            PlayerItem.RefreshByPlayer.OnNext(new RefreshByPlayerCommand() { Sender = PlayerItem });
        }
        
        public virtual void ExecuteRefreshByPlayer(RefreshByPlayerCommand command) {
            command.Sender = PlayerItem;
            PlayerItem.RefreshByPlayer.OnNext(command);
        }
    }
}
