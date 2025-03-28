using UnityEngine;

public static class Delay
{
    public static WaitForSeconds WaitRandom(float min, float max) => new(Random.Range(min, max));
    public static WaitForSeconds Wait(float delay) => new(delay);
}