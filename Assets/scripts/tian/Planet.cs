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
        ShipOnMousedown();
    }


    void Update () {
        ButtonUpdate();
    }


    private void FixedUpdate()
    {

        ShipInFixedUpdate();

        ButtonGrow();

    }

    public void GetCaughtBy(int id)
    {
        ControlerId = id;
    }


    void ShipOnMousedown()
    {
        if (this.ControlerId == 1)
        {
            bu = GameObject.Find("button");
            if (bu == null)
            {
                buttons.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                Instantiate(buttons, transform.position, transform.rotation);
            }
        }
    }
    void ShipInFixedUpdate()
    {
        GameObject[] sh = GameObject.FindGameObjectsWithTag("ship");
        if (sh != null)
            foreach (GameObject s in sh)
            {
                Gforce = new Vector2(this.transform.position.x - s.transform.position.x,
                this.transform.position.y - s.transform.position.y);
                float R = (this.transform.position - s.transform.position).magnitude;

                Gforce = Gforce * mass / (R * R);
                if (!Input.GetMouseButton(0))
                {
                    s.GetComponent<Ship>().Rship.AddForce(Gforce);
                }

            }
    }
    void ButtonGrow()
    {
        if (bu != null)
        {
            if (bu.transform.localScale.x < 3.34f)
            {
                bu.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                bu.transform.Translate(new Vector3(0.024f, -0.024f, 0));
            }
        }
    }
    void ButtonUpdate()
    {
        bu = GameObject.Find("button");
    } 
}
