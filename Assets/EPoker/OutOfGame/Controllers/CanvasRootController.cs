namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using UniRx;
	using uFrame.Serialization;
	using uFrame.IOC;
	using uFrame.Kernel;
	using uFrame.MVVM;

    
	public class CanvasRootController : CanvasRootControllerBase
	{
        
		public override void InitializeCanvasRoot (CanvasRootViewModel viewModel)
		{
			base.InitializeCanvasRoot (viewModel);
			// This is called when a CanvasRootViewModel is created
		}

		public override void OpenClosePanel (CanvasRootViewModel viewModel, OpenClosePanelCommand arg)
		{
			base.OpenClosePanel (viewModel, arg);

			List<PanelViewModel> vmList = OutOfGameRoot.CanvasRoot.PanelCollection.Select (_ => (PanelViewModel)_).ToList ();
			vmList.ForEach (panelVM => {
				if (arg.OpenPanels.Exists (t => t == panelVM.GetType ())) {
					panelVM.ExecutePanelIn ();
				}
				if (arg.OpenPanels.Exists (t => t == panelVM.GetType ())) {
					panelVM.ExecutePanelOut ();
				}
			});
		}
	}
}
