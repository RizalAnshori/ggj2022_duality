using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private MazeSpawner mazeSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private Player[] players;
    private List<GameObject> placementList = new List<GameObject>();
    List<GameObject> itemPlacementList = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        mazeSpawner.StartGenerateMaze(onCompleted:SetupPlayer);
    }

    private void SetupPlayer()
    {
        itemPlacementList.AddRange(mazeSpawner.floors);
        placementList.AddRange(gameData.spawnPositionEnum == SpawnPositionEnum.Random ? mazeSpawner.floors : mazeSpawner.nonRamdomTargetPlacements);

        for(int i = 0; i<gameData.playerAmount;i++)
        {
            GameObject placement = placementList[Random.Range(0, placementList.Count)];
            //Player player = Instantiate(playerPrefab, new Vector3(placement.transform.position.x, playerPrefab.transform.position.y, placement.transform.position.z), Quaternion.identity);
            Player player = players[i];
            player.GoalScore = gameData.goalScore;
            player.gameObject.SetActive(true);
            player.PlayerView.SetupCamera(i+1,gameData.playerAmount);
            if (gameData.spawnPositionEnum != SpawnPositionEnum.Center)
            {
                player.transform.position = new Vector3(placement.transform.position.x, player.transform.position.y, placement.transform.position.z);
                placementList.Remove(placement);
            }
            else
            { 
                var targetPos = new Vector3(placement.transform.position.x, player.transform.position.y, placement.transform.position.z);
                float playerGap = .5f;
                switch (i)
                {
                    case 0:
                        targetPos = new Vector3(placement.transform.position.x - playerGap, player.transform.position.y, placement.transform.position.z - playerGap);
                        break;
                    case 1:
                        targetPos = new Vector3(placement.transform.position.x + playerGap, player.transform.position.y, placement.transform.position.z + playerGap);
                        break;
                    case 2:
                        targetPos = new Vector3(placement.transform.position.x + playerGap, player.transform.position.y, placement.transform.position.z - playerGap);
                        break;
                    case 3:
                        targetPos = new Vector3(placement.transform.position.x - playerGap, player.transform.position.y, placement.transform.position.z + playerGap);
                        break;
                }
                player.transform.position = targetPos;
            }
            itemPlacementList.Remove(placement);
        }

        itemSpawner.SetupItem(itemPlacementList);
        //placementList.Clear();
    }
}
