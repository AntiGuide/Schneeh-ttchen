using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : MonoBehaviour, IInteractable {
    public PlayerInventory.Items stashType;

    private int stashCount;

    public void Interact(PlayerInventory playerInventory) {
        if (playerInventory.RemoveItem(stashType)) {
            stashCount++;
        } else if (stashCount > 0) {
            playerInventory.AddItem(stashType);
            stashCount--;
        }
    }
}
