using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string interactionText = "Tekan E untuk berinteraksi";

    public UnityEvent onInteract;

    public void Interact()
    {
        onInteract.Invoke();
    }
}