using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//子弹移动，脚本挂在子弹上
public class ShellMove : MonoBehaviour {
    
    public static bool isShellCanMove =false;
    public  Rigidbody2D shellRigidbody2D;
    public  float shellMoveSpeed ;
    public static RaycastHit2D hitt;
    private static float eulerAngles;
    public  float firetime = 1.0f;
    public float speed;

    // Use this for initialization
    void Start() {
      
        shellRigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
     void Update () {
        speed = shellRigidbody2D.velocity.magnitude;

    }
    public  void  ShellCanMove(Vector3 MousePos)//发射核弹
    {
        hitt = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(MousePos), Vector2.zero);
        Vector2 p = transform.InverseTransformPoint(hitt.point);
        eulerAngles = Mathf.Atan2(p.y,p.x) * Mathf.Rad2Deg -90;//计算欧拉角
        Debug.Log("done");
        // Debug.Log(hitt.point.y);
        // Debug.Log(hitt.transform.position.x);
        transform.Rotate(0, 0, eulerAngles);//欧拉角旋转
        shellRigidbody2D.velocity = transform.up * shellMoveSpeed;
        isShellCanMove = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(this.gameObject);
        

        if (collision.tag == "Planet"&&(this.gameObject.transform.position-collision.transform.position).magnitude>2&&collision.GetComponent<Planet>().status!=1)
        {
            collision.SendMessage("TakeDamage");
            Destroy(this.gameObject);
        }
    }
    

}
