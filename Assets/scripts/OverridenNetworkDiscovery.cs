using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.Experimental.Rendering;

public class OverridenNetworkDiscovery :  NetworkDiscovery{
	private bool inGame;

	void start(){
//		showGUI = true;
//		broadcastPort = 47777;
//		broadcastKey = 2222;
//		broadcastVersion = 1;
//		broadcastSubVersion = 1;
//		broadcastInterval = 1000;
//		useNetworkManager = true;

		inGame = false;
	}

	public void setData(string data){
		broadcastData = data;
	}

	public override void OnReceivedBroadcast(string fromAddress, string data){
		data = data.TrimEnd ((char)0);

		if (inGame)
			return;
		if (!data.Equals (broadcastData)) {
			Debug.Log ("find room not target :"+data);
			return;
		}
		ConnectGame connectComponent = gameObject.GetComponent<ConnectGame> ();
		connectComponent.JoinLanGame (fromAddress);
		inGame = true;
	}
}
