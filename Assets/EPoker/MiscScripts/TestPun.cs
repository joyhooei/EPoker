using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using PlayFab;
using PlayFab.ClientModels;

public class TestPun : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		PlayFabSettings.TitleId = "4214";
		LoginToPlayFab ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void LoginToPlayFab ()
	{
		Debug.Log ("Using demo device id");
		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest ();
		request.TitleId = "4214";
		request.CreateAccount = true;
		request.CustomId = "customid001";
		PlayFabClientAPI.LoginWithCustomID (request, OnLoginSuccess, OnPlayFabError);
	}

	void OnLoginSuccess (LoginResult result)
	{
//		Debug.Log(result.PlayFabId);
//		StartCoroutine(GetUserStats());
//		this.playfabId = result.PlayFabId;
		GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest ();
		request.PhotonApplicationId = "60090e03-9030-4321-b497-270418f42a37";
//		// get an authentication ticket to pass on to Photon 
		PlayFabClientAPI.GetPhotonAuthenticationToken (request, OnPhotonAuthenticationSuccess, OnPlayFabError);
	}

	void OnPhotonAuthenticationSuccess (GetPhotonAuthenticationTokenResult result)
	{
		Debug.LogFormat ("GetPhotonAuthenticationTokenResult = {0} | {1}", result.CustomData, result.PhotonCustomAuthenticationToken);

		new LoadbalancingPeer (ConnectionProtocol.Udp);
	}

	void OnPlayFabError (PlayFabError error)
	{
		Debug.LogFormat ("PlayFabError = {0} | {1}", error.Error, error.ErrorMessage);
	}


}
