using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankBalanceScript : MonoBehaviour
{
    public Text bank;
    public void UpdateBank(int money)
    {
        bank.text = money.ToString();
    }
}
