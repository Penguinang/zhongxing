using UnityEngine;
using System.Collections;

public class PlanetIcon : MonoBehaviour
{	
	private GameObject icon;
	public void SetIconById(int id){
		SetIcon (PlanetManager.instance.GetPlanetIconByPlayerID (id));
	}

	public void SetIcon(GameObject iconPrefab){
		if (icon)
			Destroy (icon);
		icon = Instantiate (iconPrefab);
		icon.transform.SetParent (transform);
		icon.transform.localPosition = new Vector3 (0, 0, 0);
	}
}

