/*
 * Written by Michael Gonzales: mdgonzales1998@gmail.com
 * Script that handles updating displayed text in ARUI
 * TODO: Edit tui.py or make a new version that publishes the state changes to the "scooter_state" topic.
 *       Each message text is listed in the StateHandler() function.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Messages.sensor_msgs;
using Ros_CSharp;

public class ScooterWorld : MonoBehaviour
{
    [SerializeField] private ROSCore rosmaster;
    [SerializeField] private GameObject _world;
    private String _state;
    private String _pickCloudState = "GatherPickCloud";
    private String _placeCloudState = "GatherPlaceCloud";
    private NodeHandle _nh;
    private Subscriber<PointCloud2> _pcSub;
    private Subscriber<Messages.std_msgs.String> _stateSub;
    private Messages.std_msgs.String _msg = new Messages.std_msgs.String();
    [SerializeField] private Text _stateText;
    [SerializeField] private Text _promptText;

    // Start is called before the first frame update
    void Start()
    {
        _nh = rosmaster.getNodeHandle();
        _stateSub = _nh.subscribe<Messages.std_msgs.String>("scooter_state", 1, state_callback);
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();

        if (_state == _pickCloudState || _state == _placeCloudState)
        {
            //display pointcloud
            Debug.Log("Would be displaying pointcloud now");
            _world.SetActive(true);
            _stateText.text = "POINTCLOUD DISPLAY";
        }
        else
        {
            // Deactive pointcloud if active
            _world.SetActive(false);
        }
    }

    /*
     * Function that changes the text displayed in the ARUI, changes based upon the recieved topic from "scooter_state"
     * _stateText: Displays the current state
     * _promptText: Displays prompt describing what to do to the user
    */
    private void StateHandler()
    {
        switch (_state) {

            case "Drive":
                _stateText.text = "Drive";
                _promptText.text = "Press PICK to Start";
                break;
            case "GatherPickCloud":
                _stateText.text = "Pick and Place";
                _promptText.text = "Gathering an image of your world";
                break;
            case "ObjectSelection":
                _stateText.text = "Pick and Place";
                _promptText.text = "Move the joystick to find the red dots and place it over the desired object within the green area. Press the green yes button when ready";
                break;
            case "SegmentObjects":
                _stateText.text = "Pick and Place";
                _promptText.text = "Finding your object, please wait…";
                break;
            case "ObjectConfirmation":
                _stateText.text = "Pick and Place";
                _promptText.text = "Confirm Object, press YES or NO";
                break;
            case "PickObject":
                _stateText.text = "Pick and Place";
                _promptText.text = "Grabbing object...";
                break;
            case "FailedToGrab":
                _stateText.text = "Pick and Place";
                _promptText.text = "Failed to grab object, returning to drive mode";
                break;
            case "FailedToFindGrasp":
                _stateText.text = "Pick and Place";
                _promptText.text = "No way to grab object, returning to drive mode";
                break;
            case "HoldingObject":
                _stateText.text = "Pick and Place";
                _promptText.text = "Select placement option";
                break;
            case "GatherPlaceCloud":
                _stateText.text = "Pick and Place";
                _promptText.text = "Failed to grab object, returning to drive mode";
                break;
            case "PlaceSelection":
                _stateText.text = "Pick and Place";
                _promptText.text = "Move the joystick to find the red dots and place it over the desired location within the green area. Press the green yes button when ready";
                break;
            case "FindPlace":
                _stateText.text = "Pick and Place";
                _promptText.text = "Finding location";
                break;
            case "PlaceConfirmation":
                _stateText.text = "Pick and Place";
                _promptText.text = "Place object here?";
                break;
            case "Place":
                _stateText.text = "Pick and Place";
                _promptText.text = "Placing object";
                break;
            case "PlaceFailure":
                _stateText.text = "Pick and Place";
                _promptText.text = "Cannot place object here, select different location";
                break;
            case "Basket":
                _stateText.text = "Pick and Place";
                _promptText.text = "Placing object in basket";
                break;
            default:
                break;
        }
    }

    // Callback function for getting the state
    private void state_callback(Messages.std_msgs.String s)
    {
        _state = s.data;
    }

    public Text getStateText()
    {
        return _stateText;
    }

    public Text getPromptText()
    {
        return _promptText;
    }
}
