using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour {
    
    [SerializeField] PlayerInventory inventory;
    [SerializeField] PlayerMiniGameManager miniGameManager;
    [SerializeField] float maxInteractAngle;
    [SerializeField] Text target;

	public float debug;

    List<BaseInteractable> interactablesInRange = new List<BaseInteractable>();

	// Update is called once per frame
	void Update () {

        target.text = "";
        if (interactablesInRange.Count == 0)
            return;

        float minDistance = float.MaxValue;
        BaseInteractable closest = null;
        foreach (BaseInteractable interactable in interactablesInRange)
        {
			var center = interactable.center != null ? interactable.center.position : interactable.transform.position;
			var relativePos = this.transform.InverseTransformPoint(center);
            if (relativePos.z < 0)
                continue;

            float angle = Vector3.Angle(Vector3.forward, relativePos.normalized);
			this.debug = angle;

            if (angle > maxInteractAngle)
                continue;

            if (angle < minDistance)
            {
                minDistance = angle;
                closest = interactable;
            }
        }
        if (closest == null)
            return;

        target.text = closest.gameObject.name;

        if (!Input.GetButtonDown("Interact"))
            return;

        closest.Interact(inventory, miniGameManager);
	}

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<BaseInteractable>();
        if (interactable != null)
        {
            interactablesInRange.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<BaseInteractable>();
        if (interactable != null)
        {
            interactablesInRange.Remove(interactable);
        }
    }
}
