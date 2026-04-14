using System;
using System.Collections.Generic;
using UnityEngine;

// Dikkat: MonoBehaviour'dan deðil, kendi yazdýðýn KitchenObject'ten miras alýyor!
public class PlateKitchenObject : KitchenObject
{
    // Görsel scriptimizin (domatesi, peyniri göstermek için) dinleyeceði Event
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    // Bu tabaða NELER konulabilir? (Inspector'dan seçeceðiz: Ekmek, Piþmiþ Et, Kesilmiþ Domates vs.)
    // Çið et koymayý engellemek için bu liste çok önemli.
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    // Tabaðýn içinde ÞU AN neler var?
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    // Tabaða malzeme eklemeyi dener. Baþarýlý olursa True, olmazsa False döner.
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        // 1. Kural: Bu malzeme tabaða konulabilir mi? (Listede var mý?)
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }

        // 2. Kural: Bu malzemeden tabakta zaten var mý? (Ayný tabaða 2 tane ekmek konmaz)
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }

        // Kurallarý geçtiyse malzemeyi tabaða ekle
        kitchenObjectSOList.Add(kitchenObjectSO);

        // Görselin güncellenmesi için Event fýrlat
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObjectSO = kitchenObjectSO
        });

        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }


}