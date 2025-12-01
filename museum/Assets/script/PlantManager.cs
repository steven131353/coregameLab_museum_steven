using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;

    // 一共有多少种植物类型（不是数量，是“种类数”）
    public int totalPlantTypes = 5;

    // 已经收集过的类型
    private HashSet<string> collectedTypes = new HashSet<string>();

    private void Awake()
    {
        Instance = this;
    }

    // 某个类型是否已经收集过
    public bool IsTypeCollected(string plantType)
    {
        return collectedTypes.Contains(plantType);
    }

    // 收集一个新的植物类型
    public void CollectPlantType(string plantType)
    {
        if (collectedTypes.Add(plantType))
        {
            int remaining = totalPlantTypes - collectedTypes.Count;
            UIManager.Instance.UpdateRemaining(remaining);
            UIManager.Instance.ShowCollected(plantType);

            // 新增：通知图鉴
            if (PlantDexUI.Instance != null)
            {
                PlantDexUI.Instance.OnNewPlantDiscovered(plantType);
            }

            if (remaining <= 0)
            {
                UIManager.Instance.ShowAllCollected();
            }
        }
    }

}
