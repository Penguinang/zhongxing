using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using Player;

public class Rocker : MonoBehaviour {
	public void OnRockerChanged(Vector2 value){
		LobbyManager.localPlayer.GetComponent<PlayerInput> ().OnRockerValueChanged (value);
	}
}
