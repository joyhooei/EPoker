namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.IOC;
	using uFrame.MVVM;
	using uFrame.Serialization;
	using UnityEngine;
	using UniRx;
	using uFrame.Kernel;
	using PlayFab;
	using PlayFab.ClientModels;
	using Newtonsoft.Json;
	using ExitGames.Client.Photon;
	using ExitGames.Client.Photon.LoadBalancing;

	public class EPokerClient : LoadBalancingClient
	{
		public EPokerClient (Network network)
		{
			this.NetWork = network;
		}

		public Network NetWork;

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
			this.NetWork.OnEvent (photonEvent);
//			switch (photonEvent.Code) {
//			case EventCode.Join:
//				{
//					RoomPanelViewModel roomPanelVM = this.NetWork.OutOfGameRoot.CanvasRoot.PanelCollection.ToList ().Single (vm => vm is RoomPanelViewModel) as RoomPanelViewModel;
//					roomPanelVM.ExecuteRefreshRoom ();
//					break;
//				}
//			case EventCode.Leave:
//				{
//					RoomPanelViewModel roomPanelVM = this.NetWork.OutOfGameRoot.CanvasRoot.PanelCollection.ToList ().Single (vm => vm is RoomPanelViewModel) as RoomPanelViewModel;
//					roomPanelVM.ExecuteRefreshRoom ();
//					break;
//				}
//			case EventCode.PropertiesChanged:
//				{
//					RoomPanelViewModel roomPanelVM = this.NetWork.OutOfGameRoot.CanvasRoot.PanelCollection.ToList ().Single (vm => vm is RoomPanelViewModel) as RoomPanelViewModel;
//					roomPanelVM.ExecuteRefreshRoomProperties ();
//					roomPanelVM.ExecuteRefreshPlayerProperties ();
//					break;
//				}
//			default:
//				{
//					RoomPanelViewModel roomPanelVM = this.NetWork.OutOfGameRoot.CanvasRoot.PanelCollection.ToList ().Single (vm => vm is RoomPanelViewModel) as RoomPanelViewModel;
//					roomPanelVM.Execute (new RefreshEventCommand () {
//						EventCode = photonEvent.Code,
//						EventContent = photonEvent.Parameters
//					});
//					break;
//				}
//			}
		}

		public override void OnMessage (object message)
		{
			base.OnMessage (message);
		}
	}
}