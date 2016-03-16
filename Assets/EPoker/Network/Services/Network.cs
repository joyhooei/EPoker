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

	public class Network : NetworkBase
	{
		[Inject ("DebugInfoPanel")] public DebugInfoPanelViewModel DebugInfoPanel;

		public string TitleId = "4214";
		public string PlayFabId = "";
		public string DisplayName = "";
		public string CustomId = "";

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
						data.SuccessCallback.Invoke (JsonConvert.SerializeObject (loginResult));
						RefreshUserInfo ();
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
						data.SuccessCallback.Invoke (JsonConvert.SerializeObject (loginResult));
						RefreshUserInfo ();
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

		public void RefreshNetInfo (string netStatusDesc)
		{
			DebugInfoPanel.Text = string.Format ("Network[{0}]: {1}", DateTime.Now, netStatusDesc);
		}

		public void RefreshUserInfo ()
		{
			DebugInfoPanel.Text = string.Format ("PlayFabId:{0}({1}) CustomId:{2}", PlayFabId, DisplayName, CustomId);
		}
	}
}
