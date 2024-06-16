using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BonusPickUp : MonoBehaviour
{
    [SerializeField] private VisualEffect pickupEffect;

    private void OnDestroy()
    {
        pickupEffect.Play();
    }
}
