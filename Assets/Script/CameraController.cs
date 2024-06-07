using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // El objetivo a seguir
    public float mouseSensitivity = 100f; // Sensibilidad del mouse
    public float distanceFromTarget = 5f; // Distancia de la cámara al objetivo
    public Vector2 pitchMinMax = new Vector2(-40, 85); // Restricciones para el ángulo de pitch

    private float yaw;
    private float pitch;

    void Start()
    {
        // Ocultar y bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Rotar la cámara con el mouse
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

            // Calcular la nueva posición de la cámara
            Vector3 direction = new Vector3(0, 0, -distanceFromTarget);
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            transform.position = target.position + rotation * direction;

            // Apuntar la cámara al objetivo
            transform.LookAt(target.position);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
