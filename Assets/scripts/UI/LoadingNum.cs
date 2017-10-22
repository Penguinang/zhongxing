using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingNum : MonoBehaviour {
    private Text loadingNum;
    
  
	// Use this for initialization
	void Start () {
        loadingNum = GetComponentInChildren<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {
        float timer = Time.deltaTime;
        loadingNum.text = "100";
	}
}
