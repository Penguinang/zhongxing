using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using MySocket;
using System.Net.Sockets;
using Prototype.NetworkLobby;
using UnityEngineInternal;

public class LogPanel : MonoBehaviour {
	public RectTransform UsernameInput;
	public RectTransform PasswordInput;

	public LobbyManager lobbyManager;
	public RectTransform logError;
	public RectTransform alreadyLogin;
	public RectTransform matching;
	public RectTransform linking;


	private string serverIp;

	void Start(){
		serverIp = "138.68.18.64";
	}

	public void OnClickLogin(){
		StartCoroutine (tryLogin ());
	}

	public IEnumerator tryLogin(){
		ClientSocket client = new ClientSocket ();
		client.ConnectServer (serverIp,8088);
		ChangeTipTo (linking);
		Debug.Log ("successfully connect server");
		yield return 0;

		string username = UsernameInput.GetComponent<InputField> ().text;
		LobbyManager.s_Singleton.localPlayerName = username;
		string password = PasswordInput.GetComponent <InputField> ().text;

		client.SendMessage (new Item (username,password,"online").formatRecord ());
		Debug.Log ("request to log in");
		yield return 0;

		string response = client.ReceiveMessage ();
		
		Debug.Log ("get response from server");
		yield return 0;

		if (response.Contains ("success")) {
//		if(true){
			OnStartMatch ();
		} else if (response.Contains ("fail")) {
			ChangeTipTo (logError);
		} else {
			ChangeTipTo (alreadyLogin);
		}
	}

	public void OnStartMatch(){
		lobbyManager.StartMatchMaker ();
		lobbyManager.matchMaker.ListMatches (0,6,"",true,0,0,OnListGet);
	}

	private void OnListGet(bool success, string extendedInfo, List<MatchInfoSnapshot> matches){
		if (matches.Count == 0) {
			lobbyManager.matchMaker.CreateMatch(
				"game",
				(uint)lobbyManager.maxPlayers,
				true,
				"", "", "", 0, 0,
				lobbyManager.OnMatchCreate);
			lobbyManager._isMatchmaking = true;				
		} else {
			foreach (MatchInfoSnapshot room in matches)
				if (room.currentSize < room.maxSize) {
					lobbyManager.matchMaker.JoinMatch(room.networkId, "", "", "", 0, 0, lobbyManager.OnMatchJoined);
					lobbyManager._isMatchmaking = true;
				}
		}
	}

	private RectTransform current;
	private void ChangeTipTo(RectTransform target){
		if (current)
			current.gameObject.SetActive (false);
		current = target;
		if(current)
			current.gameObject.SetActive (true);
	}

	public void OnMatching(){
			ChangeTipTo (matching);
	}
}
