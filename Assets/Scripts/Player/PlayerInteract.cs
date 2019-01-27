using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    
    [SerializeField] PlayerInventory inventory;
    [SerializeField] PlayerMiniGameManager miniGameManager;

    List<BaseInteractable> interactablesInRange = new List<BaseInteractable>();

	// Update is called once per frame
	void Update () {

        if (interactablesInRange.Count == 0)
            return;

        if (!Input.GetButtonDown("Interact"))
            return;

        float minDistance = float.MaxValue;
        BaseInteractable closest = null;
        foreach (BaseInteractable interactable in interactablesInRange)
        {
            float dist = Vector3.Distance(this.transform.position, interactable.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = interactable;
            }
        }

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
