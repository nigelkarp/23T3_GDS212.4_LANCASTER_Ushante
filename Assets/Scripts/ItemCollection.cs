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

            //display the score in debug console
            Debug.Log(_score);

            return;
        }
    }

    // Function to return current score
    public int ReturnCurrentScore()
    {
        return _score;
    }

}
