using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour {
    
	// Designed to reduce framerate, value picked for Ben's MacBook
	void Update () {
        for (int i = 0; i < 500; i++)
        {
            Debug.Log(i);
        }
	}
}
