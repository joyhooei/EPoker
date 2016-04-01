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
	using Unity.Linq;

	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WII || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS3 || UNITY_XBOX360 || UNITY_NACL  || UNITY_FLASH  || UNITY_BLACKBERRY
	using Hashtable = ExitGames.Client.Photon.Hashtable;
	#endif

	public class PlayerItemView : PlayerItemViewBase
	{
        
		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
		}

		public override void Bind ()
		{
			base.Bind ();
		}

		public override void IsLocalChanged (Boolean arg1)
		{
			Toggle ready = this.gameObject.DescendantsAndSelf ().Where (go => {
				return go.name == "Ready";
			}).OfComponent<Toggle> ().Single ();

			ready.interactable = arg1;
		}

		public override void ReadyChanged (Boolean arg1)
		{
			Text ready_info = this.gameObject.Child ("Ready/Label").GetComponent<Text> ();
			ready_info.text = arg1 ? "READY" : "WAIT";


			if (PlayerItem.IsLocal) {

				Hashtable ht = new Hashtable ();
				ht.Add ("is_ready", arg1);

				Publish (new NetSetPlayerProperties () {
					ActorId = PlayerItem.ActerId,
					PropertiesToSet = ht
				});
			}
		}

		public override void ActerIdChanged (Int32 arg1)
		{
			var text_id = gameObject.Children ().Where (go => go.name == "Id").Single ().GetComponent<Text> ();
			text_id.text = Convert.ToString (arg1);
		}

    
		public override void RefreshByPlayerExecuted (RefreshByPlayerCommand command)
		{
			var toggle_ready = gameObject.Children ().Where (go => go.name == "Ready").Single ().GetComponent<Toggle> ();
			toggle_ready.isOn = Convert.ToBoolean (PlayerItem.Player.CustomProperties ["is_ready"]);
		}
	}
}
