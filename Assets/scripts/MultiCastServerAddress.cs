using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using UnityEngine.Networking;

public class MultiCastServerAddress : NetworkBehaviour {
//	private int server_port = 7777;
//	private string server_ip;
//	private int startup_port = 5100;
//	private IPAddress group_address = IPAddress.Parse ("224.0.0.224");
//	private UdpClient udp_client;
//	private IPEndPoint remote_end;
//	void Start () {
//		if (isServer)
//			StartGameServer ();
//		else
//			StartGameClient();
//	}
//
//	void StartGameServer(){
//		NetworkConnectionError init_status =  Network.InitializeServer (10,server_port,false);
//		Debug.Log ("status"+init_status);
//
//		StartBroadCast ();
//	}
//
//	void StartGameClient(){
//		remote_end = new IPEndPoint (IPAddress.Any, startup_port);
//		udp_client = new UdpClient (remote_end);
//		udp_client.JoinMulticastGroup (group_address);
//
//		udp_client.BeginReceive (new AsyncCallback (ServerLookup), null);
//		MakeConnection ();
//	}
//
//	void MakeConnection ()
//	{
//		// continues after we get server's address
//		while (server_ip == null||server_ip.Equals (""))
//			yield;
//
//		while (Network.peerType == NetworkPeerType.Disconnected)
//		{
//			Debug.Log ("connecting: " + server_ip +":"+ server_port);
//
//			// the Unity3d way to connect to a server
//			NetworkConnectionError error;
//			error = Network.Connect (server_ip, server_port);
//
//			Debug.Log ("status: " + error);
//			yield;
//		}
//	}
//
//	void ServerLookup (IAsyncResult ar)
//	{
//		// receivers package and identifies IP
//		var receiveBytes = udp_client.EndReceive (ar, remote_end);
//
//		server_ip = remote_end.Address.ToString ();
//		Debug.Log ("Server: " + server_ip);
//	}
//	// Upd
//
//	void StartBroadCast(){
//		// multicast send setup
//		udp_client = UdpClient ();
//		udp_client.JoinMulticastGroup (group_address);
//		remote_end = IPEndPoint (group_address, startup_port);
//
//		// sends multicast
//		while (true)
//		{
//			var buffer = Encoding.ASCII.GetBytes ("GameServer");
//			udp_client.Send (buffer, buffer.Length, remote_end);
//
////			yield WaitForSeconds (1);
//			yield;
//		}
//	}
}
