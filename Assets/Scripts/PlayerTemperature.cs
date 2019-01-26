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
    private float standStillModifier;

    [SerializeField]
    private float warmUpSpeed = 1;

    [SerializeField]
    private Rigidbody rb;

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
        if (!warmingUp && rb.velocity.magnitude > 0)
        {
            aktTemperature -= Time.deltaTime * temperatureLossSpeed;
        }
        else if(!warmingUp && rb.velocity.magnitude == 0f)
        {
            aktTemperature -= Time.deltaTime * temperatureLossSpeed * standStillModifier;
        }
        else if(warmingUp)
        {
            aktTemperature += Time.deltaTime * warmUpSpeed;
        }

        bodyTemperatureImage.fillAmount = aktTemperature / maxTemperature;
	}
}