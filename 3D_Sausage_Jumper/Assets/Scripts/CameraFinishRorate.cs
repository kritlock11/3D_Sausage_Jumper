using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraFinishRorate : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private CinemachineVirtualCamera _cam;
    private float _angle = 15f;

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void OffFollow()
    {
        var t = _cam.GetCinemachineComponent<CinemachineTransposer>();
        t.m_FollowOffset = new Vector3(-12, 15, -20);

        _cam.Follow = _cam.LookAt = null;

        StartCoroutine(RotAround(20));
    }

    public IEnumerator RotAround(float duration)
    {
        var t = duration;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime;
            transform.RotateAround(_player.position, Vector3.up, _angle * Time.deltaTime);
            yield return null;
        }
    }
}
