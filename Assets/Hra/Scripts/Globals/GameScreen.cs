using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameScreenType
{
    None = 0,
    Options = 1,
    Death = 2
}

public class GameScreen : MonoBehaviour
{
    [SerializeField] private GameScreenType _gameScreenType;
}
