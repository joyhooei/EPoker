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
    using uFrame.Serialization;
    using UnityEngine;
    
    
    public partial class InitGameCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class DoLoginCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class DoLogoutCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class DoEnterRoomCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class DoQuitRoomCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class DoDisconnectCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class PanelInCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class PanelOutCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class LoginCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class EnterRoomCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class QuitLobbyCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class QuitRoomCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class RefreshRoomCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class RefreshRoomPropertiesCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class RefreshPlayerPropertiesCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class SetPropertiesCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class SendEventCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class OpenClosePanelCommand : ViewModelCommand {
        
        private List<Type> _OpenPanels;
        
        private List<Type> _ClosePanels;
        
        public List<Type> OpenPanels {
            get {
                return _OpenPanels;
            }
            set {
                _OpenPanels = value;
            }
        }
        
        public List<Type> ClosePanels {
            get {
                return _ClosePanels;
            }
            set {
                _ClosePanels = value;
            }
        }
    }
    
    public partial class RefreshByPlayerCommand : uFrame.MVVM.ViewModelCommand {
    }
    
    public partial class RefreshEventCommand : ViewModelCommand {
        
        private Byte _EventCode;
        
        private Dictionary<byte, object> _EventContent;
        
        public Byte EventCode {
            get {
                return _EventCode;
            }
            set {
                _EventCode = value;
            }
        }
        
        public Dictionary<byte, object> EventContent {
            get {
                return _EventContent;
            }
            set {
                _EventContent = value;
            }
        }
    }
}
