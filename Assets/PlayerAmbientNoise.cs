using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmbientNoise : MonoBehaviour {

	[SerializeField] AudioSource ambient;
	[SerializeField] float insideVolume;
	[SerializeField] float outsideVolume;
	[SerializeField] float speed;

	bool outside = true;
	float volume;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Equals("firezone"))
			outside = false;	
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.name.Equals("firezone"))
			outside = true;
	}

	private void Update() {
		
		if (outside) {
			volume += speed * Time.deltaTime;
		}
		else {
			volume -= speed * Time.deltaTime;
		}

		volume = Mathf.Clamp(volume, insideVolume, outsideVolume);

		ambient.volume = volume;
	}

}
