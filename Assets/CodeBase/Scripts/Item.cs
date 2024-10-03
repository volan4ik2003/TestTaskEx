using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool IsCollected;
    private bool CanCollect = false;

    public bool IsCollectedProp {
        get => IsCollected;
        set => IsCollected = value;
    }

    public bool CanCollectProp
    {
        get => CanCollect;
        set => CanCollect = value;
    }
}
