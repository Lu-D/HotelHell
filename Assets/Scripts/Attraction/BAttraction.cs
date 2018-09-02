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

    public Sprite[] sprites;
    public Texture2D[] textures;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public IEnumerator holdTime(float holdTime)
    //{
    //    ++currCapacity;
    //    //collision.gameObject.GetComponent<Renderer>().enabled = false;
    //    //collision.gameObject.GetComponent<EnemyControlScript>().isCaptured = true;
    //    //collision.gameObject.GetComponent<EnemyControlScript>().energy -= energySubtraction;

    //    yield return new WaitForSeconds(holdTime);

    //    --currCapacity;
    //}
}
