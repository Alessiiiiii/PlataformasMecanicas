using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class rigidbody : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Vector2 inputVector;
    public Vector3 velocity;
    public Rigidbody Rigidbody;
    public bool CanJump;
    public int collectedItems;
    public float velocityMagnitude;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI warningText;

    // Start is called before the first frame update
    void Start()
    {
        warningText.enabled = false;
        Rigidbody = GetComponent<Rigidbody>();
        CanJump = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Obtener direcciones de la cámara
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Evitar movimiento en el eje Y para no inclinar el jugador
        forward.y = 0f;
        right.y = 0f;

        // Obtener el input del jugador
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        // Transformar la dirección del movimiento según la cámara
        Vector3 moveDirection = (forward * inputVector.y + right * inputVector.x).normalized;

        // Aplicar fuerza de movimiento
        Rigidbody.AddForce(moveDirection * speed, ForceMode.Impulse);

        // Rotar el jugador en la dirección del movimiento
        if (moveDirection.magnitude > 0.1f && Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }


        // 🚀 **Mueve el código del salto dentro de `FixedUpdate()`**
        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            Rigidbody.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            CanJump = false;
        }
    

}
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Choque cotra : " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Piso"))
        {
            CanJump = true;
        }

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("Enemigo");
            SceneManager.LoadScene(0);
        }
        if (collision.gameObject.CompareTag("Win"))
        {
            SceneManager.LoadScene("Winner");
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);

            collectedItems++;
            scoreText.text = collectedItems.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter:" + other.gameObject.name);
        if (other.gameObject.CompareTag("Item"))
        {
            Destroy(other.gameObject);
            collectedItems++;
            scoreText.text = collectedItems.ToString();
        }
        if (other.gameObject.CompareTag("KillZone"))
        {
            warningText.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit:" + other.gameObject.name);


        if (other.gameObject.CompareTag("KillZone"))
        {
            warningText.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Stay:" + other.gameObject.name);
    }

}
