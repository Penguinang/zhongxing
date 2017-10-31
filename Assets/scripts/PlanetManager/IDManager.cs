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

	/// <summary>
	/// player* is planetid for player *
	/// </summary>
	[SyncVar]
	public int player0;
	[SyncVar]
	public int player1;
	[SyncVar]
	public int player2;
	[SyncVar(hook = "TransDict")]
	public int player3;

	void Awake(){
		Debug.Log ("asd");
	}

	Dictionary<int,int> ids = new Dictionary<int,int>();					// Dictionary<planet,player>  
//	[ClientRpc]
	public void TransDict(int id){
		Debug.Log ("player0's planet : "+player0);
		Debug.Log ("player1's planet : "+player1);
		Debug.Log ("player2's planet : "+player2);
		Debug.Log ("player3's planet : "+player3);
		Debug.Log ("transdict param id is "+id);
		player3 = id;
		ids.Add (	player0,0);
		ids.Add (	player1,1);
		ids.Add (	player2,2);
		ids.Add (	player3,3);
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
		//PATCH
		if (ids.Count < 4) {
			ids.Add (	player0,0);
			ids.Add (	player1,1);
			ids.Add (	player2,2);
			ids.Add (	player3,3);
			Debug.Log ("patch update ids");
		}

		List<int> planetIDs = new List<int> ();
		foreach (KeyValuePair<int,int> pair in ids) {
			if (pair.Value == playerID)
				planetIDs.Add (pair.Key);
		}
		return planetIDs.ToArray ();
	}

	/// <summary>
	/// when a planet is caught,we need change its playerID in IDManager
	/// </summary>
	/// <param name="planet">Caught Planet.</param>
	/// <param name="oldPlayer">The Old player.</param>
	/// <param name="newPlayer">The New player.</param>
	public void ChangePlanetOwner(int planet,int oldPlayer,int newPlayer){
		ids.Remove (planet);
		ids.Add (planet,newPlayer);
	}
}

