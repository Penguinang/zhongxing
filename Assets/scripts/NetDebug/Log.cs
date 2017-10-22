using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NetDebug{
	public class Log : MonoBehaviour {
		public static int xpos = -300, ypos = -300, width = 300, height = 100;
		public Log(string message){
			GUI.Label (new Rect (xpos, ypos, width, height), message);
		}
	}

}