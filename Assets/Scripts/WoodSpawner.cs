using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSpawner : MonoBehaviour {

    [SerializeField] GameObject woodPrefab;

    public float SecondsSpawn = 3f;
    public short Slots = 3;
    public float SpawnRadius = 10f;
    public float NoSpawnRadius = 1f;
    public float WoodClearance = 0.5f;

    private short usedSlots = 0;
    private List<GameObject> spawnedWood = new List<GameObject>();
    private bool spawningWood = false;

    public void RemoveWoodDrop(GameObject go) {
        if (!spawnedWood.Remove(go)) {
            return;
        }
        
        usedSlots--;
    }

    // Use this for initialization
    private void Start () {
        if (SpawnRadius < float.Epsilon || SpawnRadius < NoSpawnRadius) {
            Application.Quit();
        }

        while (usedSlots < Slots) {
            SpawnWood();
        }
	}

    private void Update() {
        if (!spawningWood && usedSlots < Slots) {
            StartCoroutine(InitSpawning());
        }
    }

    private void SpawnWood() {
        Vector2 pos;
        float mag;
        bool freeSpace;
        do {
            do {
                pos = Random.insideUnitCircle * SpawnRadius;
                mag = pos.magnitude;
            } while (mag < float.Epsilon && mag > -float.Epsilon);
            
            pos *= mag < NoSpawnRadius ? Random.Range(NoSpawnRadius, SpawnRadius) / mag : 1f;
            freeSpace = true;
            foreach (var w in spawnedWood) {
                if (Vector3.Distance(w.transform.position, new Vector3(pos.x, 0f, pos.y)) < WoodClearance) {
                    freeSpace = false;
                    break;
                }
            }
        } while (!freeSpace);
       
        var go = Instantiate(woodPrefab, this.transform.position + new Vector3(pos.x, 0, pos.y), Quaternion.identity, transform);
        spawnedWood.Add(go);
        var woodDrop = go.GetComponent<WoodDrop>();
        woodDrop.ParentSpawner = this;
        usedSlots++;
    }

    private IEnumerator InitSpawning() {
        spawningWood = true;
        yield return new WaitForSeconds(SecondsSpawn);
        SpawnWood();
        spawningWood = false;
    }
}
