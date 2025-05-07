using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // Asigna el objeto a seguir desde el Inspector
    public Vector3 offset = new Vector3(0, 2, -5); // Ajusta la posición de la cámara

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset; // Sigue al objeto con el offset
            transform.LookAt(target); // Mantiene la cámara mirando al objeto
        }
    }
}