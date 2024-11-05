using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
