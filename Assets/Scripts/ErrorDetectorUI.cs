using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorDetectorUI : MonoBehaviour
{
    [SerializeField] private TMP_Text errorTextMesh;

    private void Start()
    {
        Application.logMessageReceived += Application_logMessageReceived;

        Hide();
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            Show();
            errorTextMesh.text = "Error:" + condition + "\n" + stackTrace;
        }
        //throw new System.NotImplementedException();
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= Application_logMessageReceived;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
