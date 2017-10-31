using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using Player;

public class ProtectManager : MonoBehaviour {

    public int ThisId;
    public int LastId;
    public int FirstId;
    public int status;
    public List<int> Ids;

    public int[] planets;
    public List<int> Planets;



    private void Awake()
    {
        status = 0;
        ThisId = -1;
        LastId = -2;
        FirstId = -3;
        Planets = new List<int>();
    }

    //public int SingleProtect()
    //{
    //    if((this.ThisId==this.LastId))
    //    {
            
    //        return FirstId;
    //        status = 0;
    //        ThisId = -1;
    //        LastId = -2;
    //    }
    //    else
    //    {
    //        return -1;
    //    }
    //}


    //public List<int> MultiProtect()
    //{
    //    if((FirstId==ThisId)&&(ThisId!=LastId))
    //    {
    //        return Ids;
    //    }
    //    else
    //    {
    //        return (new List<int>());
    //    }
    //}


    public void Update()
    {
        //DEBUG
        if((FirstId==-3)&&(FirstId!=ThisId)&&(ThisId!=-1))
        {
            FirstId = ThisId;
        }
        

        Check();

        if ((FirstId != -3) && (FirstId == ThisId) && (LastId != -1))
        {



            status = 0;
            ThisId = -1;
            LastId = -2;
            FirstId = -3;
            Ids = new List<int>();
        }

        

        //若按下按钮，check返回需要保护的
        
    }
    public List<int> Check()
    {
        List<int> single = new List<int>();
        if ((ThisId == LastId) && (ThisId == FirstId))
        {
			LobbyManager.localPlayer.GetComponent<PlayerInput>().OnProtectionClick(Planets.ToArray ());
			Debug.Log ("done");
            single.Add(ThisId);
            planets = new int[1];
            planets[0] = single[0];
            Planets = single;
            //DEBUG
			//LobbyManager.localPlayer.GetComponent<PlayerInput>().OnProtectionClick(new int[2]{0,1});
            return (single);
        }
        else if ((ThisId == FirstId) && (ThisId != LastId)&& (FirstId != -3) && (LastId != -1))
        {
			LobbyManager.localPlayer.GetComponent<PlayerInput>().OnProtectionClick(Planets.ToArray ());
			Debug.Log ("done");
            //Ids.Add(ThisId);
            Ids.Add(LastId);
            //Debug.Log("done");
            Planets = Ids;
            planets = new int[Planets.Count];
            for(int i=0;i<Planets.Count;i++)
            {
                planets[i] = Planets[i];
            }
            status = 0;
            ThisId = -1;
            LastId = -2;
            FirstId = -3;
            return (Ids);
            Ids = new List<int>();
            //DEBUG
			//LobbyManager.localPlayer.GetComponent<PlayerInput>().OnProtectionClick(new int[2]{0,1});

        }
        else return (new List<int>());
    }
}
