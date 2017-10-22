using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.NetworkLobby
{
    public class LobbyPlayer : NetworkLobbyPlayer
    {
//        public InputField nameInput;

        public override void OnClientEnterLobby()
        {
            base.OnClientEnterLobby();

			
            LobbyPlayerList._instance.AddPlayer(this);
			Debug.Log ("add player");
        }

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
        }

        public override void OnClientReady(bool readyState)
        {
            if (readyState)
            {
//                nameInput.interactable = false;
            }
            else
            {
//                nameInput.interactable = isLocalPlayer;
            }
        }

        [ClientRpc]
        public void RpcUpdateCountdown(int countdown)
        {
			Debug.Log ("replace countdownpanel");
//            LobbyManager.s_Singleton.countdownPanel.UIText.text = "Match Starting in " + countdown;
//            LobbyManager.s_Singleton.countdownPanel.gameObject.SetActive(countdown != 0);
        }

//        public void OnDestroy()
//        {
////            LobbyPlayerList._instance.RemovePlayer(this);
//        }

		void Start(){
			StartCoroutine (waitMatchPlayers ());
		}

		public IEnumerator waitMatchPlayers(){
			LobbyPlayerList playerList = LobbyPlayerList._instance;
			Transform waitingCircle = playerList.waitingCircle;
			Vector3 rotation = new Vector3 (0, 0, 1);
			while(playerList.getPlayerNum ()<1){
				yield return 0;
				rotation += new Vector3 (0, 0, 1);
				waitingCircle.Rotate (rotation);
			}
			SendReadyToBeginMessage ();
		}
    }
}
