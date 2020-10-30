using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Messages.sensor_msgs;
using Ros_CSharp;

public class ScooterWorld : MonoBehaviour
{
    [SerializeField] ROSCore rosmaster;
    private Messages.sensor_msgs.PointCloud2 _world;
    private sbyte _state;
    private sbyte _pickCloudState = 2;
    private sbyte _placeCloudState = 8;
    private Ros_CSharp.NodeHandle _nh;
    private Subscriber<Messages.sensor_msgs.PointCloud2> _pcSub;
    private Subscriber<Messages.std_msgs.Int8> _stateSub;
    private Publisher<Messages.std_msgs.Int8> pub;
    private Messages.std_msgs.Int8 msg = new Messages.std_msgs.Int8();

    // Start is called before the first frame update
    void Start()
    {
        _pickCloudState = 2;
        _placeCloudState = 8;
        _nh = rosmaster.getNodeHandle();
        _pcSub = _nh.subscribe<Messages.sensor_msgs.PointCloud2>("camera/depth/points", 1, pc_callback);
        _stateSub = _nh.subscribe<Messages.std_msgs.Int8>("scooter_state", 1, state_callback);
        pub = _nh.advertise<Messages.std_msgs.Int8>("scooter_state", 10);
    }

    // Update is called once per frame
    void Update()
    {

        if (_state == _pickCloudState || _state == _placeCloudState)
        {
            //display pointcloud
            Debug.Log("Would be displaying pointcloud now");
        }

    }

    private void pc_callback(Messages.sensor_msgs.PointCloud2 pc)
    {
        _world = pc;
    }

    private void state_callback(Messages.std_msgs.Int8 s)
    {
        _state = s.data;
    }
}
