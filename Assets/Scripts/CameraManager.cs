using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _offset = CalculateOffset();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Transform targetTransform;
        if (_target == null)
        {
            targetTransform = transform;
        }
        else
        {
            targetTransform = _target.transform;
        }

        transform.position = Vector3.Lerp(transform.position, _target.transform.position + _offset, 0.01f);
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
        _offset = CalculateOffset();
    }

    public void SetOffset(Vector3 offset)
    {
        _offset = offset;
    }

    private Vector3 CalculateOffset()
    {
        return _offset = transform.position - _target.transform.position;
    }
}
