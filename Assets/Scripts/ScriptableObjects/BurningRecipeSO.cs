using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;    // Piþmiþ köfte
    public KitchenObjectSO output;   // Yanmýþ köfte
    public float burningTimerMax;    // Kaç saniyede yanar
}