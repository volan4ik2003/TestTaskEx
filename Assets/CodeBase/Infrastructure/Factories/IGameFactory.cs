using UnityEngine;

public interface IGameFactory
{
   
    public GameObject SpawnHero(Vector3 at);

    void Cleanup();
}