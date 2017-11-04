using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using NUnit.Framework;
using System.Collections.Generic;

public class PlanetProtectDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Material lineEffect;
	LineRenderer line;
	List<int> planets;
	public void OnBeginDrag(PointerEventData eventData){
		if (!ButtonProtect.ProtectInput)
			return;
		line = gameObject.AddComponent<LineRenderer> ();
		line.positionCount = 1;
		line.material = lineEffect;
		planets = new List<int> ();
	}

	public void OnDrag(PointerEventData eventData){
		if (!ButtonProtect.ProtectInput)
			return;
		Vector3 curpos = Camera.main.ScreenToWorldPoint (eventData.position);
		curpos -= new Vector3 (0, 0, Camera.main.gameObject.transform.position.z);
		line.SetPosition (line.positionCount-1,curpos);
		for (int i = 0; i < PlanetManager.GetPlanetCount (); i++) {
			GameObject planet = PlanetManager.GetPlanet (i);
			Debug.Log ("check planet : "+i);
			Debug.Log ("curpos : "+curpos+" , planet pos : "+planet.transform.position+" , magnitude : "+(curpos - planet.transform.position).magnitude);
			if ((curpos - planet.transform.position).magnitude < 1&&!planets.Contains (i)) {
				line.SetPosition (line.positionCount-1,planet.transform.position);
				line.positionCount += 1;
				planets.Add (i);
			}
		}
		Debug.Log ("on drag continue , cur pos is : "+curpos);
	}

	public void OnEndDrag(PointerEventData eventData){
		if (!ButtonProtect.ProtectInput)
			return;
		line.positionCount -= 1;
		Debug.Log ("On Drag End, planets is "+planets.Count);
		ButtonProtect.ProtectInput = false;
	}
}

