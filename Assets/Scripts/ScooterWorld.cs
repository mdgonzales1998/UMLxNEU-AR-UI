using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Messages.sensor_msgs;
using Ros_CSharp;

public class ScooterWorld : MonoBehaviour
{
    [SerializeField] ROSCore rosmaster;
    private Messages.sensor_msgs.PointCloud2 _world;
    private String _state;
    private String _pickCloudState = "Pick Cloud";
    private String _placeCloudState = "Place Cloud";
    private NodeHandle _nh;
    private Subscriber<Messages.sensor_msgs.PointCloud2> _pcSub;
    private Subscriber<Messages.std_msgs.String> _stateSub;
    private Publisher<Messages.std_msgs.String> _pub;
    private Messages.std_msgs.String _msg = new Messages.std_msgs.String();
    [SerializeField] private Text _stateText;
    [SerializeField] private Text _promptText;

    // Start is called before the first frame update
    void Start()
    {
        _nh = rosmaster.getNodeHandle();
        _pcSub = _nh.subscribe<Messages.sensor_msgs.PointCloud2>("camera/depth/points", 1, pc_callback);
        _stateSub = _nh.subscribe<Messages.std_msgs.String>("scooter_state", 1, state_callback);
        _pub = _nh.advertise<Messages.std_msgs.String>("scooter_state", 10);
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();

        if (_state == _pickCloudState || _state == _placeCloudState)
        {
            //display pointcloud
            Debug.Log("Would be displaying pointcloud now");
        }
    }

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
                _promptText.text = "Move the joystick to find the red dots and place it over the desired object within the green area. Press the green yes button when ready";
                break;
            case "SegmentObjects":
                _promptText.text = "Finding your object, please wait…";
                break;
            case "ObjectConfirmation":
                _promptText.text = "Confirm Object, press YES or NO";
                break;
            case "PickObject":
                _promptText.text = "Grabbing object...";
                break;
            case "FailedToGrab":
                _promptText.text = "Failed to grab object, returning to drive mode";
                break;
            case "FailedToFindGrasp":
                _promptText.text = "No way to grab object, returning to drive mode";
                break;
            case "HoldingObject":
                _promptText.text = "Select placement option";
                break;
            case "GatherPlaceCloud":
                _promptText.text = "Failed to grab object, returning to drive mode";
                break;
            case "PlaceSelection":
                _promptText.text = "Move the joystick to find the red dots and place it over the desired location within the green area. Press the green yes button when ready";
                break;
            case "FindPlace":
                _promptText.text = "Finding location";
                break;
            case "PlaceConfirmation":
                _promptText.text = "Place object here";
                break;
            case "Place":
                _promptText.text = "Placing object";
                break;
            case "PlaceFailure":
                _promptText.text = "Cannot place object here, select different location";
                break;
            case "Basket":
                _promptText.text = "Placing object in basket";
                break;
            default:
                break;
        }
    }

    private void pc_callback(Messages.sensor_msgs.PointCloud2 pc)
    {
        _world = pc;
    }

    private void state_callback(Messages.std_msgs.String s)
    {
        _state = s.data;
    }
}
