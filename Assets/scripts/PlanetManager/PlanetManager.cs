using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class PlanetManager : MonoBehaviour
{
	static public PlanetManager instance;
	public GameObject[] planets;
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
}

