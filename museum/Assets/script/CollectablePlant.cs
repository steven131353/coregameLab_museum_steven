using UnityEngine;

public class CollectablePlant : MonoBehaviour, IInteractable
{
    [Header("Plant Settings")]
    public string plantType;           // 例如 "Pine" / "Fern"
    public GameObject outlineObject;   // 拖那个 Outline 子物体进来

    private bool collectedThisInstance = false;

    private void Start()
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(false);
        }
    }

    // 这个实例是否还可以被互动
    public bool CanInteract()
    {
        // 如果这个类型已经被收集过，或者这棵已经被收集，就不能再互动
        if (PlantManager.Instance.IsTypeCollected(plantType)) return false;
        if (collectedThisInstance) return false;
        return true;
    }

    // 描边开关
    public void SetHighlight(bool on)
    {
        if (outlineObject != null)
        {
            outlineObject.SetActive(on && CanInteract());
        }
    }

    // 按 E 时被调用
    // 按 E 时被调用
    public void Interact()
    {
        if (!CanInteract()) return;

        collectedThisInstance = true;

        // 通知管理器：这个类型被收集了
        PlantManager.Instance.CollectPlantType(plantType);

        // 只关闭描边，不隐藏植物
        SetHighlight(false);

        // 删除这行（或者注释掉）
        // gameObject.SetActive(false);
    }

}
