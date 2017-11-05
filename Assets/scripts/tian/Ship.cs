using UnityEngine;
using System.Collections;
using Prototype.NetworkLobby;
using Player;

public class Ship : MonoBehaviour
{
    public static Planet[] planets;
    private SpriteRenderer ren;
    public Vector2 FirstPos;
    public Vector2 SecondPos;
    public float WidthToDestroy;
    public float HeigtToDestroy;
    public Vector3 AwakePos;
    public float speed;
    public Rigidbody2D Rship;
    public int id;
    public Planet ParentPlanet;
    public float mass;
	public int planetID = -1;



    public Planet[] Ps;
    public Planet TargetPlanet;
    public float CatchRange;
    public Vector3 ToTarget;
    public float MaxV;
    public GameObject[] Gs;
    public float[] ranges;
    public int CatchStatus;
    public float CatchRangeMin;

    public Planet AwakePlanet;
    public Vector2 V;
    public float CatchTime;
    public int CatchDirection;

    public float debug;

    // Use this for initialization
    void Awake()
    {
        this.AwakePos=this.transform.position;
        Rship.mass = this.mass;

        Gs = GameObject.FindGameObjectsWithTag("Planet");
        Ps = new Planet[Gs.Length];
        for (int i = 0; i < Gs.Length; i++)
        {
            Ps[i] = Gs[i].GetComponent<Planet>();
        }
        ranges = new float[Ps.Length];
    }

    private void Start()
    {
        //ShipManager.ships.Add(this);
        //Debug.Log(ShipManager.ships[0]);
        CatchStatus = 0;
    }

    // Update is called once per frame
    void Update()
    {      
		V = Rship.velocity;

        if ((this.transform.position - AwakePos).magnitude < 1)
            if (Input.GetMouseButton(0))
        {
            //获取鼠标的坐标，鼠标是屏幕坐标，Z轴为0，这里不做转换
            Vector3 mouse = Input.mousePosition;
            //获取物体坐标，物体坐标是世界坐标，将其转换成屏幕坐标，和鼠标一直
            Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);

                Vector3 mouPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 back =  mouPos-transform.position;
                //Debug.Log((this.transform.position - AwakePos).magnitude);
                if(back.magnitude>=7.5f)
                {
                    back = 7.5f*back/back.magnitude;
                }
                transform.position = (AwakePos+back/10f);
                //transform.position -= Back;
            //屏幕坐标向量相减，得到指向鼠标点的目标向量，即黄色线段
            Vector3 direction = mouse - obj;
            //将Z轴置0,保持在2D平面内
            direction.z = 0f;
            //将目标向量长度变成1，即单位向量，这里的目的是只使用向量的方向，不需要长度，所以变成1
            direction = direction.normalized;
         
            transform.right= -direction;
           
        }


        if (Rship.position.x > WidthToDestroy/2 || Rship.position.x < -WidthToDestroy/2 || 
            Rship.position.y > HeigtToDestroy/2 || Rship.position.y< -HeigtToDestroy/2)
        {
            Destroy(this.gameObject);
        }




        int i = 0;
        Vector3 ToParentPlanet = transform.position - AwakePos;
        if(TargetPlanet==null)
        {
            foreach (Planet p in Ps)
            {
                ranges[i] = (p.transform.position - transform.position).magnitude;
                i = i + 1;

                if (((p.transform.position - this.transform.position).magnitude < CatchRange) &&
                    ((transform.position - AwakePos).magnitude > 2) && (p != AwakePlanet))
                {
                    TargetPlanet = p;
                    ToTarget = p.transform.position - transform.position;
                    //Debug.Log(ToTarget.x * ToParentPlanet.x + ToTarget.y * ToParentPlanet.y);
                    debug = ToTarget.x * V.x + ToTarget.y * V.y;
					if (/*ToTarget.x * V.x+ToTarget.y* V.y >= 0*/Vector3.Cross (V,ToTarget).z<0)
                    {
                        CatchDirection = -1;// clockwise
                    }
                    else
                    {
                        CatchDirection = 1;// anticlockwise
                    }
                }
            }
        }
        

        if (TargetPlanet != null)
        {
            ToTarget = TargetPlanet.transform.position - transform.position;

            
            //Debug.Log(Rship.velocity.x * ToTarget.x + Rship.velocity.y * ToTarget.y);
            if ((Rship.velocity.x * ToTarget.x + Rship.velocity.y * ToTarget.y <= 0.1) && (Rship.velocity.magnitude < MaxV)&&(ToTarget.magnitude>=CatchRangeMin))
            {
                //Rship.AddForce(((Rship.velocity.x * Rship.velocity.x) + (Rship.velocity.y * Rship.velocity.y) * Rship.mass) * 8 * ToTarget / ToTarget.magnitude / ToTarget.magnitude);
                //transform.RotateAround(TargetPlanet.transform.position, transform.up, 30 * Time.deltaTime);
                CatchStatus = 1;
            }

            foreach (Planet p in Ps)
            {
                Rship.AddForce(-p.Gforce);
            }
        }

    }
    private void FixedUpdate()
    {        
        if (TargetPlanet != null)
        {
            CatchTime -= 1f;
            if(CatchTime>0)
            {
                transform.RotateAround(TargetPlanet.transform.position, Vector3.forward, 60 * Time.deltaTime*CatchDirection);
                //transform.Translate(ToTarget * 0.01f);
                Rship.velocity = new Vector2(0, 0);
            }
            else
            {
                transform.Translate(ToTarget * 0.06f);
            }
            
        }


        if  ((this.transform.position - AwakePos).magnitude < 0.8)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                SecondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//----------------------------------------------------YangPengBo-----------------------------------------------------------------
                //Rship.velocity = (new Vector3(SecondPos.x,SecondPos.y,0) - AwakePos) * speed;
				Vector3 velocity = -(SecondPos-(Vector2)AwakePos)*speed;

				if(planetID!=-1)
					LobbyManager.localPlayer.GetComponent<PlayerInput>().OnShipClick (planetID,velocity);
				Destroy (gameObject);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D col)
	{
		// If it hits an enemy...

		if ((col.tag == "Planet") && ((this.transform.position - AwakePos).magnitude > 1.8)) {
			//col.gameObject.GetComponent<Planet>().GetCaughtBy(this.id);
            
			Destroy (this.gameObject);

            //			col.GetComponent<Planet> ().status = this.id;
            //			col.gameObject.transform.GetComponent<Planet> ().status = this.id;
            //----------------------------------------YangPengBo---------------------------------------------------------------
            if((CatchStatus==1)&&(col.gameObject.GetComponent<Planet>()==TargetPlanet))
            {
                int localPlayerID = LobbyManager.localPlayer.GetComponent<PlayerMessage>().ID;
                int planetID = col.GetComponent<Planet>().id;
                int oldPlayer = IDManager.instance.GetPlayerIDForPlanet(planetID);
                int newPlayer = IDManager.instance.GetPlayerIDForPlanet(this.id);
                IDManager.instance.ChangePlanetOwner(planetID, oldPlayer, newPlayer);

                if (newPlayer == localPlayerID){//this.id is attacking planets'id 
                    col.GetComponent<Planet>().status = 1;
                	}
                else
                    col.GetComponent<Planet>().status = 0;
				Sprite sprite = Resources.Load<Sprite> (Planet.PlanetSprite[IDManager.instance.GetPlayerIDForPlanet(this.id)]);
				Debug.Log ("planet : "+col.GetComponent<Planet>().id+" is changing sprite to "+Planet.PlanetSprite[IDManager.instance.GetPlayerIDForPlanet(this.id)]);
				Debug.Log ("ship belongs to player : "+IDManager.instance.GetPlayerIDForPlanet(this.id));
				col.gameObject.GetComponent<SpriteRenderer> ().sprite = sprite;

				col.GetComponent<PlanetIcon> ().SetIconById (IDManager.instance.GetPlayerIDForPlanet(this.id));
            }
            CatchStatus = 0;                  
		}
		if ((col.tag == "Planet") && ((this.transform.position - AwakePos).magnitude < 3)) {

			GameObject p = col.gameObject;
//            this.id=p.transform.GetComponent<Planet>().status;
			//----------------------------------------YangPengBo---------------------------------------------------------------
			this.id = p.GetComponent<Planet> ().id;
            AwakePlanet = p.GetComponent<Planet>();

        }

	}
 
    
}
