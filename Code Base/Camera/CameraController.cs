using UnityEngine;

internal class CameraController: MonoBehaviour
{
    public float MaxZoom = 65;
    public float MinZoom = 5;
    public void Zoom(Vector2 value)
    {
        var zoom = value.y > 0
            ? 1
            : -1;
        if (zoom < 0 && transform.position.y < MaxZoom || zoom > 0 && transform.position.y > MinZoom)
        {
            transform.Translate(new Vector3(0,-zoom*.5f,zoom));
        }
    }
}