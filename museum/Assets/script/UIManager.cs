using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text remainingText;
    public TMP_Text messageText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 启动时从 PlantManager 初始化一次
        int total = PlantManager.Instance.totalPlantTypes;
        UpdateRemaining(total);
        messageText.text = "";
    }

    public void UpdateRemaining(int remaining)
    {
        remainingText.text = "Plants remaining: " + remaining;
    }

    public void ShowCollected(string plantType)
    {
        messageText.text = "Collected: " + plantType;
        CancelInvoke(nameof(ClearMessage));
        Invoke(nameof(ClearMessage), 2f);
    }

    public void ShowAllCollected()
    {
        messageText.text = "All plant types collected!";
    }

    private void ClearMessage()
    {
        messageText.text = "";
    }
}
