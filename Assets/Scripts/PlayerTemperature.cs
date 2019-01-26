using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTemperature : MonoBehaviour {
    [SerializeField]
    private Image bodyTemperatureImage;

    [SerializeField]
    private float temperatureLossSpeed;

    [SerializeField]
    private float warmUpSpeed = 1;

    [HideInInspector]
    public bool warmingUp;
    
    private float aktTemperature = 100;

    private float maxTemperature;

	// Use this for initialization
	void Start () {
        maxTemperature = aktTemperature;
	}
	
	// Update is called once per frame
	void Update () {
        if (!warmingUp)
        {
            aktTemperature -= Time.deltaTime * temperatureLossSpeed;
        }
        else
        {
            aktTemperature += Time.deltaTime * warmUpSpeed;
        }

        bodyTemperatureImage.fillAmount = aktTemperature / maxTemperature;
	}
}
