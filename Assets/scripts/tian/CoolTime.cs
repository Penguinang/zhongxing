using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour {

    public float ShellTime;
    public float ShipTime;
    public Image dark_shell;
    public Image dark_ship;
    public float ShellCoolSpeed;
    public float ShipCoolSpeed;

    private void Awake()
    {
        ShellTime = 0;
        ShipTime = 0;
    }


    private void FixedUpdate()
    {
        if(ShellTime>0)
        {
            ShellTime = ShellTime - ShellCoolSpeed;
        }
        if(ShipTime>0)
        {
            ShipTime = ShipTime - ShipCoolSpeed;
        }
        if(ShellTime<0.1f)
        {
            ShellTime = 0;
        }
        if(ShipTime<0.1f)
        {
            ShipTime = 0;
        }

    }


}
