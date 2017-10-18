using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class TestPlayer : MonoBehaviour {
	public GameObject player;

	private PlayerInput playerInput;

	void Start(){
		Invoke ("bindPlayer",0.1f);
	}

	void bindPlayer(){
		if (!player) {
			player = ConnectGame._singleton.localPlayer;
			if (!player) {
				Debug.Log ("localPLayer is null");
				return;
			}
			playerInput = player.GetComponent<PlayerInput> ();
		}
	}
	void Update(){
		bool protect = Input.GetKey ("p");
		if (protect) 
			playerInput.OnProtectionClick ();
		
	}
}
