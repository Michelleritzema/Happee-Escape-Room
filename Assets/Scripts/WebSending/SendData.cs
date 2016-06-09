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

    /*
     * Creates a post function in the WWW module with the URL and the dictionary, 
     * puts the content of the dictionary in the form to be sent to webservice.
     */
    public WWW POST(string url, Dictionary<string,string> dataPost)
    {
        Settings settings = GetComponent<Settings>();
        
        settings.postData = dataPost;
        WWWForm WwForm = new WWWForm();
        foreach(KeyValuePair<String, String> data in dataPost)
        {
            WwForm.AddField(data.Key, data.Value);
        }
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

}