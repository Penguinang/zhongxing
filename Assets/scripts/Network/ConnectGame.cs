using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using System.Configuration;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class ConnectGame : MonoBehaviour {
	static public ConnectGame _singleton;

	public NetworkLobbyManager networkManager;
	public OverridenNetworkDiscovery networkDiscovery;
	public InputField roomInput;
	public GameObject roomPanel;
	public GameObject createGamePanel;

	private string roomName;

	// ip是由服务器决定后，广播给客户端，再由客户端获得
	public string ipAddress;
	// port 应该在为服务器和客户端确定相同的值
	public int port ;

	public GameObject localPlayer;

	void Awake(){
		if (_singleton == null) {
			_singleton = this;
		}
		ipAddress = "localhost";
		port = 7777;
		DontDestroyOnLoad (this);
	}

	// ------------------------common callback ---------------------------

	// ------------------------ server clickcallback ------------------------
	public void OnHostClick(){
		roomName = roomInput.text;
		if (roomName.Equals ("")) {
			Debug.Log ("please input a room name");
			return;
		}

		CreateLanHostGame ();
		roomPanel.SetActive (true);
		createGamePanel.SetActive (false);
	}

	public void CreateLanHostGame(){
		if (ipAddress.Equals("")) {
			Debug.Log ("wrong ip adress");
			return;
		}
		networkManager.networkAddress = ipAddress;
		networkManager.networkPort = port;

		networkManager.StartHost ();
		StartBrocastAddressAsServer ();
		//DEBUG
		Debug.Log ("success create server");
	}

	void StartBrocastAddressAsServer(){
		networkDiscovery.Initialize ();
		networkDiscovery.setData (roomName);
		networkDiscovery.StartAsServer ();
	}

	// ------------------------ client clickcallback ------------------------
	public void OnJoinClick(){		
		roomName = roomInput.text;
		if (roomName.Equals ("")) {
			Debug.Log ("please input a room name");
			return;
		}
		StartListenforAddressAsClient ();
		roomPanel.SetActive (true);
	}

	void StartListenforAddressAsClient(){
		//DEBUG
		Debug.Log ("success create client");
		networkDiscovery.Initialize ();
		networkDiscovery.setData (roomName);
		networkDiscovery.StartAsClient ();
	}

	public void JoinLanGame(string serverAddress){
		if ("".Equals (serverAddress)) {
			Debug.Log ("wrong ip address");
			return;
		}
		//DEBUG
		Debug.Log ("get server ip,and join lan game  "+serverAddress);
		networkManager.networkAddress = serverAddress;

		networkManager.networkPort = port;

		networkManager.StartClient ();
	}

	// ------------------------ server handlers ------------------------
	public void OnServerConnect(NetworkConnection conn){	}

	// ------------------------ client handlers ------------------------
	public void OnClientConnect(NetworkConnection conn){
		Debug.Log ("success connect to server");
	}

	public void StartGame(){
		networkManager.ServerChangeScene (networkManager.playScene);
		roomPanel.SetActive (false);
		createGamePanel.SetActive (false);
	
		networkDiscovery.StopBroadcast ();
		Debug.Log ("isserver");

		LoadScene ();
	}

	public void LoadScene(){
		Debug.Log ("networkServer change scene");
//		networkManager.ServerChangeScene (networkManager.playScene);
	}
}