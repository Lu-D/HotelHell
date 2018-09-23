using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TownControlScript
//Maintains trash bar
public class TownControlScript : MonoBehaviour {
    public int trashCount = 0;
    public int trashCapacity;
    public int trashPerTime;
    public float timeForEachSubtract;
    public Slider trashSlider;

    //trashToZero
    //resets bar
    public void trashToZero()
    {
        trashCount = 0;
    }

	//Start
    //on initialization
	void Start () {
        StartCoroutine(subtractTrash());
	}
	
	// Update is called once per frame
    // updates bar every frame with the trash count
	void Update () {

        if(trashCount < 0)
        {
            trashCount = 0;
        }

        trashSlider.value = trashCount;
        trashSlider.maxValue = trashCapacity;
	}

    //subtractTrash
    //continually decrement the trashCount
    public IEnumerator subtractTrash()
    {
        while (trashCount < 2 * trashCapacity)
        {
            trashCount -= trashPerTime;
            yield return new WaitForSeconds(timeForEachSubtract);
        }
    }
}
