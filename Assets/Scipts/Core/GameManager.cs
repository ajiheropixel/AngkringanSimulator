using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int currentDay = 1;
    public int money = 0;

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.nKey.wasPressedThisFrame)
        {
            NextDay();
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;

        Debug.Log("Uang bertambah: Rp" + amount);
        Debug.Log("Total uang: Rp" + money);
    }

    public void NextDay()
    {
        currentDay++;

        Debug.Log("Sekarang Hari " + currentDay);
    }
}