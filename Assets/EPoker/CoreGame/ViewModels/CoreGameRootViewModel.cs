namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.IOC;
	using uFrame.Kernel;
	using uFrame.MVVM;
	using uFrame.MVVM.Bindings;
	using uFrame.Serialization;
	using UnityEngine;
	using UniRx;
	using GameDataEditor;

	public partial class CoreGameRootViewModel : CoreGameRootViewModelBase
	{
		[Inject] public Network Network;

		public Dictionary<string, Vector3> PosIdPosition = new Dictionary<string, Vector3> ();

		public bool IsAllReady {
			get {
				return Network.Client.CurrentRoom.Players.ToList ().Exists (kv => Convert.ToBoolean (kv.Value.CustomProperties ["is_ready"]) == false) == false;
			}
		}

		public bool CanMatchBegan {
			get {
				GDESDebugData dd = new GDESDebugData (GDEItemKeys.SDebug_DefaultDebug);
				if (dd.SinglePlayerStartForTest) {
					return IsAllReady;
				} else {
					return IsAllReady && Network.Client.CurrentRoom.PlayerCount > 1;
				}
			}
		}

		public PlayerViewModel GetPlayerByActorId (int actorId)
		{
			return PlayerCollection.Where (vm => vm.ActorId == actorId).SingleOrDefault ();
		}

		public int WinPlayersCount {
			get {
				return Network.Client.CurrentRoom.Players.Count (kv => Convert.ToBoolean (kv.Value.CustomProperties ["is_win"]));
			}
		}

		public int GetTeamPlayersCount (int team_id)
		{
			return Network.Client.CurrentRoom.Players.Count (kv => {

				int tid = 0;
				if (kv.Value.CustomProperties.TryGetInt ("team_id", out tid)) {
					if (tid == team_id) {
						return true;
					}
				}

				return false;
			});
		}
	}
}
