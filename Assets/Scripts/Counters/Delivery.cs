using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // 1. Oyuncunun elinde bir þey var mý?
        if (player.HasKitchenObject())
        {
            // 2. MODERN C#: Pattern Matching (Desen Eþleþtirme)
            // Eðer elindeki obje bir 'PlateKitchenObject' ise, onu anýnda 'plateKitchenObject' deðiþkenine dönüþtür ve içeri gir!
            if (player.GetKitchenObject() is PlateKitchenObject plateKitchenObject)
            {
                // 3. Tabaðýn içindeki malzemelerin listesini al ve Hakem'e (Manager) gönder!
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject.GetKitchenObjectSOList());

                // 4. Teslimat yapýldýktan sonra tabaðý yok et
                player.GetKitchenObject().DestroySelf();
            }
            else
            {
                // Oyuncu elinde tabak olmayan bir þeyle (örn: Domates) geldi. Hiçbir þey yapma.
                Debug.Log("Sadece tabakla teslimat yapabilirsin!");
            }
        }
    }
}