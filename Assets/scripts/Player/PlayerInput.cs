using System.Collections;
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

		// -------------------------------player function API-------------------------------------
		/// <summary>
		/// Receive Protection Button Input;
		/// </summary>
		/// <param name="planets">planets which need be in a protection cover.</param>
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
