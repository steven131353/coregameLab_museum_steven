using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;

    // 一共有多少种植物类型（不是数量，是“种类数”）
    // 记得在 Inspector 里改成 7，或者这里直接写 7
    public int totalPlantTypes = 7;

    // 已经收集过的类型
    private HashSet<string> collectedTypes = new HashSet<string>();

    private void Awake()
    {
        Instance = this;
    }

    // 当前已经收集了多少种植物（给别人查用）
    public int CollectedTypeCount
    {
        get { return collectedTypes.Count; }
    }

    // 是否已经收集完所有类型（给门那边判断用）
    public bool AreAllTypesCollected()
    {
        return collectedTypes.Count >= totalPlantTypes;
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

            // 通知图鉴
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
