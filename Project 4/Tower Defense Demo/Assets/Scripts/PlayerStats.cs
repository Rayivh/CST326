using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money, Lives;
    public int startMoney = 400;
    public int startLives = 20;
    void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }
}
