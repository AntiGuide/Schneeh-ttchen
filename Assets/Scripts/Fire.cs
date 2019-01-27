using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : BaseInteractable {
    [SerializeField] FireZone firezone;

    [SerializeField]
    private float fireFadeModifierPerHole;

    [SerializeField]
    private int aktWoodCount = 2;

    [SerializeField]
    private int fireTimerSecondsPerWoodLog = 30;

    private float fireTimerInSeconds = 60f;

    private GameObject[] holes = new GameObject[3];

    // Use this for initialization
    void Start()
    {
        fireTimerInSeconds = fireTimerSecondsPerWoodLog;
    }

    // Update is called once per frame
    void Update()
    {
        if (firezone.fireBurning)
        {
            if (holes.Length == 0)
            {
                fireTimerInSeconds -= Time.deltaTime;
            }
            else
            {
                fireTimerInSeconds -= Time.deltaTime * (fireFadeModifierPerHole * holes.Length);
            }
        }

        if (fireTimerInSeconds < 0)
        {
            if (aktWoodCount == 0)
            {
                firezone.fireBurning = false;
            }
            else
            {
                aktWoodCount--;
                fireTimerInSeconds = fireTimerSecondsPerWoodLog;
            }
        }

        if (!firezone.fireBurning && aktWoodCount > 0)
            firezone.fireBurning = true;
      
    }

  

    public override void Interact(PlayerInventory playerInventory, PlayerMiniGameManager miniGameManager) {
        if (!playerInventory.RemoveItem(PlayerInventory.Items.WOOD)) {
            return;
        }
        
        aktWoodCount++;
        if (aktWoodCount == 1) {
            firezone.fireBurning = true;
            fireTimerInSeconds = fireTimerSecondsPerWoodLog;
        }
    }
}