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
	using GameDataEditor;

	public class GameService : GameServiceBase
	{
		[Inject] public Network Network;
		[Inject ("CoreGameRoot")] public CoreGameRootViewModel CoreGameRoot;

		public static class EventCode
		{
			public const byte MatchBegan = 10;
			public const byte ShowCardAndTurnNext = 11;
			public const byte PassAndTurnNext = 12;
			public const byte MatchOver = 13;
		}

		public static List<CardInfo> GetDeck (bool disorder)
		{
			List<CardInfo> card_info_list = new List<CardInfo> ();
			for (int s = 0; s < 4; s++) {
				for (int n = 0; n < 13; n++) {
					card_info_list.Add (new CardInfo ((Suit)s, (NumericalValue)n));
				}
			}
			card_info_list.Add (new CardInfo (Suit.SMALL_JOKER, NumericalValue.NV_SMALL_JOKER));
			card_info_list.Add (new CardInfo (Suit.BIG_JOKER, NumericalValue.NV_BIG_JOKER));

			if (disorder) {
				card_info_list = card_info_list.OrderBy (x => Guid.NewGuid ()).ToList ();
			}

			int force_card_count = card_info_list.Count;
			GDESDebugData dd = new GDESDebugData (GDEItemKeys.SDebug_DefaultDebug);
			if (dd.LessCardsCountForTest > 0) {
				force_card_count = Math.Min (dd.LessCardsCountForTest, card_info_list.Count);

				card_info_list = card_info_list.GetRange (0, force_card_count);
			}

			return card_info_list;
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

			CoreGameRoot.PlayerCollection.Clear ();

			Publish (new UnloadSceneCommand () {
				SceneName = "CoreGameScene"
			});
		}

		public override void OnCoreGameEventHandler (OnCoreGameEvent data)
		{
			base.OnCoreGameEventHandler (data);

			switch (data.EventCode) {
			case EventCode.MatchBegan:
				{
					CoreGameRoot.ExecuteRootMatchBegan ();
					break;
				}
			case EventCode.ShowCardAndTurnNext:
				{
					int current_cards_actor_id = Convert.ToInt32 (Network.Client.CurrentRoom.CustomProperties ["current_cards_actor_id"]);
					PlayerViewModel currentCardsPlayer = CoreGameRoot.PlayerCollection.SingleOrDefault (vm => vm.ActorId == current_cards_actor_id);
					currentCardsPlayer.ExecuteShowCardsToPile ();
					CoreGameRoot.ExecuteRefreshCoreGame ();
					break;
				}
			case EventCode.PassAndTurnNext:
				{
					CoreGameRoot.ExecuteRefreshCoreGame ();
					break;
				}

			case EventCode.MatchOver:
				{
					CoreGameRoot.ExecuteRootMatchOver ();
					break;
				}
			default:
				break;
			}
		}
	}
}
