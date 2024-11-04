using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    public Animator fadeAnimator; // Assign this in the Inspector to the Image Animator
    private static readonly int FadeOutTrigger = Animator.StringToHash("FadeOut");
    private static readonly int FadeInTrigger = Animator.StringToHash("FadeIn");
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep SceneManager between scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
        
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Start the fade-out animation
        fadeAnimator.SetTrigger(FadeOutTrigger);

        // Wait until the fade-out animation is complete (assuming 1 second)
        yield return new WaitForSeconds(1.0f);
        fadeAnimator.ResetTrigger(FadeOutTrigger);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        if(sceneName == "Level1")
        {
            Player.instance.transform.position = new Vector3(2.45f, 0.13f, 0);
        }
        else if(sceneName == "Lobby")
        {
            Player.instance.transform.position = new Vector3(-1.5f, -2.0f, 0);
        }
        // Start the fade-in animation
        fadeAnimator.SetTrigger(FadeInTrigger);

        yield return new WaitForSeconds(1.0f);
        fadeAnimator.ResetTrigger(FadeInTrigger);
    }

    public void FadeSceneWithoutLoad(bool changeDay)
    {
        StartCoroutine(FadeScene(changeDay));
    }

    private IEnumerator FadeScene(bool changeDay)
    {
        // Start the fade-out animation
        fadeAnimator.SetTrigger(FadeOutTrigger);
        // Wait until the fade-out animation is complete (assuming 1 second)
        yield return new WaitForSeconds(1.0f);
        fadeAnimator.ResetTrigger(FadeOutTrigger);
        if(changeDay)
        {
            DayNightCycle.instance.UpdateLights();
        }
        // Start the fade-in animation
        fadeAnimator.SetTrigger(FadeInTrigger);
        yield return new WaitForSeconds(1.0f);
        fadeAnimator.ResetTrigger(FadeInTrigger);
        
    }
}
