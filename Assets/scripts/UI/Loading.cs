using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {
    public float speed = 90;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
        Vector3 zero = new Vector3(512, 152, 0);
        transform.RotateAround(zero, Vector3.back,speed* Time.deltaTime);
	}
}
