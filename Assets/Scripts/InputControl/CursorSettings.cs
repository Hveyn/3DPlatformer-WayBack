using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorSettings : MonoBehaviour
{
    
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
    }
}
