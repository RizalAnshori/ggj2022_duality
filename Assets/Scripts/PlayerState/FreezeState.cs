using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeState : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerMovement playerMovement;
    private Coroutine waitCanMoveCoroutine;

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
        if(item is FreezeItem && owner != player)
        {
            var freezeItem = item as FreezeItem;
            playerMovement.CanMove = false;
            if (waitCanMoveCoroutine != null) StopCoroutine(waitCanMoveCoroutine);
            waitCanMoveCoroutine = StartCoroutine(WaitIE(freezeItem.FreezeDuration, () => 
            {
                playerMovement.CanMove = true;
            }));
        }
    }

    private IEnumerator WaitIE(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);
        onComplete?.Invoke();
    }
}
