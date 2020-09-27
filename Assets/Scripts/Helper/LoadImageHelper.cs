using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoadImageHelper 
{
    public static IEnumerator loadSpriteImageFromUrl(string URL, Action<Sprite> callback = null)
    {

        WWW www = new WWW(URL);
        while (!www.isDone)
        {
            Debug.Log("Download image on progress" + www.progress);
            yield return null;
        }

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Download failed");
        }
        else
        {
            Debug.Log("Download succes");
            Texture2D texture = new Texture2D(1, 1);
            www.LoadImageIntoTexture(texture);

            Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            callback(sprite);
        }
    }
}
