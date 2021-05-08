# UMLxNEU-AR-UI

Installation Notes:
Two folders are missing from Assets/Plugins/ROS.NET. Go to https://github.com/uml-robotics/ros.net_unity and follow the steps to extract the necessary files for Unity. Then copy the Messages and Resources folders into Assets/Plugins/ROS.NET

# TODO
- Test with scooter machine to enure messages are going through correctly
- Update TF Tree game object for tracking the head
- Notes from meeting with Jordan Allspaw
    make script to set location based off of headset in pointcloud object
    
    sam make tf for user headhis world is unity 000
    take hmd world tf, make some game object and track that transform

    take 5 files from tf onto game object to make tf tree visualizer (Should already be there, but double check with Jordan)

    fixed frame = global frame/top level frame

    take location of head in ROS world, get equivalant in Unity, display pointcloud in a location relative to the head
