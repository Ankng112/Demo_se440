using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    // DAY LA CLASS SINGLETON HET
    public class SingleTon<T> : MonoBehaviour where T : SingleTon<T>
    {
        private static T _instance;
        public static T Instance => _instance;
        [SerializeField] private bool isDontDestroyOnload;
            
        private void Awake()
        {
            // tui tao cái instance cho class này
            // oke 

            if (_instance == null)
            {
                _instance = (T) this; 
            }
            else
            {
                Destroy(gameObject);
            }

            if (isDontDestroyOnload)
            {
                DontDestroyOnLoad(this);
            }
        }
    }
}