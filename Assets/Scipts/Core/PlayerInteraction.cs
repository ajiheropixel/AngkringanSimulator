using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;

    private Interactable currentInteractable;

    void Update()
    {
        FindInteractable();

        if (currentInteractable != null)
        {
            if (Keyboard.current != null &&
                Keyboard.current.eKey.wasPressedThisFrame)
            {
                Interact();
            }
        }
    }

    void FindInteractable()
    {
        currentInteractable = null;

        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            interactionRange
        );

        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            Interactable interactable =
                collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                float distance =
                    Vector3.Distance(
                        transform.position,
                        collider.transform.position
                    );

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    currentInteractable = interactable;
                }
            }
        }

        if (currentInteractable != null)
        {
            interactionUI.SetActive(true);

            interactionText.text =
                "[E] " + currentInteractable.interactionText;
        }
        else
        {
            interactionUI.SetActive(false);
        }
    }

    void Interact()
{
    currentInteractable.Interact();
}
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            interactionRange
        );
    }
}