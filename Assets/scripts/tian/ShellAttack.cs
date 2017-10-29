using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//发射导弹，脚本挂在发射按钮上
public class ShellAttack :MonoBehaviour{
    public static Transform FirePosition;
    public static bool isNavigationCanDispear = true;
    public GameObject Nbomb;
    private GameObject Nbombpre;
    public static bool isNbomb=false;
    public ShellMove shellmove;
    public RaycastHit2D hit;

    public static Rigidbody2D shellRigidbody2D;

    // Use this for initialization
    void Start () {
        FirePosition = this.transform.parent.Find("FirePosition");
      
       
    }
    
    //实例化核弹
    private void OnMouseDown()
    {
        if (isNbomb == false)
        {
            Nbombpre = Instantiate(Nbomb, transform.position+new Vector3(0,4,0), transform.rotation);
            isNbomb = true;
            Planes.isNavigationCanDispear = false;
            //Destroy(this.transform.parent.gameObject);
            //Destroy(this.transform.parent.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        //没有点击敌方星球而是点击了空白背景，删除实例化出来的核弹
		 if(hit.collider==null &&isNbomb !=false)
        {
            if (Input.GetMouseButtonDown(0)&&isNavigationCanDispear)
            {
                Destroy(Nbombpre);
                isNbomb = false;

                Destroy(this.transform.parent.gameObject);
                Destroy(this.transform.parent.parent.gameObject);
                //Planes.isNavigationCanDispear = true;
            }
        }
        //如果点击了其他星球 Debug.Log("done");，则发射核弹      
        if(hit.collider !=null && isNbomb != false)
        {
            if(hit.collider.tag=="Planet"
                && Input.GetMouseButtonDown(0))
            {
                //DEBUG
                // LobbyManager.localPlayer.gameObject = hideFlags = GetComponent<PlayerInput>().OnShellClick(int ID, Vector3 velocity);

                ShellLaunch(Input.mousePosition);//


                isNbomb = false;
                Destroy(this.transform.parent.gameObject);
                Destroy(this.transform.parent.parent.gameObject);
                //isNavigationCanDispear = true;
                //Planes.isNavigationCanDispear = true;
            }

            

        }



       

    }

    public  void ShellLaunch(Vector3 mousePos)
    {
        Nbombpre.GetComponent<ShellMove>().ShellCanMove(mousePos);
      //  ShellMove.isShellCanMove = true;
    }
    

}
