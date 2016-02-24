using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

internal class DataDownloader<T>
{
    private string dataUri;
    private JsonSerializer _serializer;
    public int RequestTimeoutMS = 50;

    private JsonSerializer Serializer
    {
        get
        {
            return _serializer ?? (_serializer = JsonSerializer.CreateDefault());
        }
    }

    public DataDownloader(string dataUri)
    {
        this.dataUri = dataUri;
    }

    public T DownloadData()
    {
        HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(dataUri);
        wr.Timeout = RequestTimeoutMS;
        var response = (HttpWebResponse)wr.GetResponse();
        if (response.StatusCode != HttpStatusCode.OK)
            return default(T);

        return Deserialize(response.GetResponseStream());

    }

    private T Deserialize(Stream stream)
    {
        using (stream)
        using (var tr = new StreamReader(stream))
        using (var jr = new JsonTextReader(tr))
        {
            return Serializer.Deserialize<T>(jr);
        }
    }
}