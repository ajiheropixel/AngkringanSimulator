using UnityEngine;
using UnityEngine.InputSystem;

public class AngkringanManager : MonoBehaviour
{
    public bool isOpen = false;

    [Header("Economy")]
    public GameManager gameManager;
    public int salePrice = 5000;

    void Update()
    {
        if (!isOpen)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.bKey.wasPressedThisFrame)
        {
            SellOnePortion();
        }
    }

    public void ToggleAngkringan()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            Debug.Log("Angkringan sekarang BUKA!");
        }
        else
        {
            Debug.Log("Angkringan sekarang TUTUP!");
        }
    }

    public void SellOnePortion()
    {
        if (!isOpen)
        {
            Debug.Log("Angkringan masih tutup!");
            return;
        }

        if (gameManager == null)
        {
            Debug.LogWarning("GameManager belum dihubungkan!");
            return;
        }

        gameManager.AddMoney(salePrice);

        Debug.Log("Satu porsi berhasil terjual!");
    }
}