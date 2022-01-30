using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerUINotification : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private float notificationDuration = 2;

    [Space(20)]
    [SerializeField] private GameObject winningParent;
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text winningText;

    private Coroutine waitCoroutine;

    private void Start()
    {
        Item.ItemInteracted += OnItemInteracted;
    }

    private void OnDestroy()
    {
        Item.ItemInteracted -= OnItemInteracted;
    }

    public void ShowWinningScreen()
    {
        animator.Play("down");
        winningText.text = $"{player.gameObject.name} survived";
        winningParent.SetActive(true);
    }

    private void OnItemInteracted(Item item, Player owner)
    {
        if (owner == player) return;
        switch (item)
        {
            case BackwardItem backwardItem:
                notificationText.text += $"{owner.gameObject.name} uses {backwardItem.name}, You Step Back in Board Game\n";
                break;
            case MoveItemInBoardgame moveItemInBoardgame:
                notificationText.text += $"{owner.gameObject.name} uses {moveItemInBoardgame.name}, {owner.gameObject.name} Step Forward in Board Game\n";
                break;
            case FreezeItem freezeItem:
                notificationText.text += $"{owner.gameObject.name} uses {freezeItem.name}, Cannot move for {freezeItem.FreezeDuration}\n";
                break;
        }
        Wait(notificationDuration, onComplete: () => { notificationText.text = ""; });
    }

    private void Wait(float duration, Action onComplete)
    {
        if (waitCoroutine != null) StopCoroutine(waitCoroutine);
        waitCoroutine = StartCoroutine(WaitIE(duration,onComplete));
    }

    private IEnumerator WaitIE(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);
    }
}
