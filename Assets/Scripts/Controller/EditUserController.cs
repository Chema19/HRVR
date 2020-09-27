using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditUserController : MonoBehaviour
{
    // Start is called before the first frame update
    ResponseEntity<UserEntity> User = new ResponseEntity<UserEntity>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        var Id = PlayerPrefs.GetString("UserId", "No Name");
        var url = "https://hawkeyessurveillance.com/homerepairvr/apiusers/viewusers/" + Id;
        StartCoroutine(ConsumeServiceHelper.Get(url, this.ResponseCallback));
    }

    private void ResponseCallback(string data)
    {
        User = JsonUtility.FromJson<ResponseEntity<UserEntity>>(data);
        GameObject.Find("Canvas/PanelAddUser/InputNombreUsuarioUpdate").GetComponent<InputField>().text = User.Data.UserName;
    }

    public void UpdateUser()
    {
        var Id = PlayerPrefs.GetString("UserId", "No Name");
        UserEntity model = new UserEntity();
        model.UserId = Id;
        model.UserName = GameObject.Find("Canvas/PanelAddUser/InputNombreUsuarioUpdate").GetComponent<InputField>().text;
        model.UserUrlPicture = "gaa";
        model.DateUpdate = DateTime.Now.ToString();

        this.StartCoroutine(ConsumeServiceHelper.Post("https://hawkeyessurveillance.com/homerepairvr/apiusers/addeditusers", model, this.ResponseUpdateUserCallback));
    }

    private void ResponseUpdateUserCallback(string data)
    {
        ResponseEntity<String> response = JsonUtility.FromJson<ResponseEntity<String>>(data);
        if (!String.IsNullOrEmpty(response.Data))
        {
            SceneManager.LoadScene(1);
        }
    }
}
