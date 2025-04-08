using NaughtyAttributes;
using UnityEngine;

public class DebugExp : MonoBehaviour
{
    [SerializeField] private int exp;

    [Button]
    private void AddExp()
    {
        Entities.Player.Self.Exp += exp;
    }
}