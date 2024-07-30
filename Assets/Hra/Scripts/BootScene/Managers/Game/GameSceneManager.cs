using UnityEngine;

public class GameSceneManager
{
    public void SetUpGameScene()
    {
        PlayerWeapons weapons = GameObject.FindObjectOfType<PlayerWeapons>();
        if (weapons != null)
        {
            weapons.SetWeapon(LocalDataStorage.Instance.PlayerData.LoadoutData);
        }

        // play tutorial if its setting up for the first time
    }
}
