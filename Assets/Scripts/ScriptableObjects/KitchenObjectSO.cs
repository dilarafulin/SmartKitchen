using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/KitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;       // 3D model
    public Sprite sprite;          // UI ikonu
    public string objectName;      // "Domates", "Ekmek" vs.
}