using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
 
    ResponseEntity<List<UserEntity>> ListUsers = new ResponseEntity<List<UserEntity>>();
    [SerializeField]
    private GameObject buttonProfile;
    int x = -245;
    int y = 115;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        this.StartCoroutine(ConsumeServiceHelper.Get("https://hawkeyessurveillance.com/homerepairvr/apiusers/listusers", this.ResponseCallback));
    }

    private void ResponseCallback(string data)
    {
        ListUsers = JsonUtility.FromJson<ResponseEntity<List<UserEntity>>>(data);
        foreach (var item in ListUsers.Data)
        {
            GameObject button = Instantiate(buttonProfile) as GameObject;

            button.SetActive(true);
            button.name = item.UserId.ToString();
            button.transform.position = new Vector3(x, y, 0.0f);
            y = y - 40;
            button.GetComponent<TextButton>().SetText(item.UserName);
            button.transform.SetParent(buttonProfile.transform.parent, false);

        }
    }
    public void ChangeProfileToActivity(Int32 index)
    {
        SceneManager.LoadScene(index);
    }
    public void ChangeProfileToCreateUser(Int32 index)
    {
        SceneManager.LoadScene(index);
    }
    public void ChangeProfileToUpdateUser(Int32 index)
    {
        SceneManager.LoadScene(index);
    }
    public void ChangeCreateUserToProfile(Int32 index)
    {
        SceneManager.LoadScene(index);
    }
    public void SaveUser()
    {
        UserEntity model = new UserEntity();
        model.UserId = "";
        model.UserName = GameObject.Find("Canvas/PanelAddUser/InputNombreUsuario").GetComponent<InputField>().text;
        model.UserUrlPicture = "";

        this.StartCoroutine(ConsumeServiceHelper.Post("https://hawkeyessurveillance.com/homerepairvr/apiusers/addeditusers", model ,this.ResponseCreateUserCallback));
    }
    private void ResponseCreateUserCallback(string data)
    {
        ResponseEntity<String> response = JsonUtility.FromJson<ResponseEntity<String>>(data);
        if (!String.IsNullOrEmpty(response.Data)) {
            SceneManager.LoadScene(1);
        }
    }
    public void DeleteUser() {
        var Id = PlayerPrefs.GetString("UserId", "No Name");
        this.StartCoroutine(ConsumeServiceHelper.Get("https://hawkeyessurveillance.com/homerepairvr/apiusers/deleteusers/"+Id, this.ResponseDeleteUserCallback));
    }
    private void ResponseDeleteUserCallback(string data)
    {
        ResponseEntity<String> response = JsonUtility.FromJson<ResponseEntity<String>>(data);
        if (!String.IsNullOrEmpty(response.Data))
        {
            SceneManager.LoadScene(1);
        }
    }
}
