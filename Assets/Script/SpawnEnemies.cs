using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameController : MonoBehaviourPun
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            PhotonNetwork.Instantiate(enemyPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
