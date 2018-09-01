using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToBuild : MonoBehaviour {

    public GameObject[] buildings;
    public bool validBuild;
    public bool isBuilding;
    public string buildType;
    public float rotationDegrees;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;

	// Use this for initialization
	void Start () {
        validBuild = false;
        isBuilding = false;
        buildType = "";
        rotationDegrees = 0;
	}

    public void buildSelect() {
        isBuilding = true;
    }

    public void testAttract()
    {
        buildType = "testAttract";
    }

    public void testAttract2()
    {
        buildType = "testAttract2";
    }

    // Update is called once per frame
    void Update () {

        if (isBuilding && Input.GetKeyDown("space"))
        {
            rotationDegrees += 90;
            Debug.Log(rotationDegrees);
            Debug.Log("key pressed");

        }

        if (isBuilding && buildType == "testAttract")
        {
            Cursor.SetCursor(buildings[0].GetComponent<SpriteRenderer>().sprite.texture, Vector2.zero, cursorMode);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

        if (Input.GetMouseButtonDown(0) && isBuilding && buildType == "testAttract")
        {

            if (validBuild) {
                GameObject building = (GameObject)Instantiate(buildings[0], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), buildings[0].transform.rotation);
                building.transform.Rotate(0, 0, rotationDegrees, Space.Self);
                Debug.Log(building.transform.rotation);
                isBuilding = false;
                buildType = "";
                rotationDegrees = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && isBuilding && buildType == "testAttract2")
        {

            if (validBuild)
            {
                Instantiate(buildings[1], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), buildings[0].transform.rotation);
                isBuilding = false;
                buildType = "";
            }
        }
    }
}
