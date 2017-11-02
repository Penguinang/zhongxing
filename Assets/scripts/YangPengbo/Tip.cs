using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour {
	public Text text;
	public Image image;
	void Start(){
		StartCoroutine (Animation ());
	}

	IEnumerator Animation(){
		float yStart = 308;
		float yMax = 486;
		float yEnd = 476;
		float keepTime = 3;
		float fadeTime = 1.5f;
		float velo = 20f;
		Vector3 velocity = new Vector3 (0,velo,0);

		RectTransform trans = GetComponent<RectTransform> ();
		trans.localPosition = new Vector2 (trans.localPosition.x, yStart);
		while (trans.localPosition.y < yMax) {
			trans.localPosition += velocity;
			if (transform.localPosition.y > yEnd)
				velocity -= new Vector3 (0, 0.3f, 0);
			yield return null;
		}

		while (trans.localPosition.y > yEnd) {
			trans.localPosition += -velocity;
			velocity -= new Vector3 (0, 0.6f, 0);
			Debug.Log ("y is : "+trans.localPosition.y);
			yield return null;
		}
			
		while (keepTime > 0) {
			keepTime -= Time.deltaTime;
			yield return null;
		}

		float curFadeTime = 0;
		float k = -0.8f / fadeTime;
		while(curFadeTime<fadeTime){
			curFadeTime += Time.deltaTime;
			SetTransparent (curFadeTime*k+1);
			yield return null;
		}
		Destroy (gameObject);
	}

	private void SetTransparent(float tr){
		Color cur = image.color;
		image.color = new Color (cur.r, cur.g, cur.b, tr);

		cur = text.color;
		text.color = new Color (cur.r, cur.g, cur.b, tr);
	}
}
