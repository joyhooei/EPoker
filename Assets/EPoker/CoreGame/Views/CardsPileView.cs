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
    
    
    public class CardsPileView : CardsPileViewBase {
        
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
        }
    
    public override uFrame.MVVM.ViewBase CardsCreateView(uFrame.MVVM.ViewModel viewModel) {
        return InstantiateView(viewModel);
    }
    
    public override void CardsAdded(uFrame.MVVM.ViewBase view) {
    }
    
    public override void CardsRemoved(uFrame.MVVM.ViewBase view) {
    }
    }
}