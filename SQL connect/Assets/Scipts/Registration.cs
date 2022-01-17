using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php",form);
        yield return www;
        if(www.downloadHandler.text == "0")
        {
            Debug.Log("User created successfully.");
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("User created failed. Error #" + www.downloadHandler.text);
        }
    }

    public void VerifyInput()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
