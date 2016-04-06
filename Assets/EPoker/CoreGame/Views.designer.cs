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
    
    
    public class CoreGameRootViewBase : uFrame.MVVM.ViewBase {
        
        [UnityEngine.SerializeField()]
        private PlayerPositionsVC _PlayerPositionsVC;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public uFrame.MVVM.ViewBase _BackGround;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Int32 _PlayerCount;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public uFrame.MVVM.ViewBase _Pile;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _PlayerName;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public ExitGames.Client.Photon.LoadBalancing.Room _LBRoom;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Boolean _SinglePlayerStartForTest;
        
        [UFToggleGroup("PlayerCollection")]
        [UnityEngine.HideInInspector()]
        public bool _BindPlayerCollection = true;
        
        [UFGroup("PlayerCollection")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_PlayerCollectionparent")]
        protected UnityEngine.Transform _PlayerCollectionParent;
        
        [UFGroup("PlayerCollection")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_PlayerCollectionviewFirst")]
        protected bool _PlayerCollectionViewFirst;
        
        [UFToggleGroup("CoreGameStatus")]
        [UnityEngine.HideInInspector()]
        public bool _BindCoreGameStatus = true;
        
        [UFGroup("CoreGameStatus")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_CoreGameStatusonlyWhenChanged")]
        protected bool _CoreGameStatusOnlyWhenChanged;
        
        [UFToggleGroup("QuitCoreGame")]
        [UnityEngine.HideInInspector()]
        public bool _BindQuitCoreGame = true;
        
        [UFGroup("QuitCoreGame")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_QuitCoreGamebutton")]
        protected UnityEngine.UI.Button _QuitCoreGameButton;
        
        public override string DefaultIdentifier {
            get {
                return "CoreGameRoot";
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(CoreGameRootViewModel);
            }
        }
        
        public CoreGameRootViewModel CoreGameRoot {
            get {
                return (CoreGameRootViewModel)ViewModelObject;
            }
        }
        
        public virtual PlayerPositionsVC PlayerPositionsVC {
            get {
                return _PlayerPositionsVC ?? (_PlayerPositionsVC = this.gameObject.EnsureComponent<PlayerPositionsVC>());
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as CoreGameRootViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var coregamerootview = ((CoreGameRootViewModel)model);
            coregamerootview.BackGround = this._BackGround == null ? null :  ViewService.FetchViewModel(this._BackGround) as BackGroundViewModel;
            coregamerootview.PlayerCount = this._PlayerCount;
            coregamerootview.Pile = this._Pile == null ? null :  ViewService.FetchViewModel(this._Pile) as CardsPileViewModel;
            coregamerootview.PlayerName = this._PlayerName;
            coregamerootview.LBRoom = this._LBRoom;
            coregamerootview.SinglePlayerStartForTest = this._SinglePlayerStartForTest;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.CoreGameRoot to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindPlayerCollection) {
                this.BindToViewCollection(this.CoreGameRoot.PlayerCollection, this.PlayerCollectionCreateView, this.PlayerCollectionAdded, this.PlayerCollectionRemoved, _PlayerCollectionParent, _PlayerCollectionViewFirst);
            }
            if (_BindCoreGameStatus) {
                this.BindStateProperty(this.CoreGameRoot.CoreGameStatusProperty, this.CoreGameStatusChanged, _CoreGameStatusOnlyWhenChanged);
            }
            if (_BindQuitCoreGame) {
                this.BindButtonToCommand(_QuitCoreGameButton, this.CoreGameRoot.QuitCoreGame);
            }
        }
        
        public virtual uFrame.MVVM.ViewBase PlayerCollectionCreateView(uFrame.MVVM.ViewModel viewModel) {
            return InstantiateView(viewModel);
        }
        
        public virtual void PlayerCollectionAdded(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void PlayerCollectionRemoved(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void CoreGameStatusChanged(Invert.StateMachine.State arg1) {
            if (arg1 is Waiting) {
                this.OnWaiting();
            }
            if (arg1 is Playing) {
                this.OnPlaying();
            }
        }
        
        public virtual void OnWaiting() {
        }
        
        public virtual void OnPlaying() {
        }
        
        public virtual void ExecuteRefreshCoreGame() {
            CoreGameRoot.RefreshCoreGame.OnNext(new RefreshCoreGameCommand() { Sender = CoreGameRoot });
        }
        
        public virtual void ExecuteRootMatchBegan() {
            CoreGameRoot.RootMatchBegan.OnNext(new RootMatchBeganCommand() { Sender = CoreGameRoot });
        }
        
        public virtual void ExecuteQuitCoreGame() {
            CoreGameRoot.QuitCoreGame.OnNext(new QuitCoreGameCommand() { Sender = CoreGameRoot });
        }
        
        public virtual void ExecutePlayerJoin() {
            CoreGameRoot.PlayerJoin.OnNext(new PlayerJoinCommand() { Sender = CoreGameRoot });
        }
        
        public virtual void ExecutePlayerLeave() {
            CoreGameRoot.PlayerLeave.OnNext(new PlayerLeaveCommand() { Sender = CoreGameRoot });
        }
        
        public virtual void ExecuteCalcPosIdAndRepos() {
            CoreGameRoot.CalcPosIdAndRepos.OnNext(new CalcPosIdAndReposCommand() { Sender = CoreGameRoot });
        }
        
        public virtual void ExecuteTurnNext() {
            CoreGameRoot.TurnNext.OnNext(new TurnNextCommand() { Sender = CoreGameRoot });
        }
        
        public virtual void ExecuteRefreshCoreGame(RefreshCoreGameCommand command) {
            command.Sender = CoreGameRoot;
            CoreGameRoot.RefreshCoreGame.OnNext(command);
        }
        
        public virtual void ExecuteRootMatchBegan(RootMatchBeganCommand command) {
            command.Sender = CoreGameRoot;
            CoreGameRoot.RootMatchBegan.OnNext(command);
        }
        
        public virtual void ExecuteQuitCoreGame(QuitCoreGameCommand command) {
            command.Sender = CoreGameRoot;
            CoreGameRoot.QuitCoreGame.OnNext(command);
        }
        
        public virtual void ExecutePlayerJoin(PlayerJoinCommand command) {
            command.Sender = CoreGameRoot;
            CoreGameRoot.PlayerJoin.OnNext(command);
        }
        
        public virtual void ExecutePlayerLeave(PlayerLeaveCommand command) {
            command.Sender = CoreGameRoot;
            CoreGameRoot.PlayerLeave.OnNext(command);
        }
        
        public virtual void ExecuteCalcPosIdAndRepos(CalcPosIdAndReposCommand command) {
            command.Sender = CoreGameRoot;
            CoreGameRoot.CalcPosIdAndRepos.OnNext(command);
        }
        
        public virtual void ExecuteTurnNext(TurnNextCommand command) {
            command.Sender = CoreGameRoot;
            CoreGameRoot.TurnNext.OnNext(command);
        }
    }
    
    public class BackGroundViewBase : uFrame.MVVM.ViewBase {
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(BackGroundViewModel);
            }
        }
        
        public BackGroundViewModel BackGround {
            get {
                return (BackGroundViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as BackGroundViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.BackGround to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
        }
    }
    
    public class CardViewBase : uFrame.MVVM.ViewBase {
        
        [UnityEngine.SerializeField()]
        private CardTouchVC _CardTouchVC;
        
        [UnityEngine.SerializeField()]
        private CardShadowVC _CardShadowVC;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public CardInfo _Info;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public CardFace _Face;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public CardPlace _Place;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Int32 _PosIdx;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Int32 _TotalCount;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Int32 _OwnerActorId;
        
        [UFToggleGroup("Info")]
        [UnityEngine.HideInInspector()]
        public bool _BindInfo = true;
        
        [UFGroup("Info")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_InfoonlyWhenChanged")]
        protected bool _InfoOnlyWhenChanged;
        
        [UFToggleGroup("Face")]
        [UnityEngine.HideInInspector()]
        public bool _BindFace = true;
        
        [UFGroup("Face")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_FaceonlyWhenChanged")]
        protected bool _FaceOnlyWhenChanged;
        
        [UFToggleGroup("LocalPosition")]
        [UnityEngine.HideInInspector()]
        public bool _BindLocalPosition = true;
        
        [UFGroup("LocalPosition")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_LocalPositiononlyWhenChanged")]
        protected bool _LocalPositionOnlyWhenChanged;
        
        [UFToggleGroup("SelectedStatus")]
        [UnityEngine.HideInInspector()]
        public bool _BindSelectedStatus = true;
        
        [UFGroup("SelectedStatus")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_SelectedStatusonlyWhenChanged")]
        protected bool _SelectedStatusOnlyWhenChanged;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(CardViewModel);
            }
        }
        
        public CardViewModel Card {
            get {
                return (CardViewModel)ViewModelObject;
            }
        }
        
        public virtual CardTouchVC CardTouchVC {
            get {
                return _CardTouchVC ?? (_CardTouchVC = this.gameObject.EnsureComponent<CardTouchVC>());
            }
        }
        
        public virtual CardShadowVC CardShadowVC {
            get {
                return _CardShadowVC ?? (_CardShadowVC = this.gameObject.EnsureComponent<CardShadowVC>());
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as CardViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var cardview = ((CardViewModel)model);
            cardview.Info = this._Info;
            cardview.Face = this._Face;
            cardview.Place = this._Place;
            cardview.PosIdx = this._PosIdx;
            cardview.TotalCount = this._TotalCount;
            cardview.OwnerActorId = this._OwnerActorId;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Card to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindInfo) {
                this.BindProperty(this.Card.InfoProperty, this.InfoChanged, _InfoOnlyWhenChanged);
            }
            if (_BindFace) {
                this.BindProperty(this.Card.FaceProperty, this.FaceChanged, _FaceOnlyWhenChanged);
            }
            if (_BindLocalPosition) {
                this.BindProperty(this.Card.LocalPositionProperty, this.LocalPositionChanged, _LocalPositionOnlyWhenChanged);
            }
            if (_BindSelectedStatus) {
                this.BindStateProperty(this.Card.SelectedStatusProperty, this.SelectedStatusChanged, _SelectedStatusOnlyWhenChanged);
            }
        }
        
        public virtual void InfoChanged(CardInfo arg1) {
        }
        
        public virtual void FaceChanged(CardFace arg1) {
        }
        
        public virtual void LocalPositionChanged(Vector3 arg1) {
        }
        
        public virtual void SelectedStatusChanged(Invert.StateMachine.State arg1) {
            if (arg1 is CardInit) {
                this.OnCardInit();
            }
            if (arg1 is CardSelected) {
                this.OnCardSelected();
            }
            if (arg1 is CardUnselected) {
                this.OnCardUnselected();
            }
        }
        
        public virtual void OnCardInit() {
        }
        
        public virtual void OnCardSelected() {
        }
        
        public virtual void OnCardUnselected() {
        }
        
        public virtual void ExecuteSelectCard() {
            Card.SelectCard.OnNext(new SelectCardCommand() { Sender = Card });
        }
        
        public virtual void ExecuteDeselectCard() {
            Card.DeselectCard.OnNext(new DeselectCardCommand() { Sender = Card });
        }
        
        public virtual void ExecuteSelectCard(SelectCardCommand command) {
            command.Sender = Card;
            Card.SelectCard.OnNext(command);
        }
        
        public virtual void ExecuteDeselectCard(DeselectCardCommand command) {
            command.Sender = Card;
            Card.DeselectCard.OnNext(command);
        }
    }
    
    public class PlayerViewBase : uFrame.MVVM.ViewBase {
        
        [UnityEngine.SerializeField()]
        private PlayerTestToolsVC _PlayerTestToolsVC;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _Id;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Int32 _ActorId;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _PosId;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Int32 _OrderIdx;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public RoomIdentity _PlayerRoomIdentity;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _PlayerName;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public Boolean _IsSelf;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public String _ReadyStatusText;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public ExitGames.Client.Photon.LoadBalancing.Player _LBPlayer;
        
        [UnityEngine.SerializeField()]
        [UFGroup("View Model Properties")]
        [UnityEngine.HideInInspector()]
        public PlayerNodeMode _PlayerNodeMode;
        
        [UFToggleGroup("Status")]
        [UnityEngine.HideInInspector()]
        public bool _BindStatus = true;
        
        [UFGroup("Status")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_StatusonlyWhenChanged")]
        protected bool _StatusOnlyWhenChanged;
        
        [UFToggleGroup("ReadyStatusText")]
        [UnityEngine.HideInInspector()]
        public bool _BindReadyStatusText = true;
        
        [UFGroup("ReadyStatusText")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_ReadyStatusTextinput")]
        protected UnityEngine.UI.Text _ReadyStatusTextInput;
        
        [UFToggleGroup("IsSelf")]
        [UnityEngine.HideInInspector()]
        public bool _BindIsSelf = true;
        
        [UFGroup("IsSelf")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_IsSelfonlyWhenChanged")]
        protected bool _IsSelfOnlyWhenChanged;
        
        [UFToggleGroup("RefreshPlayer")]
        [UnityEngine.HideInInspector()]
        public bool _BindRefreshPlayer = true;
        
        [UFToggleGroup("PosId")]
        [UnityEngine.HideInInspector()]
        public bool _BindPosId = true;
        
        [UFGroup("PosId")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_PosIdonlyWhenChanged")]
        protected bool _PosIdOnlyWhenChanged;
        
        [UFToggleGroup("ButtonReadyClicked")]
        [UnityEngine.HideInInspector()]
        public bool _BindButtonReadyClicked = true;
        
        [UFGroup("ButtonReadyClicked")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_ButtonReadyClickedbutton")]
        protected UnityEngine.UI.Button _ButtonReadyClickedButton;
        
        [UFToggleGroup("ButtonStartClicked")]
        [UnityEngine.HideInInspector()]
        public bool _BindButtonStartClicked = true;
        
        [UFGroup("ButtonStartClicked")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_ButtonStartClickedbutton")]
        protected UnityEngine.UI.Button _ButtonStartClickedButton;
        
        [UFToggleGroup("HandCards")]
        [UnityEngine.HideInInspector()]
        public bool _BindHandCards = true;
        
        [UFGroup("HandCards")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_HandCardsparent")]
        protected UnityEngine.Transform _HandCardsParent;
        
        [UFGroup("HandCards")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_HandCardsviewFirst")]
        protected bool _HandCardsViewFirst;
        
        [UFToggleGroup("PlayerNodeMode")]
        [UnityEngine.HideInInspector()]
        public bool _BindPlayerNodeMode = true;
        
        [UFGroup("PlayerNodeMode")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_PlayerNodeModeonlyWhenChanged")]
        protected bool _PlayerNodeModeOnlyWhenChanged;
        
        [UFToggleGroup("ButtonPassClicked")]
        [UnityEngine.HideInInspector()]
        public bool _BindButtonPassClicked = true;
        
        [UFGroup("ButtonPassClicked")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_ButtonPassClickedbutton")]
        protected UnityEngine.UI.Button _ButtonPassClickedButton;
        
        [UFToggleGroup("ButtonDealClicked")]
        [UnityEngine.HideInInspector()]
        public bool _BindButtonDealClicked = true;
        
        [UFGroup("ButtonDealClicked")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_ButtonDealClickedbutton")]
        protected UnityEngine.UI.Button _ButtonDealClickedButton;
        
        [UFToggleGroup("ShowCardsToPile")]
        [UnityEngine.HideInInspector()]
        public bool _BindShowCardsToPile = true;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(PlayerViewModel);
            }
        }
        
        public PlayerViewModel Player {
            get {
                return (PlayerViewModel)ViewModelObject;
            }
        }
        
        public virtual PlayerTestToolsVC PlayerTestToolsVC {
            get {
                return _PlayerTestToolsVC ?? (_PlayerTestToolsVC = this.gameObject.EnsureComponent<PlayerTestToolsVC>());
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as PlayerViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
            var playerview = ((PlayerViewModel)model);
            playerview.Id = this._Id;
            playerview.ActorId = this._ActorId;
            playerview.PosId = this._PosId;
            playerview.OrderIdx = this._OrderIdx;
            playerview.PlayerRoomIdentity = this._PlayerRoomIdentity;
            playerview.PlayerName = this._PlayerName;
            playerview.IsSelf = this._IsSelf;
            playerview.ReadyStatusText = this._ReadyStatusText;
            playerview.LBPlayer = this._LBPlayer;
            playerview.PlayerNodeMode = this._PlayerNodeMode;
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.Player to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindStatus) {
                this.BindStateProperty(this.Player.StatusProperty, this.StatusChanged, _StatusOnlyWhenChanged);
            }
            if (_BindReadyStatusText) {
                this.BindTextToProperty(_ReadyStatusTextInput, this.Player.ReadyStatusTextProperty);
            }
            if (_BindIsSelf) {
                this.BindProperty(this.Player.IsSelfProperty, this.IsSelfChanged, _IsSelfOnlyWhenChanged);
            }
            if (_BindRefreshPlayer) {
                this.BindCommandExecuted(this.Player.RefreshPlayer, this.RefreshPlayerExecuted);
            }
            if (_BindPosId) {
                this.BindProperty(this.Player.PosIdProperty, this.PosIdChanged, _PosIdOnlyWhenChanged);
            }
            if (_BindButtonReadyClicked) {
                this.BindButtonToCommand(_ButtonReadyClickedButton, this.Player.ButtonReadyClicked);
            }
            if (_BindButtonStartClicked) {
                this.BindButtonToCommand(_ButtonStartClickedButton, this.Player.ButtonStartClicked);
            }
            if (_BindHandCards) {
                this.BindToViewCollection(this.Player.HandCards, this.HandCardsCreateView, this.HandCardsAdded, this.HandCardsRemoved, _HandCardsParent, _HandCardsViewFirst);
            }
            if (_BindPlayerNodeMode) {
                this.BindProperty(this.Player.PlayerNodeModeProperty, this.PlayerNodeModeChanged, _PlayerNodeModeOnlyWhenChanged);
            }
            if (_BindButtonPassClicked) {
                this.BindButtonToCommand(_ButtonPassClickedButton, this.Player.ButtonPassClicked);
            }
            if (_BindButtonDealClicked) {
                this.BindButtonToCommand(_ButtonDealClickedButton, this.Player.ButtonDealClicked);
            }
            if (_BindShowCardsToPile) {
                this.BindCommandExecuted(this.Player.ShowCardsToPile, this.ShowCardsToPileExecuted);
            }
        }
        
        public virtual void StatusChanged(Invert.StateMachine.State arg1) {
            if (arg1 is Init) {
                this.OnInit();
            }
            if (arg1 is Ready) {
                this.OnReady();
            }
            if (arg1 is MatchPrepare) {
                this.OnMatchPrepare();
            }
            if (arg1 is MatchIdle) {
                this.OnMatchIdle();
            }
            if (arg1 is MatchDeal) {
                this.OnMatchDeal();
            }
            if (arg1 is MatchWin) {
                this.OnMatchWin();
            }
            if (arg1 is MatchOver) {
                this.OnMatchOver();
            }
            if (arg1 is Wait) {
                this.OnWait();
            }
        }
        
        public virtual void OnInit() {
        }
        
        public virtual void OnReady() {
        }
        
        public virtual void OnMatchPrepare() {
        }
        
        public virtual void OnMatchIdle() {
        }
        
        public virtual void OnMatchDeal() {
        }
        
        public virtual void OnMatchWin() {
        }
        
        public virtual void OnMatchOver() {
        }
        
        public virtual void OnWait() {
        }
        
        public virtual void IsSelfChanged(Boolean arg1) {
        }
        
        public virtual void RefreshPlayerExecuted(RefreshPlayerCommand command) {
        }
        
        public virtual void PosIdChanged(String arg1) {
        }
        
        public virtual uFrame.MVVM.ViewBase HandCardsCreateView(uFrame.MVVM.ViewModel viewModel) {
            return InstantiateView(viewModel);
        }
        
        public virtual void HandCardsAdded(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void HandCardsRemoved(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void PlayerNodeModeChanged(PlayerNodeMode arg1) {
        }
        
        public virtual void ShowCardsToPileExecuted(ShowCardsToPileCommand command) {
        }
        
        public virtual void ExecutePlayerReady() {
            Player.PlayerReady.OnNext(new PlayerReadyCommand() { Sender = Player });
        }
        
        public virtual void ExecutePlayerCancel() {
            Player.PlayerCancel.OnNext(new PlayerCancelCommand() { Sender = Player });
        }
        
        public virtual void ExecuteMatchBegan() {
            Player.MatchBegan.OnNext(new MatchBeganCommand() { Sender = Player });
        }
        
        public virtual void ExecuteBeganToPlay() {
            Player.BeganToPlay.OnNext(new BeganToPlayCommand() { Sender = Player });
        }
        
        public virtual void ExecuteBeganToWait() {
            Player.BeganToWait.OnNext(new BeganToWaitCommand() { Sender = Player });
        }
        
        public virtual void ExecuteTurnOn() {
            Player.TurnOn.OnNext(new TurnOnCommand() { Sender = Player });
        }
        
        public virtual void ExecuteTurnOff() {
            Player.TurnOff.OnNext(new TurnOffCommand() { Sender = Player });
        }
        
        public virtual void ExecuteWin() {
            Player.Win.OnNext(new WinCommand() { Sender = Player });
        }
        
        public virtual void ExecuteOver() {
            Player.Over.OnNext(new OverCommand() { Sender = Player });
        }
        
        public virtual void ExecuteInitOK() {
            Player.InitOK.OnNext(new InitOKCommand() { Sender = Player });
        }
        
        public virtual void ExecuteRefreshPlayer() {
            Player.RefreshPlayer.OnNext(new RefreshPlayerCommand() { Sender = Player });
        }
        
        public virtual void ExecuteButtonReadyClicked() {
            Player.ButtonReadyClicked.OnNext(new ButtonReadyClickedCommand() { Sender = Player });
        }
        
        public virtual void ExecuteButtonStartClicked() {
            Player.ButtonStartClicked.OnNext(new ButtonStartClickedCommand() { Sender = Player });
        }
        
        public virtual void ExecuteLogInfo() {
            Player.LogInfo.OnNext(new LogInfoCommand() { Sender = Player });
        }
        
        public virtual void ExecuteReorder() {
            Player.Reorder.OnNext(new ReorderCommand() { Sender = Player });
        }
        
        public virtual void ExecuteButtonPassClicked() {
            Player.ButtonPassClicked.OnNext(new ButtonPassClickedCommand() { Sender = Player });
        }
        
        public virtual void ExecuteButtonDealClicked() {
            Player.ButtonDealClicked.OnNext(new ButtonDealClickedCommand() { Sender = Player });
        }
        
        public virtual void ExecuteButtonTurnNext() {
            Player.ButtonTurnNext.OnNext(new ButtonTurnNextCommand() { Sender = Player });
        }
        
        public virtual void ExecuteShowCardsToPile() {
            Player.ShowCardsToPile.OnNext(new ShowCardsToPileCommand() { Sender = Player });
        }
        
        public virtual void ExecutePlayerReady(PlayerReadyCommand command) {
            command.Sender = Player;
            Player.PlayerReady.OnNext(command);
        }
        
        public virtual void ExecutePlayerCancel(PlayerCancelCommand command) {
            command.Sender = Player;
            Player.PlayerCancel.OnNext(command);
        }
        
        public virtual void ExecuteMatchBegan(MatchBeganCommand command) {
            command.Sender = Player;
            Player.MatchBegan.OnNext(command);
        }
        
        public virtual void ExecuteBeganToPlay(BeganToPlayCommand command) {
            command.Sender = Player;
            Player.BeganToPlay.OnNext(command);
        }
        
        public virtual void ExecuteBeganToWait(BeganToWaitCommand command) {
            command.Sender = Player;
            Player.BeganToWait.OnNext(command);
        }
        
        public virtual void ExecuteTurnOn(TurnOnCommand command) {
            command.Sender = Player;
            Player.TurnOn.OnNext(command);
        }
        
        public virtual void ExecuteTurnOff(TurnOffCommand command) {
            command.Sender = Player;
            Player.TurnOff.OnNext(command);
        }
        
        public virtual void ExecuteWin(WinCommand command) {
            command.Sender = Player;
            Player.Win.OnNext(command);
        }
        
        public virtual void ExecuteOver(OverCommand command) {
            command.Sender = Player;
            Player.Over.OnNext(command);
        }
        
        public virtual void ExecuteInitOK(InitOKCommand command) {
            command.Sender = Player;
            Player.InitOK.OnNext(command);
        }
        
        public virtual void ExecuteRefreshPlayer(RefreshPlayerCommand command) {
            command.Sender = Player;
            Player.RefreshPlayer.OnNext(command);
        }
        
        public virtual void ExecuteButtonReadyClicked(ButtonReadyClickedCommand command) {
            command.Sender = Player;
            Player.ButtonReadyClicked.OnNext(command);
        }
        
        public virtual void ExecuteButtonStartClicked(ButtonStartClickedCommand command) {
            command.Sender = Player;
            Player.ButtonStartClicked.OnNext(command);
        }
        
        public virtual void ExecuteLogInfo(LogInfoCommand command) {
            command.Sender = Player;
            Player.LogInfo.OnNext(command);
        }
        
        public virtual void ExecuteAddCards(AddCardsCommand command) {
            command.Sender = Player;
            Player.AddCards.OnNext(command);
        }
        
        public virtual void ExecuteRemoveCards(RemoveCardsCommand command) {
            command.Sender = Player;
            Player.RemoveCards.OnNext(command);
        }
        
        public virtual void ExecuteReorder(ReorderCommand command) {
            command.Sender = Player;
            Player.Reorder.OnNext(command);
        }
        
        public virtual void ExecuteButtonPassClicked(ButtonPassClickedCommand command) {
            command.Sender = Player;
            Player.ButtonPassClicked.OnNext(command);
        }
        
        public virtual void ExecuteButtonDealClicked(ButtonDealClickedCommand command) {
            command.Sender = Player;
            Player.ButtonDealClicked.OnNext(command);
        }
        
        public virtual void ExecuteButtonTurnNext(ButtonTurnNextCommand command) {
            command.Sender = Player;
            Player.ButtonTurnNext.OnNext(command);
        }
        
        public virtual void ExecuteShowCardsToPile(ShowCardsToPileCommand command) {
            command.Sender = Player;
            Player.ShowCardsToPile.OnNext(command);
        }
    }
    
    public class CardsPileViewBase : uFrame.MVVM.ViewBase {
        
        [UFToggleGroup("Cards")]
        [UnityEngine.HideInInspector()]
        public bool _BindCards = true;
        
        [UFGroup("Cards")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_Cardsparent")]
        protected UnityEngine.Transform _CardsParent;
        
        [UFGroup("Cards")]
        [UnityEngine.SerializeField()]
        [UnityEngine.HideInInspector()]
        [UnityEngine.Serialization.FormerlySerializedAsAttribute("_CardsviewFirst")]
        protected bool _CardsViewFirst;
        
        public override string DefaultIdentifier {
            get {
                return base.DefaultIdentifier;
            }
        }
        
        public override System.Type ViewModelType {
            get {
                return typeof(CardsPileViewModel);
            }
        }
        
        public CardsPileViewModel CardsPile {
            get {
                return (CardsPileViewModel)ViewModelObject;
            }
        }
        
        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
            base.InitializeViewModel(model);
            // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
            // var vm = model as CardsPileViewModel;
            // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        }
        
        public override void Bind() {
            base.Bind();
            // Use this.CardsPile to access the viewmodel.
            // Use this method to subscribe to the view-model.
            // Any designer bindings are created in the base implementation.
            if (_BindCards) {
                this.BindToViewCollection(this.CardsPile.Cards, this.CardsCreateView, this.CardsAdded, this.CardsRemoved, _CardsParent, _CardsViewFirst);
            }
        }
        
        public virtual uFrame.MVVM.ViewBase CardsCreateView(uFrame.MVVM.ViewModel viewModel) {
            return InstantiateView(viewModel);
        }
        
        public virtual void CardsAdded(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void CardsRemoved(uFrame.MVVM.ViewBase view) {
        }
        
        public virtual void ExecutePileCardsReorder() {
            CardsPile.PileCardsReorder.OnNext(new PileCardsReorderCommand() { Sender = CardsPile });
        }
        
        public virtual void ExecutePileCardsReorder(PileCardsReorderCommand command) {
            command.Sender = CardsPile;
            CardsPile.PileCardsReorder.OnNext(command);
        }
    }
}
