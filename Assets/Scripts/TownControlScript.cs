using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownControlScript : MonoBehaviour {
    public int trashCount = 0;
    public int trashCapacity;
    public int trashPerTime;
    public float timeForEachSubtract;
    public Slider trashSlider;

    public void trashToZero()
    {
        trashCount = 0;
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(subtractTrash());
	}
	
	// Update is called once per frame
	void Update () {

        if(trashCount < 0)
        {
            trashCount = 0;
        }

        trashSlider.value = trashCount;
        trashSlider.maxValue = trashCapacity;
	}

    public IEnumerator subtractTrash()
    {
        while (trashCount < 2 * trashCapacity)
        {
            trashCount -= trashPerTime;
            yield return new WaitForSeconds(timeForEachSubtract);
        }
    }
}
