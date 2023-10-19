using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class Scoring : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText; //scoreUIgameobject

    [SerializeField] private GameObject _itemCollectionManager; // Item manager game object w/ management script

    //temporary update function until the updatescoreUI function is utalised everytime the player reaches the top of the water
    private void Update()
    {
        UpdateScoreUI();
    }

    // Function to display score and collected item on the UI 
    public void UpdateScoreUI()
    {
        //Call returncurrentscore function from item collection script/ manager
        int currentScore = _itemCollectionManager.GetComponent<ItemCollection>().ReturnCurrentScore();

        // Update the score UI
        _scoreText.text = currentScore.ToString();
    }
}
