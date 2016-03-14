namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.Kernel;
	using uFrame.IOC;
	using uFrame.MVVM;
	using uFrame.Serialization;
	using UniRx;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	public class CoreGameRootController : CoreGameRootControllerBase
	{

		public override void InitializeCoreGameRoot (CoreGameRootViewModel viewModel)
		{
			base.InitializeCoreGameRoot (viewModel);
		}

		public static string posid_arrange_player_count = @"{
			2: [0,3],
			3: [0,2,4],
			4: [0,1,3,5],
			5: [0,1,2,4,5],
			6: [0,1,2,3,4,5]
		}";

		public override void ResetPlayerCount (CoreGameRootViewModel viewModel)
		{
			base.ResetPlayerCount (viewModel);

			int count = viewModel.InfoJson ["player_count"].Value<int> ();

			JObject jArrange = JObject.Parse (posid_arrange_player_count);
			JArray jArrangeItem = jArrange [count.ToString ()] as JArray;

			viewModel.PlayerCount = count;

			viewModel.PlayerCollection.Clear ();

			for (int i = 0; i < count; i++) {
				PlayerViewModel player = MVVMKernelExtensions.CreateViewModel<PlayerViewModel> ();
				player.PosId = jArrangeItem [i].Value<string> ();
				player.Id = viewModel.InfoJson ["players"] [i] ["playfab_id"].Value<string> ();
				player.DisplayName = viewModel.InfoJson ["players"] [i] ["display_name"].Value<string> ();
				player.PlayerRoomIdentity = (RoomIdentity)Enum.Parse (typeof(RoomIdentity), viewModel.InfoJson ["players"] [i] ["player_room_identity"].Value<string> ());

				viewModel.PlayerCollection.Add (player);
			}
		}

		public override void RootMatchBegan (CoreGameRootViewModel viewModel)
		{
			base.RootMatchBegan (viewModel);

			viewModel.PlayerCollection.ToList ().ForEach (playerVM => {
				playerVM.ExecuteMatchBegan ();
			});
		}

		public override void SimulateMatchBegan (CoreGameRootViewModel viewModel)
		{
			base.SimulateMatchBegan (viewModel);

			// 此处应从服务器读取房间初始数据
			var json_str = @"{
			  ""player_count"": 2,
			  ""players"": [
			    {
			      ""playfab_id"": 1001,
			      ""display_name"": ""ethan"",
			      ""player_room_identity"": ""RoomMaster""
			    },
			    {
			      ""playfab_id"": 1002,
			      ""display_name"": ""dream"",
			      ""player_room_identity"": ""RoomGuest""
			    }
			  ]
			}";
			viewModel.InfoJson = JObject.Parse (json_str);

			Observable.Interval (TimeSpan.FromMilliseconds (100)).Take (4).Subscribe (step => {
				UnityEngine.Debug.LogFormat ("Simulate Match Begain: {0}", step);

				if (step == 0) {
					viewModel.ExecuteResetPlayerCount ();
				} else if (step == 1) {
					viewModel.PlayerCollection.ToList ().ForEach (playerVM => {
						playerVM.ExecuteInitOK ();
					});
				} else if (step == 2) {
					viewModel.PlayerCollection.ToList ().ForEach (playerVM => {
						playerVM.ExecutePlayerReady ();
					});
				} else if (step == 3) {
					viewModel.ExecuteRootMatchBegan ();
				}
			});
		}
	}
}
