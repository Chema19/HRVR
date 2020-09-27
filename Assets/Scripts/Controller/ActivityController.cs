using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ActivityController : MonoBehaviour
{

    ResponseEntity<List<ActivityEntity>> ListDone = new ResponseEntity<List<ActivityEntity>>();
    ResponseEntity<List<ActivityEntity>> ListNew = new ResponseEntity<List<ActivityEntity>>();
    [SerializeField]
    private GameObject buttonActivityNew;
    [SerializeField] 
    private GameObject buttonActivityDone;

    int xDone = -274;
    int yDone = 99;

    int xNew = -274;
    int yNew = -25;
    void Start()
    {
    }
    void Update()
    {

    }

    public void Awake()
    {
        var Id = PlayerPrefs.GetString("UserId", "No Name");
        var UrlNew = "https://hawkeyessurveillance.com/homerepairvr/apiactivities/newactivities/" + Id;
        StartCoroutine(ConsumeServiceHelper.Get(UrlNew, this.ResponseNewCallback));
        var UrlDone = "https://hawkeyessurveillance.com/homerepairvr/apiactivities/doneactivities/" + Id;
        StartCoroutine(ConsumeServiceHelper.Get(UrlDone, this.ResponseDoneCallback));
    }

    private void ResponseNewCallback(string data)
    {
        ListNew = JsonUtility.FromJson<ResponseEntity<List<ActivityEntity>>>(data);
        foreach (var item in ListNew.Data)
        {
            GameObject button = Instantiate(buttonActivityNew) as GameObject;

            button.SetActive(true);
            button.name = item.ActivityId.ToString();
            button.transform.position = new Vector3(xNew, yNew, 0.0f);
            xNew = xNew + 170;
            button.GetComponent<TextButton>().SetText(item.ActivityName);
            button.transform.SetParent(buttonActivityNew.transform.parent, false);

        }
    }
    private void ResponseDoneCallback(string data)
    {
        ListDone = JsonUtility.FromJson<ResponseEntity<List<ActivityEntity>>>(data);
        foreach (var item in ListDone.Data)
        {
            GameObject button = Instantiate(buttonActivityDone) as GameObject;

            button.SetActive(true);
            button.name = item.ActivityId.ToString();
            button.transform.position = new Vector3(xDone, yDone, 0.0f);
            xDone = xDone + 170;
            button.GetComponent<TextButton>().SetText(item.ActivityName);
            button.transform.SetParent(buttonActivityDone.transform.parent, false);

        }
    }

    public void ChangeActivityToProfile(Int32 index)
    {
        SceneManager.LoadScene(index);
    }

    public void ChangeActivityToDetail(Int32 index)
    {
        GameObject btnGameObject = EventSystem.current.currentSelectedGameObject;
        PlayerPrefs.SetString("ActivityId", btnGameObject.name);
        SceneManager.LoadScene(index);
    }
}
