using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public string recipeName; // Tarifin adý (Örn: "Salata")
    public List<KitchenObjectSO> kitchenObjectSOList; // Bu tarifin içindeki malzemeler
}
