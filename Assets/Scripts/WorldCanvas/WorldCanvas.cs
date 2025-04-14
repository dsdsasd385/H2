using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public abstract class WorldCanvas : MonoBehaviour
{
    private static readonly Dictionary<string, ObjectPool<WorldCanvas>> Pool = new();

    public static void InitPool()
    {
        foreach (var pool in Pool.Values)
            pool.Clear();
    }

    public static T Get<T>(Vector3 initPos, float scale = 1f) where T : WorldCanvas
    {
        var canvasName = typeof(T).Name;

        if (Pool.ContainsKey(canvasName) == false)
        {
            ObjectPool<WorldCanvas> pool = new
            (
                createFunc: () =>
                {
                    var prefab = ResourceManager.GetPrefab<T>(canvasName);
                    var canvas = Instantiate(prefab);
                    canvas.gameObject.SetActive(false);
                    return canvas;
                },
                
                actionOnGet: canvas =>
                {
                    canvas.transform.position = initPos;
                    Debug.Log("DamageText À§Ä¡: " + canvas.transform.position);

                    canvas.Cg.alpha = 1f;
                    canvas._isReleased = false;
                });

            Pool[canvasName] = pool;
        }

        var canvas = Pool[canvasName].Get();
        canvas.Initialize(scale);
        canvas.gameObject.SetActive(true);
        return canvas as T;
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/

    private CanvasGroup _cg;
    protected CanvasGroup Cg
    {
        get
        {
            if (_cg == null)
                _cg = transform.GetComponent<CanvasGroup>();
            
            if (_cg == null)
                _cg = gameObject.AddComponent<CanvasGroup>();
            return _cg;
        }
    }
    
    private Camera _cam;
    private bool   _isReleased;

    protected abstract void Initialize(float scale);

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.forward = _cam.transform.forward;
    }

    public void Release(float delay = 0f)
    {
        if (_isReleased)
            return;

        StartCoroutine(ReleaseCoroutine(delay));
    }

    private IEnumerator ReleaseCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        _isReleased = true;
        
        gameObject.SetActive(false);
        
        Pool[GetType().Name]?.Release(this);
    }
}