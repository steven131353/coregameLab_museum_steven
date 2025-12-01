using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorSceneTransition : MonoBehaviour
{
    public string sceneToLoad = "ForestMuseumScene";
    public float interactionDistance = 2.2f;

    // 引用提示 UI
    public TMP_Text interactHintText;

    private Transform playerCam;
    private bool lookingAtDoor = false;

    private void Start()
    {
        playerCam = Camera.main.transform;

        if (interactHintText != null)
            interactHintText.gameObject.SetActive(false);
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
                if (interactHintText != null)
                    interactHintText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(sceneToLoad);
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
