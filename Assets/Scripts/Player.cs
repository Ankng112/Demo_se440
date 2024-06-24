using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : NetworkBehaviour
{
    [SerializeField] private float _force = 10f;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        if (! isLocalPlayer) return;
        var cineMachine = FindObjectOfType<CinemachineVirtualCamera>();
        cineMachine.Follow = transform;
        cineMachine.LookAt = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 force = transform.forward * _force;
            _rb.AddForce(force, ForceMode.VelocityChange);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("obstacle"))
        {
            Return(other.gameObject);
        }
    }
    
    private void Return(GameObject obj)
    {
        ObjectPool.Instance.ReturnOne(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        Destroy(other.gameObject, 1f);
    }
}
