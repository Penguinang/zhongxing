using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using Player;

public class ButtonShip : MonoBehaviour {
    private bool flag;


    public GameObject ship;
    public float ship_energy=20;
    public EnergyManager EnergyManager;
    public CoolTime CT;

    void Start()
    {
        flag = false;
        EnergyManager = GameObject.Find("EnergyManager").GetComponent<EnergyManager>();
        CT = GameObject.FindGameObjectWithTag("CoolTimeManager").GetComponent<CoolTime>();
    }

    private void OnMouseDown()
    {
        //DEBUG
        //LobbyManager.localPlayer.gameObject = hideFlags = GetComponent<PlayerInput>().OnShipClick(int ID,Vector3 velocity);
        Launch();
        }

    public void Launch()
    {
        flag = true;
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        Vector3 shipPos = new Vector3(xPos - 1.4f, transform.position.y + 1.3f, 0);


        if (EnergyManager.launchable)
        {
            if (CT.ShipTime == 0)
            {
                GameObject instance = Instantiate(ship, shipPos, transform.rotation);
                GameObject planet = transform.parent.parent.parent.gameObject;
                instance.GetComponent<Ship>().planetID = planet.GetComponent<Planet>().id;
                //ShipManager.ships.Add(ship.GetComponent<Ship>());



                //DEBUG
                EnergyManager.DecreaseEnergy(20f);
            }
            CT.ShipTime = 1f;

		}

		Destroy(this.transform.parent.gameObject);
		Destroy(this.transform.parent.parent.gameObject);
    }
}
