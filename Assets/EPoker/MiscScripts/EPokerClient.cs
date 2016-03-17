namespace yigame.epoker
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using ExitGames.Client.Photon.LoadBalancing;
	using Newtonsoft.Json;

	public class EPokerClient : LoadBalancingClient
	{
		public override void DebugReturn (ExitGames.Client.Photon.DebugLevel level, string message)
		{
			base.DebugReturn (level, message);
		}

		public override void OnOperationResponse (ExitGames.Client.Photon.OperationResponse operationResponse)
		{
			base.OnOperationResponse (operationResponse);
		}

		public override void OnStatusChanged (ExitGames.Client.Photon.StatusCode statusCode)
		{
			base.OnStatusChanged (statusCode);
		}

		public override void OnEvent (ExitGames.Client.Photon.EventData photonEvent)
		{
			base.OnEvent (photonEvent);
		}

		public override void OnMessage (object message)
		{
			base.OnMessage (message);
		}
	}
}