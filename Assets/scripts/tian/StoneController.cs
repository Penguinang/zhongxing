using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StoneController : NetworkBehaviour {
    public Rigidbody2D stone;
    public float damage;
    public float maxSpeed;
    public float minTimeBetweenStone;
    public float maxTimeBetweenStone;
    public float minPosX;
    public float maxPosX;
    public float minPosY;
    public float maxPosY;


    // Use this for initialization
    void Start () {
        Random.InitState(System.DateTime.Today.Millisecond);
		if(isServer)
        StartCoroutine("Stone");
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    
   
    IEnumerator Stone()
    {
        float waitTime = Random.Range(minTimeBetweenStone, maxTimeBetweenStone);
        yield return new WaitForSeconds(waitTime);
        float posX, posY;
        bool direction = Random.Range(0, 2) == 0;
        bool x = Random.Range(0, 2) == 0;
        bool dir = Random.Range(0, 2) == 0;
        if (x)
        {
            if (dir)
                posX = maxPosX;
            else posX = minPosX;
            posY = Random.Range(minPosY, maxPosY);
            
        }
        else
        {
            if (dir)
                posY = maxPosY;
            else posY = minPosY;
            posX = Random.Range(minPosX, maxPosX);
           
        }
        Vector3 stonePos = new Vector3(posX, posY, transform.position.z);
        Rigidbody2D stoneInstance = Instantiate(stone, stonePos, Quaternion.identity) as Rigidbody2D;
        //float speedX, speedY;
        /*if ((posX >= (minPosX + maxPosX) / 2) && (posY >= (minPosY + maxPosY) / 2))
        {
            speedX = Random.Range(minSpeed, -1);
            speedY = Random.Range(minSpeed, -1);
        }else if((posX < (minPosX + maxPosX) / 2) && (posY >= (minPosY + maxPosY) / 2))
        {
            speedX = Random.Range(1,maxSpeed);
            speedY = Random.Range(minSpeed,-1);
        }else if ((posX < (minPosX + maxPosX) / 2) && (posY < (minPosY + maxPosY) / 2))
        {
            speedX = Random.Range(1,maxSpeed);
            speedY = Random.Range(1, maxSpeed);
        }else
            speedX = Random.Range(minSpeed,-1);
            speedY = Random.Range(1, maxSpeed);*/
        float endX = Random.Range(minPosX / 3, maxPosX / 3);
        float endY = Random.Range(minPosY / 3, maxPosY / 3);
        Vector3 endPos = new Vector3(endX, endY, transform.position.z);
        Vector3 d = endPos - stonePos;
		stoneInstance.velocity = new Vector2(d.normalized.x*maxSpeed, d.normalized.y*maxSpeed);
		NetworkServer.Spawn (stoneInstance.gameObject);
        StartCoroutine(Stone());
        while (stoneInstance != null)
        {
            if (stoneInstance.transform.position.x > maxPosX || stoneInstance.transform.position.x < minPosX)
                Destroy(stoneInstance.gameObject);
            if (stoneInstance.transform.position.y > maxPosY || stoneInstance.transform.position.y < minPosY)
                Destroy(stoneInstance.gameObject);
            yield return null;
        }
    }
}
