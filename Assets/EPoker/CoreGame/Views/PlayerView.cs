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
	using uFrame.IOC;
	using UniRx;
	using UnityEngine;
	using UnityEngine.UI;
	using yigame.epoker;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using Unity.Linq;

    
	public class PlayerView : PlayerViewBase
	{
		[Inject] public GameService GameService;

		public Button ReadyButton;
		public Button StartButton;

		protected override void InitializeViewModel (uFrame.MVVM.ViewModel model)
		{
			base.InitializeViewModel (model);
		}

		protected override void PreBind ()
		{
			base.PreBind ();
		}

		public override void Bind ()
		{
			base.Bind ();
		}

		#region Status Changed

		public override void StatusChanged (Invert.StateMachine.State arg1)
		{
			base.StatusChanged (arg1);
		}

		public override void OnInit ()
		{
			base.OnInit ();

			ReadyButton.gameObject.SetActive (false);
			StartButton.gameObject.SetActive (false);

			// 忽略初始设计用的 Player
			if (string.IsNullOrEmpty (Player.PlayerName))
				return;

			Player.ReadyStatusText = "Initializing...";
			Observable.Timer (TimeSpan.FromSeconds (1)).Subscribe (_ => {
				Player.ExecuteInitOK ();
				this.ExecuteRefreshPlayer ();
			}).DisposeWhenChanged (Player.StatusProperty);
		}

		public override void OnWait ()
		{
			base.OnWait ();
			Player.ReadyStatusText = string.Format ("{0}|{1} Wait...", Player.ActorId, Player.PlayerName);
			ReadyButton.gameObject.Child ("Text").GetComponent<Text> ().text = "Ready";
		}

		public override void OnReady ()
		{
			base.OnReady ();
			Player.ReadyStatusText = string.Format ("{0}|{1} Ready!", Player.ActorId, Player.PlayerName);
			ReadyButton.gameObject.Child ("Text").GetComponent<Text> ().text = "Cancel";
		}

		public override void OnMatchPrepare ()
		{
			base.OnMatchPrepare ();
		}

		public override void OnMatchDeal ()
		{
			base.OnMatchDeal ();
		}

		public override void OnMatchIdle ()
		{
			base.OnMatchIdle ();
		}


		public override void OnMatchWin ()
		{
			base.OnMatchWin ();
		}

		public override void OnMatchOver ()
		{
			base.OnMatchOver ();
		}


		#endregion

		public override void PosIdChanged (String arg1)
		{
			SetPanelPosByPosId ();
		}

		public void SetPanelPosByPosId ()
		{
			if (string.IsNullOrEmpty (Player.PosId)) {
				return;
			}

			string pos_obj_name = string.Format ("PlayerPos{0}", Player.PosId);

			Vector3 pos = Player.CoreGameRoot.PosIdPosition [pos_obj_name];
			transform.position = pos;

//			Debug.Log (pos_obj_name + pos.ToString ());
		}

		public override void IsSelfChanged (Boolean arg1)
		{
			if (arg1) {
				gameObject.Descendants ("Button_Ready").Single ().SetActive (true);
			} else {
				gameObject.Descendants ("Button_Ready").Single ().SetActive (false);
			}
		}

		public override void RefreshPlayerExecuted (RefreshPlayerCommand command)
		{
			if (Player.PlayerRoomIdentity == RoomIdentity.RoomMaster) {
				if (Player.IsSelf) {
					if (Player.CoreGameRoot.CanMatchBegan) {
						StartButton.gameObject.SetActive (true);
					} else {
						StartButton.gameObject.SetActive (false);
					}
				}
			} else {
				StartButton.gameObject.SetActive (false);
			}
		}

	}
}
