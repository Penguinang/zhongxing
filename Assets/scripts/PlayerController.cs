using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private int forceRatio;
	void Start () {
		 forceRatio= 4;
	}
	
	// Update is called once per frame
	void Update () {
		
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
