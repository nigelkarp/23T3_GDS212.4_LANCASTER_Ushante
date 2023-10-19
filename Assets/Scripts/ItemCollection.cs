using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    private int _score = 0;                                   // Player Score

    [SerializeField] List<SeaItem> collectedItems = new List<SeaItem>(); // List to store collected items

    // Function to collect an item
    public void CollectItem(SeaItem seaItem)
    {
        // Check if the iten is not already collected
        if (!collectedItems.Contains(seaItem))
        {
            // Add the item to the collected items list
            collectedItems.Add(seaItem);

            // Update the score based on the item's value
            // Need to create a function to return this value in seaitem class
            _score += seaItem.GetItemValue();

            //call update  UI function
        }

    }

    // Function to display score and collected item on the UI 
    // Update the score display on the screen

    // Update the collected items displayed on the screen

    // Update the UI to reflect the current score and sollected items

}
