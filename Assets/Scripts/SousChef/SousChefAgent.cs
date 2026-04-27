using UnityEngine;
using Unity.MLAgents; // Ýleride kullanacaðýz

public class SousChefAgent : Agent
{
    // ML-Agents kodlarýný en son buraya yazacaðýz. Þimdilik task alabilmesi için þu metodu ekleyelim:
    public void SetTask(SousChefTask task)
    {
        Debug.Log("Ajan yeni görev aldý, ama henüz beyni yok!");
    }
}