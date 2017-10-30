﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Player{
	public class PlayerController : NetworkBehaviour {
		public GameObject BarrierPrefab;
		private int forceRatio;

		void Start () {
			forceRatio= 10;
		}

		public void move(Vector3 direction){
			Rigidbody body = gameObject.GetComponent<Rigidbody>();
			body.AddForce (direction*forceRatio);
		}

		//DEBUG
		[Command]
		public void CmdMove(Vector2 dir){
			RpcMove (dir);
		}
		[ClientRpc]
		public void RpcMove(Vector2 dir){
			move (dir);
		}

		public void rotate(float degree){
			transform.Rotate (Vector3.up*degree);
			Debug.Log ("rotate");
		}

		// -------------------------------Client callback function------------------------------------
		[Command]
		public void CmdOnProtectionClick(int[] planets){
			RpcOnProtectionClick (planets);
		}

		[Command]
		public void CmdOnShipClick(int planetID,Vector3 velocity){
			RpcOnShipClick (planetID,velocity);
		}

		[Command]
		public void CmdOnShellClick(int planetID,Vector3 direction){
			RpcOnShellClick (planetID,direction);
		}
//
//		public void OnBombClick(){
//			Debug.Log ("Bomb clicked");
//		}
//
//		public void OnShipPress(){
//			Debug.Log ("Ship pressed");
//		}
//
//		public void OnShipRelease(){
//			Debug.Log ("Ship Release");
//		}

		// -------------------------------Server Callback function--------------------------------------
		[ClientRpc]
		public void RpcOnProtectionClick(int[] planets){
			Debug.Log ("Protection clicked");

			GameObject barrier = Instantiate (BarrierPrefab);
			barrier.GetComponent<Barrier> ().Init (planets);
		}

		[ClientRpc]
		public void RpcOnShipClick(int planetID,Vector3 velocity){
			OnShipClick (planetID,velocity);
		}

		[ClientRpc]
		public void RpcOnShellClick(int planetID,Vector3 direction){
			OnShellClick (planetID,direction);
		}

		// -------------------------------Real Function--------------------------------------
		public void OnShipClick(int planetID,Vector3 velocity){
			GameObject planet = PlanetManager.GetPlanet (planetID);
			if (!planet)
				return;
			planet.GetComponent<Planet> ().LaunchShip (velocity);
			//XXX
			Debug.Log ("planet whose id is  "+planetID+" release ship");
		}

		public void OnShellClick(int planetID,Vector3 direction){
			GameObject planet = PlanetManager.GetPlanet (planetID);
			if (!planet) {
				Debug.Log ("wrong planet id , can't find planet");
				return;
			}
			planet.GetComponent<Planet> ().LaunchShell (direction);
		}
	}
}