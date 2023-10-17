using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private int _playerSpeed = 5;                       // Player/fishing-net speed
    [SerializeField] private Transform _net;            // Reference to nets transform, make sure its the actual basket
    private bool _isCasting = false;                    // Check if the net is being casted, set to false by default in start/ each round?
    [SerializeField] private Collider2D _netCollider;   // The nets collider

    [SerializeField] private Transform _topOfWater;     // Reference to WaterTop object's transform
    
    [SerializeField] Camera _mainCamera;                // Refence to Main Camera GameObject
    //private bool _cameraMove = true;                  // Check if Camera can move, this might be redundant
    [SerializeField] float _followSpeed = 5f;           // How fast the camera follows the player

    [SerializeField] private SeaItem[] _seaItems;       // Array of sea items in the level  
    

    // Score

    // Update is called once per frame
    void Update()
    {
        // Getting the players y positon
        float _playerY = _net.position.y;

        // Get the cameras current pos
        Vector3 _cameraPos = _mainCamera.transform.position;

        // Set the cameras new position based on the players Y position >> reference chat gpt for this
        _cameraPos.y = Mathf.Lerp(_cameraPos.y, _playerY, _followSpeed * Time.deltaTime);

        // Apply the new camera positon
        _mainCamera.transform.position = _cameraPos;

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
            //stop camera movement
            //trigger the transition to jetty view
        }

    }

    //function to collect sea items with the net
    private void OnTriggerEnter2D(Collider2D other)
    {
        SeaItem seaItem = other.GetComponent<SeaItem>();
        if (seaItem != null)
        {
            //add points
            //score += seaItem.points;
            //destroy the seaitem
            Debug.Log("item collected");
        }
    }
}


