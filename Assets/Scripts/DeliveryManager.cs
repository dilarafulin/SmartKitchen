using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    // Singleton (Oyunun her yerinden kolayca eriþebilmek için)
    public static DeliveryManager Instance { get; private set; }

    [Header("Veri Havuzu")]
    [SerializeField] private RecipeListSO recipeListSO; // Oluþturduðun "AllRecipes" dosyasýný buraya sürükle

    private List<RecipeSO> waitingRecipeSOList; // Ekranda bekleyen aktif sipariþler
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f; // Her 4 saniyede bir sipariþ gelsin
    private int waitingRecipesMax = 4; // Ekranda maksimum 4 sipariþ birikebilsin

    // UI'ýn haberdar olmasý için Event'ler
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            // Eðer ekrandaki sipariþ sayýsý sýnýrý aþmadýysa yeni sipariþ ver
            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                // Havuzdan rastgele bir tarif seç
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                // Bekleyenler listesine ekle
                waitingRecipeSOList.Add(waitingRecipeSO);

                // Arayüze (UI) haber ver: "Yeni sipariþ geldi, ekrana çiz!"
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    // Oyuncu elinde bir tabakla teslimat tezgahýna geldiðinde bu fonksiyon çalýþacak
    public void DeliverRecipe(List<KitchenObjectSO> plateKitchenObjectSOList)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            // 1. Kural: Tabaktaki malzeme sayýsý ile tarifteki malzeme sayýsý eþit mi?
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObjectSOList.Count)
            {
                bool plateContentsMatchesRecipe = true;

                // 2. Kural: Tarifteki her bir malzeme, tabakta var mý?
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObjectSOList)
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        // Bu malzeme tabakta yok! Demek ki bu tarif deðil.
                        plateContentsMatchesRecipe = false;
                        break;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    // BAÞARILI TESLÝMAT!
                    Debug.Log("Sipariþ Baþarýyla Teslim Edildi: " + waitingRecipeSO.recipeName);

                    waitingRecipeSOList.RemoveAt(i);

                    // UI'a haber ver: "Bu sipariþ bitti, ekrandan sil!"
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // Eðer döngü bittiyse ve return olmadýysa, oyuncu yanlýþ yemek getirmiþtir.
        Debug.Log("Hata: Oyuncu yanlýþ bir yemek getirdi veya böyle bir sipariþ yok!");
    }
}