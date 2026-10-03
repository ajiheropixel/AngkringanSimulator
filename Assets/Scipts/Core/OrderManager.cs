using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public GameObject orderPanel;
    public TextMeshProUGUI orderText;

    private string currentOrder;
    private int currentQuantity;
    private int currentPrice;

    public bool hasOrder = false;
    public bool isPrepared = false;
    public bool isPaid = false;

    private string[] menu =
    {
        "Sate Usus",
        "Es Teh"
    };

    private int[] prices =
    {
        5000,
        3000
    };

    public void CreateOrder()
    {
        int randomMenu = Random.Range(0, menu.Length);

        currentOrder = menu[randomMenu];
        currentPrice = prices[randomMenu];

        currentQuantity = Random.Range(1, 4);

        hasOrder = true;
        isPrepared = false;

        orderPanel.SetActive(true);

        UpdateOrderUI();

        Debug.Log(
            "Pesanan: " +
            currentOrder +
            " x " +
            currentQuantity
        );
        hasOrder = true;
isPrepared = false;
isPaid = false;
    }

    public void PrepareOrder()
    {
        if (!hasOrder)
        {
            Debug.Log("Tidak ada pesanan.");
            return;
        }

        if (isPrepared)
        {
            Debug.Log("Pesanan sudah siap.");
            return;
        }

        isPrepared = true;

        UpdateOrderUI();

        Debug.Log("Pesanan sudah disiapkan!");
    }

    public void ServeOrder()
    {
        if (!hasOrder)
        {
            Debug.Log("Tidak ada pesanan.");
            return;
        }

        if (!isPrepared)
        {
            Debug.Log("Pesanan belum disiapkan!");
            return;
        }

        Debug.Log(
            "Pesanan " +
            currentOrder +
            " x " +
            currentQuantity +
            " berhasil disajikan!"
        );

        hasOrder = false;
        isPrepared = false;

        orderPanel.SetActive(false);
    }

    public int GetTotalPrice()
    {
        return currentPrice * currentQuantity;
    }

    public string GetCurrentOrder()
    {
        return currentOrder;
    }

    void UpdateOrderUI()
    {
        if (!hasOrder)
            return;

        string status;

        if (isPrepared)
        {
            status = "SIAP DISAJIKAN";
        }
        else
        {
            status = "BELUM DISIAPKAN";
        }

        orderText.text =
            "PESANAN PELANGGAN\n" +
            currentOrder +
            " x " +
            currentQuantity +
            "\n\n" +
            status;
    }
}