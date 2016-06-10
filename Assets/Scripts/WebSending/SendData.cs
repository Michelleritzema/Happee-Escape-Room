using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/*
 * Created by Sander de Leng
 * 
 * Creates the post request to send the game information to the server. 
 * Uses the unityWebRequest service WWW to send information the server.
 * Gets the required data through the dictionary postData located in the settings script, 
 * in here the total time, the time for each module and the team name is stored.
 */

public class SendData : MonoBehaviour {
    private string teamName, finishTime, module1Time, module2Time, module3Time, module4Time;

    /*
     * Creates a post function in the WWW module with the URL and the dictionary, 
     * puts the content of the dictionary in the form to be sent to webservice.
     */
    public WWW POST(string url)
    {
      
        WWWForm WwForm = new WWWForm();
        WwForm.AddField("endTime", finishTime);
        WwForm.AddField("teamName", teamName);
        WwForm.AddField("module1Time", module1Time);
        WwForm.AddField("module2Time", module2Time);
        WwForm.AddField("module3Time", module3Time);
        WwForm.AddField("module4Time", module4Time);
        WWW www = new WWW(url, WwForm);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    /*
     * Notifies if the request was sent or not.
     */
    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        
        if(www.error ==  null)
        {
            Debug.Log("WWW is goed: " + www.text);
        } else
        {
            Debug.Log("WWW fout: " + www.error);
        }
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

}