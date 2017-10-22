using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OverridenNetworkManager : NetworkManager{
	public override void OnServerConnect(NetworkConnection conn){
		//DEBUG a client connect
		ConnectGame connectGame = gameObject.GetComponent<ConnectGame> ();
		connectGame.OnServerConnect (conn);
	}

	public override void OnClientConnect(NetworkConnection conn){
		ClientScene.Ready (conn);
		Debug.Log ("client ready");
		ClientScene.AddPlayer (0);
	}
	public override void OnServerAddPlayer(NetworkConnection conn,short controllerid){
		Debug.Log ("default onServerAddPlayer has been screened");
	}
}
