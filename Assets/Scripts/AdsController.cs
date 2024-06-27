using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private BannerController bannerController;
    [SerializeField] private InterstitialController interstitialController;
    [SerializeField] private RewardedController rewardedController;
    [SerializeField] string _androidGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    private void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_ANDROID
        _gameId = "5646045";
#elif UNITY_EDITOR
        _gameId = "5646045";
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        bannerController.Initialize();
        interstitialController.Initialize();
        rewardedController.Initialize();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
