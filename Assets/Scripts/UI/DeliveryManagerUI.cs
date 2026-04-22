using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container; // Kartlarýn dizileceði ana kutu
    [SerializeField] private Transform recipeTemplate; // Tek bir sipariþ kartýnýn þablonu (Prefab)

    private void Awake()
    {
        // Þablonu oyun baþlarken görünmez yapýyoruz, çünkü onu sadece klonlamak (Instantiate) için kullanacaðýz
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        // DeliveryManager'daki Event'lere ABONE oluyoruz (Telsizi açtýk)
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

        UpdateVisual(); // Baþlangýçta ekraný bir kez temizle
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual(); // Yeni sipariþ geldiðinde ekraný yenile
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual(); // Sipariþ bittiðinde ekraný yenile
    }

    private void UpdateVisual()
    {
        // 1. Önce ekrandaki eski klonlanmýþ kartlarý temizle (Þablon hariç!)
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        // 2. Bekleyen sipariþ listesine bak ve her biri için yeni bir kart oluþtur
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true); // Klonu görünür yap

            // Klonlanan karta "Senin yemeðin bu!" bilgisini gönder (Bunu bir sonraki kodda yazacaðýz)
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}