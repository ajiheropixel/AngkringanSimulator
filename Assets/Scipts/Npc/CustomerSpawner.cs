using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public AngkringanManager angkringanManager;
    public OrderManager orderManager;
    public GameManager gameManager;

    public GameObject customerPrefab;

    public Transform spawnPoint;
    public Transform exitPoint;

    public Transform[] queuePoints;

    public int maxCustomers = 3;
    public float spawnDelay = 3f;

    private List<GameObject> customers =
        new List<GameObject>();

    private float timer = 0f;

    void Update()
    {
        if (angkringanManager == null)
            return;

        if (!angkringanManager.isOpen)
        {
            timer = 0f;
            return;
        }

        RemoveDestroyedCustomers();

        if (customers.Count < maxCustomers)
        {
            timer += Time.deltaTime;

            if (timer >= spawnDelay)
            {
                SpawnCustomer();
                timer = 0f;
            }
        }
    }

    void SpawnCustomer()
    {
        if (customerPrefab == null)
        {
            Debug.LogWarning(
                "Customer Prefab belum dihubungkan!"
            );

            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogWarning(
                "Spawn Point belum dihubungkan!"
            );

            return;
        }

        if (queuePoints == null ||
            queuePoints.Length == 0)
        {
            Debug.LogWarning(
                "Queue Points belum dibuat!"
            );

            return;
        }

        if (customers.Count >= queuePoints.Length)
            return;

        GameObject newCustomer = Instantiate(
            customerPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        customers.Add(newCustomer);

        CustomerNPC customerNPC =
            newCustomer.GetComponent<CustomerNPC>();

        if (customerNPC != null)
        {
            customerNPC.orderManager = orderManager;
            customerNPC.exitTarget = exitPoint;
            customerNPC.customerSpawner = this;

            int queueIndex =
                customers.Count - 1;

            bool canOrder =
                queueIndex == 0;

            customerNPC.SetQueuePosition(
                queuePoints[queueIndex],
                canOrder
            );
        }

        CustomerService customerService =
            newCustomer.GetComponent<CustomerService>();

        if (customerService != null)
        {
            customerService.orderManager =
                orderManager;
        }

        CustomerPayment customerPayment =
            newCustomer.GetComponent<CustomerPayment>();

        if (customerPayment != null)
        {
            customerPayment.orderManager =
                orderManager;

            customerPayment.gameManager =
                gameManager;
        }

        Debug.Log(
            "Pelanggan baru masuk antrean."
        );
    }

    public void CustomerFinished(
        CustomerNPC finishedCustomer
    )
    {
        GameObject finishedObject =
            finishedCustomer.gameObject;

        if (customers.Contains(finishedObject))
        {
            customers.Remove(finishedObject);
        }

        UpdateQueuePositions();

        Debug.Log(
            "Antrean diperbarui."
        );
    }

    void UpdateQueuePositions()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            GameObject customer =
                customers[i];

            if (customer == null)
                continue;

            CustomerNPC customerNPC =
                customer.GetComponent<CustomerNPC>();

            if (customerNPC != null)
            {
                bool canOrder =
                    i == 0;

                customerNPC.SetQueuePosition(
                    queuePoints[i],
                    canOrder
                );
            }
        }
    }

    void RemoveDestroyedCustomers()
    {
        customers.RemoveAll(
            customer => customer == null
        );
    }
}