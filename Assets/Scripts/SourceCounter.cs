using UnityEngine;

public class SourceCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // hangi malzeme

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Oyuncunun eli boþ — malzemeyi spawn et, direkt ele ver
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        }
        // Oyuncunun elinde bir þey varsa hiçbir þey yapma
    }
}