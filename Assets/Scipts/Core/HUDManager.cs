using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public GameManager gameManager;

    public TextMeshProUGUI dayText;
    public TextMeshProUGUI moneyText;

    void Update()
    {
        if (gameManager == null)
            return;

        dayText.text = "Hari " + gameManager.currentDay;

        moneyText.text = "Rp" + gameManager.money.ToString("N0");
    }
}