using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ros_CSharp;
using Messages;
using System;

public class ROSCore : MonoBehaviour {
    private bool IsROSStarted;
    private NodeHandle nh;

    public bool autostart;
    public string Master_URI;
    public string HOSTNAME;
    //[SerializeField] ScooterWorld scooter;
    //public string 

    public NodeHandle getNodeHandle()
    {
        if (IsROSStarted)
        {
            Debug.Log("ROS STARTED");
            return nh;
        }
        else
        {
            Debug.LogWarning("Could not return nh, ros is not ready yet, try calling StartROS first");
            return null;
        }
    }

    public void StartROS(String master_uri, String hostname, String nodename = "UnityProject")
    {
        if (!IsROSStarted)
        {
            ROS.ROS_HOSTNAME = hostname;
            ROS.ROS_MASTER_URI = master_uri;
            //scooter.getStateText().text = "calling ros init";
            ROS.Init(new String[0], nodename);
            //scooter.getStateText().text = "ros init called, creating nodehandle";
            nh = new NodeHandle();
            IsROSStarted = true;
            Debug.Log("ROS Started, Master: " + master_uri + " Hostname: " + hostname + " Node Name: " + nodename);
            //scooter.getPromptText().text = "ROS Started, Master: " + master_uri + " Hostname: " + hostname + " Node Name: " + nodename;
        }
        else
        {
            //scooter.getStateText().text = "ROS already started";
            Debug.LogWarning("Can't start ROS, it is already started");
        }
    }

    // Use this for initialization
    void Awake()
    {
        IsROSStarted = false;
        if (autostart)
            StartROS(Master_URI, HOSTNAME);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
