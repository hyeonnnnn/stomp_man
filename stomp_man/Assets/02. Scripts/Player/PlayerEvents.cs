using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static event Action OnStompedEnemy;
    public static event Action OnHitGroundWithoutStomp;

    public void InvokeStompedEnemy()
    {
        OnStompedEnemy?.Invoke();
    }

    public void InvokeHitGroundWithoutStomp()
    {
        OnHitGroundWithoutStomp?.Invoke();
    }
}