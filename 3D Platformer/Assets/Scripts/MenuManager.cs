using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] Text progressText;

    public UnityAds ads;

    private void Start()
    {
        ads.ShowBanner();
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void ToGame()
    {
        StartCoroutine(LoadAsynchronously("Game"));
        Time.timeScale = 1;
    }
    IEnumerator LoadAsynchronously(string scene)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            float progress = (operation.progress / .9f);
            slider.value = progress;
            progressText.text = ((int)progress * 100f) + " %";
            yield return null;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
