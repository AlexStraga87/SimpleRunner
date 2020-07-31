using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AboutPanelUI : MonoBehaviour
{
    [SerializeField] private RectTransform _aboutPanel;
    [SerializeField] private RectTransform _menuPanel;

    public void CloseAboutButtonClick()
    {
        _aboutPanel.DOAnchorPos(new Vector3(0, 550), 0.7f);
        _menuPanel.DOAnchorPos(Vector3.zero, 0.7f);
    }

    public void ShowAboutButtonClick()
    {
        _aboutPanel.DOAnchorPos(Vector3.zero, 0.7f);
        _menuPanel.DOAnchorPos(new Vector3(-1000, 0), 0.7f);
    }

}
