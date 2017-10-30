﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Runtime.InteropServices;

namespace Player{
	public class PlayerInput : NetworkBehaviour {
		public GameObject myPlayer;
		private float rotateVelocity;
		private PlayerController playerController;
		void OnEnable(){
			Debug.Log ("gameplayer onenable");
		}
		void Start () {
			myPlayer = gameObject;
			rotateVelocity = 10;
			playerController = GetComponent<PlayerController> ();

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
		public void OnProtectionClick(int[] planets){
			playerController.CmdOnProtectionClick (planets);
		}

		public void OnShipClick(int planetID,Vector3 velocity){
			playerController.CmdOnShipClick (planetID,velocity);
		}

		public void OnShellClick(int planetID,Vector3 direction){
			playerController.CmdOnShellClick (planetID,direction);
		}

		public void OnRockerMoved(Vector2 value){
			Debug.Log ("Rocker value changed");
			Debug.Log (value);
			playerController.CmdMove (value);
		}
	}

}
