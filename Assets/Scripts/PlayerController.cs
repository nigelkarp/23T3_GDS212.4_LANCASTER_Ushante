using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _playerSpeed = 5;           // Player/fishing-net speed
    [SerializeField] private Transform _net; // Reference to nets transform, make sure its the actual basket
    private bool _isCasting = false;        // Check if the net is being casted, set to false by default in start/ each round?

    [SerializeField] private Transform _topOfWater;

    // Score

    // Update is called once per frame
    void Update()
    {
        //Check if net is casted
        if (_isCasting)
        {
            //move the net
            MoveNet();
        }

        //Check if input has been made to cast the net
        //if button is being pressed & casting isnt set to true
        //eventually make it so the camera needs to be on the jetty view
        if (Input.GetKeyDown(KeyCode.Space) && !_isCasting)
        {
            _isCasting = true;  //set casting to true
        }

        //Update the UI to display the score
    }

    // A function to move the net
    void MoveNet()
    {
        // Get input for net movement
        float _inputX = Input.GetAxis("Horizontal"); //input 1
        float _inputY = Input.GetAxis("Vertical");  //input 2

        // Assign top of screen position
        float _topOfScreen = _topOfWater.position.y;

        // Calculate the new postion for the net, based on what is inputed
        Vector3 newPositon = _net.transform.position + new Vector3(_inputX, _inputY, 0) * _playerSpeed * Time.deltaTime;

        // Move the net to the new position
        _net.position = newPositon;

        //check if the net has reached the top, use an object or collider transform
        if (_net.transform.position.y >= _topOfScreen)
        {
            _isCasting = false;
            //trigger the transition to jetty view
        }
    }


}


