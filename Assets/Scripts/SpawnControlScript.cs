using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControlScript : MonoBehaviour {
    public float cooldown;
    public GameObject[] Enemies;
    private float maxCooldown;
    public int waveNumber;
    private List<int> waves;

    void Awake()
    {   
        maxCooldown = cooldown;
        waveNumber = 0;
        waves = new List<int>();
        for (uint i = 0; i < 3; ++i)
        {
            waves.Add(0);
        }
    }


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }
    
    public void startWave()
    {
        ++waveNumber;
        StartCoroutine(waveIterator());
    }

    IEnumerator waveIterator()
    {
        waves.Add(0);
        if (waveNumber%7 == 0 && waveNumber > 6)
        {
            waves.Add(4);
        }
        if(waveNumber%5 == 0 && waveNumber > 4)
        {
            waves.Add(3);
        }
        if(waveNumber%4 == 0 && waveNumber > 3)
        {
            waves.Add(2);
        }
        if(waveNumber%3 == 0 && waveNumber > 1)
        {
            waves.Add(1);
        }
        foreach (int enemyNum in waves)
        {
            spawn(Enemies[enemyNum]);
            yield return new WaitForSeconds(maxCooldown);
        }
    }

    void spawn(GameObject enemy)
    {
        GameObject enemyObj = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
        StartCoroutine(enemyObj.GetComponent<EnemyControlScript>().moveTowardsNext());
    }

}
