using System;
using UnityEngine;

namespace DefaultNamespace.WaterDemo
{
    [RequireComponent(typeof(Rigidbody))]
    public class BuoyancyObject : MonoBehaviour
    {
        [SerializeField] private Transform[] floatingPoints;
        [SerializeField] private float underWaterDrag = 3f;
        [SerializeField] private float underWaterAngularDrag = 1f;
        [SerializeField] private float airDrag = 0f;
        [SerializeField] private float airAngularDrag = 0.05f;
        [SerializeField] private float floatingPower = 15f;
        [SerializeField] private float waterHeight = 0;
        [SerializeField] private OceanManager oceanManager;
        private bool _isUnderWater;
        private Rigidbody _rb;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            int countUnderWater = 0;
            foreach (var fp in floatingPoints)
            {
                var pDiff = fp.position.y - oceanManager.GetWaveHeightAtPosition(fp.position);
                if (pDiff < 0)
                {
                    _isUnderWater = true;
                    _rb.AddForceAtPosition(Vector3.up * Mathf.Abs(pDiff) * floatingPower, fp.position, ForceMode.Acceleration);
                    countUnderWater++;
                    if (!_isUnderWater)
                    {
                        _isUnderWater = true;
                        SwitchState(true);
                    }
                }
            }
            
            // var diff = transform.position.y - waterHeight;
            // if (diff < 0)
            // {
            //     _rb.AddForceAtPosition(Vector3.up * Mathf.Abs(diff) * floatingPower, transform.position, ForceMode.Acceleration);
            //     if (!_isUnderWater)
            //     {
            //         _isUnderWater = true;
            //         SwitchState(true);
            //     }
            // }
            // else 
            if(_isUnderWater && countUnderWater == 0)
            {
                _isUnderWater = false;
                SwitchState(false);
            }
        }

        private void SwitchState(bool underWater)
        {
            if (underWater)
            {
                _rb.drag = underWaterDrag;
                _rb.angularDrag = underWaterAngularDrag;
            }
            else
            {
                _rb.drag = airDrag;
                _rb.angularDrag = airAngularDrag;
            }
        }
    }
}