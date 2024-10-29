// using static Define;

// using System;
// using UnityEngine;
// using GoogleMobileAds.Api;
// using System.Collections;
// using System.ComponentModel;
// using System.Collections.Generic;

// public class AdManager
// {
//   private RewardedAd rewardedAd;
//   private BannerView bannerView;

//   public bool isShowAd = false;
//   public bool isInitialized = false;

// #if !UNITY_EDITOR && UNITY_IOS
//   private readonly Dictionary<EAdType, string> unitIds = new()
//   {
//     {EAdType.Minute, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.GameOver, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Diamond, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Coin, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Potion, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Heart, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Quest, "ca-app-pub-3940256099942544/5224354917"},
//   };
// #elif !UNITY_EDITOR && UNITY_ANDROID
//   private readonly Dictionary<EAdType, string> unitIds = new()
//   {
//     {EAdType.Minute, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.GameOver, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Diamond, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Coin, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Potion, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Heart, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Quest, "ca-app-pub-3940256099942544/5224354917"},
//   };
// #else
//   private readonly Dictionary<EAdType, string> unitIds = new()
//   {
//     {EAdType.Minute, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.GameOver, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Diamond, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Coin, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Potion, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Heart, "ca-app-pub-3940256099942544/5224354917"},
//     {EAdType.Quest, "ca-app-pub-3940256099942544/5224354917"},
//   };
// #endif

//   public void Init()
//   {
//     MobileAds.Initialize(initStatus => { });
//     MobileAds.RaiseAdEventsOnUnityMainThread = true;
//     isInitialized = true;

//     // Reward Ad 목록 가져와서 초기화
//     foreach (var unitId in unitIds)
//     {
//       LoadRewardedAd(unitId.Value, null);
//     }
//   }

//   private void LoadRewardedAd(string adUnitId, Action onLoaded = null)
//   {
//     DestroyRewardedAd();
//     RewardedAd.Load(adUnitId, new AdRequest(), (RewardedAd ad, LoadAdError loadError) =>
//     {
//       if (loadError != null)
//         return;

//       rewardedAd = ad;

//       rewardedAd.OnAdFullScreenContentOpened += HandleOnOpenDefault;
//       rewardedAd.OnAdFullScreenContentClosed += () => HandleOnCloseDefault(adUnitId);
//       rewardedAd.OnAdFullScreenContentFailed += (AdError adError) => HandleOnFailedDefault(adUnitId, adError);

//       onLoaded?.Invoke();
//     });
//   }

//   public void ShowRewardedAd(string unitId, Action rewardAction, Action failedAction = null)
//   {
//     if (rewardedAd == null)
//     {
//       LoadRewardedAd(unitId, () =>
//       {
//         _ShowRewardedAd(rewardAction, failedAction);
//       });
//     }
//     else
//       _ShowRewardedAd(rewardAction, failedAction);
//   }

//   private void _ShowRewardedAd(Action rewardAction, Action failedAction = null)
//   {
//     rewardedAd.OnAdFullScreenContentOpened += () =>
//     {
//       // logger?.LogAdStart(adEventId);
//     };
//     rewardedAd.OnAdFullScreenContentFailed += (AdError adError) =>
//         {
//           Debug.LogError("Reward ad failed to present: " + adError.GetMessage());
//         };

//     rewardedAd.Show((Reward reward) =>
//     {
//       rewardAction.Invoke();
//     });
//   }

//   private void DestroyRewardedAd()
//   {
//     if (rewardedAd != null)
//     {
//       rewardedAd.Destroy();
//       rewardedAd = null;
//     }
//   }

//   private void HandleOnOpenDefault()
//   {
//     isShowAd = true;
//   }

//   private void HandleOnCloseDefault(string unitId)
//   {
//     isShowAd = false;
//     DestroyRewardedAd();
//     LoadRewardedAd(unitId, null);
//   }

//   private void HandleOnFailedDefault(string unitId, AdError adError)
//   {
//     DestroyRewardedAd();
//     LoadRewardedAd(unitId, null);
//     Debug.LogError("Rewarded ad failed to present: " + adError.GetMessage());
//   }
// }
