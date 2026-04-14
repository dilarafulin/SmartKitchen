using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    // C# 'ta Struct kullanarak Inspector'da kendi özel listemizi yaratýyoruz
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO; // Mantýk (Örn: SlicedBunSO)
        public GameObject gameObject;           // Görsel (Örn: TopBun 3D Modeli)
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        // Oyun baþladýðýnda güvenlik önlemi olarak tüm malzemeleri gizle
        foreach (KitchenObjectSO_GameObject mapping in kitchenObjectSOGameObjectList)
        {
            mapping.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        // Tabaða eklenen malzemeyi (e.kitchenObjectSO) listemizde arýyoruz
        foreach (KitchenObjectSO_GameObject mapping in kitchenObjectSOGameObjectList)
        {
            if (mapping.kitchenObjectSO == e.kitchenObjectSO)
            {
                // Eþleþme bulundu! Gizli olan 3D modeli görünür yap
                mapping.gameObject.SetActive(true);
            }
        }
    }
}
