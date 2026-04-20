using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FryingRecipeSO")]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;           
    public KitchenObjectSO output;
    public float fryingTimerMax;          
}
