using UnityEngine;

public abstract class Chapter : MonoBehaviour
{
    public void PlayChapter()
    {
        var type = GetType();
        print($"CHAPTER[{type}] STARTED!");
    }
}
