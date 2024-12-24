using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManagerInterstitial : MonoBehaviour
{
    // Singleton instance
    public static AdManagerInterstitial Instance { get; private set; }

    private InterstitialAd interstitialAd;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Makes the AdManager persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    private void Start()
    {
        RequestInterstitialAd();
    }
    public void RequestInterstitialAd()
    {
        // Replace with your own Ad Unit ID
        string adUnitId = "ca-app-pub-2171866555386370/3326234704"; // Test ad unit ID

        // Request the ad
        AdRequest adRequest = new AdRequest();

        InterstitialAd.Load(adUnitId, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              interstitialAd = ad;
              // Subscribe to the OnAdFullScreenContentClosed event
              interstitialAd.OnAdFullScreenContentClosed += HandleOnAdClosed;
              interstitialAd.OnAdFullScreenContentClosed += () =>
              {
                  Debug.Log("Interstitial Ad full screen content closed.");

                  // Reload the ad so that we can show another as soon as possible.
                  interstitialAd.Destroy();
                  RequestInterstitialAd();
              };
              // Raised when the ad failed to open full screen content.
              interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
              {
                  Debug.LogError("Interstitial ad failed to open full screen content " +
                                 "with error : " + error);

                  // Reload the ad so that we can show another as soon as possible.
                  RequestInterstitialAd();
              };
          });

       
    }

    // Method to be called when the interstitial ad is closed
    private void HandleOnAdClosed()
    {
        Debug.Log("Interstitial Ad closed");

        // Optionally, you can request another ad after the previous one closes
        interstitialAd.Destroy(); // Clean up the ad
        RequestInterstitialAd();  // Request a new ad if necessary
    }

    // Public method to show the interstitial ad
    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial ad is not ready.");
        }
    }
}

