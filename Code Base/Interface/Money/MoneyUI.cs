using System;
using System.Collections;
using System.Collections.Generic;
using Code_Base.Interface.Money;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    public void MoneyChanged(int money)
    {
        MoneyText.text = money >= 1000 ? (money / 1000).ToString() : money.ToString();
    }
}
