using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class PlantDexUI : MonoBehaviour
{
    public static PlantDexUI Instance;

    [Header("UI References")]
    public GameObject dexPanel;     // 图鉴的 Panel
    public TMP_Text dexText;        // 显示内容的 Text

    // 已解锁的植物类型
    private HashSet<string> discoveredTypes = new HashSet<string>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 游戏开始时自动打开一次图鉴
        dexPanel.SetActive(true);
        UpdateDexText();
    }

    private void Update()
    {
        // 按 K 打开 / 关闭图鉴
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleDex();
        }
    }

    public void ToggleDex()
    {
        bool newState = !dexPanel.activeSelf;
        dexPanel.SetActive(newState);
    }

    /// <summary>
    /// 当玩家首次收集到一个新的植物类型时调用
    /// </summary>
    public void OnNewPlantDiscovered(string plantType)
    {
        // 如果是第一次加入到图鉴
        if (discoveredTypes.Add(plantType))
        {
            UpdateDexText();

            // 自动打开图鉴给玩家看
            dexPanel.SetActive(true);
        }
    }

    /// <summary>
    /// 更新图鉴中的文字内容
    /// </summary>
    private void UpdateDexText()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Plant Journal");
        sb.AppendLine("-------------------------");
        sb.AppendLine("Goal:");
        sb.AppendLine("  Explore the forest and collect one of each plant type.");
        sb.AppendLine();
        sb.AppendLine("Controls:");
        sb.AppendLine("  WASD - Move");
        sb.AppendLine("  Mouse - Look");
        sb.AppendLine("  E - Collect plant");
        sb.AppendLine("  K - Open / close journal");
        sb.AppendLine();
        sb.AppendLine("Collected plants:");

        if (discoveredTypes.Count == 0)
        {
            sb.AppendLine("  (None yet. Walk around and collect your first plant!)");
        }
        else
        {
            foreach (string type in discoveredTypes)
            {
                // 以后可以在这里加描述 / 中文名
                sb.AppendLine("  - " + type);
            }
        }

        dexText.text = sb.ToString();
    }
}
