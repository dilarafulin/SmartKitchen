using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    // Progress Bar (UI) için event
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray; // Desteklenen tüm tarifler

    private int cuttingProgress;

    // E Tuþu - Eþya Koyma / Alma
    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) // Tezgah boþ
        {
            if (player.HasKitchenObject()) // Oyuncuda eþya var
            {
                // Sadece kesilebilir bir þeyse koymasýna izin ver
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0; // Ýlerlemeyi sýfýrla

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
        }
        else // Tezgahta eþya var
        {
            if (!player.HasKitchenObject()) // Oyuncu boþ
            {
                GetKitchenObject().SetKitchenObjectParent(player);

                // Oyuncu eþyayý alýnca barý sýfýrla/kapat
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
            }
            else
            {
                // OYUNCUNUN ELÝ DOLU!
                // Elindeki þey tabak mý?
                if (player.GetKitchenObject() is PlateKitchenObject plateKitchenObject)
                {
                    // Tabaksa, tahtadaki malzemeyi tabaða eklemeyi dene
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        // Tabaða baþarýyla eklendi! Tahtadaki malzemeyi yok et.
                        GetKitchenObject().DestroySelf();

                        // Kesme tahtasýnýn Progress Bar'ýný sýfýrla/gizle
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                    }
                }
            }
        }
    }

    // F Tuþu - Kesme Ýþlemi
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // Eþya var ve kesilebilir bir eþya. Kesme iþlemi baþlar.
            cuttingProgress++;

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            // UI'a ilerlemeyi bildir
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            // Kesme iþlemi bitti mi?
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                // Eski bütün eþyayý yok et
                GetKitchenObject().DestroySelf();

                // Yeni kesilmiþ eþyayý spawn et ve tezgaha koy
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    // --- YARDIMCI FONKSÝYONLAR ---

    // Elimizdeki malzemeyle eþleþen bir tarif var mý?
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    // Tarif listesinden dönüþecek eþyayý bul
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        return null;
    }

    // Girdiðimiz malzemeye ait doðru tarifi bul
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}