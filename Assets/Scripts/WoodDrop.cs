using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDrop : MonoBehaviour {
    public WoodSpawner ParentSpawner { get; set; }

    public void PickUp() {
        ParentSpawner.RemoveWoodDrop(gameObject);
    }
}
