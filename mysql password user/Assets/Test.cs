using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public InputField inpRegUsername = null;
    public InputField inpRegPassword = null;
    public InputField inpRegEmail = null;

    public InputField inpLogUsername = null;
    public InputField inpLogPassword = null;

    public enum Request { 
        register = 0,
        login = 1
    }

    private void Start()
    {
        DevelopersHub.Network.Instance.OnRequestResponded += Instance_OnRequestResponded;
    }

    public void Register()
    {
        string user = inpRegUsername.text;
        string pass = inpRegPassword.text;
        string mail = inpRegEmail.text;
        if(!string.IsNullOrEmpty(user)&& !string.IsNullOrEmpty(pass)&& !string.IsNullOrEmpty(mail))
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("username", user);
            data.Add("password", pass);
            data.Add("email", mail);
            DevelopersHub.Network.Instance.SendRequest((int)Request.register, data);
        }
    }

    public void Login()
    {
        string user = inpLogUsername.text;
        string pass = inpLogPassword.text;
        if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("username", user);
            data.Add("password", pass);
            DevelopersHub.Network.Instance.SendRequest((int)Request.login, data);
        }
    }

    private void Instance_OnRequestResponded(int requestID, string message,LitJson.JsonData data)
    {
        if (string.IsNullOrEmpty(message))
        {
            Debug.Log("Fail");
        }
        else
        {
            switch (requestID)
            {
                case 0:
                    if(!data.IsObject && data.ToString() == "ACCOUNT_CREATED")
                    {
                        Debug.Log("Register Success.");
                    }
                    else if(!data.IsObject && data.ToString() == "USERNAME_EXISTS")
                    {
                        Debug.Log("Username already taken");
                    }
                    break;
                case 1:
                    if (!data.IsObject && data.ToString() == "LOGIN_DONE")
                    {
                        Debug.Log("Login Success.");
                    }
                    else if (!data.IsObject && data.ToString() == "USERNAME_NOT_EXIST")
                    {
                        Debug.Log("No user whis this data.");
                    }
                    break;
            }
        }
    }
}
