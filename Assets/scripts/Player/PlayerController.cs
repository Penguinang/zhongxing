using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player{
	public class PlayerController : MonoBehaviour {
		private int forceRatio;
		void OnEnable(){
			Debug.Log ("player spawned");
		}

		void Awake(){
			OnEnable ();
		}

		void Start () {
			forceRatio= 4;
			OnEnable ();
		}

		public void move(Vector3 direction){
			Rigidbody body = gameObject.GetComponent<Rigidbody>();
			body.AddForce (direction*forceRatio);
		}

		public void rotate(float degree){
			transform.Rotate (Vector3.up*degree);
			Debug.Log ("rotate");
		}
	}
}