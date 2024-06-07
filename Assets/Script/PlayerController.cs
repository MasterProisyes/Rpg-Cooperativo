using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2f;
    private Rigidbody rb;
    private bool isGrounded = true;
    public GameObject playerCamera; // Referencia a la cámara del jugador

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the player.");
        }

        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned in the PlayerController script.");
        }

        // Activar la cámara solo si es el jugador local
        if (photonView.IsMine)
        {
            playerCamera.SetActive(true);
            CameraController cameraController = playerCamera.GetComponent<CameraController>();
            if (cameraController != null)
            {
                cameraController.SetTarget(transform);
            }
            else
            {
                Debug.LogError("CameraController component is missing from the player camera.");
            }
        }
        else
        {
            playerCamera.SetActive(false);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            Vector3 direction = targetRotation * movement;
            Vector3 move = direction * speed * Time.deltaTime;
            rb.MovePosition(transform.position + move);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            Vector3 extraGravityForce = (Physics.gravity * gravityMultiplier) - Physics.gravity;
            rb.AddForce(extraGravityForce);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Enviamos la posición a otros clientes
            stream.SendNext(rb.position);
        }
        else
        {
            // Recibimos la posición de otros clientes
            rb.position = (Vector3)stream.ReceiveNext();
        }
    }
}
