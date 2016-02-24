using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Common;

public class MotorUI : MonoBehaviour
{

    DataProvider Provider;
    Text[] TextBlocks;

    // Use this for initialization
    void Start()
    {
        Provider = GameObject.Find("DownloaderContainer").GetComponentInChildren<DataProvider>();
        TextBlocks = GameObject.Find("Canvas").GetComponentsInChildren<Text>().Where(a => a.name.StartsWith("MotorPos")).OrderBy(a => int.Parse(a.name[a.name.Length - 1].ToString())).ToArray();
        Debug.Log(string.Format("{0} TextBlocks Found", TextBlocks.Length));
    }

    // Update is called once per frame
    void Update()
    {
        if (Provider == null || Provider.LatestMessage.Sync0 == 0)
            return;

        var message = Provider.LatestMessage;
        if (!message.Motors.Any())
            return;

        for (var i = 0; i < TextBlocks.Length; i++)
        {
            UpdateMotorGUI(i, message);
        }
    }

    private void UpdateMotorGUI(int idx, MotorsProtocol message)
    {
        TextBlocks[idx].text = string.Format("{0:F2}mm", message.Motors[idx].ExtensionLength);
    }
}
