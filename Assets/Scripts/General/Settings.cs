using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System.Collections.Generic;
using System;

/*
 * Created by Michelle Ritzema.
 * PostData dictionary created by Sander de Leng.
 * 
 * Class that contains all the settings that the server supplied when building the application.
 * These settings are stored and can be read with the getter methods.
 */

public class Settings : MonoBehaviour {
    public Dictionary<string,string> postData;
    private JsonData settings, languageStrings;
    private string jsonString, language, teamName, password, module1, module2, module3, module4, 
        startTime, finishTime, module1Time, module2Time, module3Time, module4Time;
    private int totalMinutes;

    public string titleIntroduction, welcome, enterTeamName, emptyTeamNameWarning, next, go, notCompletedWarning;
    
    /*
     * Initializes the class by reading the JSON files and storing the collected data.
     */
    public void Start() {
        jsonString = File.ReadAllText(Application.dataPath + "/Settings/settings_test.json");
        settings = JsonMapper.ToObject(jsonString);
        password = settings["password"].ToString();
        totalMinutes = int.Parse(settings["totalMinutes"].ToString());
        module1 = settings["modules"][0].ToString();
        module2 = settings["modules"][1].ToString();
        module3 = settings["modules"][2].ToString();
        module4 = settings["modules"][3].ToString();
        language = settings["language"].ToString();
        loadStrings();
    }

    /*
     * Loads strings in the correct language. This is determined in the settings json file.
     */
    private void loadStrings()
    {
        switch(language)
        {
            case "dutch":
                jsonString = File.ReadAllText(Application.dataPath + "/Settings/strings_dutch.json");
                break;
            case "english":
                jsonString = File.ReadAllText(Application.dataPath + "/Settings/strings_english.json");
                break;
            default:
                jsonString = File.ReadAllText(Application.dataPath + "/Settings/strings_english.json");
                break;
        }
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
        this.module1Time = time;
    }

    /*
     * Stores the module 2 finish time string.
     */
    public void SetModule2Time(string time)
    {
        this.module2Time = time;
    }

    /*
     * Stores the module 3 finish time string.
     */
    public void SetModule3Time(string time)
    {
        this.module3Time = time;
    }

    /*
     * Stores the module 4 finish time string.
     */
    public void SetModule4Time(string time)
    {
        this.module4Time = time;
    }

    /*
     * Creates a new dictionary with statistics to send to the server.
     */
    public void dataValues()
    {
        postData = new Dictionary<string, string>();
        postData.Add("tijd over", finishTime);
        postData.Add("Team naam", teamName);
        postData.Add("Totale minuten", totalMinutes.ToString());
        postData.Add("module 1 tijd", module1Time);
        postData.Add("module 2 tijd", module2Time);
        postData.Add("module 3 tijd", module3Time);
    }

}