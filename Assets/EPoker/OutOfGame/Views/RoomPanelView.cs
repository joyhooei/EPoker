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

	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WII || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS3 || UNITY_XBOX360 || UNITY_NACL  || UNITY_FLASH  || UNITY_BLACKBERRY
	using Hashtable = ExitGames.Client.Photon.Hashtable;
	#endif

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
//			Debug.LogFormat ("RefreshRoomExecuted: {0}", JsonConvert.SerializeObject (RoomPanel.Players));
		}

		public override uFrame.MVVM.ViewBase PlayerItemsCreateView (uFrame.MVVM.ViewModel viewModel)
		{
			return InstantiateView (viewModel);
		}

		public override void PlayerItemsAdded (uFrame.MVVM.ViewBase view)
		{
		}

		public override void PlayerItemsRemoved (uFrame.MVVM.ViewBase view)
		{
			Destroy (view.gameObject);
		}

		public override void RefreshRoomPropertiesExecuted (RefreshRoomPropertiesCommand command)
		{
			Hashtable ht = RoomPanel.Network.Client.CurrentRoom.CustomProperties;
			string room_property_json = Convert.ToString (ht ["room_property"]);

			Debug.LogFormat ("RefreshRoomPropertiesExecuted: {0}", room_property_json);
			RoomPanel.RoomPropertiesJson = room_property_json;
		}

		public override void ExecuteRefreshEvent (RefreshEventCommand command)
		{
			base.ExecuteRefreshEvent (command);

			Dictionary<byte, object> eventContent = command.EventContent;

			string room_property_json = JsonConvert.SerializeObject (eventContent);

			Debug.LogFormat ("Event: {0}, {1}", command.EventCode, room_property_json);
			RoomPanel.EventParamsJson = room_property_json;

		}
	}
}
