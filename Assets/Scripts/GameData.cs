using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{
    public int playerAmount = 4;
    public bool spawnPlayerAtCorner = true;
}
