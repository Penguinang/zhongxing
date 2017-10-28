using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//敌人脚本，挂在敌人身上
public class Emeny : MonoBehaviour {
    public int planeHp =100;
    public int id;

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //受到伤害，子弹与陨石
    public  void TakeDamage()
    {
        //if (planeHp <= 0) return;
        planeHp -= 10;
        if (planeHp <= 10)
        {
            Destroy(this.gameObject);
            
        }
    }
    
    //

}
