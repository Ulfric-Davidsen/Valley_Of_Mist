using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;

    public void OnAfterDeserialize()
    {
    }

    public void OnBeforeSerialize()
    {
    }
}
