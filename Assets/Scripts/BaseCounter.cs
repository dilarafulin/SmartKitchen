using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    // Alt sınıflar kendi Interact'ini yazar
    public virtual void Interact(Player player)
    {
        Debug.LogError("Interact override edilmedi: " + gameObject.name);
    }
    public virtual void InteractAlternate(Player player)
    {
     
    }

    // ── IKitchenObjectParent ──────────────────────
    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;
    public void SetKitchenObject(KitchenObject obj) => kitchenObject = obj;
    public KitchenObject GetKitchenObject() => kitchenObject;
    public void ClearKitchenObject() => kitchenObject = null;
    public bool HasKitchenObject() => kitchenObject != null;
}