using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Transform FirePosition;
    public static bool isNavigationCanDispear = true;//作为判断是否UI可以被删除
    public int planeHp;
    public Transform shellPosition;
    public Transform haloPosition;
    public Transform airshipPosition;
    public Transform protectPosition;
    public GameObject ShellPre;
    public GameObject HaloPre;
    public GameObject ProtectPre;
    public GameObject AirshipPre;
    public bool isInput = false;//作为判断是否产生了UI
    public RaycastHit2D hit;
    private GameObject shell, halo, protect, airship;
    public int id;
    
   


    // Use this for initialization
    void Start () {
        planeHp = 100;
        FirePosition = transform.Find("FirePosition");
	}
    private void OnMouseDown()
    {
        if (isInput == false)
        {
            shell = Instantiate(ShellPre, shellPosition.position, shellPosition.rotation);//发射按钮，上面挂载发射子弹相关的脚本
            halo = Instantiate(HaloPre, haloPosition.position, haloPosition.rotation);//光环效果
            airship = Instantiate(AirshipPre, airshipPosition.position, airshipPosition.rotation);//飞船按钮，上面挂载发射飞船相关脚本
            protect = Instantiate(ProtectPre, protectPosition.position, protectPosition.rotation);//保护按钮，上面挂载防护层相关按钮
            isInput = true;
            
        }
    }
    
    // Update is called once per frame
    void Update () {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);//射线检测
        //删除UI的逻辑
            if (hit.collider == null && isInput ==true)
            {
                if (Input.GetMouseButtonDown(0)&&isNavigationCanDispear)
                {
                Destroy(shell);
                Destroy(halo);
                Destroy(protect);
                Destroy(airship);
                isInput = false;               
                }
           
        }
            //如果子弹发射，立即删除UI
        if (ShellMove.isShellCanMove == true)
        {
            DestroyUI();
            ShellMove.isShellCanMove = false;
        }
        //如果飞船发射，立即删除UI，飞船方法中用isAirShipCanMove作为判断飞船是否发射了
        //如果产生防护层，立即删除UI，防护层方法中isProtect作为判断是否产生了防护层
        
       
	}
    public void DestroyUI()
    {
        Destroy(shell);
        Destroy(halo);
        Destroy(protect);
        Destroy(airship);
        isInput = false;
    }
    //受到伤害子弹与陨石
    public  void TakeDamage()
    {
        if (planeHp <= 0) return;
        planeHp -= 10;
        if (planeHp <= 10)
        {
            Destroy(this.gameObject);
        }
    }

}
