using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour {
	public GameObject myPlayer;
	private float rotateVelocity;
	// Use this for initialization
	void Start () {
		myPlayer = gameObject;
		rotateVelocity = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
