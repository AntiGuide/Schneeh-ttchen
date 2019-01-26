using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameWood : MonoBehaviour, IMiniGame {

    private Callback cb;
    private int timesToPress = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            timesToPress--;
            if (timesToPress == 0) {
                cb(true);
            }
        }
	}

    public void StartGame(Callback cb) {
        this.cb = cb;
    }
}
