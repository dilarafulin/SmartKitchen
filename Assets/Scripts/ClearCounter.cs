using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // tezgahýn baþlangýç malzemesi

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Tezgah boþ — oyuncunun elindekini býrak
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            // Ýkisi de boþsa hiçbir þey yapma
        }
        else
        {
            // Tezgahta bir þey var
            if (!player.HasKitchenObject())
            {
                // Oyuncu boþ — tezgahtakini al
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            // Ýkisinde de varsa þimdilik hiçbir þey yapma
        }
    }
}