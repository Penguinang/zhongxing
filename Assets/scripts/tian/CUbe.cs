using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUbe : MonoBehaviour {
    public float speed;
    public Rigidbody2D Rship;

    private Vector3 e;
    private Vector3 up;
    private Vector3 force;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
             e = Input.mousePosition;
        if(Input.GetMouseButtonUp(0))
        {
            up = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        Vector3 mousepos= Camera.main.ScreenToWorldPoint(e);
        force = up - mousepos;
        Rship.AddForce(force);


        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float x = mousepos.x-transform.position.x;
        float y = mousepos.y-transform.position.y;
        float s = Mathf.Sqrt(x * x + y * y);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        //if (Input.GetMouseButton(0))
        //{

        //    //world = Camera.main.ScreenToWorldPoint(e);
        //    speed = 1;
        //}
        speed = 1;
        if (transform.position == e)
        {
            speed = 0;
        }

        Debug.Log(mousepos.x);
        
        //Rship.MovePosition(new Vector2(transform.position.x+x / s, transform.position.y+ y / s));
        //transform.LookAt(e);
        //gameObject.transform.Translate(new Vector3(x/s,y/s,0) * speed * Time.deltaTime);
        // gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        // this.transform.Translate(Vector3.right * Time.deltaTime*speed);
    }
}
