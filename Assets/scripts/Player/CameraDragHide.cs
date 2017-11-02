using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Security;

public class CameraDragHide : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler{
	public Transform MinMapEdge;
	public Camera MinMap;
	public float moveRatio = 100;
	public float edgeValue = 50;
	public float RecoverVelocity = 1;
	public Transform MapMinPosition;
	public Transform MapMaxPosition;

	private float curPer = 0;
	Vector2 lastPosition;
	Vector2 currPosition;
	

	public void OnBeginDrag(PointerEventData eventData){
		Debug.Log ("On Begin Drag");
	}

	public void OnDrag(PointerEventData eventData){
		Debug.Log ("OnDrag");
		currPosition = eventData.position;
		float movement = lastPosition.y-currPosition.y;
		setMapAndEdgePosBy (0);
	}

	public void OnEndDrag(PointerEventData eventData){
		if (curPer >= edgeValue)
			StartCoroutine (Move (new Vector2 (0, 1)));
		else
			StartCoroutine (Move (new Vector2 (0, -1)));
	}

	IEnumerator Move(Vector2 dir){
		while (curPer < 100 && curPer > 0) {
			setMapAndEdgePosBy (dir.y*RecoverVelocity);
			yield return null;
		}
	}

	/// <summary>
	/// per means map's position,0=completely show,1=completely hide
	/// </summary>
	/// <param name="per">Per.</param>
	void setMapAndEdgePos(float per){
		Vector2 minPos = MapMinPosition.position;
		Vector2 maxPos = MapMaxPosition.position;
		Vector2 curPos = (maxPos - minPos) * per + minPos;
		MinMapEdge.position = curPos;
		MinMap.rect = new Rect (0.7f,0.7f+per*0.3f,0.3f,0.3f);
	}

	void setMapAndEdgePosBy(float dper){
		curPer += dper;
		setMapAndEdgePos (curPer);
	}
}
