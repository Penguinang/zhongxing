using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Prototype.NetworkLobby;
using System.Net.NetworkInformation;

public class TestPlayer : MonoBehaviour {
	private PlayerInput playerInput;
	public List<GameObject> allPlanets;
	public static TestPlayer instance;

	public static GameObject getStar(int ID){
		return instance.allPlanets [ID];
	}

	void Start(){
		instance = this;
		GameObject stars = GameObject.Find ("DebugStars");
		allPlanets = new List<GameObject> ();
		for (int i = 0; i < stars.transform.childCount; i++) {
			allPlanets.Add (stars.transform.GetChild (i).gameObject);
		}

	}
	void Update(){
		if(!playerInput)
			playerInput = LobbyManager.localPlayer.GetComponent<PlayerInput> ();		
		bool protect = Input.GetKeyDown ("p");
		int[] planets = new int[4]{ 0, 1, 2, 3 };
		if (protect) 
			playerInput.OnProtectionClick (planets);		
	}
}
