namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.IOC;
	using uFrame.MVVM;
	using UnityEngine;
	using UniRx;
	using uFrame.Kernel;
	using PlayFab;
	using PlayFab.ClientModels;
	using Newtonsoft.Json;
	using ExitGames.Client.Photon.LoadBalancing;

	public class Network : NetworkBase
	{
		[Inject ("DebugInfoPanel")] public DebugInfoPanelViewModel DebugInfoPanel;

		#region 成员变量

		// playfab
		public string TitleId = "4214";
		public string PlayFabId = "";
		public string DisplayName = "";
		public string CustomId = "";

		// photon
		public string AppId = "60090e03-9030-4321-b497-270418f42a37";
		public string AppVersion = "1.0";
		public ClientState ClientState;

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
			RefreshNetInfo ("请输入 CustomId 进行登录");

			Application.runInBackground = true;
			CustomTypes.Register ();

			Client = new EPokerClient ();
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

			RefreshNetInfo ("正在登录PlayFab...");
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
						OnPlayFabSuccessLogin (data.SuccessCallback, data.ErrorCallback, loginResult);
					}, error2 => {
						ResetUserInfo ();
						data.ErrorCallback.Invoke (JsonConvert.SerializeObject (error2));
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
						OnPlayFabSuccessLogin (data.SuccessCallback, data.ErrorCallback, loginResult);
					}, re1 => {
						ResetUserInfo ();
						data.ErrorCallback.Invoke (JsonConvert.SerializeObject (re1));
					});
				}
			}, error => {
				ResetUserInfo ();
				data.ErrorCallback.Invoke (JsonConvert.SerializeObject (error));
			});
		}

		IDisposable WaitPhotonStateDisposable = null;

		public void OnPlayFabSuccessLogin (Action<string> successCallback, Action<string> errorCallback, LoginResult loginResult)
		{
			Client.PlayerName = DisplayName;

			RefreshNetInfo ("正在对接 Photon...");

			GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest ();
			request.PhotonApplicationId = AppId;
			PlayFabClientAPI.GetPhotonAuthenticationToken (request, res => {

				if (WaitPhotonStateDisposable != null) {
					WaitPhotonStateDisposable.Dispose ();
				}
				WaitPhotonStateDisposable = Observable.Interval (TimeSpan.FromMilliseconds (500f)).Subscribe (_ => {
					if (Client.State == ClientState.JoinedLobby) {
						WaitPhotonStateDisposable.Dispose ();
						WaitPhotonStateDisposable = null;

						RefreshUserInfo ();
						successCallback.Invoke (JsonConvert.SerializeObject (loginResult));
					}
				}).AddTo (this.gameObject);

				ConnectToMasterServer (PlayFabId, res.PhotonCustomAuthenticationToken);

			}, err => {
				errorCallback.Invoke (JsonConvert.SerializeObject (err));
			});

		}

		public override void NetLogoutHandler (NetLogout data)
		{
			base.NetLogoutHandler (data);

			Client.Disconnect ();

			Observable.Interval (TimeSpan.FromMilliseconds (500f)).Subscribe (_ => {
				if (Client.State == ClientState.Disconnected) {
					data.SuccessCallback.Invoke (null);
				}
			}).AddTo (this.gameObject);

		}

		public override void NetJoinOrCreateRoomHandler (NetJoinOrCreateRoom data)
		{
			base.NetJoinOrCreateRoomHandler (data);
			Client.OpJoinOrCreateRoom (data.RoomId, 0, null);
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
			ClientState = state;
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
				break;
			case ClientState.Joined:
				break;
			case ClientState.Leaving:
				break;
			case ClientState.Left:
				break;
			case ClientState.DisconnectingFromGameserver:
				RefreshNetInfo ("正在从游戏服务器断开...");
				break;
			case ClientState.QueuedComingFromGameserver:
				break;
			case ClientState.Disconnecting:
				break;
			case ClientState.Disconnected:
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
	}
}
