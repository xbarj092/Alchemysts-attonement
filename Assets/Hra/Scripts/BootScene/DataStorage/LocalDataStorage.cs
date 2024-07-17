using UnityEngine;

public class LocalDataStorage : MonoSingleton<LocalDataStorage>
{
    [field: SerializeField] public PlayerData PlayerData;
    [field: SerializeField] public GameData GameData;
    [field: SerializeField] public Catalog Catalog;

    private void Awake()
    {
        Catalog.InitializeCatalog();
    }
}
