using UnityEngine;

public static class SaveLoad
{
    private const string LAST_PLAYED_CHAPER_KEY = "LAST_CHAPTER";
    private const string PLAYER_STATUS_KEY = "PLAYER_STATUS";
    
    public static int    Load(string key, int defaultValue)    => PlayerPrefs.GetInt(key, defaultValue);
    public static float  Load(string key, float defaultValue)  => PlayerPrefs.GetFloat(key, defaultValue);
    public static string Load(string key, string defaultValue) => PlayerPrefs.GetString(key, defaultValue);

    public static void Save(string key, int value)    => PlayerPrefs.SetInt(key, value);
    public static void Save(string key, float value)  => PlayerPrefs.SetFloat(key, value);
    public static void Save(string key, string value) => PlayerPrefs.SetString(key, value);

    public static int  LoadLastChapter()          => Load(LAST_PLAYED_CHAPER_KEY, 1);
    public static void SaveLastChapter(int value) => Save(LAST_PLAYED_CHAPER_KEY, value);
    
    public static void SaveStatus(this Status status)
    {
        var data = status.ToStatusData();
        string json = JsonUtility.ToJson(data);
        Save(PLAYER_STATUS_KEY, json);
    }

    public static StatusData LoadStatus()
    {
        if (PlayerPrefs.HasKey(PLAYER_STATUS_KEY) == false)
            return new StatusData { hp = 50, power = 3000f, defense = 5f, critical = 0.05f, speed = 1f };

        string json = Load(PLAYER_STATUS_KEY, null);
        StatusData data = JsonUtility.FromJson<StatusData>(json);
        return data;
    }
}