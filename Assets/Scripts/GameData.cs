using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{
    public int playerAmount = 4;
    public int goalScore = 16;
    public SpawnPositionEnum spawnPositionEnum = SpawnPositionEnum.Corner;
}

public enum SpawnPositionEnum
{
    Corner,
    Center,
    Random
}
