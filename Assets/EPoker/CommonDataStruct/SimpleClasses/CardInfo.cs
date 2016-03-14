namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.Kernel;
	using uFrame.MVVM;
	using uFrame.MVVM.Bindings;
	using uFrame.Serialization;
	using UnityEngine;

	[System.Serializable]
	public class CardInfo : CardInfoBase
	{

		public CardInfo ()
		{
		}

		public CardInfo (Suit suit, NumericalValue numericalValue)
		{
			this.Suit = suit;
			this.NumericalValue = numericalValue;
		}

		public override string ToString ()
		{
			return string.Format ("({0},{1})", Suit.ToString (), NumericalValue.ToString ());
		}

		public string GetCardFrontSpriteName ()
		{
			string suit_str;
			string nv_str;

			switch (Suit) {
			case Suit.SPADE:
				suit_str = "A";
				break;
			case Suit.HEART:
				suit_str = "B";
				break;
			case Suit.CLUB:
				suit_str = "C";
				break;
			case Suit.DIAMOND:
				suit_str = "D";
				break;
			case Suit.BIG_JOKER:
				suit_str = "B";
				break;
			case Suit.SMALL_JOKER:
				suit_str = "A";
				break;
			default:
				throw new ArgumentOutOfRangeException ();
			}

			switch (NumericalValue) {
			case NumericalValue.NV_ACE:
				nv_str = "0";
				break;
			case NumericalValue.NV_2:
				nv_str = "1";
				break;
			case NumericalValue.NV_3:
				nv_str = "2";
				break;
			case NumericalValue.NV_4:
				nv_str = "3";
				break;
			case NumericalValue.NV_5:
				nv_str = "4";
				break;
			case NumericalValue.NV_6:
				nv_str = "5";
				break;
			case NumericalValue.NV_7:
				nv_str = "6";
				break;
			case NumericalValue.NV_8:
				nv_str = "7";
				break;
			case NumericalValue.NV_9:
				nv_str = "8";
				break;
			case NumericalValue.NV_10:
				nv_str = "9";
				break;
			case NumericalValue.NV_JACK:
				nv_str = "10";
				break;
			case NumericalValue.NV_QUEEN:
				nv_str = "11";
				break;
			case NumericalValue.NV_KING:
				nv_str = "12";
				break;
			case NumericalValue.NV_SMALL_JOKER:
				nv_str = "13";
				break;
			case NumericalValue.NV_BIG_JOKER:
				nv_str = "13";
				break;
			default:
				throw new ArgumentOutOfRangeException ();
			}

			return string.Format ("PlayingCards_{0}_{1}", suit_str, nv_str);
		}
	}
}
