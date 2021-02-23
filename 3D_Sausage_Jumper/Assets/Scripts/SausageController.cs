using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SausageController : MonoBehaviour
{
    [SerializeField] private CameraFinishRorate _cam;
    [SerializeField] private UIFinish _uiFinish;

    [SerializeField] private List<Segment> _segments;
    [SerializeField] private List<GameObject> _segmentsPos;
    [SerializeField] private AnimationCurve _curve;

    private LineRenderer _dirLine;
    [SerializeField] private Material _mat;
    [SerializeField] private Gradient _gradient;

    private Coroutine _lerpRoutine;

    private GameObject _center;
    public bool OnGround { get; private set; }
    private float _jumpForce = 12f;

    private float _cangeStiffTime = 1f;
    private float _minStiffness = 500;
    private float _maxStiffness = 1000f;

    private Vector2 _lineBounds = new Vector2(1, 6);

    private float _respawnAfter = 0.4f;

    private void Start()
    {
        FindCenterSegment();

        if (_dirLine) return;
        _dirLine = _center.gameObject.AddComponent<LineRenderer>();

        SetDirLine();
    }

    public void CheckGround()
    {
        foreach (var segment in _segments)
        {
            if (segment.CheckGround())
            {
                OnGround = true;
                break;
            }
        }
    }

    public void DrawDirLine(Vector3 v)
    {
        v.z = 0;

        _dirLine.enabled = true;
        _dirLine.positionCount = 2;

        _dirLine.SetPosition(0, _center.transform.position);
        _dirLine.SetPosition(1, _center.transform.position + v.normalized * Mathf.Clamp(v.magnitude, _lineBounds.x, _lineBounds.y));
    }

    public void StartJump(Vector3 v)
    {
        _dirLine.enabled = false;

        SetStiffness(_maxStiffness);

        var magnitude = Mathf.Clamp(v.magnitude, _lineBounds.x, _lineBounds.y);

        foreach (var t in _segments)
        {
            t.Rb.AddForceAtPosition(v.normalized * magnitude * _jumpForce, _center.transform.position + v.normalized * magnitude, ForceMode.Impulse);
        }

        OnGround = false;
        LerpStiffness(_minStiffness, _cangeStiffTime);
    }

    private void LerpStiffness(float size, float duration)
    {
        if (_lerpRoutine != null)
        {
            StopCoroutine(_lerpRoutine);
        }

        _lerpRoutine = StartCoroutine(LerpSpring(size, duration));
    }

    private IEnumerator LerpSpring(float val, float duration)
    {
        var baseS = _maxStiffness;
        var t = duration;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;
            var progress = 1f - t / duration;
            SetStiffness(Mathf.Lerp(baseS, val, _curve.Evaluate(progress)));
            yield return null;
        }
    }

    private void SetStiffness(float value)
    {
        foreach (var seg in _segments)
        {
            var s = new JointSpring { spring = value };

            seg.TryGetComponent<HingeJoint>(out var joint);
            if (joint)
            {
                joint.spring = s;
            }
        }
    }

    public void OnSegmentCollisionEnter(Collision coll)
    {
        CheckGround();

        var layer = coll.gameObject.layer;

        switch (layer)
        {

            case 11:
                coll.transform.parent.TryGetComponent<Trap>(out var trap);
                trap?.FxPlay();
                RbToKinematic();
                StartCoroutine(LoadScene(_respawnAfter));
                break;

            case 13:
                _cam.OffFollow();
                _uiFinish.ActiveFinUi();
                coll.transform.TryGetComponent<StopTrigger>(out var stop);
                stop?.FxPlay();
                break;
        }
    }

    private IEnumerator LoadScene(float t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void RbToKinematic()
    {
        var rbs = gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (var rb in rbs)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rb.isKinematic = true;
        }
    }

    private void FindCenterSegment()
    {
        var i = Mathf.CeilToInt(_segments.Count / 2f);
        _center = _segments[i].gameObject;
    }

    private void SetDirLine()
    {
        _dirLine.startWidth = 3f;
        _dirLine.endWidth = 0.3f;
        _dirLine.numCapVertices = 5;
        _dirLine.material = _mat;
        _dirLine.colorGradient = _gradient;
    }

}
