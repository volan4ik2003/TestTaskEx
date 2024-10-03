using Code.Runtime.Infrastructure.AssetManagement;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private IAssetProvider _assetProvider;
    public GameFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public GameObject SpawnHero(Vector3 at)
    {
        return _assetProvider.Instantiate(AssetPath.HeroPath, at, Quaternion.identity,null);
    }

    public void Cleanup()
    {
    
    }
}