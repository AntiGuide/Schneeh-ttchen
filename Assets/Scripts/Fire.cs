using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour {
    [SerializeField]
    private float fireFadeModifierPerHole;

    [SerializeField]
    private GameObject hole;

    [SerializeField]
    private int aktWoodCount = 2;

    [SerializeField]
    private int fireTimerSecondsPerWoodLog = 30;

    private bool fireBurning = true;

    private float fireTimerInSeconds = 60f;

    private GameObject[] holes = new GameObject[3];

    // Use this for initialization
    void Start()
    {
        fireTimerInSeconds = fireTimerSecondsPerWoodLog;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireBurning)
        {
            if (holes.Length == 0)
            {
                fireTimerInSeconds -= Time.deltaTime;
            }
            else
            {
                fireTimerInSeconds -= Time.deltaTime * (fireFadeModifierPerHole * holes.Length);
            }
        }

        if (fireTimerInSeconds < 0)
        {
            if (aktWoodCount == 0)
            {
                fireBurning = false;
            }
            else
            {
                aktWoodCount--;
                fireTimerInSeconds = fireTimerSecondsPerWoodLog;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && fireBurning)
        {
            other.GetComponent<PlayerTemperature>().warmingUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerTemperature>().warmingUp = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !fireBurning)
        {
            other.GetComponent<PlayerTemperature>().warmingUp = false;
        }
    }

    public void AddWood(ref PlayerInventory playerInventory)
    {
        playerInventory.RemoveItem(PlayerInventory.Items.WOOD);
        aktWoodCount++;

        if(aktWoodCount == 1)
        {
            fireBurning = true;
            fireTimerInSeconds = fireTimerSecondsPerWoodLog;
        }
    }
}