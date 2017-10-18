using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System;
using UnityEngine.UI;
using System.IO;

namespace Lobby{
	public class MyLobbyPlayer : NetworkLobbyPlayer {
		bool ready;
		private Button readyButton;
		void Start(){
			Debug.Log ("start lobby player");
			ready = false;
		}

		public override void OnClientEnterLobby(){
			Debug.Log ("lobby enter ");
			base.OnClientEnterLobby ();
			LobbyPlayerList._singleton.AddPlayer (this);
			readyButton = GameObject.Find ("Ready").GetComponent<Button> ();
			// XXX 发现LobbyPlayer的isLocalPlayer没有在此处赋值，所以暂时延迟0.1s后再检测
			Invoke ("bindReadyButton",0.1f);
		}

		public void bindReadyButton(){
			if (isLocalPlayer) {
				readyButton.onClick.AddListener (OnReadyClick);
			}
		}

		public void OnReadyClick(){
			Debug.Log ("ready");
			SendReadyToBeginMessage ();
			OnClientReady (true);
			readyToBegin = true;

			ready = true;
		}
	}
}
