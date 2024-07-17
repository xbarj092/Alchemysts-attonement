using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalDataStorage : MonoSingleton<LocalDataStorage>
{
    public PlayerData PlayerData;
    public GameData GameData;
}
