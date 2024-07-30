using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubSceneManager
{
    public void SetUpHubScene()
    {
        PlayerWeapons weapons = GameObject.FindObjectOfType<PlayerWeapons>();
        if (weapons != null)
        {
            weapons.SetWeapon(LocalDataStorage.Instance.PlayerData.LoadoutData);
        }

        // play tutorial if its setting up for the first time
    }
}
