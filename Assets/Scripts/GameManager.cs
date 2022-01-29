using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private MazeSpawner mazeSpawner;
    [SerializeField] private Player[] players;

    // Start is called before the first frame update
    void Start()
    {
        mazeSpawner.StartGenerateMaze(onCompleted:SetupPlayer);
    }

    private void SetupPlayer()
    {
        List<GameObject> placementList = new List<GameObject>();

        placementList.AddRange(gameData.spawnPlayerAtCorner ? mazeSpawner.cornerFloors : mazeSpawner.floors);

        print(gameData.playerAmount);

        for(int i = 0; i<gameData.playerAmount;i++)
        {
            GameObject placement = placementList[Random.Range(0, placementList.Count)];
            //Player player = Instantiate(playerPrefab, new Vector3(placement.transform.position.x, playerPrefab.transform.position.y, placement.transform.position.z), Quaternion.identity);
            Player player = players[i];
            player.gameObject.SetActive(true);
            player.transform.position = new Vector3(placement.transform.position.x, player.transform.position.y, placement.transform.position.z);
            player.PlayerView.SetupCamera(i+1,gameData.playerAmount);
            placementList.Remove(placement);
        }

        //placementList.Clear();
    }
}
