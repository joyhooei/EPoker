namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using yigame.epoker;
	using uFrame.MVVM;
	using uFrame.Serialization;
	using UniRx;
	using uFrame.Kernel;
	using uFrame.IOC;

    
	public class CardsPileController : CardsPileControllerBase
	{
        
		public override void InitializeCardsPile (CardsPileViewModel viewModel)
		{
			base.InitializeCardsPile (viewModel);
			// This is called when a CardsPileViewModel is created
		}

		public override void PileCardsReorder (CardsPileViewModel viewModel)
		{
			base.PileCardsReorder (viewModel);

			List<CardViewModel> cards = viewModel.Cards.ToList ();

			cards.Sort ((a, b) => {
				return CardInfo.SingleInHandCompare (a.Info, b.Info);
			});

			cards.Select ((vm, idx) => {
				return new {vm, idx};
			}).ToList ().ForEach (t => {
				t.vm.PosIdx = t.idx;
				t.vm.TotalCount = cards.Count;
			});
		}
	}
}
