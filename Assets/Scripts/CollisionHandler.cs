using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2F;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    AudioSource audioSource;
    bool isTransitioning = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        // Tratar as colisões com os diferentes objectos
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Touched Friendly!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        // TODO add particle effect upon success
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        // TODO add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }
    void ReloadLevel()
    {
        // Recarregar a cena em caso de bater num obstaculo
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        // Calcular próxima cena
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        // Caso estejamos na ultima cena, voltar para o inicio
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        // Carregar a cena correcta
        SceneManager.LoadScene(nextSceneIndex);
    }

}
