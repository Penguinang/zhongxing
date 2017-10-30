using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System;

public class IDManager : NetworkBehaviour
{
	public static IDManager instance;
	void OnEnable(){
		if (!instance)
			instance = this;
	}

	[SyncVar(hook = "none")]
	public int player0;
	[SyncVar(hook = "none")]
	public int player1;
	[SyncVar(hook = "none")]
	public int player2;
	[SyncVar(hook = "TransDict")]
	public int player3;

	Dictionary<int,int> ids = new Dictionary<int,int>();					// Dictionary<planet,player>  
	public void none(int id){}
 	public void TransDict(int id){
		ids.Add (	0,player0);
		ids.Add (	1,player1);
		ids.Add (	2,player2);
		ids.Add (	3,player3);
		Debug.Log ("success ditribute planet");
	}

	public void UpdateIDs(Dictionary<int,int> IDs){
		ids = IDs;
	}

	public int GetPlayerIDForPlanet(int planetID){
		int playerID = -1;
		bool exist = ids.TryGetValue (planetID,out playerID);

		if (exist)
			return playerID;
		else
			return -1;
	}

	public int[] GetPlanetIDForPlayer(int playerID){
		List<int> planetIDs = new List<int> ();
		foreach (KeyValuePair<int,int> pair in ids) {
			if (pair.Value == playerID)
				planetIDs.Add (pair.Key);
		}
		return planetIDs.ToArray ();
	}
}

