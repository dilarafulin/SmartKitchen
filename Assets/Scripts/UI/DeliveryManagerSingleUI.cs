using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText; // Yemeðin ismini yazacaðýmýz Text
    [SerializeField] private Transform iconContainer; // Malzeme ikonlarýnýn dizileceði ufak kutu
    [SerializeField] private Transform iconTemplate; // Tek bir malzemenin ikonu (Image)

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false); // Ýkon þablonunu gizle
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        // Yemeðin adýný yazdýr
        recipeNameText.text = recipeSO.recipeName;

        // Eski ikonlarý temizle
        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        // Tarifin içindeki her malzeme için bir ikon klonla
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);

            // Ýkonun resmini (Sprite) malzemeye göre deðiþtir
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}