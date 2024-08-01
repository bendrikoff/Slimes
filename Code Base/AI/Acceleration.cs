using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour
{
    public Action AccelerationStarted;
    public Action AccelerationCanceled;
    
    public float AccelerationForce;
    public bool IsAcceleration;
    public float Duration;

    public float Timer => _durationTimer;
    
    private float _durationTimer;
    private float _cooldownTimer;

    public void Use(bool flag)
    {
        if (_cooldownTimer <= 0 && flag)
        {
            StartAcceleration();
        }
        else
        {
            CancelAcceleration();
        }
    }

    private void Update()
    {
        if (_durationTimer > 0)
        {
            _durationTimer -= Time.deltaTime;
        }
        else if(IsAcceleration && _durationTimer <= 0)
        {
            CancelAcceleration();
        }

        if (_cooldownTimer > 0 && IsAcceleration == false)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    private void StartAcceleration()
    {
        IsAcceleration = true;
        _durationTimer = Duration;
        AccelerationStarted?.Invoke();
        
        _cooldownTimer = Duration;
    }

    private void CancelAcceleration()
    {
        IsAcceleration = false;
        AccelerationCanceled?.Invoke();
    }

}
