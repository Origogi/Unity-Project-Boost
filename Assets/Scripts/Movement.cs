using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rigidbody;
    private AudioSource audioSource;

    [SerializeField] private float mainThrust = 1000f;
    [SerializeField] private float rotationThrust = 50;
    [SerializeField] private AudioClip mainEngine;
    
    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem leftSideEngineParticles;
    [SerializeField] private ParticleSystem rightSideEngineParticles;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotationRight();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotationLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void StartThrusting()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }



    private void StopRotating()
    {
        leftSideEngineParticles.Stop();
        rightSideEngineParticles.Stop();
    }

    private void RotationLeft()
    {
        ApplyRotation(-rotationThrust);

        if (!leftSideEngineParticles.isPlaying)
        {
            leftSideEngineParticles.Play();
        }
    }

    private void RotationRight()
    {
        ApplyRotation(rotationThrust);
        
        if (!rightSideEngineParticles.isPlaying)
        {
            rightSideEngineParticles.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidbody.freezeRotation = false;
    }
}
