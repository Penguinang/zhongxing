using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonProtect : MonoBehaviour {

    public List<Planet> ProtectPlanets;
    public Planet ProtectedPlanet;
    public Planet Planet1;
    public Planet Planet2;
    public Planet Planet3;
    public Planet Planet4;
    public Planet Planet5;
    public Planet Planet6;
    public GameObject[] AllPlanets;
    public ProtectManager pm;

    private void Awake()
    {
        pm = GameObject.Find("ProtectManager").GetComponent<ProtectManager>();
    }
    private void OnMouseDown()
    {
        AllPlanets = GameObject.FindGameObjectsWithTag("Planet");

        if(pm.LastId>0)
        {
            int j = 0;
            foreach(int i in pm.Ids)
            {
                if(i!=pm.LastId)
                {
                    j = j+1;
                }
            }
            if((j!=0)||(pm.Ids.Count==0))
            {
                pm.Ids.Add(pm.LastId);
            }
            
        }
            pm.LastId = pm.ThisId;
		pm.ThisId = this.transform.parent.parent.gameObject.GetComponent<Button>().ButtonId;
		Destroy(this.transform.parent.gameObject);
		Destroy(this.transform.parent.parent.gameObject);
        
    }

    
}
