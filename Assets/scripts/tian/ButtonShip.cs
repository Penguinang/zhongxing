using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShip : MonoBehaviour {
    private bool flag;


    public GameObject ship;
    public float ship_energy=20;
    public EnergyManager EnergyManager;

    void Start()
    {
        flag = false;
        EnergyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
 
    }

    private void OnMouseDown()
    {
        flag = true;
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        Vector3 shipPos = new Vector3(xPos - 1.4f, transform.position.y + 1.3f, 0);
        
        Destroy(this.transform.parent.gameObject);
        Destroy(this.transform.parent.parent.gameObject);


        if(EnergyManager.launchable)
        {

            Instantiate(ship, shipPos, transform.rotation);
            //ShipManager.ships.Add(ship.GetComponent<Ship>());
            EnergyManager.GetComponent<EnergyManager>().energy -= 20;
        }
        
    }


}
