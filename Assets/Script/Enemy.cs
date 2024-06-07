using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Enemy : MonoBehaviourPun, IPunObservable
{
    public float speed = 3f;
    public int health = 100;
    private Transform player;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player != null && PhotonNetwork.IsMasterClient)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Enviamos la posición y la salud a otros clientes
            stream.SendNext(transform.position);
            stream.SendNext(health);
        }
        else
        {
            // Recibimos la posición y la salud de otros clientes
            transform.position = (Vector3)stream.ReceiveNext();
            health = (int)stream.ReceiveNext();
        }
    }
}
