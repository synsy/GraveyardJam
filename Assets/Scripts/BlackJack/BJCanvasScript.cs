using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJCanvasScript : MonoBehaviour
{
    public static BJCanvasScript Instance;
    public BlackJackManager blackJackManager;
    public BuyMenuScript buyMenuScript;
   
    private void OnEnable()
    {
        blackJackManager.RefreshMoney();
    }
}
