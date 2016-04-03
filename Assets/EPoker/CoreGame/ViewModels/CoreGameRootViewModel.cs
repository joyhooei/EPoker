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
				if (SinglePlayerStartForTest) {
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
	}
}
