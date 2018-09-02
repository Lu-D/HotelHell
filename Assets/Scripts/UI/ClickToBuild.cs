using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToBuild : MonoBehaviour {

    public GameObject[] buildings;
    public bool validBuild;
    public bool isBuilding;
    public string buildType;
    public float rotationDegrees;
    public CursorMode cursorMode = CursorMode.Auto;
    public int currMoney;
    public Text moneyText;
    public int buildArray;

    private Vector2 cursorHotspot;


    // Use this for initialization
    void Start () {
        validBuild = false;
        isBuilding = false;
        buildType = "";
        rotationDegrees = 0;
        currMoney = 4;
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
        buildArray = buildArrayIndx;
        cursorHotspot = new Vector2(buildings[buildArrayIndx].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArrayIndx].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
        Cursor.SetCursor(buildings[buildArrayIndx].GetComponent<BAttraction>().textures[0], cursorHotspot, cursorMode);     
    }

    void build(string currBuilding, int buildArrayIndx)
    {
        if (Input.GetMouseButtonDown(0) && isBuilding && buildType == currBuilding && buildings[buildArrayIndx].GetComponent<BAttraction>().costToBuild <= currMoney)
        {

            if (validBuild)
            {
                GameObject building = (GameObject)Instantiate(buildings[buildArrayIndx], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), buildings[buildArrayIndx].transform.rotation);

                if (currBuilding != "DisneyCastle")
                {
                    building.transform.Rotate(0, 0, rotationDegrees, Space.Self);
                }

                Cursor.SetCursor(null, Vector2.zero, cursorMode);
                isBuilding = false;
                buildType = "";
                currMoney -= buildings[buildArrayIndx].GetComponent<BAttraction>().costToBuild;
                rotationDegrees = 0;
                buildings[buildArrayIndx].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[0];
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("t"))
        {
            Debug.Log("works1");
            if (Time.timeScale == 3.0f)
            {
                Time.timeScale = 1.0f;
            }
            else if (Time.timeScale == 2.0f)
            {
                Time.timeScale = 3.0f;
            }
            else
            {
                Time.timeScale = 2.0f;
            }

        }

        if (isBuilding && Input.GetKeyDown("space"))
        {
            rotationDegrees += 90;
            Debug.Log(rotationDegrees);
            Debug.Log("key pressed");

            if(rotationDegrees > 360)
            {
                rotationDegrees -= 360;
            }

            if (rotationDegrees == 90)
            {
                buildings[buildArray].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[1];
                cursorHotspot = new Vector2(buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
                Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().textures[1], cursorHotspot, cursorMode);

            }
            else if (rotationDegrees == 180)
            {
                buildings[buildArray].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[2];
                cursorHotspot = new Vector2(buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
                Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().textures[2], cursorHotspot, cursorMode);
            }
            else if (rotationDegrees == 270)
            {
                buildings[buildArray].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[3];
                cursorHotspot = new Vector2(buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
                Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().textures[3], cursorHotspot, cursorMode);
            }
            else
            {
                buildings[buildArray].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[0];
                cursorHotspot = new Vector2(buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
                Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().textures[0], cursorHotspot, cursorMode);
            }

        }

        build("GameStop", 0);
        build("Hooters", 1);
        build("Supreme", 2);
        build("See-Saw", 3);
        build("AMC", 4);
        build("DisneyCastle", 5);

        moneyText.text = "Current money: " + currMoney;

        if (Input.GetMouseButton(1))
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            isBuilding = false;
            buildType = "";
            rotationDegrees = 0;
        }
    }
}
