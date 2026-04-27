using UnityEngine;

public class SousChefTaskManager : MonoBehaviour
{
    [Header("Referanslar")]
    [SerializeField] private SousChefAgent agent; // Görevi vereceğimiz ajan 

    private SousChefTask activeTask;

    // ── OYUNCUDAN (UI) KOMUT GELİR ──────────────────────────────────
    public void GiveCommand(SousChefCommand command, BaseCounter targetCounter, KitchenObjectSO itemSO = null)
    {
       // 1. Yeni bir görev paketi oluştur [cite: 25]
        activeTask = new SousChefTask(command, targetCounter, itemSO);

        // 2. Paketi Ajana teslim et 
        if (agent != null)
        {
            agent.SetTask(activeTask);
            Debug.Log($"[TaskManager] Yeni Komut Verildi: {command} → Hedef: {targetCounter.name}");
        }
        else
        {
            Debug.LogError("TaskManager'da Agent referansı eksik!");
        }
    }

    // ── AJAN GÖREVİ TAMAMLAYINCA BURAYI ÇAĞIRIR ─────────────────────
    public void OnTaskCompleted()
    {
        if (activeTask != null)
        {
             activeTask.isCompleted = true; 
             Debug.Log("[TaskManager] Görev Başarıyla Tamamlandı!"); 
            // İleride buraya "Sıradaki göreve geç" gibi otomatik bir sistem eklenebilir [cite: 27]
        }
    }

    // UI'ın (Visual) anlık durumu okuyabilmesi için Getter metodu
    public SousChefTask GetActiveTask()
    {
        return activeTask;
    }
}