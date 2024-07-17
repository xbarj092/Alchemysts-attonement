using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesScreen : GameScreen
{
    public void CloseScreen()
    {
        ScreenEvents.OnGameScreenClosedInvoke(_gameScreenType);
    }
}
