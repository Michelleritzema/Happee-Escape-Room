﻿using UnityEngine;
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

    private JsonData settings;
    private string jsonString, teamName, password, module1, module2, module3, module4;
    private int totalMinutes;

    public string titleIntroduction, welcome, enterTeamName, emptyTeamNameWarning, next, go;

    /*
     * Initializes the class by reading the JSON file and storing the collected data.
     */
    public void Start() {
        jsonString = File.ReadAllText(Application.dataPath + "/Settings/settings.json");
        settings = JsonMapper.ToObject(jsonString);
        password = settings["Settings"][0]["password"].ToString();
        totalMinutes = (int) settings["Settings"][0]["totalMinutes"];
        module1 = settings["Settings"][0]["module1"].ToString();
        module2 = settings["Settings"][0]["module2"].ToString();
        module3 = settings["Settings"][0]["module3"].ToString();
        module4 = settings["Settings"][0]["module4"].ToString();
        titleIntroduction = settings["GameStrings"][0]["titleIntroduction"].ToString();
        welcome = settings["GameStrings"][0]["welcome"].ToString();
        enterTeamName = settings["GameStrings"][0]["enterTeamName"].ToString();
        emptyTeamNameWarning = settings["GameStrings"][0]["emptyTeamNameWarning"].ToString();
        next = settings["GameStrings"][0]["next"].ToString();
        go = settings["GameStrings"][0]["go"].ToString();
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

}