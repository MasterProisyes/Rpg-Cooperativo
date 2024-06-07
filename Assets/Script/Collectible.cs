using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Plant, Mineral, Other }
    public CollectibleType type;
    public int value = 1;
    public string itemName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Inventory>().AddItem(itemName, value);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
