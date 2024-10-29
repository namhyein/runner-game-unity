using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

using IAP;
using Unity.Services.Core;
using Unity.Services.Core.Environments;


public class IAPManager : IStoreListener
{
  private IStoreController storeController;
  private IExtensionProvider storeExtensionProvider;

#if UNITY_EDITOR
  [Obsolete]
  public async void Init()
  {
    await InitializeUnityServicesAsync();
  }

  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
  {
    PurchaseItem(args.purchasedProduct.definition.id);
    return PurchaseProcessingResult.Complete;
  }

  public void RestorePurchase(Action callback)
  {
    callback.Invoke();
  }
#else
  private bool IsInitialized()
  {
    return storeController != null && storeExtensionProvider != null;
  }

  [Obsolete]
  public async void Init()
  {
    await InitializeUnityServicesAsync();
    InitializePurchasing();
  }

  public void Purchase(string productId)
  {
    if (!IsInitialized())
      return;

    Product product = storeController.products.WithID(productId);
    if (product != null && product.availableToPurchase)
      storeController.InitiatePurchase(product);
  }

  private void ComposePurchasing(ConfigurationBuilder builder)
  {
    Dictionary<string, IAP.Purchase> purchases = Managers.Data.Purchases;

    foreach (var purchase in purchases)
      builder.AddProduct(purchase.Key, ProductType.Consumable);

    UnityPurchasing.Initialize(this, builder);
  }

#if UNITY_ANDROID
  [Obsolete]
  private void InitializePurchasing()
  {
    if (IsInitialized())
      return;

    ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.GooglePlay));
    ComposePurchasing(builder);
  }

  public void RestorePurchase(Action callback)
  {
    callback.Invoke();
  }

  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
  {
    PurchaseItem(args.purchasedProduct.definition.id);
    return PurchaseProcessingResult.Complete;
  }

#elif UNITY_IOS
  [Obsolete]
  private void InitializePurchasing()
  {
    if (IsInitialized())
      return;

    ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.AppleAppStore));
    ComposePurchasing(builder);
  }

  public void RestorePurchase(Action callback)
  {
    if (IsInitialized())
    {
      storeExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions((result, message) =>
      {
        callback.Invoke();
      });
    }
  } 

  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
  {
    var product = args.purchasedProduct;

    try
    {
        var validator = new CrossPlatformValidator(
            GooglePlayTangle.Data(),
            AppleTangle.Data(), Application.identifier);
        var result = validator.Validate(product.receipt);
        if (result.Length > 0)
            PurchaseItem(product.definition.id);
    }
    catch (IAPSecurityException ex)
    {
    }

    return PurchaseProcessingResult.Complete;
  }

#endif

#endif
  private void PurchaseItem(string productId)
  {
    Purchase purchase = Managers.Data.Purchases[productId];

  }

  private async Task InitializeUnityServicesAsync()
  {
    var options = new InitializationOptions().SetEnvironmentName("production");
    await UnityServices.InitializeAsync(options);
  }

  public void OnInitialized(IStoreController controller, IExtensionProvider extension)
  {
    storeController = controller;
    storeExtensionProvider = extension;
  }

  public void OnInitializeFailed(InitializationFailureReason error)
  {
  }

  public void OnInitializeFailed(InitializationFailureReason error, string message)
  {
  }

  public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
  {
  }

  public Product GetProduct(string _productId)
  {
    return storeController.products.WithID(_productId);
  }

  public string GetProductCurrencyCode(string _productId)
  {
    return storeController.products.WithID(_productId).metadata.isoCurrencyCode;
  }

  public string GetProductLocalPriceString(string _productId)
  {
    return storeController.products.WithID(_productId).metadata.localizedPriceString;
  }

  public float GetProductLocalPrice(string _productId)
  {
    return (float)storeController.products.WithID(_productId).metadata.localizedPrice;
  }
}