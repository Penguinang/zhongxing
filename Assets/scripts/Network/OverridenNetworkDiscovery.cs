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

		// XXX “准备功能”
		/*
		 * 1.  这是为了“准备”功能写的，涉及到了一些传输层API，暂时搁置
		 */
//		DebugConnection conn = new DebugConnection();
//		byte[] byteArray = System.Text.Encoding.Default.GetBytes ("a new debugconnection");
//		Byte error;
//		conn.TransportSend (byteArray,20,23890,out error);
	}
}
