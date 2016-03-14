namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.MVVM;
	using uFrame.IOC;
	using uFrame.Kernel;
	using UniRx;
	using UnityEngine;

    
	public class GameService : GameServiceBase
	{
		public List<CardInfo> GetDeck ()
		{
			List<CardInfo> card_info_list = new List<CardInfo> ();
			for (int s = 0; s < 4; s++) {
				for (int n = 0; n < 13; n++) {
					card_info_list.Add (new CardInfo ((Suit)s, (NumericalValue)n));
				}
			}
			card_info_list.Add (new CardInfo (Suit.SMALL_JOKER, NumericalValue.NV_SMALL_JOKER));
			card_info_list.Add (new CardInfo (Suit.BIG_JOKER, NumericalValue.NV_BIG_JOKER));

			card_info_list.ForEach (ci => {
				Debug.Log (ci.ToString ());
			});

			return card_info_list;
		}
	}
}
