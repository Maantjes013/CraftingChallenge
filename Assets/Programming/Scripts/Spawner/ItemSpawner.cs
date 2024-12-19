using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoSingleton<ItemSpawner>
{
    public List<Item> items;

    [SerializeField]
    private SpawnedItem itemPrefab;   
    [SerializeField]
    private List<Transform> spawnPositions;
    [SerializeField]
    private float spawnIntervalMin = 5f;
    [SerializeField]
    private float spawnIntervalMax = 10f;

    private Transform lastSpawnPoint;

    void Start()
    {
        StartCoroutine(SpawnResources());
    }

    IEnumerator SpawnResources()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

            List<Transform> possibleSpawns = spawnPositions.Where(x => x.childCount == 0).ToList();
            if (possibleSpawns.Count != 0)
            {
                Transform spawnTransform = possibleSpawns[Random.Range(0, possibleSpawns.Count)];

                // Item cannot spawn on same position twice in a row to make sure player does not farm items in 1 spot.
                if (!spawnTransform.Equals(lastSpawnPoint))
                {
                    Item itemToSpawn = items[Random.Range(0, items.Count)];      

                    SpawnedItem spawnedItem = Instantiate(itemPrefab, spawnTransform.position, Quaternion.identity, spawnTransform);

                    spawnedItem.GetComponent<MeshFilter>().mesh = itemToSpawn.itemMesh;
                    spawnedItem.GetComponent<MeshRenderer>().material = itemToSpawn.itemMaterial;                   
                    spawnedItem.item = (Item)itemToSpawn.Clone();
                    spawnedItem.item.itemID = System.Guid.NewGuid().ToString();

                    lastSpawnPoint = spawnTransform;
                }
            }
        }
    }
}
