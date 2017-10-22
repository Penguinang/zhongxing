using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMessage : MonoBehaviour {
	public string _Name;
	public string Name{
		get{ return _Name;}
		set{ if (value != null) {
				_Name = value;
				Debug.Log ("playerName changed to " + _Name);
			}
		}
	}

	void Start(){

	}
}
