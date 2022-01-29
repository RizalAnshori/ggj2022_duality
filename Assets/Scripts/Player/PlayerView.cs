using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Camera camera;

    public void SetupCamera(int playerOrder, int maxPlayer)
    {
        camera.rect = maxPlayer == 4 ? Setup4Player(playerOrder) : Setup2Player(playerOrder);
    }

    private Rect Setup4Player(int playerOrder)
    {
        return playerOrder switch
        {
            1 => new Rect(0, 0, 0.497f, 0.495f),
            2 => new Rect(0.503f, 0, 0.497f, 0.495f),
            3 => new Rect(0.503f, 0.505f, 0.497f, 0.495f),
            4 => new Rect(0, 0.505f, 0.497f, 0.495f),
            _ => new Rect(0, 0, 0, 0),
        };
    }

    private Rect Setup2Player(int playerOrder)
    {
        return playerOrder switch
        {
            1 => new Rect(0, 0, 0.497f, 1f),
            2 => new Rect(0.503f, 0, 0.497f, 1f),
            _ => new Rect(0, 0, 0, 0),
        };
    }
}
