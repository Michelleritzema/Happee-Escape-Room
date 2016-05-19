using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;


/*
 * Created by Michelle Ritzema.
 * 
 * Class that contains all the settings that the server supplied when building the application.
 * These settings are stored and can be read with the getter methods.
 */
public class Settings : MonoBehaviour {

    private string jsonString;
    private JsonData settings;
    private string password, module1, module2, module3, module4;

    /*
     * Initializes the class by reading the JSON file and storing the collected sdata.
     */
    void Start() {
        jsonString = File.ReadAllText(Application.dataPath + "/Settings/settings.json");
        settings = JsonMapper.ToObject(jsonString);
        //Debug.Log(settings["Settings"][0]["password"].ToString());
        password = settings["Settings"][0]["password"].ToString();
        //Debug.Log(module1 = settings["Settings"][0]["module1"].ToString());
        module1 = settings["Settings"][0]["module1"].ToString();
    }

    public string GetPassword()
    {
        return password;
    }

    public string GetModule1()
    {
        return module1;
    }

    public string GetModule2()
    {
        return module1;
    }

    public string GetModule3()
    {
        return module1;
    }

    public string GetModule4()
    {
        return module1;
    }

}