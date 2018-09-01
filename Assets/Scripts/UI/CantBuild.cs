using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.

public class CantBuild : MonoBehaviour// required interface when using the OnPointerDown method.
{
    void OnMouseDown()
    {
        GameObject.Find("PlayerController").GetComponent<ClickToBuild>().validBuild = false;
    }

    private void OnMouseUp()
    {
        GameObject.Find("PlayerController").GetComponent<ClickToBuild>().validBuild = true;
    }
}
