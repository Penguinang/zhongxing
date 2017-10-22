using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;

public class ReadyButton : NetworkBehaviour {
	bool isOn;
	public GameObject selbkg;
	public GameObject uselbkg;

	void Start(){
		isOn = false;
	}

	public void OnButtonClick(){
		isOn = !isOn;
		if (isOn) {
			selbkg.SetActive (true) ;
			uselbkg.SetActive (false) ;
<<<<<<< HEAD

		} else {
			selbkg.SetActive (false) ;
			uselbkg.SetActive (true) ;

=======
		} else {
			selbkg.SetActive (false) ;
			uselbkg.SetActive (true) ;
>>>>>>> c880078b7b856404535163c79cd5b026c6252671
		}
	}
}
