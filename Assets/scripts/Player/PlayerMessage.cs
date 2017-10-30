using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMessage : NetworkBehaviour {
	public TextMesh PlayerNameText;

	[SyncVar(hook = "OnSyncNameChanged")]
	public string Name = "";

	public void OnSyncNameChanged(string name){
		Debug.Log ("SyncVar playerName changed to " + name);
		PlayerNameText.text = name;
	}

	[SyncVar]
	public int ID=-1;

	[SyncVar(hook="OnEnergyChange")]
	public float energy;

	void Start(){
		Invoke ("PatchChangeName",1);
		Invoke ("UpdatePlanetStatus",1);
	}

	void PatchChangeName(){
		PlayerNameText.text = Name;
	}

	public void UpdatePlanetStatus(){
		Debug.Log ("player successfully update planet id");
		int[] planet = IDManager.instance.GetPlanetIDForPlayer (ID);
		if (planet.Length < 1)
			return;
		if (isLocalPlayer) {
			PlanetManager.GetPlanet (planet[0]).GetComponent<Planet> ().status = 1;
		} else {
			PlanetManager.GetPlanet (planet[0]).GetComponent<Planet>().status = 0;
		}
	}

	private void OnEnergyChange(float energy){
		//XXX
		if (isLocalPlayer)
			;
	}
}
