using System;
using System.Collections.Generic;
using Code_Base.Obstacles;
using UnityEngine;

[RequireComponent(typeof(Acceleration))]
public class Player : Character, IControllable
{
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private GameObject _meshes;

    [SerializeField] private float _force;

    [SerializeField] private PlayerMoneys _playerMoneys;
    
    private CharacterController _controller;
    
    private float _colliderRadius = 50;
    
    private Acceleration _acceleration;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _acceleration = GetComponent<Acceleration>();
        
    }

    #region Control

    public void Rotate(Vector2 direction)
    {
        transform.Rotate(new Vector2(0, direction.x));
    }

    public void Zoom(Vector2 input)
    {
        _cameraController.Zoom(input);
    }

    public void Acceleration(bool flag)
    {
        _acceleration.Use(flag);
    }

    public override void Move(Vector3 direction)
    {
        var speed = GetCurrentSpeed();
        var movementVector = transform.right * direction.x + direction.z * transform.forward;
        _controller.Move(movementVector * speed * Time.fixedDeltaTime);
    }
    
    #endregion

    //Когда с объектом кто-то взаимодействует
    public override void Interact(Character character)
    {
        if (character == this) return;
        base.Interact(character);
    }

    public override void Resize(Vector3 value)
    {
        _meshes.transform.localScale += value;
        _colliderRadius += value.x/10;
    }

    public override void Eat(IEatable food)
    {
        base.Eat(food);
        _playerMoneys.GetMoney();
    }

    private void Update()
    {
        _force = Force;
        
        Vector3 sphereCenter = new Vector3(transform.position.x, transform.position.y - 50,transform.position.z);
        var hitColliders = Physics.OverlapSphere(sphereCenter, _colliderRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out ICharacterInteractable interactable))
            {
                interactable.Interact(this);
            }
        }
    }

    private float GetCurrentSpeed() => _acceleration.IsAcceleration
        ? MoveSpeed * _acceleration.AccelerationForce
        : MoveSpeed;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 sphereCenter = new Vector3(transform.position.x, transform.position.y - 50,transform.position.z);
        Gizmos.DrawWireSphere(sphereCenter, _colliderRadius);
    }
}