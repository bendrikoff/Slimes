using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputController : MonoBehaviour
{
    private IControllable _controllable;

    private GameInput _gameInput;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();

        _controllable = GetComponent<IControllable>();

        _gameInput.Gameplay.Acceleration.started += context =>
        {
            _controllable.Acceleration(true);
        };
        _gameInput.Gameplay.Acceleration.canceled += context =>
        {
            _controllable.Acceleration(false);
        };
        
        _gameInput.Gameplay.Zoom.performed += context =>
        {
            _controllable.Zoom(context.ReadValue<Vector2>());
        };
    }

    private void Update()
    {
        ReadMove();
        ReadRotate();
    }

    private void ReadRotate()
    {
        if (_gameInput.Gameplay.Click.IsInProgress())
        {
            var inputAngle = _gameInput.Gameplay.Rotation.ReadValue<Vector2>();
            _controllable.Rotate(inputAngle);
        }
    }

    private void ReadMove()
    {
        var inputDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
        _controllable.Move(new Vector3(inputDirection.x, 0, inputDirection.y));
    }
}
