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

			this.BindButtonToHandler (ReadyButton, () => {
				if (Player.Status is Wait) {
					Player.ExecutePlayerReady ();
				} else if (Player.Status is Ready) {
					Player.ExecutePlayerCancel ();
				}
			});
		}

		#region Status Changed

		public override void StatusChanged (Invert.StateMachine.State arg1)
		{
			base.StatusChanged (arg1);
		}

		public override void OnInit ()
		{
			base.OnInit ();

			Player.ReadyStatusText = "Initializing...";
			Observable.Timer (TimeSpan.FromSeconds (1)).Subscribe (_ => {
				Player.ExecuteInitOK ();
				this.ExecuteRefreshPlayer ();
			});
		}

		public override void OnWait ()
		{
			base.OnWait ();
			Player.ReadyStatusText = string.Format("{0}|{1} Wait...", Player.ActorId, Player.PlayerName);
		}

		public override void OnReady ()
		{
			base.OnReady ();
			Player.ReadyStatusText = string.Format("{0}|{1} Ready!", Player.ActorId, Player.PlayerName);
		}

		public override void OnMatchPrepare ()
		{
			base.OnMatchPrepare ();

			// room-master 进行随机牌生成,更新房间属性/触发房间事件

			if (Player.PlayerRoomIdentity == RoomIdentity.RoomMaster) {
				Player.CoreGameRoot.ExecuteCreateDeckToPile ();
			}
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

			Debug.Log (pos_obj_name + pos.ToString ());
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
		}

	}
}
