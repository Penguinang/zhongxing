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
	public NetworkManager networkManager;
	public OverridenNetworkDiscovery networkDiscovery;
	public InputField roomInput;
	public InputField numInput;

	private string roomName;
	private int playerNum;
	private int curPlayerNum;
	private List<NetworkConnection> conns;

	// ip是由服务器决定后，广播给客户端，再由客户端获得
	public string ipAddress;
	// port 应该在为服务器和客户端确定相同的值
	public int port ;
	void Start(){
		ipAddress = "localhost";
		port = 7777;
		curPlayerNum = 0;
		conns = new List<NetworkConnection> ();
	}

	public void OnHostClick(){
		roomName = roomInput.text;
		if (roomName.Equals ("")) {
			Debug.Log ("please input a room name");
			return;
		}

		try{
			playerNum = int.Parse (numInput.text);
		}
		catch(Exception e){
			Debug.Log (e.Message);
			return;
		}
		if (playerNum < 2||playerNum>4) {
			Debug.Log ("cuit number is between 2 and 4");
			return;
		}

		CreateLanHostGame ();
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

	public void OnJoinClick(){		
		roomName = roomInput.text;
		if (roomName.Equals ("")) {
			Debug.Log ("please input a room name");
			return;
		}
		StartListenforAddressAsClient ();
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
		Debug.Log ("get server ip");
		networkManager.networkAddress = serverAddress;

		networkManager.networkPort = port;

		networkManager.StartClient ();
	}

	public void OnServerConnect(NetworkConnection conn){
		conns.Add (conn);
		curPlayerNum++;
		Debug.Log ("curPLayernNUm is :"+curPlayerNum);
		// XXX “准备功能”
		/*
		 * 1.  这是为了“准备”功能写的，涉及到了一些传输层API，暂时搁置
		 */
//		DebugConnection dconn = new DebugConnection();
//		byte[] msg = new byte[20];
//		dconn.TransportRecieve (msg,20,23890);
//		Debug.Log (msg.ToString ());

		if (curPlayerNum >= playerNum) {
			Debug.Log ("start game");
			StartGame ();
		}
	}

	//XXX 踢人操作，搁置
//	public void KickOut(NetworkConnection conn){
////		NetworkServer.RemoveExternalConnection (conn);
//	}

	void StartGame(){
		//XXX 
		/*
		 *  1. 开始游戏后需要更换游戏场景，待补充
		 *  2. 游戏开始后不能立即生成玩家物体，需要显示准备状态
		 */
		networkDiscovery.StopBroadcast ();
		Debug.Log (conns.Count);
//+++++++++++++++++++++++++++++          问题代码            +++++++++++++++++++++++++++++++++++//
		SceneManager.LoadScene ("Game");
		DontDestroyOnLoad (this);
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
		foreach(NetworkConnection conn in conns){
			GameObject player = (GameObject)Instantiate (networkManager.playerPrefab,Vector3.zero,Quaternion.identity);
			NetworkServer.AddPlayerForConnection (conn,player,0);
			Debug.Log ("spawn player");
		}
	}

}