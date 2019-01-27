using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public HoldItems? playerHolds;

    private int[] limits;
    private int[] itemCounts;

    public enum HoldItems {
        AXE,
        HAMMER,
    }

    public enum Items : int {
        FISH,
        FISH_PIECE,
        WOOD,
    }

    public bool AddItem(Items itemToAdd) {
        var i = (int)itemToAdd;
        if (itemCounts[i] < limits[i]) {
            itemCounts[i]++;
            return true;
        }

        return false;
    }

    public bool RemoveItem(Items itemToRemove) {
        var i = (int)itemToRemove;
        if (itemCounts[i] > 0) {
            itemCounts[i]--;
            return true;
        }

        return false;
    }

    public bool HasItem(Items itemToCheck, int countToCheck = 1) {
        return itemCounts[(int)itemToCheck] >= countToCheck;
    }

    public int ItemCount(Items itemToCheck) {
        return itemCounts[(int)itemToCheck];
    }

    public bool CanAdd(Items itemToCheck) {
        return limits[(int)itemToCheck] > itemCounts[(int)itemToCheck];
    }

    public void PickUpHoldItem(HoldItems? item) {
        playerHolds = item;
    }

    private void Start() {
        // Limits for item count
        limits = new int[Enum.GetNames(typeof(Items)).Length];
        itemCounts = new int[limits.Length];

        limits[(int)Items.FISH] = 1;
        limits[(int)Items.FISH_PIECE] = 1;
        limits[(int)Items.WOOD] = 3;
    }
}
