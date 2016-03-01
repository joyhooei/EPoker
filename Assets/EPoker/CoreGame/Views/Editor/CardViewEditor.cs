namespace yigame.epoker {

	using UnityEngine;
	using UnityEditor;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	using Invert.Common;
	using Invert.Common.UI;
	using Invert.Core.GraphDesigner;
	using Invert.uFrame.Editor;
	using uFrame.MVVM;

	[CustomEditor(typeof(CardViewBase), true)]
	public class CardViewEditor : ViewInspector {

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI ();
		}

		public override bool DoFieldExtra (string propertyType)
		{
//			if (propertyType == "CardInfo") {
//				CardView card_view = target as CardView;
//
//				CardInfo card_info = card_view._Info;
//				var suit = EditorGUILayout.EnumPopup (card_info.Suit);
//				var numerical_value = EditorGUILayout.EnumPopup (card_info.NumericalValue);
//
//				card_view._Info = new CardInfo () {
//					Suit = (Suit)suit,
//					NumericalValue = (NumericalValue)numerical_value
//				};
//
//				return true;
//			}
			return false;
		}
	}
}