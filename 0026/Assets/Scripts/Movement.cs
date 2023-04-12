using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float _mainThrust = 100.0f;
    [SerializeField] float _rotationThrust = 50f;
    [SerializeField] AudioClip _mainEngine;
    Rigidbody _rgbody;
    AudioSource _audioSource;

    void Start()
    {
        _rgbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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
            _rgbody.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
            if (!_audioSource.isPlaying)
                _audioSource.PlayOneShot(_mainEngine);
        } 
        else
            _audioSource.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(_rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            ApplyRotation(-_rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        _rgbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rgbody.freezeRotation = false;
    }
}
