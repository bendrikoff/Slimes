using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void Move(Vector3 direction);
    void Rotate(Vector2 inputAngle);
    void Zoom(Vector2 input);
    void Acceleration(bool flag);
}
