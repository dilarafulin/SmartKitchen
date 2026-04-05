using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    // Ebeveyni deðiþtir (tezgahtan ele, elden tezgaha vs.)
    public void SetKitchenObjectParent(IKitchenObjectParent parent)
    {
        // Eski ebeveynden temizle
        if (kitchenObjectParent != null)
        {
            kitchenObjectParent.ClearKitchenObject();
        }

        kitchenObjectParent = parent;

        // Yeni ebeveyn zaten bir þey tutuyorsa hata ver
        if (parent.HasKitchenObject())
        {
            Debug.LogError("Ebeveynin zaten bir KitchenObject'i var!");
        }

        parent.SetKitchenObject(this);

        // Modeli ebeveynin üstüne taþý
        transform.parent = parent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    // Malzemeyi sahnede spawn et
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO so, IKitchenObjectParent parent)
    {
        Transform kitchenObjectTransform = Instantiate(so.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);
        return kitchenObject;
    }

    // Malzemeyi yok et
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}