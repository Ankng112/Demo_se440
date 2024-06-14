using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private float rotationSpeed = 5f;
        private void FixedUpdate()
        {
            Follow();
            Rotate();
        }
        
        private void Rotate()
        {
            
        }

        private void Follow()
        {
            Debug.Log(target.forward);
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
            Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = smoothedRotation;
        }
    }
}