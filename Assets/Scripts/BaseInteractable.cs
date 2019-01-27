using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour {
	public Transform center;
     public abstract void Interact(PlayerInventory playerInventory, PlayerMiniGameManager miniGameManager);
}
