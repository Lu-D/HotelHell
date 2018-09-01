using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToBuild : MonoBehaviour {

    public GameObject[] buildings;
    public bool validBuild;
    public bool isBuilding;
    public string buildType;
    public float rotationDegrees;
    public CursorMode cursorMode = CursorMode.Auto;
    private Vector2 cursorHotspot;

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

    public void testAttract(string building)
    {
        buildType = building;
    }

    public void setCursor(int buildArrayIndx)
    {
        cursorHotspot = new Vector2(buildings[buildArrayIndx].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArrayIndx].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
        Cursor.SetCursor(buildings[buildArrayIndx].GetComponent<SpriteRenderer>().sprite.texture, cursorHotspot, cursorMode);     
    }

    void build(string currBuilding, int buildArrayIndx)
    {
        if (Input.GetMouseButtonDown(0) && isBuilding && buildType == currBuilding)
        {

            if (validBuild)
            {
                GameObject building = (GameObject)Instantiate(buildings[buildArrayIndx], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), buildings[0].transform.rotation);
                building.transform.Rotate(0, 0, rotationDegrees, Space.Self);

                Cursor.SetCursor(null, Vector2.zero, cursorMode);
                isBuilding = false;
                buildType = "";
                rotationDegrees = 0;
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if (isBuilding && Input.GetKeyDown("space"))
        {
            rotationDegrees += 90;
            Debug.Log(rotationDegrees);
            Debug.Log("key pressed");

        }

        build("testAttract", 0);
    }
}
