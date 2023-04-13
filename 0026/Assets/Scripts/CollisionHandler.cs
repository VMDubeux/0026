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
    //bool collisionDisable = true;

    private void Start()
    {
        _2audioSource = GetComponent<AudioSource>();
        _movement = GetComponent<Movement>();
    }

    /*void Update()
    {
        DisableCollision();
    }

    void DisableCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
            Debug.Log("Mexeeu");
        }
    }*/

    private void OnCollisionEnter(Collision collision)
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
    private void OnTriggerEnter(Collider collided)
    {
        collided.gameObject.SetActive(true);
    }

    private void StartVictorySequence()
    {
        isTransitioning = true;
        AudioStartVictorySeq();
        _movement.enabled = false;
        Invoke("LoadNextScene", 2f);
    }

    private void AudioStartVictorySeq()
    {
        _2audioSource.Stop();
        _particleSuccess.Play();
        if (!_2audioSource.isPlaying)
            _2audioSource.PlayOneShot(_soundSuccess);
    }

    private void LoadNextScene()
    {
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int _nextSceneIndex = ++_currentSceneIndex;
        if (_nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            _nextSceneIndex = 0;
        }
        SceneManager.LoadScene(_nextSceneIndex);
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        _particleFailure.Play();
        AudioStartCrashSeq();
        _movement.enabled = false;
        InvokeStartCrashSeq();

    }

    private void AudioStartCrashSeq()
    {
        _2audioSource.Stop();
        if (!_2audioSource.isPlaying)
            _2audioSource.PlayOneShot(_soundFailure);
    }

    private void InvokeStartCrashSeq()
    {
        Invoke("Disappear", 0.5f);
        Invoke("ReloadCurrentScene", 1.5f);
    }

    private void Disappear()
    {
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
    }

    private void ReloadCurrentScene()
    {
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (_currentSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            _currentSceneIndex = 0;
        }
        SceneManager.LoadScene(_currentSceneIndex);
    }
}
