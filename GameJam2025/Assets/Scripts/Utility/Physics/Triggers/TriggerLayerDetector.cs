using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerLayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;

    public event Action<Collider> OnEnter;
    public event Action<Collider> OnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            OnEnter?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            OnExit?.Invoke(other);
        }
    }
}
