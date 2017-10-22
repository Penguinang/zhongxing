using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

namespace Player{
	public class PlayerInput : NetworkBehaviour {
		public GameObject myPlayer;
		private float rotateVelocity;
		// Use this for initialization

		void Start () {
			myPlayer = gameObject;
			rotateVelocity = 10;

			if (isLocalPlayer) {
				Debug.Log ("find localPLayer");
				LobbyManager.localPlayer = gameObject;
			}
			else 
				Debug.Log ("isn't localPLayer");
		}

		void FixedUpdate(){
			if(!isLocalPlayer)
				return;
			float horizontal = Input.GetAxis ("Horizontal");
			float vertical = Input.GetAxis ("Vertical");
			PlayerController player = myPlayer.GetComponent<PlayerController>();
			player.move (Vector3.forward*vertical);
			player.move (Vector3.right * horizontal);

			bool ro_right = Input.GetKey ("k");
			if (ro_right)
				player.rotate (rotateVelocity);
			bool ro_left = Input.GetKey ("j");
			if(ro_left)
				player.rotate (rotateVelocity*-1);
		}

		// -------------------------------player function API-------------------------------------
		public void OnProtectionClick(){
			CmdOnProtectionClick ();
		}

		// -------------------------------Client callback function------------------------------------
		[Command]
		public void CmdOnProtectionClick(){
			RpcOnProtectionClick ();
		}

		public void OnBombClick(){
			Debug.Log ("Bomb clicked");
		}

		public void OnShipPress(){
			Debug.Log ("Ship pressed");
		}

		public void OnShipRelease(){
			Debug.Log ("Ship Release");
		}

		// -------------------------------Server Callback function--------------------------------------
		[ClientRpc]
		public void RpcOnProtectionClick(){
			Debug.Log ("Protection clicked");
			// XXX
			if (!Application.isEditor) {
				
			}
		}
	}

}
