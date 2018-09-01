using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControlScript : MonoBehaviour {
    public float cooldown;
    public GameObject[] Enemies;
    private float maxCooldown;


	// Use this for initialization
	void Start () {
        maxCooldown = cooldown;
	}
	
	// Update is called once per frame
	void Update () {

        spawn(Enemies[0]);
	}

    void spawn(GameObject enemy)
    {
        --cooldown;
        if(cooldown <= 0)
        {
            GameObject enemyObj = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
            enemyObj.GetComponent<Rigidbody2D>().velocity = (new Vector3(0, -1, 0)) * enemyObj.GetComponent<EnemyControlScript>().moveSpeed;
            cooldown = maxCooldown;
        }
    }
}
