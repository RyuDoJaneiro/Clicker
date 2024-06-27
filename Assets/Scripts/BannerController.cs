using UnityEngine.Advertisements;
using UnityEngine;

public class BannerController : MonoBehaviour
{
    [SerializeField] private string _androidAdUnitId = "Banner_Android";
    private string _adUnitId = null;

    private void Start()
    {
#if UNITY_ANDROID
        _adsUnitId = _androidAdsUnitId;
#elif UNITY_EDITOR
        _adUnitId = _androidAdUnitId;
#endif
    }

    public void Initialize()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(_adUnitId, options);
    }

    private void OnBannerLoaded()
    {
        Advertisement.Banner.Show(_adUnitId);
        Debug.Log("Banner loaded succesfuly");
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner error: {message}");
    }
}
