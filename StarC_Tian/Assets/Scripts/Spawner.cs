using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.

    public GameObject[] planets;

	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
        
		Instantiate(enemies[enemyIndex], transform.position, transform.rotation);

        //for(int i=0;i<planets.Length;i++)
        //{
        //    Instantiate(planets[i], transform.position, transform.rotation);
        //    if((transform.position-planets[i-1].GetComponent<Planet>().GetPlanetPos()).magnitude>
        //        planets[i - 1].GetComponent<Planet>().GetR()+ planets[i].GetComponent<Planet>().GetR())
        //    {
        //        Destroy(planets[i]);
        //    }
            
        //}
        

        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}
}
