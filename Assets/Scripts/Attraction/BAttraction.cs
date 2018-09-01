using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAttraction : MonoBehaviour {

    private int energySubtraction;
    private int moneyEarned;
    private int capacity;
    private float timeSpentIn;
    private int costToBuild;
    private Collider2D myCollider;

	// Use this for initialization
	void Start () {
        //myCollider = gameObject.transform.GetChild(0).GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public int getEnergySubtraction()
    {
        return energySubtraction;
    }

    public int getMoneyEarned()
    {
        return moneyEarned;
    }

    public int getCapacity()
    {
        return capacity;
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
