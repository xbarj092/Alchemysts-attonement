using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasController : MonoBehaviour
{
    [SerializeField] private MenuMainButtons _menuMainButtons;

    private void OnEnable()
    {
        // _menuMainButtons.OnOptionsOpened += XXX;
    }

    private void OnDisable()
    {
        // _menuMainButtons.OnOptionsOpened -= XXX;
    }
}
