using System;
using Code_Base.Obstacles;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Character : MonoBehaviour, IEatable
{
    public float MoveSpeed;
    
    public float Force { get; set; }

    public Vector3 Position => transform.position;
    
    public GameObject GameObject => gameObject;

    //Съел что-то
    public Action<IEatable> OnEat;
    
    public Action OnDeath;
    protected virtual void OnEnable()
    {
        OnEat += Eat;
    }
    protected virtual void OnDisable()
    {
        OnEat -= Eat;
    }

    protected void Start()
    {
        Force = 5;
    }

    public abstract void Move(Vector3 direction);

    public virtual void Interact(Character character)
    {
        if (character.Force < Force)
        {
            Eat(character);
            character.gameObject.SetActive(false);
            character.OnDeath?.Invoke();
        }
        else
        {
            character.Eat(this);
            gameObject.SetActive(false);
            OnDeath?.Invoke();
        }
    }
    
    public virtual void Eat(IEatable food)
    {
        Force++;
        Resize(new Vector3(0.5f,0.5f,0.5f));
    }

    public virtual void Resize(Vector3 value) => transform.localScale += value;

    public void SizeMultiplier(int value)
    {
        Force += value;
        Resize(new Vector3(value/2,value/2,value/2));
    }
    
}
