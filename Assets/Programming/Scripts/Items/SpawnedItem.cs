using System;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    public Item item;

    private float rotateSpeed = 40f;

    void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!PlayerInventory.Instance.TryAddItemToInventory(item))
            return;

        Destroy(gameObject);
    }
}
