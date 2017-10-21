using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using MySocket;
using System.Net.Sockets;
using Prototype.NetworkLobby;

public class LogPanel : MonoBehaviour {
	public RectTransform UsernameInput;
	public RectTransform PasswordInput;
	public Button Login;

	public LobbyManager lobbyManager;
	public RectTransform waiting;
	public RectTransform wrongPassword;
	public RectTransform alreadyLogin;
	public RectTransform wrongUsername;

	private string serverIp;

	void Start(){
		serverIp = "138.68.18.64";
	}

	public void OnClickLogin(){
		StartCoroutine (tryLogin ());
	}

	public IEnumerator tryLogin(){
//		ClientSocket client = new ClientSocket ();
//		client.ConnectServer (serverIp,8088);
//		Debug.Log ("successfully connect server");
//		yield return 0;
//
//		string username = UsernameInput.GetComponent<Text> ().text;
//		string password = PasswordInput.GetComponent<Text> ().text;
//
//		client.SendMessage (new Item (username,password,"online").formatRecord ());
//		Debug.Log ("request to log in");
//		yield return 0;
//
//		string response = client.ReceiveMessage ();
//		Debug.Log ("get response from server");
//		yield return 0;

//		if (response.Contains ("success")) {
		yield return 0;
		if(true){
			OnStartMatch ();
		}
		else {
			wrongPassword.gameObject.SetActive (true);
		};
	}

	public void OnStartMatch(){
		lobbyManager.StartMatchMaker ();
		lobbyManager.matchMaker.ListMatches (0,6,"",true,0,0,OnListGet);
		waiting.gameObject.SetActive (true);
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
		waiting.gameObject.SetActive (false );
	}

}
