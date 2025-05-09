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
    void Update()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        Rigidbody.AddForce(inputVector.x * speed, 0f, inputVector.y * speed, ForceMode.Impulse);

        velocity = Rigidbody.linearVelocity;
        

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

        if (collision.gameObject.CompareTag("KillZone"))
        {
            Debug.Log("Kill Mee");
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
        if (other.gameObject.CompareTag("Win"))
        {
            warningText.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit:" + other.gameObject.name);


        if (other.gameObject.CompareTag("Win"))
        {
            warningText.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Stay:" + other.gameObject.name);
    }

}
