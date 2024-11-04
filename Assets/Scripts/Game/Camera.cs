using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = Player.instance.transform;
        GetComponent<CinemachineVirtualCamera>().LookAt = Player.instance.transform;
    }
}
