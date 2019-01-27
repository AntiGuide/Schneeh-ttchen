using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameFishCutting : MonoBehaviour, IMiniGame {
    public Image image;
    private Callback cb;
    private int timesToPress = 10;
    private int timesToPressStart;

    // Use this for initialization
    void Start() {
        timesToPressStart = timesToPress;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            timesToPress--;
            image.fillAmount = (timesToPressStart - timesToPress) / timesToPressStart;
            if (timesToPress == 0) {
                cb(true);
                gameObject.SetActive(false);
            }
        }
    }

    public void StartGame(Callback cb) {
        this.cb = cb;
    }
}
