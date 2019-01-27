using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pot : BaseInteractable {
    public float cookingTimeStart;
    public Image cookingBarBackground;
    public Image cookingBarContent;

    private int charges;
    private bool cooking;
    private float cookingTime;

    public override void Interact(PlayerInventory playerInventory, PlayerMiniGameManager miniGameManager) {
        if (charges > 0) {
            var hunger = playerInventory.gameObject.GetComponent<PlayerHunger>();
            hunger.EatFood();
            charges--;
            return;
        }

        if (!playerInventory.RemoveItem(PlayerInventory.Items.FISH_PIECE)) {
            return;
        }

        cooking = true;
        cookingBarBackground.enabled = cooking;
        cookingBarContent.enabled = cooking;
        cookingTime = cookingTimeStart;
    }

    private void Update() {
        if (cooking) {
            cookingTime -= Time.deltaTime;
            cookingBarContent.fillAmount = cookingTime / cookingTimeStart;
            if (cookingTime < float.Epsilon) {
                charges = 2;
                cooking = false;
                cookingBarBackground.enabled = cooking;
                cookingBarContent.enabled = cooking;
            }
        }
    }
}
