using UnityEngine;
using System.Collections;

public class PlanetControl : MonoBehaviour
{
    private Vector2 FirstPos;
    private Vector2 SecondPos;
    private int status;
    private UnityEngine.UI.Button bomb_button;
    private Ship ship;


    // Use this for initialization
    void Start()
    {
        this.status = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            FirstPos = Input.GetTouch(0).position;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            SecondPos = Input.GetTouch(0).position;
        }
        Vector3 vector3 = SecondPos - FirstPos;
        
    }
}
