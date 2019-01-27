using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour {


    public bool fireBurning = true;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && fireBurning) {
            other.GetComponent<PlayerTemperature>().warmingUp = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<PlayerTemperature>().warmingUp = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (fireBurning)
                other.GetComponent<PlayerTemperature>().warmingUp = true;
            else
                other.GetComponent<PlayerTemperature>().warmingUp = false;
        }
    }
}
