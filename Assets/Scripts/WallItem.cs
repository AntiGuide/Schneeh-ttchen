using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallItem : MonoBehaviour, IInteractable {
    public PlayerInventory.HoldItems Item;
    public Renderer holdItem;

    public bool itemPresent = true;

    public void Interact(PlayerInventory playerInventory, PlayerMiniGameManager miniGameManager) {
        if (playerInventory.playerHolds != Item && playerInventory.playerHolds != null) {
            return;
        }

        var newHoldItem = itemPresent ? Item : (PlayerInventory.HoldItems?)null;
        playerInventory.PickUpHoldItem(newHoldItem);
        itemPresent = !itemPresent;
        holdItem.enabled = itemPresent;
    }
}
