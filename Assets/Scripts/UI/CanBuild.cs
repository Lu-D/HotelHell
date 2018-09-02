using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.

public class CanBuild : MonoBehaviour// required interface when using the OnPointerDown method.
{

    private void OnMouseEnter()
    {
        GameObject.Find("PlayerController").GetComponent<ClickToBuild>().validBuild = true;
    }

    private void OnMouseExit()
    {
        GameObject.Find("PlayerController").GetComponent<ClickToBuild>().validBuild = false;
    }
}
