using UnityEngine;

public class CustomerNPC : MonoBehaviour
{
    public Transform target;
    public Transform exitTarget;

    public float moveSpeed = 2f;

    public OrderManager orderManager;
    public CustomerSpawner customerSpawner;

    private bool hasArrived = false;
    private bool isLeaving = false;
    private bool canOrder = false;

    void Update()
    {
        if (isLeaving)
        {
            MoveToExit();
            return;
        }

        if (target == null || hasArrived)
            return;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;

        Vector3 direction =
            targetPosition - transform.position;

        if (direction.magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            transform.rotation =
                Quaternion.LookRotation(direction);
        }
        else
        {
            hasArrived = true;

            Debug.Log("Pelanggan tiba di posisi antrean.");

            if (canOrder && orderManager != null)
            {
                orderManager.CreateOrder();

                Debug.Log(
                    "Pelanggan depan mendapatkan pesanan."
                );
            }
        }
    }

    public void SetQueuePosition(
        Transform newTarget,
        bool shouldOrder
    )
    {
        target = newTarget;
        canOrder = shouldOrder;
        hasArrived = false;

        Interactable interactable =
            GetComponent<Interactable>();

        if (interactable != null)
        {
            interactable.enabled = shouldOrder;
        }
    }

    public void LeaveCustomer()
    {
        if (exitTarget == null)
        {
            Debug.LogWarning(
                "CustomerExitPoint belum dihubungkan!"
            );

            return;
        }

        isLeaving = true;
        hasArrived = false;
        canOrder = false;

        Interactable interactable =
            GetComponent<Interactable>();

        if (interactable != null)
        {
            interactable.enabled = false;
        }

        Debug.Log(
            "Pelanggan mulai meninggalkan angkringan."
        );
    }

    void MoveToExit()
    {
        Vector3 exitPosition = exitTarget.position;
        exitPosition.y = transform.position.y;

        Vector3 direction =
            exitPosition - transform.position;

        if (direction.magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                exitPosition,
                moveSpeed * Time.deltaTime
            );

            transform.rotation =
                Quaternion.LookRotation(direction);
        }
        else
        {
            Debug.Log(
                "Pelanggan sudah meninggalkan angkringan."
            );

            if (customerSpawner != null)
            {
                customerSpawner.CustomerFinished(this);
            }

            Destroy(gameObject);
        }
    }
}