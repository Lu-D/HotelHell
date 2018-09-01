using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAttraction : MonoBehaviour {

    private int energySubtraction;
    private int moneyEarned;
    private int currCapacity;
    private int maxCapacity;
    private float timeSpentIn;
    private int costToBuild;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && currCapacity != maxCapacity)
        {
            holdTime(timeSpentIn, collision);

        }
    }

    public IEnumerator holdTime(float holdTime, Collision2D collision)
    {
        currCapacity += 1;
        collision.gameObject.GetComponent<Renderer>().enabled = false;
        collision.gameObject.GetComponent<EnemyControlScript>().isCaptured = true;

        yield return new WaitForSeconds(holdTime);

        currCapacity -= 1;
        collision.gameObject.GetComponent<Renderer>().enabled = true;
        collision.gameObject.GetComponent<EnemyControlScript>().isCaptured = false;
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
