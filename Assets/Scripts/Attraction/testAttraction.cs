using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAttraction : BAttraction {

	// Use this for initialization
	void Start () {
        energySubtraction = 1;
        moneyEarned = 5;
        currCapacity = 0;
        maxCapacity = 3;
        timeSpentIn = 3;
        costToBuild = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
