using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//spawnControlScript
//controls waves of enemies
public class SpawnControlScript : MonoBehaviour {
    public float cooldown;
    public GameObject[] Enemies;
    public int waveNumber;
    private float maxCooldown;
    private List<int> waves;
    private int currEnemy = 0;

    //Awake
    //sets necessary variables and adds three waves of enemies
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
    
    //startWave
    //begins the waveIterator coroutine
    public void startWave()
    {
        currEnemy = 0;
        ++waveNumber;
        GameObject.Find("StartText").GetComponent<Text>().text = "Wave: " + waveNumber;
        StartCoroutine(waveIterator());

    }

    //waveIterator
    //Fills waves with enemies based on wave number
    IEnumerator waveIterator()
    {
        GameObject.Find("StartButton").GetComponent<Button>().interactable = false;

        waves.Add(0);
        waves.Add(0);
        if (waveNumber%5 == 0 && waveNumber > 4)
        {
            waves.Add(3);
            waves.Add(3);
        }
        if(waveNumber%4 == 0 && waveNumber > 3)
        {
            waves.Add(2);
            waves.Add(2);
        }
        if(waveNumber%3 == 0 && waveNumber > 1)
        {
            waves.Add(1);
            waves.Add(1);
        }
        foreach (int enemyNum in waves)
        {
            ++currEnemy;
            if (currEnemy == (waves.Count))
            {
                spawnLast(Enemies[enemyNum]);
            }
            else
            {
                spawn(Enemies[enemyNum]);
                yield return new WaitForSeconds(maxCooldown);
            }
        }
    }

    //spawn
    //spawns an enemy on the start waypoint
    void spawn(GameObject enemy)
    {
        GameObject enemyObj = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
        enemyObj.transform.Rotate(0, 0, -180f, Space.Self);
        enemyObj.GetComponent<EnemyControlScript>().moveTowardsNext();
    }

    //spawnLast
    //spawns the last enemy in a wave for tracking
    void spawnLast(GameObject enemy)
    {
        GameObject enemyObj = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
        enemyObj.transform.Rotate(0, 0, -180f, Space.Self);
        enemyObj.GetComponent<EnemyControlScript>().isLast = true;
        enemyObj.GetComponent<EnemyControlScript>().moveTowardsNext();
    }

}
