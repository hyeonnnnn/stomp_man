using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance => _instance;

    private const string SaveDataKey = "SaveDataKey";

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public void Save(int score)
    {
        SaveData data = new SaveData();
        data.score = score;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveDataKey, json);
        PlayerPrefs.Save();
    }

    public SaveData Load()
    {
        if (PlayerPrefs.HasKey(SaveDataKey))
        {
            string json = PlayerPrefs.GetString(SaveDataKey);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data;
        }
        return null;
    }
}
