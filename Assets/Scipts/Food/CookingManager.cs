using UnityEngine;
using TMPro;

public enum CookingQuality
{
    None,
    Perfect,
    Normal,
    Burned
}
public class CookingManager : MonoBehaviour
{
    public OrderManager orderManager;

    public GameObject cookingUI;
    public TextMeshProUGUI cookingText;

    public float cookingTime = 5f;
    public float perfectTime = 3f;
public float normalTime = 5f;

    private float currentCookingTime = 0f;

    private bool isCooking = false;

    private bool hasFinishedCooking = false;
    public CookingQuality currentQuality =
    CookingQuality.None;

    void Update()
{
    if (!isCooking)
        return;

    currentCookingTime += Time.deltaTime;

    UpdateCookingUI();
}

    public void StartCooking()
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
            Debug.Log(
                "Tidak ada pesanan."
            );

            return;
        }

        if (isCooking)
        {
            Debug.Log(
                "Pesanan sedang dimasak."
            );

            return;
        }

        if (orderManager.isPrepared)
        {
            Debug.Log(
                "Pesanan sudah siap."
            );

            return;
        }

        isCooking = true;
        hasFinishedCooking = false;

        currentCookingTime = 0f;

        cookingUI.SetActive(true);

        Debug.Log(
            "Mulai memasak " +
            orderManager.GetCurrentOrder()
        );
    }

    void UpdateCookingUI()
    {
        float progress =
            currentCookingTime / cookingTime;

        int percentage =
            Mathf.RoundToInt(progress * 100f);

        cookingText.text =
            "MEMASAK...\n" +
            percentage +
            "%";
    }

    void FinishCooking()
{
    isCooking = false;
    hasFinishedCooking = true;

    currentQuality = CookingQuality.Normal;

    cookingText.text =
        "MASAK SELESAI!";

    Debug.Log(
        "Masakan mencapai batas waktu normal."
    );

    orderManager.PrepareOrder();
}
public void TakeFood()
{
    if (!isCooking)
    {
        Debug.LogWarning("Tidak ada masakan yang sedang dimasak.");
        return;
    }

    isCooking = false;
    hasFinishedCooking = true;

    if (currentCookingTime <= perfectTime)
    {
        currentQuality = CookingQuality.Perfect;
    }
    else if (currentCookingTime <= normalTime)
    {
        currentQuality = CookingQuality.Normal;
    }
    else
    {
        currentQuality = CookingQuality.Burned;
    }

    ShowCookingResult();

    if (orderManager != null)
    {
        orderManager.PrepareOrder();
    }
    else
    {
        Debug.LogError("OrderManager belum terhubung!");
    }
}
void ShowCookingResult()
{
    string result = "";

    switch (currentQuality)
    {
        case CookingQuality.Perfect:
            result = "PERFECT!";
            break;

        case CookingQuality.Normal:
            result = "NORMAL";
            break;

        case CookingQuality.Burned:
            result = "BURNED!";
            break;
    }

    cookingText.text = result;

    Debug.Log(
        "Hasil masakan: " + result
    );
}
public bool IsCooking()
{
    return isCooking;
}

public bool HasFinishedCooking()
{
    return hasFinishedCooking;
}
public void InteractWithStation()
{
    Debug.Log("Tombol E terdeteksi di CookingStation.");

    if (isCooking)
    {
        Debug.Log("Pemain mencoba mengangkat masakan.");

        TakeFood();
    }
    else if (!hasFinishedCooking)
    {
        Debug.Log("Memulai proses memasak.");

        StartCooking();
    }
    else
    {
        Debug.Log("Masakan sudah diangkat.");
    }
}
}