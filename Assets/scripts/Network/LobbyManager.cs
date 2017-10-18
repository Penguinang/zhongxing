using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using Prototype.NetworkLobby;


namespace Lobby{
	public class LobbyManager : NetworkLobbyManager {
		public override void OnServerConnect(NetworkConnection conn){
			base.OnServerConnect (conn);
			ConnectGame._singleton.OnServerConnect (conn);
		}

		public override void OnClientConnect(NetworkConnection conn){
			base.OnClientConnect (conn);
			ConnectGame._singleton.OnClientConnect(conn);
		}

		public override void OnLobbyServerPlayersReady(){
			// 内部类会自己管理所有玩家的准备状态，一旦所有玩家准备好后就会自动转换场景开始游戏，这个函数就是传递消息的
//			base.OnLobbyServerPlayersReady ();
			bool allready = true;
			foreach (NetworkLobbyPlayer player in lobbySlots) {
				if(player)
					allready &= player.readyToBegin;	
			}

			if (allready) {
				Debug.Log ("allready ,start!!");
				ConnectGame._singleton.StartGame ();
//				ServerChangeScene (playScene);
			}

		}
			
		public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
		{
			Debug.Log (gamePlayer);
			return true;
		}
	}

}