using UnityEngine;

public class StopTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private BoxCollider _coll;

    public void FxPlay()
    {
        _particle.Play();
        _coll.enabled = true;
    }
}
