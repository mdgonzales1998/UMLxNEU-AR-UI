/*
 * Written by Michael Gonzales: mdgonzales1998@gmail.com
 * Test publisher script to test ROS connection and use of scooter_state topic
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ros_CSharp;

public class state_publisher : MonoBehaviour
{

    [SerializeField] private ROSCore _rosmaster;
    [SerializeField] private string text;
    private NodeHandle _nh;
    private Publisher<Messages.std_msgs.String> _pub;
    

    private void Start()
    {
        _nh = _rosmaster.getNodeHandle();
        _pub = _nh.advertise<Messages.std_msgs.String>("scooter_state", 10);
    }

    private void Update()
    {
        Messages.std_msgs.String msg = new Messages.std_msgs.String();
        msg.data = text;
        msg.Serialized = null;
        _pub.publish(msg);
    }

}
