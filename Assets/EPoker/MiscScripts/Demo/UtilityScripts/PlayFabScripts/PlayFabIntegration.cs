﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.LoadBalancing;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;
using Hashtable = ExitGames.Client.Photon.Hashtable;


/// <summary>
/// Play fab integration. Contains all the logic and GUI for illustrating how playfab credentials can be used with Photon custom authentication.
/// </summary>
public class PlayFabIntegration : MonoBehaviour {
	// gui variables for the playfab overlay
	public int gui_depth = 0;
	public Texture2D pf_logo;
	public Rect logoDims;
	public enum GuiStates {off=0, login=1, createRoom=2, customEvents=3, loading=4 }
	public GuiStates activeState = GuiStates.off; 
	public Color32 pf_orange = new Color32(250, 130,0, 255);
	public Color32 pf_blue = new Color32(0, 173,239,255);
	
	// reference to the photon demo GUI script
	public DemoGUI photonComponent; 
	
	// using PlayFab testing account and title
	const string deviceId = "a931f26995380c5a";
	const string playfabTitleId = "4214"; 

	// some vars used in gui logic
	private bool isAuthenticated = false;
	private bool isJoined = false;
	private bool hideTips = false;
	private string team = string.Empty;
	private string playfabId = string.Empty;
	
	// userStats maps to the stats stored on each PlayFab user id  
	private Dictionary<string, int> userStats = new Dictionary<string, int>();

#region unity engine
	void Start()
	{
		
		PlayFabSettings.TitleId = playfabTitleId; // PlayFab title must be stored for the web API calls to succeed.
		this.activeState = GuiStates.login; // Initialization of our active state, awaitng the player's login to playfab.
	}
	
	void OnGUI()
	{
	
		// await our photon session, and poll for certain state changes.
		if(photonComponent.GameInstance != null)
		{
			// this is triggered when our session has joined the master server.
			if(photonComponent.GameInstance.State == ExitGames.Client.Photon.LoadBalancing.ClientState.JoinedLobby && this.isAuthenticated == false)
			{
				this.isAuthenticated = true;
				this.activeState = GuiStates.createRoom;
				// refresh our player stats after the photon webhooks have had time to run
				StartCoroutine(GetUserStats(1.0f));
			}
			// this is triggered when our session has joined a photon game room
			else if(photonComponent.GameInstance.State == ExitGames.Client.Photon.LoadBalancing.ClientState.Joined && this.isJoined == false)
			{
				this.isJoined = true;		
				this.activeState = GuiStates.customEvents;
				// refresh our player stats after the photon webhooks have had time to run
				StartCoroutine(GetUserStats(1.0f));
			}
		}
		
//	Draw the PlayFab overlay prompts 
		if(this.activeState != GuiStates.off)
		{
			GUI.depth = this.gui_depth;
		
			if(pf_logo != null && this.hideTips == false)
			{	
				GUI.Box(new Rect(0, Screen.height * .75f, Screen.width, Screen.height*25f), "");
				GUI.Box(new Rect(0, Screen.height * .75f, Screen.width, Screen.height*25f), "");
				GUI.Box(new Rect(0, Screen.height * .75f, Screen.width, Screen.height*25f), "");
				GUI.Box(new Rect(0, Screen.height * .75f, Screen.width, Screen.height*25f), "");
				GUI.DrawTexture(new Rect(5, Screen.height - this.logoDims.height, this.logoDims.width, this.logoDims.height), this.pf_logo);	
					
			}
			
			if(this.activeState == GuiStates.createRoom || this.activeState == GuiStates.customEvents || this.activeState == GuiStates.loading)
			{
				Rect statsRect = new Rect(Screen.width*.82f -5, Screen.height*.75f, Screen.width*.25f, Screen.height*.25f);
				GUI.Box(statsRect, "");
				
				GUILayout.BeginArea(statsRect);
					string redTeamValue =  this.userStats.ContainsKey("RedTeamJoinedCount") ? this.userStats["RedTeamJoinedCount"].ToString() : "...";
					string bluTeamValue =  this.userStats.ContainsKey("BluTeamJoinedCount") ? this.userStats["BluTeamJoinedCount"].ToString() : "...";
					string mvpValue =  this.userStats.ContainsKey("MVPsWonCount") ? this.userStats["MVPsWonCount"].ToString() : "...";
					string expValue =  this.userStats.ContainsKey("ExperiencePoints") ? this.userStats["ExperiencePoints"].ToString() : "...";
					
						GUI.color = this.pf_orange;
						GUILayout.BeginHorizontal();
						GUILayout.Label("PlayFab Player Stats:");
						GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUI.color = this.pf_blue;
					GUILayout.Label(string.Format("RedTeamJoinedCount: {0}", redTeamValue));
					GUILayout.Label(string.Format("BluTeamJoinedCount: {0}", bluTeamValue));
					GUILayout.Label(string.Format("MVPsWonCount: {0}", mvpValue));
					GUILayout.Label(string.Format("ExperiencePoints: {0}", expValue));
					GUI.color = Color.white;
				GUILayout.EndArea();
			}
		
			Rect centralBox = new Rect();
			
			if(hideTips == false)
			{
				switch(this.activeState)
				{
					case GuiStates.login:
						centralBox = new  Rect(Screen.width/4, Screen.height*.75f, Screen.width/2, Screen.height*.25f);
						
						 GUILayout.BeginArea(centralBox);
							GUI.color = this.pf_orange;
							GUILayout.Label("Photon Custom Authentication with Playfab Credentials:");
							GUI.color = Color.white;
							GUILayout.Label("For the purposes of this demo, we will be using a testing device id for the login credentials. We provide these credentials to Photon, which then validates and permits entry into the MasterServer--MainLobby. From the main lobby a user can create and join rooms and games. Doing so will in turn fire the web hook associated with the player action.");
							GUILayout.Label("");
							GUI.color = Color.white;
						GUILayout.EndArea();	
					break;
					
					case GuiStates.createRoom:
						centralBox = new  Rect(Screen.width/4, Screen.height*.75f, Screen.width/2, Screen.height*.25f);
						
						GUILayout.BeginArea(centralBox);	
							GUI.color = this.pf_orange;
							GUILayout.Label("PlayFab Authentication Successful!");
							GUI.color = Color.white;
							GUILayout.Label("In the bottm right you can see the most up-to-date PlayFab PlayerStatistics for this user. You will see these stats change in real time as web hooks are called to process game logic.");
							GUILayout.Label("From the MainLobby, a player can join existing rooms or create a new one. When you create and join a room our cloud script is randomly assigning the player to a team (Red or Blu) and tracking the results in Player Stats. Watch the stats to see on which team you are assigned.");
							GUI.color = Color.white;
						GUILayout.EndArea();	
					break;
					
					case GuiStates.customEvents:
						centralBox = new  Rect(Screen.width/4, Screen.height*.75f, Screen.width/2, Screen.height*.25f);
						
						GUILayout.BeginArea(centralBox);
							GUI.color = this.pf_orange;
							GUILayout.Label(string.Format("You are now in the game, welcome to the {0} team", this.team));
							GUI.color = Color.white;
							GUILayout.Label("While the game is running your loops and mechanics, custom room events can be raised to augment your logic flow. These events can have custom input parameters as well as access basic Photon room details. This provides an all-purpose, flexible event system to benifit most game mode types");
							GUILayout.Label("");
							GUI.color = Color.white;
							
							if(GUILayout.Button("Ok, I got it. [hide these tips]"))
							{
								this.hideTips = true;
							}
							
							GUILayout.Label("");
						GUILayout.EndArea();	
					break;
					
					default:
						GUILayout.BeginArea(centralBox);
							GUILayout.Label("Awaiting Server...");
						GUILayout.EndArea();
					break;
				}	
			}
		}
	}
#endregion

#region API Call Examples and Callbacks	
	// Example of how to raise a custom event which triggers the RoomEventRaised web hook.
	public void AwardMVP()
	{
		// Trigger Custom Event
		photonComponent.GameInstance.OpRaiseEvent(
			eventCode: 15,
			customEventContent: new Hashtable() { { "eventType", "mvp" }},
			sendReliable: true,
			raiseEventOptions: new RaiseEventOptions()
			{
				ForwardToWebhook = true,
			});
			StartCoroutine(GetUserStats(1.25f));	
	}
	
	// Example of how to raise a custom event which triggers the RoomEventRaised web hook and supplies custom parameters.
	public void GrantExperience()
	{
		//Trigger Custom Event
		int expAward = UnityEngine.Random.Range(5000,10000);
		Debug.Log("EXP: " + expAward);
		photonComponent.GameInstance.OpRaiseEvent(
			eventCode: 15,
		customEventContent: new Hashtable() { { "eventType", "exp" }, { "amt", expAward } },
		sendReliable: true,
		raiseEventOptions: new RaiseEventOptions()
		{
			ForwardToWebhook = true,
		});
		StartCoroutine(GetUserStats(1.25f));
	}
	
	// standard example for logging in with an Android device id
	public void LoginToPlayFab()
	{
		Debug.Log("Using demo device id");
		LoginWithAndroidDeviceIDRequest request = new LoginWithAndroidDeviceIDRequest();
		request.AndroidDeviceId = deviceId;
		request.TitleId = playfabTitleId;
		request.CreateAccount = true;
		PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnPlayFabError);
		this.activeState = GuiStates.loading;
	}
	
	// callback on successful LoginToPlayFab request 
	void OnLoginSuccess(LoginResult result)
	{
		Debug.Log(result.PlayFabId);
		StartCoroutine(GetUserStats());
		this.playfabId = result.PlayFabId;
		GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest();
		request.PhotonApplicationId = photonComponent.AppId.Trim();
		// get an authentication ticket to pass on to Photon 
		PlayFabClientAPI.GetPhotonAuthenticationToken(request, OnPhotonAuthenticationSuccess, OnPlayFabError);
	}
	
	// callback on successful GetPhotonAuthenticationToken request 
	void OnPhotonAuthenticationSuccess(GetPhotonAuthenticationTokenResult result)
	{
		photonComponent.ConnectToMasterServer(this.playfabId, result.PhotonCustomAuthenticationToken);
	}
	
	// Example for getting the user statistics for a player.
	IEnumerator GetUserStats(float sec = 0)
	{
		yield return new WaitForSeconds(sec);
		GetUserStatisticsRequest request = new GetUserStatisticsRequest();
		PlayFabClientAPI.GetUserStatistics(request, OnGetUserStatsSuccess, OnPlayFabError);
	}
	
	// callback on successful GetUserStats request
	void OnGetUserStatsSuccess(GetUserStatisticsResult result)
	{
		// some logic for determineing if the player's team changed.
		if(result.UserStatistics.ContainsKey("BluTeamJoinedCount") && this.userStats.ContainsKey("BluTeamJoinedCount"))
		{
			if(result.UserStatistics["BluTeamJoinedCount"] > this.userStats["BluTeamJoinedCount"] )
			{
				this.team = "Blu";
				Debug.Log(result.UserStatistics["BluTeamJoinedCount"] + " : " + this.userStats["BluTeamJoinedCount"]);
			} 
		}
		else if(result.UserStatistics.ContainsKey("BluTeamJoinedCount") && !this.userStats.ContainsKey("BluTeamJoinedCount"))
		{
			if(this.userStats.Count != 0)
			{
				this.team = "Blu";
				Debug.Log(result.UserStatistics["BluTeamJoinedCount"] + " : blue" );
			}
		}
		
		if(result.UserStatistics.ContainsKey("RedTeamJoinedCount") && this.userStats.ContainsKey("RedTeamJoinedCount"))
		{
			if(result.UserStatistics["RedTeamJoinedCount"] > this.userStats["RedTeamJoinedCount"] )
			{
				this.team = "Red";
				Debug.Log(result.UserStatistics["RedTeamJoinedCount"] + " : " + this.userStats["RedTeamJoinedCount"]);
			} 
		}
		else if(result.UserStatistics.ContainsKey("RedTeamJoinedCount") && !this.userStats.ContainsKey("RedTeamJoinedCount"))
		{
			if(this.userStats.Count != 0)
			{
				this.team = "Red";
				Debug.Log(result.UserStatistics["RedTeamJoinedCount"] + " : red");
			}
		}
		// save user stats for game useage
		this.userStats = result.UserStatistics;
	}

	// Generic PlayFab callback for errors.
	void OnPlayFabError(PlayFabError error)
	{
		Debug.Log(error.ErrorMessage);
	}
#endregion

#region helper functions
	/// <summary>
	/// Provided for resetting the demo state.
	/// </summary>
	public void LeaveGame()
	{
		this.isAuthenticated = false;
		this.isJoined = false;
		this.activeState = GuiStates.loading;
		this.team = string.Empty;
		this.hideTips = false;
	}
}
#endregion
