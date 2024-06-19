using System;
using UnityEngine;

namespace DefaultNamespace.WaterDemo
{
    public class OceanManager : MonoBehaviour
    {
        [Range(0,5)]
        [SerializeField] private float wavesPower;
        
        [SerializeField] private GameObject ocean;

        private Material _oceanMat;
        private Texture2D _oceanTex;
        
        

        private void Start()
        {
            SetVariable();
        }

        private void SetVariable()
        {
            _oceanMat = ocean.GetComponent<Renderer>().sharedMaterial;
            _oceanTex = (Texture2D)_oceanMat.GetTexture("_secondTex");
        }

        private void OnValidate()
        {
            if (!ocean)
            {
                SetVariable();
            }

            UpdateVariable();
        }

        public float GetWaveHeightAtPosition(Vector3 pos)
        {
            float result = _oceanTex.GetPixelBilinear(pos.x, pos.z).g * wavesPower * ocean.transform.localScale.x;
            return result;
        }

        private void UpdateVariable()
        {
            _oceanMat.SetFloat("_wavePower", wavesPower);
        }
    }
}