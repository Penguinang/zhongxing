using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipCool : MonoBehaviour {

    public CoolTime CT;

    public Image ShipDark;
    private void Awake()
    {
        CT = GameObject.Find("CoolTimeManager").GetComponent<CoolTime>();
    }
    private void Update()
    {
        ShipDark.fillAmount = CT.ShipTime;
    }
}
