using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//无主星球的脚本，挂在无主星球上
public class Plane : MonoBehaviour
{
    public int planeHp = 100;
    public int id;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    

    //变成玩家星球,飞船的功能
    public void ChangeToPlayer()
    {
    }
    //碰撞检测，子弹与陨石的伤害
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
