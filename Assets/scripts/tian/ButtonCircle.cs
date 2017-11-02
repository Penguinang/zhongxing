using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCircle : MonoBehaviour {

   private void OnMouseDown()
    {
		Destroy(this.transform.parent.gameObject);
		Destroy(this.transform.parent.parent.gameObject);
    }
}
