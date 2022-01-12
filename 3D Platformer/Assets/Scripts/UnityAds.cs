using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityAds : MonoBehaviour, IUnityAdsListener
{
    string gameId = "4172171";
    string bannerId = "banner";
    string interstitialId = "interstitial";
    string rewardedId = "rewardedVideo";

    public bool testMode;
    public string sceneName;
    void Start()
    {

        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);
    }

    public void ShowInterstitial(string name)
    {
        if (Advertisement.IsReady(interstitialId))
        {
            sceneName = name;
            Advertisement.Show(interstitialId);
        }
    }

    public void ShowBanner()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show();
    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(rewardedId);
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }


    public void OnUnityAdsReady(string placementID)
    {
        if (placementID == bannerId)
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Show();
        }
    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementID)
    {

    }

    public void OnUnityAdsDidFinish(string placementID, ShowResult showResult)
    {
        if (placementID == rewardedId)
        {
            if (showResult == ShowResult.Finished)
            {
                SceneManager.LoadScene("Game");
            }
            else if (showResult == ShowResult.Skipped)
            {
                SceneManager.LoadScene("Game");
            }
            else if (showResult == ShowResult.Failed)
            {
                SceneManager.LoadScene("Game");
            }
        }
        if (placementID == interstitialId)
        {
            if (showResult == ShowResult.Finished)
            {
                SceneManager.LoadScene(sceneName);
                Time.timeScale = 1;
            }
            else if (showResult == ShowResult.Skipped)
            {
                SceneManager.LoadScene(sceneName);
                Time.timeScale = 1;
            }
            else if (showResult == ShowResult.Failed)
            {
                SceneManager.LoadScene(sceneName);
                Time.timeScale = 1;
            }
        }
    }
    void Update()
    {

    }
}
