using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using System.Configuration;

public class ConnectGame : MonoBehaviour {
	public NetworkManager networkManager;
	public OverridenNetworkDiscovery networkDiscovery;
	public InputField roomInput;

	private string roomName;

	// ip是由服务器决定后，广播给客户端，再由客户端获得
	public string ipAddress;
	// port 应该在为服务器和客户端确定相同的值
	public int port ;
	void Start(){
		networkDiscovery = gameObject.GetComponent<OverridenNetworkDiscovery>();

		ipAddress = "localhost";
		port = 7777;
	}

	public void OnServerConnect(){
		//XXX 停止广播地址，应该在房间人数满后，此处应修改
		networkDiscovery.StopBroadcast ();
	}

	public void OnHostClick(){
		roomName = roomInput.text;
		if (roomName.Equals ("")) {
			Debug.Log ("please input a room name");
			return;
		}
		CreateLanHostGame ();
	}

	public void OnJoinClick(){		
		roomName = roomInput.text;
		if (roomName.Equals ("")) {
			Debug.Log ("please input a room name");
			return;
		}
		StartListenforAddressAsClient ();
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
	}
	
	public void JoinLanGame(string serverAddress){
		if ("".Equals (serverAddress)) {
			Debug.Log ("wrong ip address");
			return;
		}
		networkManager.networkAddress = serverAddress;

		networkManager.networkPort = port;

		networkManager.StartClient ();
	}

	void StartBrocastAddressAsServer(){
		networkDiscovery.Initialize ();
		networkDiscovery.setData (roomName);
		networkDiscovery.StartAsServer ();
	}

	void StartListenforAddressAsClient(){
		networkDiscovery.Initialize ();
		networkDiscovery.setData (roomName);
		networkDiscovery.StartAsClient ();
	}
}