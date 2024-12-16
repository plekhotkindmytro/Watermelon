using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using System;
using TMPro;

public class IAPManager : MonoBehaviour,
IDetailedStoreListener
{
    public static IAPManager Instance;
    public int defaultConsumableAmount = 10;

    public PopupPanel noInternet;
    
    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    
   
    public static string mouse = "mouse";
    public static string bee = "bee";
    public static string fish = "fish";

    IGooglePlayStoreExtensions googlePlayStoreExtensions;
    IAppleExtensions appleExtensions;

    private const string k_Environment = "production";

    private const string noInternetRestartAppText = "No Internet. Restart the app.";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
            

        Initialize(OnSuccess, OnError);
    }

        void Initialize(Action onSuccess, Action<string> onError)
        {
            try
            {
                var options = new InitializationOptions().SetEnvironmentName(k_Environment);

                UnityServices.InitializeAsync(options).ContinueWith(task => onSuccess());
            }
            catch (Exception exception)
            {
                onError(exception.Message);
            }
        }

        void OnSuccess()
        {
            var text = "Congratulations!\nUnity Gaming Services has been successfully initialized.";
            Debug.Log(text);
        }

        void OnError(string message)
        {
            noInternet.Activate(noInternetRestartAppText);
            var text = $"Unity Gaming Services failed to initialize with error: {message}.";
            Debug.LogError(text);
        }

    void Start()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
                var text =
                    "Error: Unity Gaming Services not initialized.\n" +
                    "To initialize Unity Gaming Services, open the file \"InitializeGamingServices.cs\" " +
                    "and uncomment the line \"Initialize(OnSuccess, OnError);\" in the \"Awake\" method.";
                Debug.LogError(text);
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    UpdateUIWithNoInternet();
                }
        } else
        {
            InitializePurchasing();
        }
        
    }


    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add our purchasable product and indicate its type.
        builder.AddProduct(mouse, ProductType.Consumable);
        builder.AddProduct(bee, ProductType.Consumable);
        builder.AddProduct(fish, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    

    public void BuyMouse()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            noInternet.Activate("No Internet");
            return;
        }
        BuyProductId(mouse);
    }

    public void BuyBee()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            noInternet.Activate("No Internet");
            return;
        }
        BuyProductId(bee);
    }

    public void BuyFish()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            noInternet.Activate("No Internet");
            return;
        }
        BuyProductId(fish);
    }


    private bool IsInitialized() {
        return storeController != null && storeExtensionProvider != null;
    }

    private void BuyProductId(string productId) {
        if(IsInitialized()) {
            Product product = storeController.products.WithID(productId);
            //var products = storeController.products.all;
            //print("products count: " + products.Length);
            //foreach (var prod in products)
            //{
            //    print(prod.definition.id + ". Available: " + prod.availableToPurchase);
            //}
            if(product != null && product.availableToPurchase) {
                storeController.InitiatePurchase(product);
            } else {
                print("Cannot purchase product: '" + product + "', it may not bee available.");
            }
        }  
        else
        {
            noInternet.Activate(noInternetRestartAppText);
            print("BuyProductId. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        storeController = controller;
        storeExtensionProvider = extensions;
        googlePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
        appleExtensions = extensions.GetExtension<IAppleExtensions>();

        UpdateUI();
    }

    public void UpdateUI()
    {

        Product mouseProduct = storeController.products.WithID(mouse);
        Product beeProduct = storeController.products.WithID(bee);
        Product fishProduct = storeController.products.WithID(fish);


        //string title = product.definition.id.ToUpper();
        //string price = product.metadata.localizedPriceString;
        ////string description = product.metadata.localizedDescription;
        //titleText.text = title;
        //priceText.text = price;

        UiManager.Instance.mousePriceText.text = mouseProduct.metadata.localizedPriceString;
        UiManager.Instance.beePriceText.text = beeProduct.metadata.localizedPriceString;
        UiManager.Instance.fishPriceText.text = fishProduct.metadata.localizedPriceString;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        OnInitializeFailed(error, null);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

        if (message != null)
        {
            errorMessage += $" More details: {message}";
        }

        Debug.Log(errorMessage);
        noInternet.Activate(noInternetRestartAppText);
        UpdateUIWithNoInternet();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // Retrieve the purchased product
        var product = args.purchasedProduct;

        if(String.Equals(product.definition.id, mouse, StringComparison.Ordinal)) {
            BoostManager.Instance.AddMouse(defaultConsumableAmount);
        } else if(String.Equals(product.definition.id, bee, StringComparison.Ordinal)) {
            BoostManager.Instance.AddBee(defaultConsumableAmount);
        } else if(String.Equals(product.definition.id, fish, StringComparison.Ordinal)) {
            BoostManager.Instance.AddFish(defaultConsumableAmount);
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        //UpdateUI();

        // We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        noInternet.Activate("Purchase failed");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
            $" Purchase failure reason: {failureDescription.reason}," +
            $" Purchase failure details: {failureDescription.message}");
        noInternet.Activate("Purchase failed");
    }

    
    private void UpdateUIWithNoInternet()
    {
        // Hide Shop buttons..
        // Say that shop is not available while no internet
        // On Shop Panel enable check internet and update UI accordingly.
        string unavailable = "unavailable";
        UiManager.Instance.mousePriceText.text = unavailable;
        UiManager.Instance.beePriceText.text = unavailable;
        UiManager.Instance.fishPriceText.text = unavailable;

    }

   

    public void RestorePurchases() {
         if (storeController == null || storeExtensionProvider == null)
        {
            Debug.Log("IAP is not initialized");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("Restoring purchases...");
            var apple = storeExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result, error) =>
            {
                Debug.Log("Restore purchases result: " + result);
                //UpdateUI();
                if (!string.IsNullOrEmpty(error))
                {
                    Debug.LogError("Restore purchases error: " + error);
                }
            });
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            // On Android, the Play Store handles the restore process automatically
            Debug.Log("Restore purchases is not explicitly needed on Android.");
        }
        else
        {
            Debug.Log("Restore purchases not supported on this platform.");
        }
    }

}
