using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private bool _isCasting = false;                    // Check if the net is being casted, set to false by default in start/ each round?
    private int _playerSpeed = 5;                       // Player/fishing-net speed
    private float _castTimer;     
    private bool _isMoveable = false;
    private bool _itemsInteractable = false;            // Cant interact with items
    private bool _canCast = true;                       // can the player press the spacebar
    private bool _isUnderwater = false;                 // sets the active camera

    [SerializeField] private float _castTime = 10.0f; 
    //[SerializeField] private float _castSpeed = 2.0f;

    [SerializeField] private Collider2D _netCollider;   // The nets collider

    [SerializeField] private Transform _net;            // Reference to nets transform, make sure its the actual basket
    [SerializeField] private GameObject _topOfWater;     // Reference to WaterTop object

    [SerializeField] private Transform _aboveWaterPos;  // Reference to AboveWaterPos objects transform
    [SerializeField] private Transform _seaBedPos;      // Reference to SeaBedPos objects transform

    [SerializeField] Camera _CameraUp;                  // Reference to the Top camera GameObject
    [SerializeField] Camera _CameraDown;                // Refence to Bottom Camera GameObject
    [SerializeField] float _followSpeed = 5f;           // How fast the camera follows the player

    private bool _cameraMove = true;                    // Check if Camera can move, this might be redundant

    [SerializeField] private SeaItem[] _seaItems;       // Array of sea items in the level

    [SerializeField] private GameObject _itemCollectionManager; // Item manager game object w/ management script

    private void Start()
    {
        //Turn off the top of water/ top out of bounds by default
        _topOfWater.gameObject.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        swapCameras();

        //Check if the camera can move
        if (_cameraMove)
        {
            // Getting the players y positon
            float _playerY = _net.position.y;

            // Get the cameras current pos
            Vector3 _cameraPos = _CameraDown.transform.position;

            // Set the cameras new position based on the players Y position >> reference chat gpt for this
            _cameraPos.y = Mathf.Lerp(_cameraPos.y, _playerY, _followSpeed * Time.deltaTime);

            // Apply the new camera positon
            _CameraDown.transform.position = _cameraPos;
        }

        if (!_isUnderwater) // change to the camera swapping variable
        {
            _canCast = true;
        }

        // Check if net is casted
        if (_isCasting)
        {
            CastNet();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isCasting && _canCast)
        {
            //swap cameras
            _isUnderwater = true;

            _isCasting = true;
            _castTimer = 0f;
        }

        // Check if player can move controller
        if (_isMoveable)
        {
            MoveNet();
        }
    }

    // A function to move the net
    void MoveNet()
    {
        // Get input for net movement
        float _inputX = Input.GetAxis("Horizontal"); //input 1
        float _inputY = Input.GetAxis("Vertical");  //input 2

        // Calculate the new postion for the net, based on what is inputed
        Vector3 newPositon = _net.transform.position + new Vector3(_inputX, _inputY, 0) * _playerSpeed * Time.deltaTime;

        // Move the net to the new position
        _net.position = newPositon;
    }

    // Function to 'cast' the net
    void CastNet()
    {
        _castTimer += Time.deltaTime;

        if (_castTimer < _castTime)
        {
            // Calculate the current progress of the cast
            float castProgress = _castTimer / _castTime;

            // Move the player from the above water towards the seabed
            Vector3 currentPosition = Vector3.Lerp(_aboveWaterPos.position, _seaBedPos.position, castProgress);
            transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
        }
        else
        {
            Debug.Log("cast finished");

            //  Dont let the player cast again
            _canCast = false;

            // Cast finished
            _isCasting = false;

            //allow playermovement
            _isMoveable = true;

            //allow items to be interactable
            _itemsInteractable = true;

            //set top out of bounds as active
            _topOfWater.gameObject.SetActive(true);
        }
        
    }

    //function to swap cameras
    void swapCameras()
    {
        //if a value is true turn on up and turn off down
        if (!_isUnderwater)
        {
            _CameraUp.enabled = true;
            _CameraDown.enabled = false;
        }
        else         //if the value is false do the opposite
        {
            _CameraUp.enabled = false;
            _CameraDown.enabled = true;
        }

    }

    //function to collect sea items with the net
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SeaItem") && _itemsInteractable)
        {
            SeaItem seaItem = other.GetComponent<SeaItem>();

            if (seaItem != null)
            {
                //call collect item function from itemcollection script/ manager
                _itemCollectionManager.GetComponent<ItemCollection>().CollectItem(seaItem);

                //destroy the seaitem
                Debug.Log("item collected");
            }
        }
        else if (other.CompareTag("WaterTop"))
        {
            //Stopping movement once the net/ player has reached the top of the screen
            _isMoveable = false;

            //switch the camera, the space bar is enabled with the enabling of the camera
            _isUnderwater = false;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("CameraStopZone"))
        {
            //Debug.Log("Camera stop zone hit");

            //Stop the camera from moving once its entered the stop zone trigger
            _cameraMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CameraStopZone"))
        {
            Debug.Log("Camera stop zone left");

            //Start the camera movement once its entered the stop zone trigger
            _cameraMove = true;
        }
    }

}


