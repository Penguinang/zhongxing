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

    

    // Use this for initialization
    void Awake()
    {
		this.AwakePos=this.transform.position;
        Rship.mass = this.mass;
    }

    private void Start()
    {
        //ShipManager.ships.Add(this);
        //Debug.Log(ShipManager.ships[0]);
    }

    // Update is called once per frame
    void Update()
    {

		if ((this.transform.position - AwakePos).magnitude < 0.1)
            if (Input.GetMouseButton(0))
        {
            //获取鼠标的坐标，鼠标是屏幕坐标，Z轴为0，这里不做转换
            Vector3 mouse = Input.mousePosition;
            //获取物体坐标，物体坐标是世界坐标，将其转换成屏幕坐标，和鼠标一直
			Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);
            //屏幕坐标向量相减，得到指向鼠标点的目标向量，即黄色线段
            Vector3 direction = mouse - obj;
            //将Z轴置0,保持在2D平面内
            direction.z = 0f;
            //将目标向量长度变成1，即单位向量，这里的目的是只使用向量的方向，不需要长度，所以变成1
            direction = direction.normalized;
         
			transform.right= direction;
           
        }


        if (Rship.position.x > WidthToDestroy/2 || Rship.position.x < -WidthToDestroy/2 || 
            Rship.position.y > HeigtToDestroy/2 || Rship.position.y< -HeigtToDestroy/2)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void FixedUpdate()
    {
		if  ((this.transform.position - AwakePos).magnitude < 0.1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                SecondPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//----------------------------------------------------YangPengBo-----------------------------------------------------------------
//                Rship.velocity = (new Vector3(SecondPos.x,SecondPos.y,0) - AwakePos) * speed;
				Vector3 velocity = (SecondPos-(Vector2)AwakePos)*speed;
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
			int localPlayerID = LobbyManager.localPlayer.GetComponent<PlayerMessage>().ID;
			int planetID = col.GetComponent<Planet> ().id;
			int oldPlayer = IDManager.instance.GetPlayerIDForPlanet (planetID);
			int newPlayer = IDManager.instance.GetPlayerIDForPlanet (this.id);
			IDManager.instance.ChangePlanetOwner (planetID,oldPlayer,newPlayer);

			if (newPlayer== localPlayerID) //this.id is attacking planets'id 
				col.GetComponent<Planet> ().status = 1;
			else
				col.GetComponent<Planet> ().status = 0;
			col.gameObject.GetComponent<PlanetIcon> ().SetIconById (IDManager.instance.GetPlayerIDForPlanet (id));
		}
		if ((col.tag == "Planet") && ((this.transform.position - AwakePos).magnitude < 3)) {

			GameObject p = col.gameObject;
//            this.id=p.transform.GetComponent<Planet>().status;
			//----------------------------------------YangPengBo---------------------------------------------------------------
			this.id = p.GetComponent<Planet> ().id;
		}
        

	}
 
    
}
