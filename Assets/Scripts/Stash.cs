using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stash : BaseInteractable {
    public PlayerInventory.Items stashType;

    private int stashCount;

    public override void Interact(PlayerInventory playerInventory, PlayerMiniGameManager miniGameManager) {
        if (playerInventory.RemoveItem(stashType)) {
            stashCount++;
        } else if (stashCount > 0) {
            playerInventory.AddItem(stashType);
            stashCount--;
        }
    }
}
