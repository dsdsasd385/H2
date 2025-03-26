using UnityEngine;

public static class SaveLoad
{
    private const string LAST_PLAYED_CHAPER_KEY = "LAST_CHAPTER";
    
    public static int    Load(string key, int defaultValue)    => PlayerPrefs.GetInt(key, defaultValue);
    public static float  Load(string key, float defaultValue)  => PlayerPrefs.GetFloat(key, defaultValue);
    public static string Load(string key, string defaultValue) => PlayerPrefs.GetString(key, defaultValue);

    public static void Save(string key, int value)    => PlayerPrefs.SetInt(key, value);
    public static void Save(string key, float value)  => PlayerPrefs.SetFloat(key, value);
    public static void Save(string key, string value) => PlayerPrefs.SetString(key, value);

    public static int  LoadLastChapter()          => Load(LAST_PLAYED_CHAPER_KEY, 1);
    public static void SaveLastChapter(int value) => Save(LAST_PLAYED_CHAPER_KEY, value);
}