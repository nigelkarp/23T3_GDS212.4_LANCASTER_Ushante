using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaItem : MonoBehaviour
{
    [SerializeField] private int points = 1; // Points given on collection, 1 for now to represent each item

    //A function that gives the point value of the item to the item collection script
    public int GetItemValue()
    {
        return points;
    }
}
