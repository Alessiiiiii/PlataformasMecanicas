using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // El jugador
    public Vector3 offset = new Vector3(0, 3, -6); // Puedes ajustar esto
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        // Posici�n deseada
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Suavizado de la posici�n
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Hacer que mire al jugador solo si quer�s (opcional, puede sacarse)
        // transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
