using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class ConsumeServiceHelper 
{
    public static IEnumerator Get(string uri, Action<string> callback = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                var data = webRequest.downloadHandler.text;
                if (callback != null)
                    callback(data);
            }
        }

    }

    public static IEnumerator Delete(string uri, Action<string> callback = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Delete(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                if (callback != null)
                    callback(webRequest.downloadHandler.text);
            }
        }
    }

    public static IEnumerator Post(string uri, UserEntity model, Action<string> callback = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("UserId", model.UserId);
        form.AddField("UserName", model.UserName);
        form.AddField("UserUrlPicture", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/768px-User_icon_2.svg.png");
        form.AddField("DateCreate", " ");
        form.AddField("Status", " ");
        form.AddField("DateUpdate", " ");


        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                if (callback != null)
                    callback(www.downloadHandler.text);
            }
        }
    }

    public static IEnumerator Put()
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
        using (UnityWebRequest www = UnityWebRequest.Put("http://www.my-server.com/upload", myData))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
        }
    }
}
