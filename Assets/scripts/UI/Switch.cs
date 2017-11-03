using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Switch : MonoBehaviour {
    public GameObject isOnGameObject;
    public GameObject isOffGameObject;
    private Toggle toggle;

    // Use this for initialization
    void Start () {
        toggle = GetComponent<Toggle>();   
    }

    public void OnValueChange(bool isOn)
    {
		isOn = toggle.isOn;
        isOnGameObject.SetActive(isOn);
        isOffGameObject.SetActive(!isOn);
    }

	public void OnMusicChange(bool isOn){
		isOn = toggle.isOn;
		AudioManager.instance.SwitchMusic (isOn);
	}

	public void OnEffectChange(bool isOn){
		isOn = toggle.isOn;
		AudioManager.instance.SwitchEffects (isOn);
	}
}
