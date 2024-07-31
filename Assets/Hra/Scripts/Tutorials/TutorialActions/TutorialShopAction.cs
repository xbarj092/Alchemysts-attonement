using System;
using System.Collections;
using UnityEngine;

public class TutorialShopAction : TutorialAction
{
    [Header("TextPositions")]
    [SerializeField] private Transform _defaultTransform;
    [SerializeField] private Transform _shopClickTransform;
    [SerializeField] private Transform _weaponClickTransform;
    [SerializeField] private Transform _elementClickTransform;
    [SerializeField] private Transform _elementSlotClickTransform;
    [SerializeField] private Transform _afterPurchaseTransform;
    [SerializeField] private Transform _loadoutClickTransform;
    [SerializeField] private Transform _elementLoadoutClickTransform;
    [SerializeField] private Transform _statsLoadoutClickTransform;
    [SerializeField] private Transform _postLoadoutTransform;

    public override void StartAction()
    {
        _tutorialPlayer.SetTextPosition(_shopClickTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        ScreenEvents.OnGameScreenOpened += OnScreenOpened1;
    }

    private void OnScreenOpened1(GameScreenType type)
    {
        if (type == GameScreenType.Upgrades)
        {
            OnShopOpened();
        }
    }

    private void OnShopOpened()
    {
        ScreenEvents.OnGameScreenOpened -= OnScreenOpened1;
        _tutorialPlayer.transform.SetAsLastSibling();
        _tutorialPlayer.SetTextPosition(_weaponClickTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();

        foreach (UpgradeSlot upgradeSlot in FindObjectsOfType<UpgradeSlot>())
        {
            if (upgradeSlot.FriendlyId != "weapon_straight_sword")
            {
                upgradeSlot.Button.interactable = false;
            }
        }

        TutorialEvents.OnItemClicked += OnWeaponClicked;
    }

    private void OnWeaponClicked(string friendlyId)
    {
        foreach (UpgradeSlot upgradeSlot in FindObjectsOfType<UpgradeSlot>())
        {
            upgradeSlot.Button.interactable = true;
        }

        if (friendlyId == "weapon_straight_sword")
        {
            OnStraightSwordPurchased();
        }
    }

    private void OnStraightSwordPurchased()
    {
        TutorialEvents.OnItemClicked -= OnWeaponClicked;
        _tutorialPlayer.SetTextPosition(_elementClickTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();

        foreach (UpgradeSlot upgradeSlot in FindObjectsOfType<UpgradeSlot>())
        {
            if (upgradeSlot.FriendlyId != "element_life")
            {
                upgradeSlot.Button.interactable = false;
            }
        }

        TutorialEvents.OnItemClicked += OnElementClicked;
    }

    private void OnElementClicked(string friendlyId)
    {
        foreach (UpgradeSlot upgradeSlot in FindObjectsOfType<UpgradeSlot>())
        {
            upgradeSlot.Button.interactable = true;
        }

        if (friendlyId == "element_life")
        {
            OnLifeElementPurchased();
        }
    }

    private void OnLifeElementPurchased()
    {
        TutorialEvents.OnItemClicked -= OnElementClicked;
        _tutorialPlayer.SetTextPosition(_elementSlotClickTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();

        foreach (UpgradeSlot upgradeSlot in FindObjectsOfType<UpgradeSlot>())
        {
            if (upgradeSlot.FriendlyId != "item_element_slot")
            {
                upgradeSlot.Button.interactable = false;
            }
        }

        TutorialEvents.OnItemClicked += OnElementSlotClicked;
    }

    private void OnElementSlotClicked(string friendlyId)
    {
        foreach (UpgradeSlot upgradeSlot in FindObjectsOfType<UpgradeSlot>())
        {
            upgradeSlot.Button.interactable = true;
        }

        if (friendlyId == "item_element_slot")
        {
            OnLifeElementSlotPurchased();
        }
    }

    private void OnLifeElementSlotPurchased()
    {
        TutorialEvents.OnItemClicked -= OnElementSlotClicked;
        _tutorialPlayer.SetTextPosition(_afterPurchaseTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        FindObjectOfType<UpgradesScreen>().CanClose = true;
        ScreenEvents.OnGameScreenClosed += OnScreenClosed;
    }

    private void OnScreenClosed(GameScreenType type)
    {
        if (type == GameScreenType.Upgrades)
        {
            OnShopClosed();
        }
    }

    private void OnShopClosed()
    {
        ScreenEvents.OnGameScreenClosed -= OnScreenClosed;
        _tutorialPlayer.SetTextPosition(_loadoutClickTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        ScreenEvents.OnGameScreenOpened += OnScreenOpened2;
    }

    private void OnScreenOpened2(GameScreenType type)
    {
        if (type == GameScreenType.Loadout)
        {
            OnLoadoutOpened();
        }
    }

    private void OnLoadoutOpened()
    {
        ScreenEvents.OnGameScreenOpened -= OnScreenOpened2;
        _tutorialPlayer.transform.SetAsLastSibling();
        _tutorialPlayer.SetTextPosition(_elementLoadoutClickTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        TutorialEvents.OnElementClicked += OnElementLoadoutClicked;
    }

    private void OnElementLoadoutClicked()
    {
        TutorialEvents.OnElementClicked -= OnElementLoadoutClicked;
        _tutorialPlayer.SetTextPosition(_statsLoadoutClickTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        FindObjectOfType<LoadoutScreen>().CanClose = true;
        ScreenEvents.OnGameScreenClosed += OnGameScreenClosed;
    }

    private void OnGameScreenClosed(GameScreenType type)
    {
        if (type == GameScreenType.Loadout)
        {
            StartCoroutine(OnLoadoutClosed());
        }
    }

    private IEnumerator OnLoadoutClosed()
    {
        ScreenEvents.OnGameScreenClosed -= OnGameScreenClosed;
        _tutorialPlayer.SetTextPosition(_postLoadoutTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
        yield return new WaitForSeconds(5);
        OnActionFinishedInvoke();
    }

    public override void Exit()
    {
    }
}
