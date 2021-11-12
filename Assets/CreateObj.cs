using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirtableUnity.PX.Model;
using UnityEngine.Networking;

public class CreateObj : MonoBehaviour
{
    public string url = "";
    public string artName = "";
    public int artID;
    public GameObject artCanvas;
    void Update()
    {
        if (url !="")
        {
            StartCoroutine(DownloadImage(url));
            url = "";
        }
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            Instantiate(artCanvas, new Vector3(artID * 2.0F, 0, 0), Quaternion.identity);
            artCanvas.GetComponent<Renderer>().sharedMaterial.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            artCanvas.name = artName;

        }
    }
}
