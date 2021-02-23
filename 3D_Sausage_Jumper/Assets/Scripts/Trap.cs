using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    public void FxPlay()
    {
        _particle.Play();
    }
}