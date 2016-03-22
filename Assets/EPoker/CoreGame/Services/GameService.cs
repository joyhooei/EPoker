namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.MVVM;
	using uFrame.IOC;
	using uFrame.Kernel;
	using UniRx;
	using UnityEngine;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using ExitGames.Client.Photon.LoadBalancing;

	public class GameService : GameServiceBase
	{
		[Inject] public Network Network;
		[Inject ("CoreGameRoot")] public CoreGameRootViewModel CoreGameRoot;

		public List<CardInfo> GetDeck (bool disorder)
		{
			List<CardInfo> card_info_list = new List<CardInfo> ();
			for (int s = 0; s < 4; s++) {
				for (int n = 0; n < 13; n++) {
					card_info_list.Add (new CardInfo ((Suit)s, (NumericalValue)n));
				}
			}
			card_info_list.Add (new CardInfo (Suit.SMALL_JOKER, NumericalValue.NV_SMALL_JOKER));
			card_info_list.Add (new CardInfo (Suit.BIG_JOKER, NumericalValue.NV_BIG_JOKER));

//			card_info_list.ForEach (ci => {
//				string s = ci.ToString ();
//				CardInfo ci_p = CardInfo.Parse (s);
//				Debug.Log (JsonConvert.SerializeObject (ci_p));
//			});

			if (disorder) {
				card_info_list = card_info_list.OrderBy (x => Guid.NewGuid ()).ToList ();
			}

			return card_info_list;
		}

		public override void UploadInfoJsonHandler (UploadInfoJson data)
		{
			base.UploadInfoJsonHandler (data);
			// 将 CoreGameRoot InfoJson 数据上传房间属性
		}

		public override void OnInfoJsonUpdateHandler (OnInfoJsonUpdate data)
		{
			base.OnInfoJsonUpdateHandler (data);

			// 构造 InfoJson

			JObject infoJson = new JObject ();
			infoJson.Add (new JProperty ("player_count", Network.Client.CurrentRoom.PlayerCount));
			infoJson.Add (new JProperty ("players", new JArray ()));

			int idx = 0;
			Network.Client.CurrentRoom.Players.OrderBy (_ => _.Key).ToList ().ForEach (kv => {
				int actorId = kv.Key;
				Player player = kv.Value;

				JObject jo_players = (JObject)infoJson ["players"];
				jo_players.Add (new JProperty ("idx", idx));
				jo_players.Add (new JProperty ("actor_id", player.ID));
				jo_players.Add (new JProperty ("playfab_id", ""));
				jo_players.Add (new JProperty ("display_name", player.Name));
				jo_players.Add (new JProperty ("player_room_identity", player.IsMasterClient));
				jo_players.Add (new JProperty ("get_card_first", false));
				jo_players.Add (new JProperty ("hand_cards", new JArray ()));

				idx++;
			});

			infoJson.Add (new JProperty ("pile_for_show", new JArray ()));

			CoreGameRoot.InfoJson = infoJson;

			// 刷新

		}

		public override void OpenCoreGameHandler (OpenCoreGame data)
		{
			base.OpenCoreGameHandler (data);
			Publish (new LoadSceneCommand () {
				SceneName = "CoreGameScene"
			});
		}

		public override void CloseCoreGameHandler (CloseCoreGame data)
		{
			base.CloseCoreGameHandler (data);
			Publish (new UnloadSceneCommand () {
				SceneName = "CoreGameScene"
			});
		}
	}
}
