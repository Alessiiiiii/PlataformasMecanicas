using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // El jugador
    public Vector3 offset = new Vector3(0, 2, -5); // Ajusta según lo que necesites

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}