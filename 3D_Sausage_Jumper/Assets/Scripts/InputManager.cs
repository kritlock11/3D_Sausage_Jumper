using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static event Action<Vector3> OnMouseButton;
    public static event Action<Vector3> OnMouseButtonDown;
    public static event Action<Vector3> OnMouseButtonUp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown?.Invoke(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            OnMouseButton?.Invoke(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp?.Invoke(Input.mousePosition);
        }
    }
}
