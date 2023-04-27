using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem rightEngineParticle;
    [SerializeField] ParticleSystem leftEngineParticle;

    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            startThrusting();
        }
        else
        {
            stopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRatating();
        }
    }

    private void stopThrusting()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }

    private void startThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }
    }

    private void StopRatating()
    {
        rightEngineParticle.Stop();
        leftEngineParticle.Stop();
    }

    private void RotateRight()
    {
        applyRotation(-rotationThrust);
        if (!leftEngineParticle.isPlaying)
        {
            leftEngineParticle.Play();
        }
    }

    private void RotateLeft()
    {
        applyRotation(rotationThrust);
        if (!rightEngineParticle.isPlaying)
        {
            rightEngineParticle.Play();
        }
    }

    void applyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rb.freezeRotation = false;
    }

}
