using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAttraction : MonoBehaviour {

    public int energySubtraction;
    public int moneyEarned;
    public int currCapacity;
    public int maxCapacity;
    public float timeSpentIn;
    public int costToBuild;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(holdTime(timeSpentIn, collision));

        }
    }

    public IEnumerator holdTime(float holdTime, Collision2D collision)
    {
        currCapacity += 1;
        //collision.gameObject.GetComponent<Renderer>().enabled = false;
        //collision.gameObject.GetComponent<EnemyControlScript>().isCaptured = true;
        //collision.gameObject.GetComponent<EnemyControlScript>().energy -= energySubtraction;

        yield return new WaitForSeconds(holdTime);

        currCapacity -= 1;
 

    }

    public int getEnergySubtraction()
    {
        return energySubtraction;
    }

    public int getMoneyEarned()
    {
        return moneyEarned;
    }

    public int getMaxCapacity()
    {
        return maxCapacity;
    }

    public int getCurrCapacity()
    {
        return currCapacity;
    }

    public float getTimeSpentIn()
    {
        return timeSpentIn;
    }

    public int getCostToBuild()
    {
        return costToBuild;
    }
}
