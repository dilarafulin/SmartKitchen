using UnityEngine;

//veri paketi
public class SousChefTask
{
    public SousChefCommand command;      // Þef ne yapacak? (Yukarýdaki Enum'dan seçilecek)
    public BaseCounter targetCounter;    // Þef nereye gidecek? (Örn: Kesme Tezgahý 1)
    public KitchenObjectSO targetItemSO; // Hangi malzemeyle ilgili? (Opsiyonel - Örn: Domates)
    public bool isCompleted;             // Görev bitti mi?

    // Constructor (Yapýcý Metot): Bu kargo paketi oluþturulurken içine zorunlu olarak konacak bilgiler.
    public SousChefTask(SousChefCommand cmd, BaseCounter counter, KitchenObjectSO itemSO = null)
    {
        command = cmd;
        targetCounter = counter;
        targetItemSO = itemSO;
        isCompleted = false; // Görev yeni oluþturulduðunda doðal olarak henüz bitmemiþtir.
    }
}