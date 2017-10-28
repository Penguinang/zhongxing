using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public int ControlerId;
    private Vector3 PlanetPos;
    private float R;
    private int health;
    private SpriteRenderer ren;         
    private Transform frontCheck;      
    private bool dead = false;
    private float grow = 0;




    public Vector2 Gforce;
    public GameObject ship;
    public GameObject buttons;
    public float mass;
    public Rigidbody2D Rship;
    public GameObject bu;

   

    // Use this for initialization
    void Start () {
        this.health = 100;
        this.R = Random.Range(5, 8);
        ChangeToControlable();
        this.PlanetPos = transform.position;
	}


    private void Awake()
    {
        //Instantiate(this, transform.position, transform.rotation);
        //Instantiate(ship, transform.position, transform.rotation);

        
    }

    // Update is called once per frame


    private void OnMouseDown()
    {
        if(this.ControlerId==1)
        {
            bu = GameObject.Find("button");
            if(bu==null)
            {
                buttons.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                Instantiate(buttons, transform.position, transform.rotation);
            }
            
            
            //buttons.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            //Instantiate(ship, transform.position, transform.rotation);
            //ship.GetComponent<Ship>().id = this.ControlerId;

        }
    }


    void Update () {
        bu= GameObject.Find("button");
    }


    private void FixedUpdate()
    {
        
        


        GameObject[] sh=GameObject.FindGameObjectsWithTag("ship");

        if(sh!=null)
        foreach(GameObject s in sh)
        {
                Gforce = new Vector2(this.transform.position.x - s.transform.position.x,
                this.transform.position.y - s.transform.position.y);
                float R = (this.transform.position - s.transform.position).magnitude;
               
                Gforce = Gforce * mass / (R * R);
                if(!Input.GetMouseButton(0))
                {
                    s.GetComponent<Ship>().Rship.AddForce(Gforce);
                }
                
        }





        if (ShipManager.ships!=null)
        {
            foreach (Ship s in ShipManager.ships)
            {
                s.Rship.AddForce(Gforce);
            }

            if (ship.gameObject.GetComponent<Ship>().id == this.ControlerId)
            {
                ship.gameObject.GetComponent<Ship>().Rship.AddForce(Gforce);
            }
        }
        



        if (bu != null)
        {
            if (bu.transform.localScale.x<3.34f)
            {
                bu.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                bu.transform.Translate(new Vector3(0.024f,-0.024f,0));
            }
        }





    }
    void GetHarm(int harm)
    {
        this.health = this.health - harm;
    }
    public void GetCaughtBy(int id)
    {
        ControlerId = id;
    }
    void ChangeToControlable()
    {
        if (this.ControlerId != -1)
        {
            
        }
    }
    public Vector3 GetPlanetPos()
    {
        return this.PlanetPos;
    }
    public float GetR()
    {
        return this.R;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="ship")
        {
            
        }
    }

}
