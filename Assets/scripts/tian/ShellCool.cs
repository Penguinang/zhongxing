using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellCool : MonoBehaviour {

    public CoolTime CT;

    public Image ShellDark;
    private void Awake()
    {
        CT = GameObject.Find("CoolTimeManager").GetComponent<CoolTime>();
    }
    private void Update()
    {
        ShellDark.fillAmount = CT.ShellTime;
    }
}
