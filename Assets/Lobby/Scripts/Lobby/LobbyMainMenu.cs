using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;

namespace Prototype.NetworkLobby
{
    //Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
    public class LobbyMainMenu : MonoBehaviour 
    {
        public LobbyManager lobbyManager;

		public RectTransform waiting;

		public void OnMatchClick(){
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
}
