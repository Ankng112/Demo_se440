using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public enum UIType
{
    Gameplay,
    mainmenu,
    win,
    lose
}

public class UIManager : SingleTon<UIManager>
{
    [SerializeField] private List<UIBase> _uiBases;
    private List<UIBase> _uiCache= new List<UIBase>();

    private void Start()
    {
        // foreach (var ui in _uiBases)
        // {
        //     Hide(ui.UIType);
        // }
        Show(UIType.mainmenu);
    }

    public void Show(UIType uiType)
    {
        var ui = _uiBases.First(x => x.UIType == uiType);
        var go = Instantiate(ui, transform);
        _uiCache.Add(go);

    }

    public void Hide(UIType uiType)
    {
        var ui = _uiCache.First(x => x.UIType == uiType);
        if (ui == null) return;
        
        ui.gameObject.SetActive(false);
        Destroy(ui,1);
    }
}
