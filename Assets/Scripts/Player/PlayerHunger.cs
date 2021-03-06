﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHunger : MonoBehaviour {
    [SerializeField]
    private Image hungerImage;

    [SerializeField]
    private float hungerSpeed = 1;

    [SerializeField]
    private float foodValue = 25;

    private float aktHunger;

    private float maxHunger = 100;

	// Use this for initialization
	void Start () {
        aktHunger = maxHunger;
	}
	
	// Update is called once per frame
	void Update () {
        aktHunger -= Time.deltaTime * hungerSpeed;

        hungerImage.fillAmount = aktHunger / maxHunger;

        if (aktHunger < float.Epsilon) {
            Death();
        }
	}

    public void EatFood()
    {
        aktHunger += foodValue;
    }

    public static void Death() {
        SceneManager.LoadScene(0);
    }
}