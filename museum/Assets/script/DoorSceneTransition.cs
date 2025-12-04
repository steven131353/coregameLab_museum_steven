using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorSceneTransition : MonoBehaviour
{
    public string sceneToLoad = "ForestMuseumScene";
    public float interactionDistance = 2.2f;

    // UI 提示
    public TMP_Text interactHintText;     // “Press E”
    public TMP_Text requirementText;      // “You need all 7 plants…”

    [Header("Optional Requirement")]
    public bool requireAllPlantTypes = false;   // 是否需要收集完所有植物

    private Transform playerCam;
    private bool lookingAtDoor = false;

    private void Start()
    {
        playerCam = Camera.main.transform;

        if (interactHintText != null)
            interactHintText.gameObject.SetActive(false);

        if (requirementText != null)
            requirementText.gameObject.SetActive(false);
    }

    private void Update()
    {
        Ray ray = new Ray(playerCam.position, playerCam.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                lookingAtDoor = true;

                // 显示按 E 提示
                if (interactHintText != null)
                    interactHintText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    TryEnterDoor();
                }
            }
            else
            {
                HideHint();
            }
        }
        else
        {
            HideHint();
        }
    }

    private void TryEnterDoor()
    {
        // 如果这扇门要求收集完所有植物
        if (requireAllPlantTypes)
        {
            if (PlantManager.Instance != null &&
                !PlantManager.Instance.AreAllTypesCollected())
            {
                // 植物未收集完，显示错误提示，不传送
                if (requirementText != null)
                {
                    requirementText.text = "You need to collect all plants before leaving.";
                    requirementText.gameObject.SetActive(true);

                    // 2 秒后自动隐藏
                    CancelInvoke(nameof(HideRequirementText));
                    Invoke(nameof(HideRequirementText), 2f);
                }
                return;
            }
        }

        // 条件满足 → 正常切场景
        SceneManager.LoadScene(sceneToLoad);
    }

    private void HideRequirementText()
    {
        if (requirementText != null)
            requirementText.gameObject.SetActive(false);
    }

    private void HideHint()
    {
        if (lookingAtDoor)
        {
            lookingAtDoor = false;

            if (interactHintText != null)
                interactHintText.gameObject.SetActive(false);
        }
    }
}
