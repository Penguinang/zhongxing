using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static bool effect = true;
	public static AudioManager instance;
	public AudioSource AudioComponent;

	public AudioClip Explosion;
	public AudioClip Ship;
	public AudioClip Shell;

	public static GameObject AddSound(AudioClip soundParam){
		if (!effect)
			return null;
		GameObject sound = new GameObject();
		sound.AddComponent<AudioSource> ().clip = soundParam;
		sound.GetComponent<AudioSource> ().Play ();
		instance.StartCoroutine (instance.DestroySound(sound,soundParam.length));
		Debug.Log ("play sound");
		return sound;
	}

	IEnumerator DestroySound(GameObject sound,float time){
		yield return new WaitForSeconds (time);
		Destroy (sound);
	}

	public void SwitchMusic(bool play){
		AudioComponent.mute = play;
	}

	public void SwitchEffects(bool play){
		effect = play;
	}

	void Start(){
		if (!instance)
			instance = this;
		DontDestroyOnLoad (instance);
	}
}
