using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProtectionSimplify : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler{

	public void OnBeginDrag(PointerEventData eventData){
		Debug.Log ("begin drag");
	}

	public void OnDrag(PointerEventData eventData){
		Debug.Log ("on drag continue");
	}

	public void OnEndDrag(PointerEventData eventData){
		Debug.Log ("On Drag End");
	}
}
