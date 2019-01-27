using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : BaseInteractable {
    public GameObject miniGame;
    public PlayerMiniGameManager miniGameManager;

    private bool isInMinigame;
    private PlayerInventory playerInventory;

    public override void Interact(PlayerInventory playerInventory, PlayerMiniGameManager miniGameManager) {
        if (!playerInventory.HasItem(PlayerInventory.Items.FISH) || !playerInventory.CanAdd(PlayerInventory.Items.FISH_PIECE) || isInMinigame || playerInventory.playerHolds != null) {
            return;
        }

        this.playerInventory = playerInventory;
        miniGame.SetActive(true);
        miniGame.GetComponent<IMiniGame>().StartGame(new Callback(MiniGame));
        isInMinigame = true;
        miniGameManager.IsInMiniGame = isInMinigame;
        Debug.Log("FishCutting Minigame started");

    }

    public void MiniGame(bool won) {
        if (won) {
            Debug.Log("FishCutting finished (Minigame won)");
            playerInventory.AddItem(PlayerInventory.Items.FISH_PIECE);
            playerInventory.RemoveItem(PlayerInventory.Items.FISH);
        }

        isInMinigame = false;
        miniGameManager.IsInMiniGame = isInMinigame;
    }
}
