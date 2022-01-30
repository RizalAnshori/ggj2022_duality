using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeItem : Item
{
    [SerializeField] private float freezeDuration;

    public float FreezeDuration { get { return freezeDuration; } }
}
