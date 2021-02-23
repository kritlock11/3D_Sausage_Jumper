using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector3 _camOffset;

    private Camera _cam;

    [SerializeField] private SausageController _sausage;

    private void Awake()
    {
        _cam = Camera.main;
        _camOffset = new Vector3(0, 0, 20f);
    }

    private void OnEnable()
    {
        InputManager.OnMouseButton += OnMouseButton;
        InputManager.OnMouseButtonDown += OnMouseButtonDown;
        InputManager.OnMouseButtonUp += OnMouseButtonUp;
    }
    private void OnDisable()
    {
        InputManager.OnMouseButton -= OnMouseButton;
        InputManager.OnMouseButtonDown -= OnMouseButtonDown;
        InputManager.OnMouseButtonUp -= OnMouseButtonUp;
    }

    private void OnMouseButtonDown(Vector3 v3)
    {
        _sausage.CheckGround();
        _startPos = _cam.ScreenToWorldPoint(v3 + _camOffset);
    }

    private void OnMouseButton(Vector3 v3)
    {
        _endPos = _cam.ScreenToWorldPoint(v3 + _camOffset);

        var v = _startPos - _endPos;

        if (_sausage.OnGround && v.magnitude >= 0.5f)
        {
            _sausage.DrawDirLine(v);
        }
    }

    private void OnMouseButtonUp(Vector3 v3)
    {
        var v = _startPos - _endPos;

        if (_sausage.OnGround && v.magnitude >= 0.5f)
        {
            _sausage.StartJump(v);
        }
    }
}
