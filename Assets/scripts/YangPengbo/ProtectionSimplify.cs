using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProtectionSimplify : MonoBehaviour {
	public GameObject PlanetTouchCircle;
	public GameObject PlanetTouches;
	void Start(){
		GameObject touch = Instantiate (PlanetTouchCircle);
		touch.transform.SetParent (PlanetTouches.transform);
		touch.transform.position = Camera.main.WorldToScreenPoint (transform.position);
		Debug.Log ("instantiate touch");
	}
}

