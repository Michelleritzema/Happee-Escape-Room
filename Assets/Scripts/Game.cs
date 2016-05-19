using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Game : MonoBehaviour {

    public Light indicatorLight;
    public Door escapeDoor, puzzleDoor;
    public string[] modules = new string[4];

    public class Item
    {
        public string password;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Escaping()
    {
        indicatorLight.color = Color.green;
        escapeDoor.GetComponent<Door>().DoorMovable(true);
        TriggerDoorAnimation(escapeDoor);
    }

    public void Escaped()
    {
        TriggerDoorAnimation(escapeDoor);
        if (puzzleDoor.GetComponent<Door>().GetOpened())
            TriggerDoorAnimation(puzzleDoor);
        modules[0] = GameObject.Find("Game").GetComponent<Settings>().GetModule1();
        modules[1] = GameObject.Find("Game").GetComponent<Settings>().GetModule2();
        modules[2] = GameObject.Find("Game").GetComponent<Settings>().GetModule3();
        modules[3] = GameObject.Find("Game").GetComponent<Settings>().GetModule4();
        for(int i = 0; i < 1; i++)
        {
            GameObject.Find(modules[i]).SetActiveRecursively(false);
        }
        escapeDoor.DoorMovable(false);
        puzzleDoor.DoorMovable(false);
    }

    /*
     * Triggers the supplied door's animation, if it is not already running.
     * If it is closed, the door is opened. If it is opened, the door is closed.
     */
    public void TriggerDoorAnimation(Door door)
    {
        if (door.Running == false)
            StartCoroutine(door.Open());
    }

}
