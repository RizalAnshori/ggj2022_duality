using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    public enum MazeGenerationAlgorithm
    {
        PureRecursive
    }
    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;
    public int Rows = 5;
    public int Columns = 5;
    public float CellWidth = 5;
    public float CellHeight = 5;
    public bool AddGaps = false;
    public GameObject GoalPrefab = null;
    public List<GameObject> floors = new List<GameObject>();
    public List<GameObject> nonRamdomTargetPlacements = new List<GameObject>();
    private BasicMazeGenerator mMazeGenerator = null;

    // Start is called before the first frame update
    public void StartGenerateMaze(Action onCompleted = null)
    {
        if (!FullRandom)
        {
            Random.InitState(RandomSeed);
        }
        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeAlgorithm(Rows, Columns);
                break;
        }
        mMazeGenerator.GenerateMaze();
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                var floor = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                floor.name = $"floor: {row},{column}";
                floors.Add(floor);
                if (gameData.spawnPositionEnum == SpawnPositionEnum.Corner)
                {
                    if (IsCorner(row, column))
                    {
                        nonRamdomTargetPlacements.Add(floor);
                    }
                }
                else if (gameData.spawnPositionEnum == SpawnPositionEnum.Center)
                {
                    if(column == Columns/2 && row == Rows/2)
                    {
                        nonRamdomTargetPlacements.Add(floor);
                    }
                }

                floor.transform.parent = transform;
                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
                    tmp.transform.parent = floor.transform;
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
                    tmp.transform.parent = floor.transform;
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
                    tmp.transform.parent = floor.transform;
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
                    tmp.transform.parent = floor.transform;
                }
                if (cell.IsGoal && GoalPrefab != null)
                {
                    tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                    tmp.transform.parent = transform;
                }
            }
        }
        if (Pillar != null)
        {
            for (int row = 0; row < Rows + 1; row++)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = column * (CellWidth + (AddGaps ? .2f : 0));
                    float z = row * (CellHeight + (AddGaps ? .2f : 0));
                    GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2), Pillar.transform.rotation) as GameObject;
                    tmp.transform.parent = transform;
                }
            }
        }
        if(gameData.spawnPositionEnum == SpawnPositionEnum.Center)
        {
            for(int i = 0; i < nonRamdomTargetPlacements[0].transform.childCount;i++)
            {
                nonRamdomTargetPlacements[0].transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        onCompleted?.Invoke();
    }

    private bool IsCorner(int row, int column)
    {
        if(row == 0 && column == 0)
        {
            return true;
        }
        else if(row == Rows-1 && column == Columns-1)
        {
            return true;
        }
        else if(row == 0 && column == Columns-1)
        {
            return true;
        }
        else if(row == Rows-1 && column == 0)
        {
            return true;
        }
        return false;
    }
}
