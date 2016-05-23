using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

public class Setup : MonoBehaviour {

    private JsonData settings;
    public string jsonString, titleIntroduction;

    // Use this for initialization
    public void Start () {
        jsonString = File.ReadAllText(Application.dataPath + "/Settings/settings.json");
        settings = JsonMapper.ToObject(jsonString);
        titleIntroduction = settings["GameStrings"][0]["titleIntroduction"].ToString();
        Debug.Log("introduction: " + titleIntroduction);
    }

}
