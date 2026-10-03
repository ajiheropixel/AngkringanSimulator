using UnityEngine;

public class CustomerService : MonoBehaviour
{
    public OrderManager orderManager;
    public CustomerPayment customerPayment;

    public void ServeCustomer()
    {
        if (orderManager == null)
        {
            Debug.LogError(
                "CustomerService: OrderManager tidak ditemukan!"
            );

            return;
        }

        if (!orderManager.hasOrder)
        {
            Debug.Log("Tidak ada pesanan aktif.");
            return;
        }

        if (!orderManager.isPrepared)
        {
            Debug.Log("Pesanan belum disiapkan!");
            return;
        }

        if (customerPayment == null)
        {
            Debug.LogError(
                "CustomerPayment belum terpasang pada Customer!"
            );

            return;
        }

        customerPayment.ProcessPayment();

        orderManager.ServeOrder();

        Debug.Log(
            "Pelanggan menerima pesanannya!"
        );

        CustomerNPC customerNPC =
            GetComponent<CustomerNPC>();

        if (customerNPC != null)
        {
            customerNPC.LeaveCustomer();
        }
        else
        {
            Debug.LogWarning(
                "CustomerNPC tidak ditemukan!"
            );
        }
    }
}