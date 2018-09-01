using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToBuild : MonoBehaviour {

    public GameObject[] buildings;
    public Camera c;
    bool validBuild;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetMouseButtonDown(0))

        if (Input.GetMouseButtonDown(0) && validBuild)
        {
            Instantiate(buildings[0], c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)), Quaternion.identity);
        }
	}
}
