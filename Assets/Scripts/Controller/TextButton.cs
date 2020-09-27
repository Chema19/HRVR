using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;
    private GameObject buttonContinuar;
    private GameObject buttonEditar;
    private GameObject buttonEliminar;
    public Image imageToDisplay;
    ResponseEntity<UserEntity> User = new ResponseEntity<UserEntity>();

    public void SetText(string textString)
    {
        myText.text = textString;
    }

    public void MakeClickProfile()
    {
        GameObject btnGameObject = EventSystem.current.currentSelectedGameObject;
        ColorBlock btnColors = btnGameObject.GetComponent<Button>().colors;

        buttonContinuar = GameObject.Find("Canvas/PanelListUsers/ButtonContinuar");
        buttonContinuar.SetActive(true);
        buttonEditar = GameObject.Find("Canvas/PanelListUsers/ButtonEditar");
        buttonEditar.SetActive(true);
        buttonEliminar = GameObject.Find("Canvas/PanelListUsers/ButtonEliminar");
        buttonEliminar.SetActive(true);

     
        var url = "https://hawkeyessurveillance.com/homerepairvr/apiusers/viewusers/" + btnGameObject.name;
        StartCoroutine(ConsumeServiceHelper.Get(url, this.ResponseCallback));
        //var c = User.Data.UserUrlPicture;
        //StartCoroutine(LoadImageHelper.loadSpriteImageFromUrl(User.Data.UserUrlPicture, ResponseImageCallback));
    }

    private void ResponseCallback(string data)
    {
        User = JsonUtility.FromJson<ResponseEntity<UserEntity>>(data);
        PlayerPrefs.SetString("UserId", User.Data.UserId);
        var imageToDisplays = GameObject.Find("Canvas/PanelListUsers/ImagePerfil");
        this.imageToDisplay = imageToDisplays.GetComponent<Image>();
        StartCoroutine(LoadImageHelper.loadSpriteImageFromUrl(User.Data.UserUrlPicture, ResponseImageCallback));
    }

    private void ResponseImageCallback(Sprite data)
    {
        this.imageToDisplay.sprite = data;
    }

}
