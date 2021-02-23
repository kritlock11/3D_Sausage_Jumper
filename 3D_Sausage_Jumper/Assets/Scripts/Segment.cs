using UnityEngine;

public class Segment : MonoBehaviour
{
    private SausageController _parent;
    public Rigidbody Rb { get; private set; }
    private float _radius = 0.5f;
    private LayerMask _whatIsGround;

    private void Awake()
    {
        _parent = transform.parent.GetComponent<SausageController>();
        Rb = GetComponent<Rigidbody>();
        _whatIsGround = 1 << LayerMask.NameToLayer("Stairs") | 1 << LayerMask.NameToLayer("Floor");
    }

    private void OnCollisionEnter(Collision coll)
    {
        _parent.OnSegmentCollisionEnter(coll);
    }

    public bool CheckGround()
    {
        return Physics.OverlapSphere(transform.position, _radius, _whatIsGround).Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

}
