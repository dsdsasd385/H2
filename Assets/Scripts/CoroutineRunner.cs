using UnityEngine;
using System.Collections;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;

    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("[CoroutineRunner]");
                _instance = go.AddComponent<CoroutineRunner>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public IEnumerator RunCoroutine(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
    }
}
