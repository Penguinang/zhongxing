using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using System.ComponentModel;
using UnityEngine.UI;

public class BroadcastManager : MonoBehaviour {
	private static BroadcastManager instance;
	public GameObject MessagePrefab;
	public Transform Container;

	public static void AddMessage(string content){
		GameObject message = Instantiate (instance.MessagePrefab);
		message.transform.SetParent (instance.Container);
		message.GetComponentInChildren<Text> ().text=content;
	}
	void OnEnable(){
		if (!instance)
			instance = this;
	}
}
