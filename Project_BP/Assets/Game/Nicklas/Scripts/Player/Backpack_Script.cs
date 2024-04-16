using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack_Script : MonoBehaviour
{
    private Transform _bpOriginalParent;
    private Rigidbody _rb;

    private void Start()
    {
        _bpOriginalParent = transform.parent;
        _rb = GetComponent<Rigidbody>();
    }

    public void BackPackSetParent(Transform newParent)
    {
        _rb.constraints = (RigidbodyConstraints)RigidbodyConstraints2D.None;
        _bpOriginalParent = transform.parent;
        transform.parent = newParent;
    }
    public void BackPackResetParent()
    {
        _rb.constraints = (RigidbodyConstraints)RigidbodyConstraints2D.FreezePositionX;
        transform.parent = null;
    }
}
