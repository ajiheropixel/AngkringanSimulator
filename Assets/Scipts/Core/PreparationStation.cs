using UnityEngine;

public class PreparationStation : MonoBehaviour
{
    public OrderManager orderManager;

    public void PrepareFood()
    {
        if (orderManager == null)
        {
            Debug.LogWarning(
                "OrderManager belum dihubungkan!"
            );

            return;
        }

        if (!orderManager.hasOrder)
        {
            Debug.Log("Tidak ada pesanan yang harus disiapkan.");
            return;
        }

        if (orderManager.isPrepared)
        {
            Debug.Log("Pesanan sudah siap disajikan.");
            return;
        }

        orderManager.PrepareOrder();
    }
}