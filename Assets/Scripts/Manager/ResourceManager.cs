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

    [SerializeField] private List<Sprite> imgList;

    private Dictionary<string, Sprite> _sprites;

    private void Awake()
    {
        _sprites = imgList
            .ToDictionary(img => img.name, img => img);
    }

    public static Sprite GetSprite(string spriteName)
    {
        if (Instance._sprites.TryGetValue(spriteName, out var img) == false)
            throw new($"Image[{spriteName}] has not added to ResourceManager.");
        
        return img;
    }
}
