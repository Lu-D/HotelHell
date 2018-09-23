using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ClickToBuild
//Build attractions
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
    public GameObject buildTextures;
    private int currMouseTexture;

    // Start
    // on initialization
    void Start () {
        validBuild = false;
        isBuilding = false;
        buildType = "";
        rotationDegrees = 0;
        currMoney = 4;
	}

    //buildSelect
    //Changes bool when corresponding message is received
    public void buildSelect() {
        isBuilding = true;
    }

    //testAttract
    //sets current building to be built as described by string
    //turns on textures on map that can be built on
    //only executes when message is received from button press
    public void testAttract(string building)
    {
        buildType = building;
        buildTextures.SetActive(true);
    }

    //setCursor
    //changes texture to corresponding texture about to be built
    //only executes when message is received from buttone press
    public void setCursor(int buildArrayIndx)
    {
        buildArray = buildArrayIndx;
        cursorHotspot = new Vector2(buildings[buildArrayIndx].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArrayIndx].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
        currMouseTexture = 0;
        Cursor.SetCursor(buildings[buildArrayIndx].GetComponent<BAttraction>().textures[0], cursorHotspot, cursorMode);     
    }

    //build
    //builds building if all parameters are met
    void build(string currBuilding, int buildArrayIndx)
    {
        //if input is received, player is currently building, and player has enough money
        if (Input.GetMouseButtonDown(0) && isBuilding && buildType == currBuilding && buildings[buildArrayIndx].GetComponent<BAttraction>().costToBuild <= currMoney)
        {
            //if the ground is a valid place to build
            if (validBuild)
            {
                GameObject building = (GameObject)Instantiate(buildings[buildArrayIndx], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), buildings[buildArrayIndx].transform.rotation);

                //dont allow disney castle to rotate
                if (currBuilding != "DisneyCastle")
                {
                    building.transform.Rotate(0, 0, rotationDegrees, Space.Self);
                }

                //reset all fields back to default
                Cursor.SetCursor(null, Vector2.zero, cursorMode);
                isBuilding = false;
                buildType = "";
                currMoney -= buildings[buildArrayIndx].GetComponent<BAttraction>().costToBuild;
                rotationDegrees = 0;
                buildTextures.SetActive(false);
                buildings[buildArrayIndx].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[0];
            }
        }
    }

    // Update is called once per frame
    void Update () {

        //speeds up game if t is pressed
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

        //if player is building but the ground is not a valid build, indicate that build is invalid
        if(isBuilding && !validBuild)
        {
            cursorHotspot = new Vector2(buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
            Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().invalidTextures[currMouseTexture], cursorHotspot, cursorMode);
        }
        //if player is building and the ground is a valid build, set cursor texture
        else if(isBuilding && validBuild)
        {
            Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().textures[currMouseTexture], cursorHotspot, cursorMode);
        }

        //rotate building if space bar is pressed
        if (isBuilding && Input.GetKeyDown("space"))
        {
            rotationDegrees += 90;
            Debug.Log(rotationDegrees);
            Debug.Log("key pressed");

            //reset rotation if it reaches 360
            if(rotationDegrees > 360)
            {
                rotationDegrees -= 360;
            }

            //change textures and building orientation at each degrees
            if (rotationDegrees == 90)
            {
                buildings[buildArray].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[1];
                cursorHotspot = new Vector2(buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
                currMouseTexture = 1;
                Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().textures[1], cursorHotspot, cursorMode);

            }
            else if (rotationDegrees == 180)
            {
                buildings[buildArray].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[2];
                cursorHotspot = new Vector2(buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
                currMouseTexture = 2;
                Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().textures[2], cursorHotspot, cursorMode);
            }
            else if (rotationDegrees == 270)
            {
                buildings[buildArray].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[3];
                cursorHotspot = new Vector2(buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
                currMouseTexture = 3;
                Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().textures[3], cursorHotspot, cursorMode);
            }
            else
            {
                buildings[buildArray].GetComponent<SpriteRenderer>().sprite = buildings[buildArray].GetComponent<BAttraction>().sprites[0];
                cursorHotspot = new Vector2(buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.width / 2, buildings[buildArray].GetComponent<SpriteRenderer>().sprite.texture.height / 2);
                currMouseTexture = 0;
                Cursor.SetCursor(buildings[buildArray].GetComponent<BAttraction>().textures[0], cursorHotspot, cursorMode);
            }

        }

        //builds game objects if parameters are met
        build("GameStop", 0);
        build("Hooters", 1);
        build("Supreme", 2);
        build("See-Saw", 3);
        build("AMC", 4);
        build("DisneyCastle", 5);

        //sets moeny counter
        moneyText.text = "Current money: " + currMoney;

        //cancels builds and resets defaults
        if (Input.GetMouseButton(1))
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            isBuilding = false;
            buildType = "";
            buildTextures.SetActive(false);
            rotationDegrees = 0;
            currMouseTexture = 0;
        }
    }
}
