namespace yigame.epoker
{
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
	using UnityEngine.UI;

	public class SummaryPlayerItemView : SummaryPlayerItemViewBase
	{
        
		public Text RankText;
		public Text NameText;
		public Text TeamText;
		public Text WinText;

		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
			// NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
			// var vm = model as SummaryPlayerItemViewModel;
			// This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		}

		public override void Bind ()
		{
			base.Bind ();
			// Use this.SummaryPlayerItem to access the viewmodel.
			// Use this method to subscribe to the view-model.
			// Any designer bindings are created in the base implementation.
		}

		public override void RankChanged (Int32 arg1)
		{
			if (arg1 > 0) {
				RankText.text = string.Format ("{0}.", arg1);
			} else {
				RankText.text = "?";
			}
		}

		public override void IsMeChanged (Boolean arg1)
		{
			Color c = Color.white;
			if (arg1) {
				c = new Color32 (255, 200, 80, 255);
			}

			RankText.color = c;
			NameText.color = c;
		}

		public override void TeamChanged (Int32 arg1)
		{
			if (arg1 == 1) {
				TeamText.text = "Ace Team";
			} else {
				TeamText.text = "";
			}
		}

		public override void IsWinChanged (Boolean arg1)
		{
			if (arg1) {
				WinText.text = "Win";
			} else {
				WinText.text = "Lose";
			}
		}
	}
}