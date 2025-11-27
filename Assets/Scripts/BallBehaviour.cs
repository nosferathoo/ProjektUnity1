using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BallBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip ballHitSound, ballJumpSound;
    private Rigidbody rb;
    private Vector2 joystickVector;
    private AudioSource audioSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1f))
        //if (rb.linearVelocity.y == 0)
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            audioSource.PlayOneShot(ballJumpSound);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        audioSource.PlayOneShot(ballHitSound);
    }

    public void OnMove(InputValue value)
    {
        joystickVector = value.Get<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Koniec planszy");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Update()
    {
        rb.AddForce(joystickVector.x, 0, joystickVector.y);

        if (transform.position.y < -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
