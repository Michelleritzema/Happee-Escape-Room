using UnityEngine;
using System.IO;
using LitJson;
using System.Collections.Generic;
using System.Reflection;

/*
 * Created by Michelle Ritzema.
 * PostData dictionary created by Sander de Leng.
 * 
 * Class that contains all the settings that the server supplied when building the application.
 * These settings are stored and can be read with the getter methods.
 */

public class Settings : MonoBehaviour {

    private JsonData settings, languageStrings;
    private string jsonString, language, teamName, password, module1, module2, module3, module4,
        startTime, finishTime, module1Time, module2Time, module3Time, module4Time;
    private int totalMinutes;

    public Dictionary<string, string> postData;
    public string titleIntroduction, welcome, enterTeamName, emptyTeamNameWarning, next, go, notCompletedWarning;

    /*
     * Initializes the class by reading the JSON files and storing the collected data.
     */
    public void Start()
    {
        GetSettingsJsonString();
        settings = JsonMapper.ToObject(jsonString);
        password = settings["password"].ToString();
        totalMinutes = int.Parse(settings["totalMinutes"].ToString());
        module1 = settings["modules"][0].ToString();
        module2 = settings["modules"][1].ToString();
        module3 = settings["modules"][2].ToString();
        module4 = settings["modules"][3].ToString();
        language = settings["language"].ToString();
        LoadStrings();
    }

    /*
     * Reads the JSON settings file and returns a string containing the JSON.
     * If it is run in the Unity editor, the test data set is used.
     */
    private void GetSettingsJsonString()
    {
#if UNITY_EDITOR
        jsonString = File.ReadAllText(Application.dataPath + "/Settings/settings_test.json");
#else
        string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        DirectoryInfo dataDirectory = Directory.GetParent(path).Parent;
        StreamReader settingsReader = new StreamReader(dataDirectory + "/settings.json");
        string jsonText="";
        while (jsonText != null)
        {
	        jsonText = settingsReader.ReadLine();
	        if (jsonText != null)
            {
		        jsonString = jsonString + jsonText;
            }
        }
        settingsReader.Close();
#endif
    }

    /*
     * Determines which path to use to fetch the correct language file.
     */
    private string DetermineLanguageFile()
    {
        string path;
        switch (language)
        {
            case "dutch":
                path = "Strings/strings_dutch";
                break;
            case "english":
                path = "Strings/strings_english";
                break;
            default:
                path = "Strings/strings_english";
                break;
        }
        return path;
    }

    /*
     * Loads strings in the correct language.
     * Which language is determined in the settings json file.
     */
    private void LoadStrings()
    {
        string path = DetermineLanguageFile();
        jsonString = Resources.Load(path).ToString();
        languageStrings = JsonMapper.ToObject(jsonString);
        titleIntroduction = languageStrings["titleIntroduction"].ToString();
        welcome = languageStrings["welcome"].ToString();
        enterTeamName = languageStrings["enterTeamName"].ToString();
        emptyTeamNameWarning = languageStrings["emptyTeamNameWarning"].ToString();
        next = languageStrings["next"].ToString();
        go = languageStrings["go"].ToString();
        notCompletedWarning = languageStrings["notCompletedWarning"].ToString();
    }

    /*
     * Creates a new dictionary with statistics to send to the server.
     */
    public void DataValues()
    {
        postData = new Dictionary<string, string>();
        postData.Add("tijd over", finishTime);
        postData.Add("Team naam", teamName);
        postData.Add("Totale minuten", totalMinutes.ToString());
        postData.Add("module 1 tijd", module1Time);
        postData.Add("module 2 tijd", module2Time);
        postData.Add("module 3 tijd", module3Time);
    }

    /*
     * Fetches the stored total minutes integer.
     */
    public int GetTotalMinutes()
    {
        return totalMinutes;
    }

    /*
     * Fetches the stored team name string.
     */
    public string GetTeamName()
    {
        return teamName;
    }

    /*
     * Stores the team name string.
     */
    public void SetTeamName(string teamName)
    {
        this.teamName = teamName;
    }

    /*
     * Fetches the stored password string.
     */
    public string GetPassword()
    {
        return password;
    }

    /*
     * Fetches the module string that matches with the supplied module integer.
     */
    public string GetModule(int module)
    {
        return settings["modules"][module].ToString();
    }

    /*
     * Fetches the stored module1 string.
     */
    public string GetModule1()
    {
        return module1;
    }

    /*
     * Fetches the stored module2 string.
     */
    public string GetModule2()
    {
        return module2;
    }

    /*
     * Fetches the stored module3 string.
     */
    public string GetModule3()
    {
        return module3;
    }

    /*
     * Fetches the stored module4 string.
     */
    public string GetModule4()
    {
        return module4;
    }

    /*
     * Stores the start time string.
     */
    public void SetStartTime(string startTime)
    {
        this.startTime = startTime;
    }

    /*
     * Stores the finish time string.
     */
    public void SetFinishTime(string finishTime)
    {
        this.finishTime = finishTime;
    }

    /*
     * Stores the module 1 finish time string.
     */
    public void SetModule1Time(string time)
    {
        module1Time = time;
    }

    /*
     * Stores the module 2 finish time string.
     */
    public void SetModule2Time(string time)
    {
        module2Time = time;
    }

    /*
     * Stores the module 3 finish time string.
     */
    public void SetModule3Time(string time)
    {
        module3Time = time;
    }

    /*
     * Stores the module 4 finish time string.
     */
    public void SetModule4Time(string time)
    {
        module4Time = time;
    }

    public string GetFinishTime()
    {
        return finishTime;
    }

    /*
     * gets the module 1 finish time string.
     */
    public string GetModule1Time()
    {
        return module1Time;
    }

    /*
     * Stores the module 2 finish time string.
     */
    public string GetModule2Time()
    {
        return module2Time;
    }

    /*
     * Stores the module 3 finish time string.
     */
    public string GetModule3Time()
    {
        return module3Time;
    }

    /*
     * Stores the module 4 finish time string.
     */
    public string GetModule4Time()
    {
        return module4Time;
    }
}