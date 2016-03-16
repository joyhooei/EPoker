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
    using yigame.epoker;
    
    
    public class OutOfGameLoaderBase : uFrame.Kernel.SystemLoader {
        
        private OutOfGameRootViewModel _OutOfGameRoot;
        
        private DebugInfoPanelViewModel _DebugInfoPanel;
        
        private OutOfGameRootController _OutOfGameRootController;
        
        private CanvasRootController _CanvasRootController;
        
        private PanelController _PanelController;
        
        private LoginPanelController _LoginPanelController;
        
        private LobbyPanelController _LobbyPanelController;
        
        private RoomPanelController _RoomPanelController;
        
        private DebugInfoPanelController _DebugInfoPanelController;
        
        [uFrame.IOC.InjectAttribute("OutOfGameRoot")]
        public virtual OutOfGameRootViewModel OutOfGameRoot {
            get {
                if (this._OutOfGameRoot == null) {
                    this._OutOfGameRoot = this.CreateViewModel<OutOfGameRootViewModel>( "OutOfGameRoot");
                }
                return _OutOfGameRoot;
            }
            set {
            }
        }
        
        [uFrame.IOC.InjectAttribute("DebugInfoPanel")]
        public virtual DebugInfoPanelViewModel DebugInfoPanel {
            get {
                if (this._DebugInfoPanel == null) {
                    this._DebugInfoPanel = this.CreateViewModel<DebugInfoPanelViewModel>( "DebugInfoPanel");
                }
                return _DebugInfoPanel;
            }
            set {
            }
        }
        
        [uFrame.IOC.InjectAttribute()]
        public virtual OutOfGameRootController OutOfGameRootController {
            get {
                if (_OutOfGameRootController==null) {
                    _OutOfGameRootController = Container.CreateInstance(typeof(OutOfGameRootController)) as OutOfGameRootController;;
                }
                return _OutOfGameRootController;
            }
            set {
                _OutOfGameRootController = value;
            }
        }
        
        [uFrame.IOC.InjectAttribute()]
        public virtual CanvasRootController CanvasRootController {
            get {
                if (_CanvasRootController==null) {
                    _CanvasRootController = Container.CreateInstance(typeof(CanvasRootController)) as CanvasRootController;;
                }
                return _CanvasRootController;
            }
            set {
                _CanvasRootController = value;
            }
        }
        
        [uFrame.IOC.InjectAttribute()]
        public virtual PanelController PanelController {
            get {
                if (_PanelController==null) {
                    _PanelController = Container.CreateInstance(typeof(PanelController)) as PanelController;;
                }
                return _PanelController;
            }
            set {
                _PanelController = value;
            }
        }
        
        [uFrame.IOC.InjectAttribute()]
        public virtual LoginPanelController LoginPanelController {
            get {
                if (_LoginPanelController==null) {
                    _LoginPanelController = Container.CreateInstance(typeof(LoginPanelController)) as LoginPanelController;;
                }
                return _LoginPanelController;
            }
            set {
                _LoginPanelController = value;
            }
        }
        
        [uFrame.IOC.InjectAttribute()]
        public virtual LobbyPanelController LobbyPanelController {
            get {
                if (_LobbyPanelController==null) {
                    _LobbyPanelController = Container.CreateInstance(typeof(LobbyPanelController)) as LobbyPanelController;;
                }
                return _LobbyPanelController;
            }
            set {
                _LobbyPanelController = value;
            }
        }
        
        [uFrame.IOC.InjectAttribute()]
        public virtual RoomPanelController RoomPanelController {
            get {
                if (_RoomPanelController==null) {
                    _RoomPanelController = Container.CreateInstance(typeof(RoomPanelController)) as RoomPanelController;;
                }
                return _RoomPanelController;
            }
            set {
                _RoomPanelController = value;
            }
        }
        
        [uFrame.IOC.InjectAttribute()]
        public virtual DebugInfoPanelController DebugInfoPanelController {
            get {
                if (_DebugInfoPanelController==null) {
                    _DebugInfoPanelController = Container.CreateInstance(typeof(DebugInfoPanelController)) as DebugInfoPanelController;;
                }
                return _DebugInfoPanelController;
            }
            set {
                _DebugInfoPanelController = value;
            }
        }
        
        public override void Load() {
            Container.RegisterViewModelManager<OutOfGameRootViewModel>(new ViewModelManager<OutOfGameRootViewModel>());
            Container.RegisterController<OutOfGameRootController>(OutOfGameRootController);
            Container.RegisterViewModelManager<CanvasRootViewModel>(new ViewModelManager<CanvasRootViewModel>());
            Container.RegisterController<CanvasRootController>(CanvasRootController);
            Container.RegisterViewModelManager<PanelViewModel>(new ViewModelManager<PanelViewModel>());
            Container.RegisterController<PanelController>(PanelController);
            Container.RegisterViewModelManager<LoginPanelViewModel>(new ViewModelManager<LoginPanelViewModel>());
            Container.RegisterController<LoginPanelController>(LoginPanelController);
            Container.RegisterViewModelManager<LobbyPanelViewModel>(new ViewModelManager<LobbyPanelViewModel>());
            Container.RegisterController<LobbyPanelController>(LobbyPanelController);
            Container.RegisterViewModelManager<RoomPanelViewModel>(new ViewModelManager<RoomPanelViewModel>());
            Container.RegisterController<RoomPanelController>(RoomPanelController);
            Container.RegisterViewModelManager<DebugInfoPanelViewModel>(new ViewModelManager<DebugInfoPanelViewModel>());
            Container.RegisterController<DebugInfoPanelController>(DebugInfoPanelController);
            Container.RegisterViewModel<OutOfGameRootViewModel>(OutOfGameRoot, "OutOfGameRoot");
            Container.RegisterViewModel<DebugInfoPanelViewModel>(DebugInfoPanel, "DebugInfoPanel");
        }
    }
}
