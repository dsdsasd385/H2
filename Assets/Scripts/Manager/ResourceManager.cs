using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Singleton
    private static ResourceManager _instance;
    private static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ResourceManager>();
            if (_instance == null)
            {
                var prefab = Resources.Load<ResourceManager>("[ResourceManager]");
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            
            return _instance;
        }
    }
    #endregion

    [SerializeField] private List<GameObject> prefabList;
    [SerializeField] private List<Sprite>     imgList;

    private Dictionary<string, GameObject> _prefabs;
    private Dictionary<string, Sprite>     _sprites;

    private void Awake()
    {
        _prefabs = prefabList
            .ToDictionary(prefab => prefab.name, prefab => prefab);
        
        _sprites = imgList
            .ToDictionary(img => img.name, img => img);
    }

    public static Sprite GetSprite(string spriteName)
    {
        if (Instance._sprites.TryGetValue(spriteName, out var img) == false)
            throw new($"Image[{spriteName}] has not added to ResourceManager.");
        
        return img;
    }

    public static T GetPrefab<T>(string prefabName) where T : Component
    {
        if (Instance._prefabs.TryGetValue(prefabName, out var prefab) == false)
            throw new($"Prefab[{prefabName}] has not added to ResourceManager.");

        return prefab.GetComponent<T>();
    }
}
