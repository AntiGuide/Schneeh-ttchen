using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDrop : MonoBehaviour, IInteractable {
    public WoodSpawner ParentSpawner;
    public float ChanceDoubleStack = 0.05f;

    private States state = States.RAW;
    private int stackCount;

    private enum States {
        RAW,
        PROCESSED,
    }

    private void Start() {
        stackCount = Random.value < ChanceDoubleStack ? 2 : 1;
    }

    public void Interact(PlayerInventory playerInventory) {
        switch (state) {
            case States.RAW:
                state = playerInventory.playerHolds == PlayerInventory.HoldItems.AXE ? States.PROCESSED : state;
                break;
            case States.PROCESSED:
                if (playerInventory.AddItem(PlayerInventory.Items.WOOD)) {
                    stackCount--;
                    if (stackCount == 0) {
                        ParentSpawner.RemoveWoodDrop(gameObject);
                        Destroy(gameObject);
                    }
                }
                break;
            default:
                break;
        }
    }
}
