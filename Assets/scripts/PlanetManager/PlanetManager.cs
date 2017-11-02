using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class PlanetManager : MonoBehaviour
{
	static public PlanetManager instance;
	public GameObject[] planets;

	public GameObject RedIcon;
	public GameObject YellowIcon;
	public GameObject LightBlueIcon;
	public GameObject DarkBlueIcon;

	void OnEnable(){
		if (!instance)
			instance = this;
	}
	public static GameObject GetPlanet(int ID){
		if (ID < instance.planets.Length)
			return instance.planets [ID];
		else
			return null;
	}

	public static int GetPlanetCount(){
		return instance.planets.Length;
	}

	public GameObject GetPlanetIconByPlayerID(int id){
		GameObject icon=null;
		switch (id) {
		case 0:
			icon = RedIcon;
			break;
		case 1:
			icon = YellowIcon;
			break;
		case 2:
			icon = LightBlueIcon;
			break;
		case 3:
			icon = DarkBlueIcon;
			break;
		}
		return icon;
	}
}

