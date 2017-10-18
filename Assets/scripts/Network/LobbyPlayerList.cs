using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Configuration;

namespace Lobby{
	public class LobbyPlayerList : MonoBehaviour {
		static public LobbyPlayerList _singleton=null;
		public RectTransform listsArea;

		private List<MyLobbyPlayer> _players;
		public void Awake(){
			if (_singleton == null) {
				_singleton = this;
			}
			_players = new List<MyLobbyPlayer> ();
		}

		public void AddPlayer(MyLobbyPlayer lobbyPlayer){
			lobbyPlayer.transform.SetParent (listsArea,false);
			lobbyPlayer.transform.localPosition = new Vector3 ();
			if (_players.Count == 0) {

			}				
			else {
				MyLobbyPlayer lastPlayer = _players [_players.Count - 1];
				lobbyPlayer.transform.position = lastPlayer.transform.position + new Vector3 (0, -250, 0);
			}
			_players.Add (lobbyPlayer);
			Debug.Log ("add player");
		}
	}
}
