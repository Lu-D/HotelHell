using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToBuild : MonoBehaviour {

    public GameObject[] buildings;
    public Camera c;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            c.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
            Instantiate(buildings[0], mousePosition, Quaternion.identity);
        }
	}
}
