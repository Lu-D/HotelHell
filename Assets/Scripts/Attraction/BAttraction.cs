using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BAttraction
//Base variables for Attractions
public class BAttraction : MonoBehaviour {

    public int energySubtraction;
    public int moneyEarned;
    public int currCapacity;
    public int maxCapacity;
    public float timeSpentIn;
    public int costToBuild;

    public Sprite[] sprites;
    public Texture2D[] textures;
    public Texture2D[] invalidTextures;
}
