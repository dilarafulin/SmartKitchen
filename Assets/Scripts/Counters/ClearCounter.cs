using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // tezgahýn baþlangýç malzemesi

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // 1. SENARYO: Tezgah tamamen boþ
            if (player.HasKitchenObject())
            {
                // Oyuncuda eþya var, tezgaha býrak
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            // 2. SENARYO: Tezgahta kesinlikle bir eþya var
            if (player.HasKitchenObject())
            {
                // A) OYUNCUNUN DA ELÝ DOLU (Birleþtirme Senaryolarý)

                // DURUM 1: Tezgahtaki þey bir Tabak mý?
                if (GetKitchenObject() is PlateKitchenObject plateKitchenObject)
                {
                    if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }
                }
                // DURUM 2: Oyuncunun elindeki þey bir Tabak mý?
                else if (player.GetKitchenObject() is PlateKitchenObject playerPlateKitchenObject)
                {
                    if (playerPlateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                // B) OYUNCUNUN ELÝ BOÞ (Tezgahtakini Alma Senaryosu)
                // Bu kod artýk baðýmsýz bir 'else' bloðunda olduðu için kusursuz çalýþacak.
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}