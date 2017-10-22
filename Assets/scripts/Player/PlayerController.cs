using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Player{
	public class PlayerController : NetworkBehaviour {
		private int forceRatio;

		void Start () {
			forceRatio= 4;
		}

		public void move(Vector3 direction){
			Rigidbody body = gameObject.GetComponent<Rigidbody>();
			body.AddForce (direction*forceRatio);
		}

		public void rotate(float degree){
			transform.Rotate (Vector3.up*degree);
			Debug.Log ("rotate");
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