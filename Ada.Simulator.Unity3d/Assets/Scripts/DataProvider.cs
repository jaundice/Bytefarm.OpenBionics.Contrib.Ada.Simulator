using UnityEngine;
using System.Collections;
using System.Net;
using Bytefarm.OpenBionics.Contrib.Ada.Simulator.Common;

public class DataProvider : MonoBehaviour
{

    public string DataUri = "http://localhost:8080/api/proxy";

    private DataDownloader<MotorsProtocol> Downloader;

    public MotorsProtocol LatestMessage;

    public int PollFrameFrequency = 5;

    private long _frame;

    // Use this for initialization
    void Start()
    {
        Downloader = new DataDownloader<MotorsProtocol>(DataUri);
    }

    // Update is called once per frame
    void Update()
    {
        if (_frame++ % PollFrameFrequency == 0)
            LatestMessage = Downloader.DownloadData();
        //Debug.Log(LatestMessage.SequenceId);
    }
}
