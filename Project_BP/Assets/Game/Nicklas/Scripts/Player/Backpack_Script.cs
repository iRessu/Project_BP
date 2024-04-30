using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack_Script : MonoBehaviour
{
    private Transform _bpOriginalParent;
    private Rigidbody _rb;
    private GameObject _spawnedObject;
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public float removalRange = 2f;

    private void Start()
    {
        _bpOriginalParent = transform.parent;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))

        {
            if (_spawnedObject == null)
            {
                SpawnObject();
            }
            else
            {
                if (IsPlayerInRange())
                {
                    RemoveObject();
                }
            }
        }
    }

    public void BackPackSetParent(Transform newParent)
    {
        _bpOriginalParent = transform.parent;
        transform.parent = newParent;
    }
    public void BackPackResetParent()
    {
        transform.parent = null;
    }

    private void SpawnObject()
    {
        _spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
    }

    private void RemoveObject()
    {
        Destroy(_spawnedObject);
        _spawnedObject = null;
    }

    private bool IsPlayerInRange()
    {
        if(_spawnedObject != null)
        {
            float distance = Vector3.Distance(transform.position, _spawnedObject.transform.position);
            return distance <= removalRange;
        }
        return false;
    }

}
