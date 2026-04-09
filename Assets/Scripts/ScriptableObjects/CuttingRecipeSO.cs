using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;           // Ne kesilecek? (Örn: Normal Domates)
    public KitchenObjectSO output;          // Neye dönüþecek? (Örn: Dilimli Domates)
    public int cuttingProgressMax;          // Kaç kere F'ye basýlacak? (Örn: 5)
}
