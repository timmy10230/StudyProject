// This is paid service version of Unity Google Translator script https://gist.github.com/IJEMIN/a48f8f302190044de05e3e3fea342fbd

using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleTranslatorWithAuth : MonoBehaviour
{
    private const string APIKey = "YOUR GOOGLE TRANSLATOR V2 API KEY HERE";

    private void Awake()
    {
        DoExample();
    }

    // Remove this method after understanding how to use.
    private void DoExample()
    {
        TranslateText("en", "ko", "I'm a real gangster.", (success, translatedText) =>
        {
            if (success) Debug.Log(translatedText);
        });

        TranslateText("ko", "en", "나는 리얼 갱스터다.", (success, translatedText) =>
        {
            if (success) Debug.Log(translatedText);
        });
    }

    public void TranslateText(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        StartCoroutine(TranslateTextRoutine(sourceLanguage, targetLanguage, sourceText, callback));
    }

    private IEnumerator TranslateTextRoutine(string sourceLanguage, string targetLanguage, string sourceText, Action<bool, string> callback)
    {
        var formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("Content-Type", "application/json; charset=utf-8"),
            new MultipartFormDataSection("source", sourceLanguage),
            new MultipartFormDataSection("target", targetLanguage),
            new MultipartFormDataSection("format", "text"),
            new MultipartFormDataSection("q", sourceText)
        };

        var uri = $"https://translation.googleapis.com/language/translate/v2?key={APIKey}";

        var webRequest = UnityWebRequest.Post(uri, formData);
        
        yield return webRequest.SendWebRequest();

        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.LogError(webRequest.error);
            callback.Invoke(false, string.Empty);

            yield break;
        }

        var parsedTexts = JSONNode.Parse(webRequest.downloadHandler.text);
        var translatedText = parsedTexts["data"]["translations"][0]["translatedText"];

        callback.Invoke(true, translatedText);
    }
}