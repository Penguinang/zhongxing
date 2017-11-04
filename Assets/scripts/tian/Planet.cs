using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Planet : MonoBehaviour {

    //标明星球是否是自己的星球（为-1时标明无主星球，为0为敌对星球，为1为自己的星球）
    public int status;
    public Slider sli;
    private float currentHealth;
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
	/// <summary>
	/// the id(index) of this planet in planetmanager's array
	/// </summary>
    public int id;
    public Vector3 PlanetPosition;
    public bool isDead;
 
	public static string[] PlanetSprite = new string[5]{"Planet1","Planet2","Planet3","Planet5","Planet6"};

    //sun
    public static Transform FirePosition;
    public static bool isNavigationCanDispear = true;//作为判断是否UI可以被删除
    public float planeHp = 100;
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

    public Image HealthBar;

	public ProtectManager pm;

    // Use this for initialization
    void Start () {
        //currentHealth = sli.maxValue;
		HealthBar = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
    }


    private void Awake()
    {

		pm = GameObject.Find("ProtectManager").GetComponent<ProtectManager>();
        
    }

    // Update is called once per frame


    private void OnMouseDown()
    {
        ShipOnMousedown();

		this.PlanetPosition = this.transform.position;
        


    }


    void Update () {
        ButtonUpdate();
        HealthBar.fillAmount = planeHp / 100;

        if(this.planeHp>0)
        {
            this.isDead = false;
        }
        else
        {
            this.isDead = true;
            Destroy(this.gameObject);
        }


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
		if (!ButtonProtect.ProtectInput) {
			if (this.status == 1) { 
				bu = GameObject.Find ("button");
				if (bu == null) {
					buttons.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
					buttons.GetComponent<Button> ().ButtonId = this.id;
					Instantiate (buttons, transform.position, transform.rotation).transform.parent = transform;
				}
			}
		} else {
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
			pm.ThisId = this.id;
		}
    }

    void ShipInFixedUpdate()
    {
        GameObject[] sh = GameObject.FindGameObjectsWithTag("ship");
        if (sh != null)
            foreach (GameObject s in sh)
            {

                if(s.GetComponent<Ship>().id!=this.id)
                {
                Gforce = new Vector2(this.transform.position.x - s.transform.position.x,
                this.transform.position.y - s.transform.position.y);
                float R = (this.transform.position - s.transform.position).magnitude;
                Gforce = Gforce * mass / (R * R);
				if (R < 0.3)
					Gforce = new Vector2 ();
                if (!Input.GetMouseButton(0))
                {
                    s.GetComponent<Ship>().Rship.AddForce(Gforce);
                }
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




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "stone")
        {
            //sli.value = currentHealth;
            Destroy(collision.gameObject);
            if(this.status!=-1)
            {
                this.planeHp -= GameController.damage;
            }
           
        }
    }

	//-------------------------------------------------YangPengBo--------------------------------------------------------------
	public GameObject shipPrefab;
	public GameObject shellPrefab;
	public void LaunchShip(Vector2 velocity){
		GameObject ship = Instantiate (shipPrefab,transform.position,new Quaternion());
		ship.GetComponent<Rigidbody2D> ().velocity = velocity;
	}
	public void LaunchShell(Vector2 direction){
		
		GameObject shell = Instantiate (shellPrefab, transform.position+new Vector3(0,0.5f,0), new Quaternion ());
		float shellSpeed = shell.GetComponent<ShellMove> ().shellMoveSpeed;
		shell.transform.up = direction;
		shell.GetComponent <Rigidbody2D>().velocity =direction*shellSpeed;
		shell.GetComponent<ShellMove> ().planetID = this.id;
	}
}
