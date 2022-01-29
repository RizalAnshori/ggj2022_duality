using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerMovement playerMovement;

    public PlayerView PlayerView { get { return playerView; } }

}
