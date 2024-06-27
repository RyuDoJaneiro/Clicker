using UnityEngine.Advertisements;
using UnityEngine;

public class RewardedController : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] ButtonController _buttonController;
    [SerializeField] private bool _adLoaded;
    private string _adUnitId = null;

    void Start()
    {
#if UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#elif UNITY_EDITOR
        _adUnitId = _androidAdUnitId;
#endif
    }

    public void Initialize()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        _adLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Starting showing ad");
        if (_adLoaded)
            _buttonController.PowerUp();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Clicking ad");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (_adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            // Por alguna razón no lo toma por completo
            if (_adLoaded)
                _buttonController.PowerUp();
        }
    }
}
