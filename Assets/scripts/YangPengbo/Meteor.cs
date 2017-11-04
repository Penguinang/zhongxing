using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
	public GameObject KillParticle;
	void OnDestroy(){
		Instantiate (KillParticle).transform.position = transform.position;
	}
}

