using System;
using Code_Base.Obstacles;
using UnityEngine;

public class Food : MonoBehaviour, IEatable
{
    public Action OnEated;
    public float StartForce;
    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;
    
    public float Force
    {
        get => StartForce;
        set
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
            Force = value;
        }
    }

    public void Interact(Character character)
    {
        character.OnEat?.Invoke(this);
        OnEated?.Invoke();
    }
    
}