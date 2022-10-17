using GoogleMobileAds.Api;
using System;
using UnityEngine;


public enum TestMode
{
    TEST,
    DEV
}
public class AdsManager : MonoBehaviour
{
    public TestMode TestMode;
    [Space]
    public static AdsManager instance;
    [Header("Android")]
    [SerializeField] string bannerAndroid = "ca-app-pub-3201044964854975/8357058906";
    [SerializeField] string interstitialAndroid = "ca-app-pub-3201044964854975/2381474733";
    [SerializeField] string videoAndroid = "ca-app-pub-3201044964854975/7037497554";
    [Header("iOS")]
    [SerializeField] string bannerios = "ca-app-pub-7256717599918069/1281342485";
    [SerializeField] string interstitialios = "ca-app-pub-7256717599918069/7655179145";
    [SerializeField] string videoios = "ca-app-pub-7256717599918069/9975038962";
    [Space]
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private BannerView bannerAd;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        // RewardedNewMethod();
        CreateAndLoadRewardedAd();


    }

    private void RewardedNewMethod()
    {
        string adUnitId;
        if (TestMode == TestMode.DEV)
        {

#if UNITY_ANDROID
            adUnitId = videoAndroid;
#elif UNITY_IPHONE
            adUnitId = videoios;
#else
            adUnitId = "unexpected_platform";
#endif
        }
        else
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif    
        }

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

        this.rewardedAd = new RewardedAd(adUnitId);

    }

    private void RequestInterstitial()
    {
        string adUnitId;
        if (TestMode == TestMode.DEV)
        {

#if UNITY_ANDROID
            adUnitId = interstitialAndroid;
#elif UNITY_IPHONE
            adUnitId = interstitialios;
#else
            adUnitId = "unexpected_platform";
#endif
        }
        else
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif
        }

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    #region handlers Intersticial
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {

    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {

    }
    #endregion
    public void ShowIntersticial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    #region RewardedHandler
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        this.CreateAndLoadRewardedAd();

    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        print("received: " + amount + " " + type);

    }
    #endregion
    public void CreateAndLoadRewardedAd()
    {
        string adUnitId;

        if (TestMode == TestMode.DEV)
        {

#if UNITY_ANDROID
            adUnitId = videoAndroid;
#elif UNITY_IPHONE
            adUnitId = videoios;
#else
            adUnitId = "unexpected_platform";
#endif

        }
        else
        {
#if UNITY_ANDROID
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            adUnitId = "unexpected_platform";
#endif
        }
        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }

    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
}
