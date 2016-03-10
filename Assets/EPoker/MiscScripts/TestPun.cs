using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;
using PlayFab;
using PlayFab.ClientModels;

public class TestPun : MonoBehaviour
{
	public readonly string TitleId = "4214";
	public readonly string CustomId = "customid001";
	public readonly string PhotonApplicationId = "60090e03-9030-4321-b497-270418f42a37";

	public string PlayFabId;

	// Use this for initialization
	void Start ()
	{
		PlayFabSettings.TitleId = TitleId;
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
		request.TitleId = TitleId;
		request.CreateAccount = true;
		request.CustomId = CustomId;
		PlayFabClientAPI.LoginWithCustomID (request, OnLoginSuccess, OnPlayFabError);
	}

	void OnLoginSuccess (LoginResult result)
	{
		PlayFabId = result.PlayFabId;
		GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest ();
		request.PhotonApplicationId = PhotonApplicationId;
//		// get an authentication ticket to pass on to Photon 
		PlayFabClientAPI.GetPhotonAuthenticationToken (request, OnPhotonAuthenticationSuccess, OnPlayFabError);
	}

	void OnPhotonAuthenticationSuccess (GetPhotonAuthenticationTokenResult result)
	{
		Debug.LogFormat ("GetPhotonAuthenticationTokenResult = {0} | {1}", result.CustomData, result.PhotonCustomAuthenticationToken);

		PhotonNetwork.ConnectToMasterServerWithAuthParams (PlayFabId, result.PhotonCustomAuthenticationToken, PhotonApplicationId, "0.1");
	}

	void OnPlayFabError (PlayFabError error)
	{
		Debug.LogFormat ("PlayFabError = {0} | {1}", error.Error, error.ErrorMessage);
	}


}

