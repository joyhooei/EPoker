namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
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

		public static bool ValueEqual (CardInfo a, CardInfo b)
		{
			return a.Suit == b.Suit && a.NumericalValue == b.NumericalValue;
		}

		public override string ToString ()
		{
			return string.Format ("({0},{1})", Suit.ToString (), NumericalValue.ToString ());
		}

		private static readonly string _parse_pattern = "\\(([_A-Z0-9]+),([_A-Z0-9]+)\\)";

		public static CardInfo Parse (string str)
		{
			Match m = Regex.Match (str, _parse_pattern);
			return new CardInfo (
				(Suit)Enum.Parse (typeof(Suit), m.Groups [1].Value), 
				(NumericalValue)Enum.Parse (typeof(NumericalValue), m.Groups [2].Value)
			);
		}

		public static int SingleInHandCompare (CardInfo a, CardInfo b)
		{
			return NumericalValueToCardSize (a.NumericalValue).CompareTo (NumericalValueToCardSize (b.NumericalValue));
		}

		public static int NumericalValueToCardSize (NumericalValue nv)
		{
			if (nv >= NumericalValue.NV_3 && nv <= NumericalValue.NV_KING) {
				return (int)nv;
			} else if (nv == NumericalValue.NV_ACE) {
				return 20;
			} else if (nv == NumericalValue.NV_2) {
				return 21;
			} else if (nv == NumericalValue.NV_SMALL_JOKER) {
				return 30;
			} else if (nv == NumericalValue.NV_BIG_JOKER) {
				return 31;
			}
			return -1;
		}

		public string GetCardFrontPrefabName ()
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
				suit_str = "joker";
				break;
			case Suit.SMALL_JOKER:
				suit_str = "joker";
				break;
			default:
				throw new ArgumentOutOfRangeException ();
			}

			switch (NumericalValue) {
			case NumericalValue.NV_ACE:
				nv_str = "1";
				break;
			case NumericalValue.NV_2:
				nv_str = "2";
				break;
			case NumericalValue.NV_3:
				nv_str = "3";
				break;
			case NumericalValue.NV_4:
				nv_str = "4";
				break;
			case NumericalValue.NV_5:
				nv_str = "5";
				break;
			case NumericalValue.NV_6:
				nv_str = "6";
				break;
			case NumericalValue.NV_7:
				nv_str = "7";
				break;
			case NumericalValue.NV_8:
				nv_str = "8";
				break;
			case NumericalValue.NV_9:
				nv_str = "9";
				break;
			case NumericalValue.NV_10:
				nv_str = "10";
				break;
			case NumericalValue.NV_JACK:
				nv_str = "11";
				break;
			case NumericalValue.NV_QUEEN:
				nv_str = "12";
				break;
			case NumericalValue.NV_KING:
				nv_str = "13";
				break;
			case NumericalValue.NV_SMALL_JOKER:
				nv_str = "small";
				break;
			case NumericalValue.NV_BIG_JOKER:
				nv_str = "big";
				break;
			default:
				throw new ArgumentOutOfRangeException ();
			}

			return string.Format ("{0}_{1}", suit_str, nv_str);
		}
	}



}
