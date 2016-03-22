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
		[Inject] public GameService GameService;
		[Inject ("OutOfGameRoot")] public OutOfGameRootViewModel OutOfGameRoot;

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
			int idx = viewModel.InfoJson ["players"]
				.Select ((jt, _idx) => new {jt, _idx})
				.Where (kv => kv.jt ["playfab_id"].Value<string> () == viewModel.MyId)
				.Select (kv => kv._idx)
				.Single ();
			
			JObject jArrange = JObject.Parse (posid_arrange_player_count);
			JArray jArrangeItem = jArrange [count.ToString ()] as JArray;

			viewModel.PlayerCount = count;

			viewModel.PlayerCollection.Clear ();


			for (int i = 0; i < count; i++) {

				int idx_in_players = (i + idx) % count;
				var jPlayer = viewModel.InfoJson ["players"] [idx_in_players];

				PlayerViewModel player = MVVMKernelExtensions.CreateViewModel<PlayerViewModel> ();
				player.PosId = jArrangeItem [i].Value<string> ();
				player.Id = jPlayer ["playfab_id"].Value<string> ();
				player.DisplayName = jPlayer ["display_name"].Value<string> ();
				player.PlayerRoomIdentity = (RoomIdentity)Enum.Parse (typeof(RoomIdentity), jPlayer ["player_room_identity"].Value<string> ());

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

			// 初始化本人 id
			viewModel.MyId = "1002";

			// 此处应从服务器读取房间初始数据
			var json_str = @"
							{
							  ""player_count"": 2,
							  ""players"": [
							    {
							      ""idx"": 0,
							      ""playfab_id"": ""1001"",
							      ""display_name"": ""ethan"",
							      ""player_room_identity"": ""RoomMaster"",
							      ""get_card_first"": true,
							      ""hand_cards"": []
							    },
							    {
							      ""idx"": 1,
							      ""playfab_id"": ""1002"",
							      ""display_name"": ""dream"",
							      ""player_room_identity"": ""RoomGuest"",
							      ""get_card_first"": false,
							      ""hand_cards"": []
							    }
							  ],
							  ""pile_for_show"": []
							}
						";
			viewModel.InfoJson = JObject.Parse (json_str);

			Observable.Interval (TimeSpan.FromMilliseconds (100)).Take (4).Subscribe (step => {
				UnityEngine.Debug.LogFormat ("Simulate Match Began: {0}", step);

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

		public override void CreateDeckToPile (CoreGameRootViewModel viewModel)
		{
			base.CreateDeckToPile (viewModel);

			List<CardInfo> card_info_list = this.GameService.GetDeck (true);
			JObject jInfo = CoreGameRoot.InfoJson;
//			jInfo ["pile_for_show"] = JArray.Parse (JsonConvert.SerializeObject (card_info_list.Select (ci => ci.ToString ())));


			int get_card_first_idx = jInfo ["players"]
				.Where (jp => jp ["get_card_first"].Value<bool> ())
				.Select (jp => jp ["idx"].Value<int> ())
				.Single ();

			int i = get_card_first_idx;
			card_info_list.ForEach (ci => {
				JArray j_hand_cards = jInfo ["players"] [i] ["hand_cards"] as JArray;
				j_hand_cards.Add (ci.ToString ());
				i = (i + 1) % viewModel.PlayerCount;
			});

			UnityEngine.Debug.Log ("jInfo: " + JsonConvert.SerializeObject (jInfo, Formatting.Indented));

			Publish (new UploadInfoJson ());
		}

		public override void DealPile (CoreGameRootViewModel viewModel)
		{
			base.DealPile (viewModel);
		}

		public override void QuitCoreGame (CoreGameRootViewModel viewModel)
		{
			base.QuitCoreGame (viewModel);

			Publish (new NetLeaveRoom () {
				SuccessCallback = s => {
					OutOfGameRoot.ExecuteDoQuitRoom ();
					Publish (new CloseCoreGame ());
				}
			});
		}
	}
}
