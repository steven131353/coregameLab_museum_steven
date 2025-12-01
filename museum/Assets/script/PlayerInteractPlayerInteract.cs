using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;          // 射线长度
    public LayerMask interactLayer;              // 只检测植物的 Layer

    private CollectablePlant currentHighlight;    // 当前被描边的植物

    void Update()
    {
        HandleHighlight();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    // 自动描边 / 取消描边
    void HandleHighlight()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            CollectablePlant plant = hit.collider.GetComponent<CollectablePlant>();

            if (plant != null && plant.CanInteract())
            {
                // 目标变了 → 取消之前的高亮
                if (currentHighlight != plant)
                {
                    ClearHighlight();
                    currentHighlight = plant;
                    plant.SetHighlight(true);
                }
                return;
            }
        }

        // 射线没撞到植物 → 清除高亮
        ClearHighlight();
    }

    void ClearHighlight()
    {
        if (currentHighlight != null)
        {
            currentHighlight.SetHighlight(false);
            currentHighlight = null;
        }
    }

    // 按 E 时执行交互
    void TryInteract()
    {
        if (currentHighlight != null)
        {
            currentHighlight.Interact();
        }
    }
}
