using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static event Action<Item,Player> ItemInteracted;

    [SerializeField] private string itemName;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Color color;
    public GameObject currentFloor;

    public void SetupItem()
    {
        renderer.gameObject.name = itemName;
        renderer.material.color = color;
    }

    public void SetCurrentFloor(GameObject currentFloor)
    {
        this.currentFloor = currentFloor;
    }

    public virtual void Interact(Player owner)
    {
        ItemInteracted.Invoke(this, owner);
    }
}
