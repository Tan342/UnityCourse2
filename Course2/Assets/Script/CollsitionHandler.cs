using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollsitionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable)
        {
            return;
        }

        if (other.gameObject.tag == "Friendly")
        {

        }
        else if (other.gameObject.tag == "Finish")
        {
            StartSuccessSequence();
        }
        else if (other.gameObject.tag == "Fuel")
        {

        }
        else
        {
            StartCrashSequence();
        }
    }

    void StartSuccessSequence()
    {
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        isTransitioning = true;
        successParticle.Play();
    }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        isTransitioning = true;
        crashParticle.Play();
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
