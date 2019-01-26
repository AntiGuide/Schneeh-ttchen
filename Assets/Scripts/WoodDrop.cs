using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDrop : MonoBehaviour, IInteractable {
    public WoodSpawner ParentSpawner;
    public float ChanceDoubleStack = 0.05f;
    public GameObject miniGame;

    private States state = States.RAW;
    private int stackCount;
    private bool isInMinigame;

    private enum States {
        RAW,
        PROCESSED,
    }

    private void Start() {
        stackCount = Random.value < ChanceDoubleStack ? 2 : 1;
    }

    public void Interact(PlayerInventory playerInventory) {
        if (isInMinigame) {
            return;
        }

        switch (state) {
            case States.RAW:
                if (playerInventory.playerHolds == PlayerInventory.HoldItems.AXE) {
                    miniGame.SetActive(true);
                    miniGame.GetComponent<IMiniGame>().StartGame(new Callback(MiniGame));
                    isInMinigame = true;
                    Debug.Log("Wood Minigame started");
                }
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

    public void MiniGame(bool won) {
        if (won) {
            state = States.PROCESSED;
            Debug.Log("Wood processed (Minigame won)");
        }
        
        isInMinigame = false;
    }
}
