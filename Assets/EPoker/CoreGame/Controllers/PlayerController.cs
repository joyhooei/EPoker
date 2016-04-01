namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using yigame.epoker;
	using UniRx;
	using uFrame.MVVM;
	using uFrame.Kernel;
	using uFrame.IOC;
	using uFrame.Serialization;
	using ExitGames.Client.Photon.LoadBalancing;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WII || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS3 || UNITY_XBOX360 || UNITY_NACL  || UNITY_FLASH  || UNITY_BLACKBERRY
	using Hashtable = ExitGames.Client.Photon.Hashtable;
	#endif
    
	public class PlayerController : PlayerControllerBase
	{
		[Inject] public Network Network;

		public override void InitializePlayer (PlayerViewModel viewModel)
		{
			base.InitializePlayer (viewModel);
			// This is called when a PlayerViewModel is created
		}

		public override void PlayerReady (PlayerViewModel viewModel)
		{
			base.PlayerReady (viewModel);
			if (viewModel.IsSelf) {
				Hashtable ht = new Hashtable ();
				ht.Add ("is_ready", true);
				Publish (new NetSetPlayerProperties () {
					ActerId = viewModel.ActorId,
					PropertiesToSet = ht
				});
			}
		}

		public override void PlayerCancel (PlayerViewModel viewModel)
		{
			base.PlayerCancel (viewModel);
			if (viewModel.IsSelf) {
				Hashtable ht = new Hashtable ();
				ht.Add ("is_ready", false);
				Publish (new NetSetPlayerProperties () {
					ActerId = viewModel.ActorId,
					PropertiesToSet = ht
				});
			}
		}

		public override void MatchBegan (PlayerViewModel viewModel)
		{
			base.MatchBegan (viewModel);
		}

		public override void BeganToPlay (PlayerViewModel viewModel)
		{
			base.BeganToPlay (viewModel);
		}

		public override void BeganToWait (PlayerViewModel viewModel)
		{
			base.BeganToWait (viewModel);
		}

		public override void TurnOn (PlayerViewModel viewModel)
		{
			base.TurnOn (viewModel);
		}

		public override void TurnOff (PlayerViewModel viewModel)
		{
			base.TurnOff (viewModel);
		}

		public override void Win (PlayerViewModel viewModel)
		{
			base.Win (viewModel);
		}

		public override void Over (PlayerViewModel viewModel)
		{
			base.Over (viewModel);
		}

		public override void InitOK (PlayerViewModel viewModel)
		{
			base.InitOK (viewModel);
		}

		public override void RefreshPlayer (PlayerViewModel viewModel)
		{
			base.RefreshPlayer (viewModel);

			// ready

			if (viewModel.IsSelf == false) {
				bool is_ready = false;
				if (viewModel.LBPlayer.CustomProperties.ContainsKey ("is_ready")) {
					is_ready = Convert.ToBoolean (viewModel.LBPlayer.CustomProperties ["is_ready"]);
				}
				if (is_ready) {
					viewModel.ExecutePlayerReady ();
				} else {
					viewModel.ExecutePlayerCancel ();
				}
			}
		}

		public override void ButtonReadyClicked (PlayerViewModel viewModel)
		{
			base.ButtonReadyClicked (viewModel);

			if (viewModel.Status is Wait || viewModel.Status is Init) {
				viewModel.ExecutePlayerReady ();	
			} else if (viewModel.Status is Ready) {
				viewModel.ExecutePlayerCancel ();
			}

			viewModel.ExecuteRefreshPlayer ();
		}

		public override void ButtonStartClicked (PlayerViewModel viewModel)
		{
			base.ButtonStartClicked (viewModel);
		}
	}
}
