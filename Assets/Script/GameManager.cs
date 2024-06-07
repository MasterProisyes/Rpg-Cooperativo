using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        // Asegúrate de que el jugador solo se genera cuando está listo y conectado a la sala
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.InRoom)
            {
                SpawnPlayer();
            }
        }
    }

    public override void OnJoinedRoom()
    {
        // Llamado automáticamente cuando el jugador se une a una sala
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            // Selecciona un punto de spawn basado en el número de actor del jugador
            Transform spawnPoint = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber % spawnPoints.Length];
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("Player prefab is not set in GameManager");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Lógica para manejar cuando otro jugador entra en la sala, si es necesario
        Debug.Log($"{newPlayer.NickName} joined the room.");
    }
}
