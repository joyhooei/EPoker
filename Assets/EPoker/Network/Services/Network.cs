namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.IOC;
	using uFrame.MVVM;
	using uFrame.Kernel;
	using UnityEngine;
	using UniRx;
	using PlayFab;
	using PlayFab.ClientModels;
	using Newtonsoft.Json;
	using ExitGames.Client.Photon;
	using ExitGames.Client.Photon.LoadBalancing;

	public class Network : NetworkBase
	{
		[Inject ("DebugInfoPanel")] public DebugInfoPanelViewModel DebugInfoPanel;
		[Inject ("OutOfGameRoot")] public OutOfGameRootViewModel OutOfGameRoot;
		[Inject ("CoreGameRoot")] public CoreGameRootViewModel CoreGameRoot;

		#region 成员变量

		// playfab
		public string TitleId = "4214";
		public string PlayFabId = "";
		public string DisplayName = "";
		public string CustomId = "";

		// photon
		public string AppId = "60090e03-9030-4321-b497-270418f42a37";
		public string AppVersion = "1.0";
		public ReactiveProperty<ClientState> ClientStateRP;

		public EPokerClient Client;

		#endregion

		public void ResetUserInfo ()
		{
			PlayFabId = "";
			DisplayName = "";
			CustomId = "";
		}

		public override void NetInitHandler (NetInit data)
		{
			base.NetInitHandler (data);
			PlayFab.PlayFabSettings.TitleId = TitleId;
			ClientStateRP = new ReactiveProperty<ClientState> (ClientState.Uninitialized);

			RefreshNetInfo ("请输入 CustomId 进行登录");

			Application.runInBackground = true;
			CustomTypes.Register ();

			Client = new EPokerClient (this);
			Client.AppId = AppId;
			Client.AppVersion = AppVersion;

			Client.OnStateChangeAction += this.OnStateChanged;

			Observable.EveryUpdate ().Subscribe (_ => {
				Client.Service ();
			}).AddTo (this.gameObject);
		}

		public override void NetLoginHandler (NetLogin data)
		{
			base.NetLoginHandler (data);

			if (Client.State == ClientState.Uninitialized) {

				RefreshNetInfo ("正在登录PlayFab...");
				Client.State = ClientState.Queued;

				LoginWithCustomIDRequest request = new LoginWithCustomIDRequest () {
					TitleId = TitleId,
					CreateAccount = true,
					CustomId = data.CustomID
				};
				PlayFabClientAPI.LoginWithCustomID (request, loginResult => {

					PlayFabId = loginResult.PlayFabId;
					CustomId = data.CustomID;

					if (loginResult.NewlyCreated) {
						RefreshNetInfo ("登录成功,正在为新用户设置名称...");

						UpdateUserTitleDisplayNameRequest request2 = new UpdateUserTitleDisplayNameRequest () {
							DisplayName = data.CustomID
						};
						PlayFabClientAPI.UpdateUserTitleDisplayName (request2, result2 => {
							DisplayName = data.CustomID;
							RefreshUserInfo ();
							// PlayFab 部分登录及初始化完毕
							OnPlayFabSuccessLogin ();
						}, error2 => {
							ResetUserInfo ();
							Client.State = ClientState.Uninitialized;
							RefreshNetInfo ("错误:未能更新用户显示名称");
						});
					} else {
						RefreshNetInfo ("登录成功,正在同步用户信息...");

						GetUserCombinedInfoRequest r1 = new GetUserCombinedInfoRequest () {
							PlayFabId = PlayFabId
						};
						PlayFabClientAPI.GetUserCombinedInfo (r1, rs1 => {
							DisplayName = rs1.AccountInfo.TitleInfo.DisplayName;
							RefreshUserInfo ();
							// PlayFab 部分登录及初始化完毕
							OnPlayFabSuccessLogin ();
						}, re1 => {
							ResetUserInfo ();
							Client.State = ClientState.Uninitialized;
							RefreshNetInfo ("错误:未能获取用户信息");
						});
					}
				}, error => {
					ResetUserInfo ();
					Client.State = ClientState.Uninitialized;
					RefreshNetInfo ("错误:未能成功登录 PlayFab");
				});
			}
		}

		IDisposable WaitPhotonStateDisposable = null;

		public void OnPlayFabSuccessLogin ()
		{
			Client.PlayerName = DisplayName;

			RefreshNetInfo ("正在对接 Photon...");

			GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest ();
			request.PhotonApplicationId = AppId;
			PlayFabClientAPI.GetPhotonAuthenticationToken (request, res => {

				if (WaitPhotonStateDisposable != null) {
					WaitPhotonStateDisposable.Dispose ();
				}

				ConnectToMasterServer (PlayFabId, res.PhotonCustomAuthenticationToken);

			}, err => {
				RefreshNetInfo ("错误:无法从 PlayFab 获取 Photon Token");
			});

		}

		public override void NetLogoutHandler (NetLogout data)
		{
			base.NetLogoutHandler (data);

			if (Client.IsConnectedAndReady) {
				Client.Disconnect ();
			}
		}

		public override void NetJoinOrCreateRoomHandler (NetJoinOrCreateRoom data)
		{
			base.NetJoinOrCreateRoomHandler (data);

			if (Client.IsConnectedAndReady) {
				Client.OpJoinOrCreateRoom (data.RoomId, 0, null);
			}
		}

		public override void NetLeaveRoomHandler (NetLeaveRoom data)
		{
			base.NetLeaveRoomHandler (data);
			if (Client.IsConnectedAndReady) {
				Client.OpLeaveRoom ();
			}
		}

		public override void NetSetRoomPropertiesHandler (NetSetRoomProperties data)
		{
			base.NetSetRoomPropertiesHandler (data);
			Client.CurrentRoom.SetCustomProperties (data.PropertiesToSet);
		}

		public override void NetRaiseEventHandler (NetRaiseEvent data)
		{
			base.NetRaiseEventHandler (data);
			Client.OpRaiseEvent (data.EventCode, data.EventContent, true, null);
		}

		public override void NetSetPlayerPropertiesHandler (NetSetPlayerProperties data)
		{
			base.NetSetPlayerPropertiesHandler (data);
			Client.OpSetPropertiesOfActor (data.ActerId, data.PropertiesToSet);
		}

		public void RefreshNetInfo (string netStatusDesc)
		{
			DebugInfoPanel.Text = string.Format ("Network[{0}]: {1}", DateTime.Now, netStatusDesc);
		}

		public void RefreshUserInfo ()
		{
			DebugInfoPanel.Text = string.Format ("PlayFabId:{0}({1}) CustomId:{2}", PlayFabId, DisplayName, CustomId);
		}

		//////////////////////////
		// photon
		//////////////////////////
		private void OnStateChanged (ClientState state)
		{
			Debug.Log ("photon state changed: " + state.ToString ());
			ClientStateRP.Value = state;

			switch (state) {
			case ClientState.Uninitialized:
				break;
			case ClientState.ConnectingToMasterserver:
				RefreshNetInfo ("正在连接主服务器...");
				break;
			case ClientState.ConnectedToMaster:
				RefreshNetInfo ("已连接主服务器");
				break;
			case ClientState.Queued:
				break;
			case ClientState.Authenticated:
				RefreshNetInfo ("已授权");
				break;
			case ClientState.JoinedLobby:
				RefreshNetInfo ("已连接大厅");
				OutOfGameRoot.ExecuteDoLogin ();
				break;
			case ClientState.DisconnectingFromMasterserver:
				RefreshNetInfo ("正在从主服务器断开...");
				break;
			case ClientState.ConnectingToGameserver:
				RefreshNetInfo ("正在连接游戏服务器...");
				break;
			case ClientState.ConnectedToGameserver:
				RefreshNetInfo ("已连接游戏服务器");
				break;
			case ClientState.Joining:
				RefreshNetInfo ("正在加入游戏..");
				break;
			case ClientState.Joined:
				RefreshNetInfo ("已经加入游戏");
				OutOfGameRoot.ExecuteDoEnterRoom ();
				Publish (new OpenCoreGame ());
				break;
			case ClientState.Leaving:
				RefreshNetInfo ("正在离开游戏..");
				OutOfGameRoot.ExecuteDoQuitRoom ();
				Publish (new CloseCoreGame ());
				break;
			case ClientState.Left:
				RefreshNetInfo ("已经离开游戏");
				// 在此没有响应
				break;
			case ClientState.DisconnectingFromGameserver:
				RefreshNetInfo ("正在从游戏服务器断开...");
				break;
			case ClientState.QueuedComingFromGameserver:
				break;
			case ClientState.Disconnecting:
				RefreshNetInfo ("正在断开..");
				break;
			case ClientState.Disconnected:
				RefreshNetInfo ("已经断开与服务器的连接");
				OutOfGameRoot.ExecuteDoDisconnect ();
				Publish (new CloseCoreGame ());
				break;
			case ClientState.ConnectingToNameServer:
				RefreshNetInfo ("正在连接名字服务器...");
				break;
			case ClientState.ConnectedToNameServer:
				RefreshNetInfo ("已连接名字服务器");
				break;
			case ClientState.Authenticating:
				RefreshNetInfo ("正在授权...");
				break;
			case ClientState.DisconnectingFromNameServer:
				RefreshNetInfo ("正在从名字服务器断开...");
				break;
			default:
				throw new ArgumentOutOfRangeException ();
			}

		}

		public void ConnectToMasterServer (string id, string ticket)
		{	
			Debug.Log (string.Format ("Id: {0}, Ticket: {1}", id, ticket));
			if (Client.CustomAuthenticationValues != null) {
				Client.CustomAuthenticationValues.SetAuthParameters (id, ticket);
			} else {
				Client.CustomAuthenticationValues = new AuthenticationValues () {
					AuthType = CustomAuthenticationType.Custom,
					Secret = null,
					AuthParameters = null,
				};
				Client.CustomAuthenticationValues.SetAuthParameters (id, ticket);
			}
			Client.ConnectToRegionMaster ("US");
		}

		public void OnEvent (ExitGames.Client.Photon.EventData photonEvent)
		{
			switch (photonEvent.Code) {
			case EventCode.Join:
				{
					RoomPanelViewModel roomPanelVM = OutOfGameRoot.CanvasRoot.PanelCollection.ToList ().Single (vm => vm is RoomPanelViewModel) as RoomPanelViewModel;
					roomPanelVM.ExecuteRefreshRoom ();
					CoreGameRoot.ExecutePlayerJoin ();
					break;
				}
			case EventCode.Leave:
				{
					RoomPanelViewModel roomPanelVM = OutOfGameRoot.CanvasRoot.PanelCollection.ToList ().Single (vm => vm is RoomPanelViewModel) as RoomPanelViewModel;
					roomPanelVM.ExecuteRefreshRoom ();
					CoreGameRoot.ExecutePlayerLeave ();
					break;
				}
			case EventCode.PropertiesChanged:
				{
					RoomPanelViewModel roomPanelVM = OutOfGameRoot.CanvasRoot.PanelCollection.ToList ().Single (vm => vm is RoomPanelViewModel) as RoomPanelViewModel;
					roomPanelVM.ExecuteRefreshRoomProperties ();
					roomPanelVM.ExecuteRefreshPlayerProperties ();
					CoreGameRoot.ExecuteRefreshCoreGame ();
					break;
				}
			default:
				{
					RoomPanelViewModel roomPanelVM = OutOfGameRoot.CanvasRoot.PanelCollection.ToList ().Single (vm => vm is RoomPanelViewModel) as RoomPanelViewModel;
					roomPanelVM.Execute (new RefreshEventCommand () {
						EventCode = photonEvent.Code,
						EventContent = photonEvent.Parameters
					});
					break;
				}
			}
		}
	}
}
