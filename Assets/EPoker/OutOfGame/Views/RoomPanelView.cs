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
	using Newtonsoft.Json;

	public class RoomPanelView : RoomPanelViewBase
	{

		public Transform PanelTransform;

		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
			// NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
			// var vm = model as RoomPanelViewModel;
			// This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		}

		public override void Bind ()
		{
			base.Bind ();
			// Use this.RoomPanel to access the viewmodel.
			// Use this method to subscribe to the view-model.
			// Any designer bindings are created in the base implementation.
		}

		public override Transform GetPanelRoot ()
		{
			return PanelTransform;
		}

		public override void RefreshRoomExecuted (RefreshRoomCommand command)
		{
			Debug.LogFormat ("RefreshRoomExecuted: {0}", JsonConvert.SerializeObject (RoomPanel.Players));
		}
	}
}
