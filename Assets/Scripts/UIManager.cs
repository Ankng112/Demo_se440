using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI txtInformation;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowInfoPanel(GameObject go, Vector3 position)
    {
        transform.position = position;
        txtInformation.text = go.name;
        infoPanel.GetComponent<CanvasGroup>().alpha = 1;
    }
}
