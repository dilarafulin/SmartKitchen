using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter; // Dinleyeceðimiz Ana Mantýk (Beyin)
    [SerializeField] private Transform counterTopPoint; // Tabaklarýn oluþacaðý baþlangýç noktasý
    [SerializeField] private Transform plateVisualPrefab; // Sadece görselliði olan tabak modeli

    private List<GameObject> plateVisualGameObjectList;

    // Tabaklarýn birbirinin içine girmemesi için aralarýndaki Y ekseni mesafesi (Kalýnlýk)
    private float plateOffsetY = 0.1f;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        // Mantýk scriptindeki Event'lere abone oluyoruz (Kulaklýk takýp dinlemeye baþlýyoruz)
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        // 1. Yeni bir tabak görseli yarat ve onu counterTopPoint'in içine (Child olarak) koy
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        // 2. Tabaklarýn üst üste binmesi için yüksekliði hesapla 
        // (Örn: Listede 2 tabak varsa, 3. tabak 0.2f yüksekliðinde doðar)
        float plateOffsetYPosition = plateOffsetY * plateVisualGameObjectList.Count;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetYPosition, 0);

        // 3. Yarattýðýmýz bu görseli listeye ekle
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        // Mantýk kodundan "Bir tabak alýndý" haberi gelince:
        // 1. Listedeki EN SON tabak görselini bul
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];

        // 2. Onu listeden çýkar
        plateVisualGameObjectList.Remove(plateGameObject);

        // 3. Sahneden (Dünyadan) sil
        Destroy(plateGameObject);
    }
}