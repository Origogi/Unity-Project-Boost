
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayLoadNextLevel = 1f;
    [SerializeField] float delayReLoadCurrentLevel = 1f;

    private AudioSource audioSource;

    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip fail;

    private bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {

        if (isTransitioning)
        {
            return;
            
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;
            case "Finish":
                Debug.Log("Congrats, you finished!");
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("You picked up fuel");
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
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayLoadNextLevel);
    }


void StartCrashSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash
        isTransitioning = true;        
        audioSource.Stop();
        audioSource.PlayOneShot(fail);

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayReLoadCurrentLevel);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(nextLevel);

    }
}
