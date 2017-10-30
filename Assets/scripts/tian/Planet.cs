using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    //标明星球是否是自己的星球（为-1时标明无主星球，为0为敌对星球，为1为自己的星球）
    public int status  = -1;
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
    public int id;
    public Vector3 PlanetPosition;
 


    //sun
    public static Transform FirePosition;
    public static bool isNavigationCanDispear = true;//作为判断是否UI可以被删除
    public int planeHp = 100;
    //public Transform shellPosition;
    //public Transform haloPosition;
    public Transform airshipPosition;
    public Transform protectPosition;
    //public GameObject ShellPre;
    //public GameObject HaloPre;
    public GameObject ProtectPre;
    public GameObject AirshipPre;
    public bool isInput = false;//作为判断是否产生了UI
    public RaycastHit2D hit;
    private GameObject shell, protect;
    //public bool isPlayer, isEmeny, isNone;//作为是否是玩家，敌人，还是无主星球的判定



    // Use this for initialization
    void Start () {
        
        FirePosition = transform.Find("FirePosition");
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

        this.PlanetPosition = this.transform.position;
        


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
        status = id;
    }


    void ShipOnMousedown()
    {
        if (this.status == 1)
        {
            //Debug.Log("done");
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


    public void TakeDamage()
    {
        if (planeHp <= 0) return;
        planeHp -= 10;
        if (planeHp <= 10)
        {
            Destroy(this.gameObject);
        }
    }
}
