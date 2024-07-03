using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public enum WheelType
    {
        Front,
        Rear
    }
    
    [System.Serializable]
    public struct Wheel
    {
        public WheelType type;
        public WheelCollider collider;
        public Transform transform;
        public ParticleSystem dust;
    }
    
    [SerializeField]
    private List<Wheel> wheels = new List<Wheel>();

    [SerializeField] private float speed = 50f;
    [SerializeField] private float steerSpeed = 30f;
    [SerializeField] private float maxSteerAngle = 30f;
    [SerializeField] private Vector3 centerOfMass;
    private float _moveInput;
    private float _steerInput;
    
    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = Input.GetAxis("Vertical");
        _steerInput = Input.GetAxis("Horizontal");
        WheelAnimation();
        BrakeControl();
    }

    private void PlayFx(bool isPlay)
    {
        foreach (var w in wheels)
        {
            if (isPlay)
            {
                w.dust.Play();
            }
            else
            {
                w.dust.Stop();
            }
        }
    }

    private void BrakeControl()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.collider.brakeTorque = 1000;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.collider.brakeTorque = 0;
            }
        }
    }

    private void WheelAnimation()
    {
        foreach (var wheel in wheels)
        {
            Vector3 pos;
            Quaternion rot;
            wheel.collider.GetWorldPose(out pos, out rot);
            wheel.transform.position = pos;
            wheel.transform.rotation = rot;
        }
    }

    private void LateUpdate()
    {
        Move();
        Steer();
    }

    private void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.type == WheelType.Front)
            {
                float steerAngle = _steerInput * maxSteerAngle * steerSpeed;
                wheel.collider.steerAngle = 
                    Mathf.Lerp(wheel.collider.steerAngle, steerAngle, 0.5f);
            }
        }
    }

    private void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.collider.motorTorque = _moveInput * speed;
        }
    }
}
