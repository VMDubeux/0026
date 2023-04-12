using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip _soundSuccess;
    [SerializeField] AudioClip _soundFailure;
    [SerializeField] ParticleSystem _particleSuccess;
    [SerializeField] ParticleSystem _particleFailure;
    AudioSource _2audioSource;
    Movement _movement;
    bool isTransitioning = false;

    void Start()
    {
        _2audioSource = GetComponent<AudioSource>();
        _movement = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly tagged");
                    break;
                case "Fuel":
                    Debug.Log("Fuel tagged");
                    break;
                case "Finish":
                    StartVictorySequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }
    void OnTriggerEnter(Collider collided)
    {
        collided.gameObject.SetActive(true);
    }

    void StartVictorySequence() 
    {
        isTransitioning = true;
        _2audioSource.Stop();
        _particleSuccess.Play();
        if (!_2audioSource.isPlaying)
            _2audioSource.PlayOneShot(_soundSuccess);
        _movement.enabled = false;
        Invoke ("LoadNextScene", 2f);
    }

    void LoadNextScene() 
    {
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int _nextSceneIndex = ++_currentSceneIndex;
        if (_nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        { 
            _nextSceneIndex = 0;
        }
        SceneManager.LoadScene(_nextSceneIndex);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        _2audioSource.Stop();
        _particleFailure.Play();
        if (!_2audioSource.isPlaying)
            _2audioSource.PlayOneShot(_soundFailure);
        _movement.enabled = false;
        Invoke("ReloadCurrentScene", 1f);
    }

    void ReloadCurrentScene()
    {
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (_currentSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            _currentSceneIndex = 0;
        }
        SceneManager.LoadScene(_currentSceneIndex);
    }
}
