using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerUINotification playerUINotification;
    [SerializeField] private GameObject exitPortal;
    private int goalScore;
    private int currentScore=0;

    public int GoalScore { set { goalScore = value; } }
    public PlayerView PlayerView { get { return playerView; } }

    private void Start()
    {
        Item.ItemInteracted += OnItemInteracted;
    }

    private void OnDestroy()
    {
        Item.ItemInteracted -= OnItemInteracted;
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactedItem = other.GetComponentInParent<Item>();

        if (interactedItem != null)
        {
            if (interactedItem is ExitPortal && other.gameObject == this.exitPortal)
            {
                playerUINotification.ShowWinningScreen();
                return;
            }
            interactedItem.Interact(this);
        }
    }

    private void OnItemInteracted(Item item, Player owner)
    {
        if (item is BackwardItem)
        {
            if(owner != this)
            {
                currentScore=Mathf.Max(0,currentScore--);
                Debug.Log("Interacted Item Back");
            }
        }
        else if (item is MoveItemInBoardgame)
        {
            if(owner == this)
            {
                currentScore++;
                Debug.Log("Interacted Item Forward");
            }
        }

        if(currentScore >= goalScore)
        {
            exitPortal.SetActive(true);
        }
    }
}
