using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseInteractable {

    [SerializeField] float openAngle;
    [SerializeField] float speed;

    float closeAngle;
    bool open;

    float t;

    // Use this for initialization
    void Start () {
        closeAngle = this.transform.localRotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {

        if (open)
            t += speed * Time.deltaTime;
        else
            t -= speed * Time.deltaTime;

        t = Mathf.Clamp(t, 0, 1);
        Vector3 rotation = this.transform.localRotation.eulerAngles;
        rotation.y = Mathf.Lerp(closeAngle, openAngle, t);
        this.transform.localRotation = Quaternion.Euler(rotation);
	}

    public override void Interact(PlayerInventory playerInventory, PlayerMiniGameManager miniGameManager)
    {
        open = !open;
    }
}
