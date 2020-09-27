using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LoadSceneHelper
{
    public static void SceneLoader(int index)
    {
        SceneManager.LoadScene(index);
    }
}
