using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float waveWait = 10.0f;
    public int stoneCount;
    public float spawnWait;
    public Rigidbody2D stone;
    public static float damage=10;
    public float maxSpeed;
    public float minPosX;
    public float maxPosX;
    public float minPosY;
    public float maxPosY;



    // Use this for initialization
    void Start()
    {
        Random.InitState(System.DateTime.Today.Millisecond);
        StartCoroutine("Stone");
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    IEnumerator Stone()
    {
        while (true)
        {
            for (int i = 0; i < stoneCount; i++)
            {
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
                float endX = Random.Range(minPosX / 3, maxPosX / 3);
                float endY = Random.Range(minPosY / 3, maxPosY / 3);
                Vector3 endPos = new Vector3(endX, endY, transform.position.z);
                Vector3 d = endPos - stonePos;
                stoneInstance.velocity = new Vector2(d.normalized.x * maxSpeed, d.normalized.y * maxSpeed);
                yield return new WaitForSeconds(spawnWait);
            }
           // StartCoroutine(Stone());
            yield return new WaitForSeconds(waveWait);
        }


    }
}
