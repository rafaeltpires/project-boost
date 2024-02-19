using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    
    [SerializeField] private AudioClip lose;
    [SerializeField] private AudioClip success;
    
    [SerializeField] ParticleSystem loseParticles;
    [SerializeField] ParticleSystem successParticles;
    
    private AudioSource mAudioSource;

    private bool sceneFinished = false;

    private void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
        successParticles = GameObject.Find("Success Particles").GetComponent<ParticleSystem>();
        loseParticles = GameObject.Find("Explosion Particles").GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                break;
            case "Friendly":
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        if(!sceneFinished) {
            mAudioSource.PlayOneShot(success);
            successParticles.Play();
            sceneFinished = true;
        }
        
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(NextLevel), levelLoadDelay);
    }

    
    private void StartCrashSequence()
    {
        if(!sceneFinished) {
            mAudioSource.PlayOneShot(lose);
            loseParticles.Play();
            sceneFinished = true;
        }
        
        //todo: particales fx
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }
    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;
        // Se estivermos na ultima cena, voltamos para a primeira.
        if (nextIndex == SceneManager.sceneCountInBuildSettings) nextIndex = 0;
        
        SceneManager.LoadScene(nextIndex);
    }
}
