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

        if (!TutorialManager.Instance.CompletedTutorials.Contains(TutorialID.Shop))
        {
            TutorialManager.Instance.InstantiateTutorial(TutorialID.Shop);
        }
        // play tutorial if its setting up for the first time
    }
}
