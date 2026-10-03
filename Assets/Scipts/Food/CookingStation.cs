
using UnityEngine;

public class CookingStation : MonoBehaviour
{
    public CookingManager cookingManager;

    public string interactionText = "Masak Pesanan";

    private Interactable interactable;

    void Start()
    {
        interactable = GetComponent<Interactable>();

        if (interactable == null)
        {
            Debug.LogError(
                "CookingStation membutuhkan component Interactable!"
            );
            return;
        }

        interactable.interactionText = interactionText;
    }

    void Update()
    {
        if (cookingManager == null || interactable == null)
            return;

        if (cookingManager.IsCooking())
        {
            interactable.interactionText = "Angkat Masakan";
        }
        else if (cookingManager.HasFinishedCooking())
        {
            interactable.interactionText = "Masakan Selesai";
        }
        else
        {
            interactable.interactionText = interactionText;
        }
    }

    public void Interact()
    {
        if (cookingManager == null)
        {
            Debug.LogWarning(
                "CookingManager belum dihubungkan!"
            );
            return;
        }

        // Jika sedang memasak, angkat makanan
        if (cookingManager.IsCooking())
        {
            Debug.Log("Mengangkat masakan.");
            cookingManager.TakeFood();
            return;
        }

        // Jika proses sebelumnya sudah selesai
        if (cookingManager.HasFinishedCooking())
        {
            Debug.Log("Masakan sudah diangkat.");
            return;
        }

        // Jika belum memasak, mulai memasak
        Debug.Log("Memulai proses memasak.");
        cookingManager.StartCooking();
    }
}