using UnityEngine;

public static class Delay
{
    public static WaitForSeconds GetRandom(float min, float max) => new(Random.Range(min, max));
}