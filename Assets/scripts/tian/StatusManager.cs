using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour {

    public int status;
    private void Awake()
    {
        this.status = 0;
    }
}
