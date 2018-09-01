using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToBuild : MonoBehaviour {

    public GameObject[] buildings;
    public bool validBuild;
    public bool isBuilding;

	// Use this for initialization
	void Start () {
        validBuild = false;
        isBuilding = false;
	}

    public void buildSelect() {
        isBuilding = true;
    }

	// Update is called once per frame
	void Update () {

        Debug.Log(validBuild);

        if (Input.GetMouseButtonDown(0) && isBuilding)
        {

            if (validBuild) {
                Instantiate(buildings[0], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), buildings[0].transform.rotation);
                isBuilding = false;
            }
        }
	}
}
