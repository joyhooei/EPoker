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
	using yigame.epoker;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

    
	public class PlayerView : PlayerViewBase
	{
		[Inject] public GameService GameService;

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
		}

		public override void OnReady ()
		{
			base.OnReady ();
		}

		public override void OnMatchPrepare ()
		{
			base.OnMatchPrepare ();

			// room-master 进行随机牌生成,更新房间属性/触发房间事件

			if (Player.PlayerRoomIdentity == RoomIdentity.RoomMaster) {
				Player.CoreGameRoot.ExecuteCreateDeckToPile ();
			}
		}

		public override void OnMatchIdle ()
		{
			base.OnMatchIdle ();
		}

		public override void OnMatchDeal ()
		{
			base.OnMatchDeal ();
		}

		public override void OnMatchWin ()
		{
			base.OnMatchWin ();
		}

		public override void OnMatchOver ()
		{
			base.OnMatchOver ();
		}

		public override void OnWait ()
		{
			base.OnWait ();
		}

		#endregion

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

	}
}
