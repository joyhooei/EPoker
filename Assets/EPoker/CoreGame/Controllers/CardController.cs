namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.Kernel;
	using uFrame.IOC;
	using uFrame.MVVM;
	using uFrame.Serialization;
	using UniRx;

    
	public class CardController : CardControllerBase
	{
        
		public override void InitializeCard (CardViewModel viewModel)
		{
			base.InitializeCard (viewModel);
		}

		public override void SelectCard (CardViewModel viewModel)
		{
			base.SelectCard (viewModel);
		}

		public override void DeselectCard (CardViewModel viewModel)
		{
			base.DeselectCard (viewModel);
		}
	}
}
