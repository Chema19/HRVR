using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetailActivityController : MonoBehaviour
{
    ResponseEntity<ActivityEntity> Activity = new ResponseEntity<ActivityEntity>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        var Id = PlayerPrefs.GetString("ActivityId", "No Name");
        var UrlNew = "https://hawkeyessurveillance.com/homerepairvr/apiactivities/viewactivities/" + Id;
        StartCoroutine(ConsumeServiceHelper.Get(UrlNew, this.ResponseNewCallback));
    }

    private void ResponseNewCallback(string data)
    {
        Activity = JsonUtility.FromJson<ResponseEntity<ActivityEntity>>(data);
        GameObject.Find("Canvas/Panel/DifficultValue").GetComponent<Text>().text = "3";
        GameObject.Find("Canvas/Panel/TimeValue").GetComponent<Text>().text = Activity.Data.ActivityTime;
        GameObject.Find("Canvas/Panel/Title").GetComponent<Text>().text = Activity.Data.ActivityName;
        GameObject.Find("Canvas/Panel/Description").GetComponent<Text>().text = Activity.Data.ActivityDescription;
    }

    public void ChangeDetailToActivity(Int32 index)
    {
        SceneManager.LoadScene(index);
    }
}
