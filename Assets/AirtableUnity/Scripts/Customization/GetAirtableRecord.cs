using System.Collections;
using System.Collections.Generic;
using AirtableUnity.PX.Model;
using AirtableUnity.PX.Customization;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;

public class GetAirtableRecord : MonoBehaviour
{
    [Header("Record Id")] 
    public string TableName;
    public GameObject _ArtObj;
    public GameObject _ArtLabel;

    public GameObject[] GalleryArts;
    public bool autoGallery = true;
    private bool hasGet = false;
    public string[] RecordIdList;
    private string RecordId;
    private int currentID = 0;

    private void Start()
    {
        GalleryArts = GameObject.FindGameObjectsWithTag("Art");
    }

    void Update()
    {
        if (RecordIdList.Length > 0 && hasGet == false)
        {
            for (int i = 0; i< RecordIdList.Length; i++)
            {
                RecordId = RecordIdList[i];
                GetRecord();
            }
            hasGet = true;
        }
    }
    

    [ContextMenu("Get Record")]
    public void GetRecord()
    {
        StartCoroutine(GetRecordCo());
    }

    private IEnumerator GetRecordCo()
    {
        yield return StartCoroutine(AirtableUnity.PX.Proxy.GetRecordCo<AirTableField>(TableName, RecordId, OnResponseFinish));
    }

    private void OnResponseFinish(BaseRecord<AirTableField> record)
    {
        var msg = "record id: " + record?.id + "\n";
        msg += "created at: " + record?.createdTime?.ToString();
        
        Debug.Log("[Airtable Unity] - Get Record: " + "\n" + msg);
        
        StartCoroutine(DownloadImage(record?.fields.url, record?.fields.Name, Convert.ToInt32(record?.fields.ID), record?.fields.Bio, record?.fields.Subtitle));
        currentID += 1;
    }

    IEnumerator DownloadImage(string MediaUrl, string artName, int artID, string artBio, string artSubtitle)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            GalleryArts = GameObject.FindGameObjectsWithTag("Art");
            // set the art frame with texture
            GameObject art;
            GameObject art_label;
            if (autoGallery)
            {
                art = Instantiate(_ArtObj, new Vector3((artID - 1) * 1.5f, 2, 5), Quaternion.identity);
            }
            else
            {
                art = GalleryArts[artID - 1];
            }
            art.name = artName;
            art.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (art.transform.Find("ArtTitle"))
            {
                art.transform.Find("ArtTitle").GetComponent<TextMeshPro>().text = artName;
                art.transform.Find("ArtSubTitle").GetComponent<TextMeshPro>().text = artSubtitle;
                art.transform.Find("ArtDescription").GetComponent<TextMeshPro>().text = artBio;
            }
            if (art.transform.GetComponent<Proximity>())
            {
                art.transform.GetComponent<Proximity>().newTitle = artName;
                art.transform.GetComponent<Proximity>().newSubtitle = artSubtitle;
                art.transform.GetComponent<Proximity>().newDesc = artBio;
            }
        }
    }
}
