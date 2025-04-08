using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Projectile : MonoBehaviour
{
    private static readonly Dictionary<string, ObjectPool<Projectile>> Pool = new();
    
    public static void InitPool()
    {
        foreach (var pool in Pool.Values)
            pool.Clear();
    }

    public static T Get<T>(Vector3 initPosition, Transform target, float scale = 1f) where T : Projectile
    {
        var projectileName = typeof(T).Name;

        if (Pool.ContainsKey(projectileName) == false)
        {
            ObjectPool<Projectile> pool = new
            (
                createFunc: () =>
                {
                    var prefab = ResourceManager.GetPrefab<T>(projectileName);
                    var projectile = Instantiate(prefab);
                    projectile.gameObject.SetActive(false);
                    return projectile;
                },
                
                actionOnGet: p =>
                {
                    p._isReleased = false;
                    p.view.enabled = true;
                    p.transform.position = initPosition;
                    p.transform.rotation = Quaternion.LookRotation(target.position);
                    p.transform.localScale = Vector3.one * scale;
                });

            Pool[projectileName] = pool;
        }

        var projectile = Pool[projectileName].Get();
        projectile.Initialize();
        projectile.gameObject.SetActive(true);
        return projectile as T;
    }
    
    /******************************************************************************************************************/
    /******************************************************************************************************************/

    [SerializeField] private SpriteRenderer view;

    private Camera _cam;
    private bool   _isReleased;

    protected abstract void Initialize();

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void LateUpdate()
    {
        view.transform.forward = _cam.transform.forward;
    }

    public void Release(float delay = 0f)
    {
        if (_isReleased)
            return;

        StartCoroutine(ReleaseCoroutine(delay));
    }

    private IEnumerator ReleaseCoroutine(float delay)
    {
        view.enabled = false;
        
        yield return new WaitForSeconds(delay);

        _isReleased = true;
        
        gameObject.SetActive(false);
        
        Pool[GetType().Name]?.Release(this);
    }
}