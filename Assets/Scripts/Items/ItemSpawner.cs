using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct InitialItemSpawn
    {
        public Item Item;
        public int amountInMaze;
    }

    [SerializeField] private InitialItemSpawn[] itemPrefabs;
    [SerializeField] private List<GameObject> floors = new List<GameObject>();
    [SerializeField] private GameObject[] exitPortals;

    public void SetupItem(List<GameObject> floors)
    {
        this.floors.AddRange(floors);
        SpawnItem();
        SetupExitGate();
    }

    private void Start()
    {
        Item.ItemInteracted += OnItemInteracted;
    }

    private void OnDestroy()
    {
        Item.ItemInteracted -= OnItemInteracted;
    }

    private void OnItemInteracted(Item item, Player owner)
    {
        var targetPlacement = floors[Random.Range(0, floors.Count)];
        var prevFloor = item.currentFloor;
        item.gameObject.transform.position = targetPlacement.transform.position;
        item.SetCurrentFloor(targetPlacement);
        floors.Remove(targetPlacement);
        floors.Add(prevFloor);
    }

    private void SpawnItem()
    {
        foreach(InitialItemSpawn initialItemSpawn in itemPrefabs)
        {
            for(int i =0; i < initialItemSpawn.amountInMaze; i++)
            {
                var targetPlacement = floors[Random.Range(0, floors.Count)];
                var itemObj = Instantiate(initialItemSpawn.Item, targetPlacement.transform.position, Quaternion.identity);
                itemObj.SetupItem();
                itemObj.SetCurrentFloor(targetPlacement);
                floors.Remove(targetPlacement);
            }
        }
    }

    private void SetupExitGate()
    {
        var tempList = new List<GameObject>();
        tempList.AddRange(floors);
        foreach(var exitPortal in exitPortals)
        {
            var targetPlacement = tempList[Random.Range(0, tempList.Count)];
            exitPortal.transform.position = targetPlacement.transform.position;
            tempList.Remove(targetPlacement);
        }
    }
}
