
using UnityEngine;

public class CustomerPayment : MonoBehaviour
{
    public OrderManager orderManager;
    public GameManager gameManager;
    public CookingManager cookingManager;

    [Header("Pengaturan Pembayaran")]
    public float perfectMultiplier = 1.2f;
    public float normalMultiplier = 1.0f;
    public float burnedMultiplier = 0.5f;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        if (orderManager == null)
        {
            orderManager = FindFirstObjectByType<OrderManager>();
        }

        if (cookingManager == null)
        {
            cookingManager = FindFirstObjectByType<CookingManager>();
        }
    }

    public void ProcessPayment()
    {
        if (orderManager == null)
        {
            Debug.LogError("CustomerPayment: OrderManager tidak ditemukan!");
            return;
        }

        if (gameManager == null)
        {
            Debug.LogError("CustomerPayment: GameManager tidak ditemukan!");
            return;
        }

        if (cookingManager == null)
        {
            Debug.LogError("CustomerPayment: CookingManager tidak ditemukan!");
            return;
        }

        if (!orderManager.hasOrder || !orderManager.isPrepared)
        {
            Debug.LogWarning("Pesanan belum siap untuk dibayar!");
            return;
        }

        if (orderManager.isPaid)
        {
            Debug.LogWarning("Pesanan ini sudah dibayar!");
            return;
        }

        float multiplier;

        switch (cookingManager.currentQuality)
        {
            case CookingQuality.Perfect:
                multiplier = perfectMultiplier;
                break;

            case CookingQuality.Normal:
                multiplier = normalMultiplier;
                break;

            case CookingQuality.Burned:
                multiplier = burnedMultiplier;
                break;

            default:
                Debug.LogWarning("Kualitas masakan belum ditentukan!");
                return;
        }

        int basePrice = orderManager.GetTotalPrice();
        int totalPayment = Mathf.RoundToInt(basePrice * multiplier);

        gameManager.AddMoney(totalPayment);
        orderManager.isPaid = true;

        Debug.Log("Kualitas masakan: " + cookingManager.currentQuality);
        Debug.Log("Harga pesanan: Rp" + basePrice.ToString("N0"));
        Debug.Log("Total pembayaran: Rp" + totalPayment.ToString("N0"));
        Debug.Log("Pembayaran berhasil. Uang sekarang: Rp" +
                  gameManager.money.ToString("N0"));
    }
}