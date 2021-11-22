﻿using System.Collections;
using System.Collections.Generic;
using AirtableUnity.PX.Model;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;

public class GetAirtableRecord : MonoBehaviour
{
    [Header("Record Id")] 
    public string TableName;
    public GameObject Artobj;
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
        yield return StartCoroutine(AirtableUnity.PX.Proxy.GetRecordCo<BaseField>(TableName, RecordId, OnResponseFinish));
    }

    private void OnResponseFinish(BaseRecord<BaseField> record)
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
            if (autoGallery)
            {
                art = Instantiate(Artobj, new Vector3((artID - 1) * 1.5f, 2, 5), Quaternion.identity);
            }
            else
            {
                art = GalleryArts[artID - 1];
            }
            art.name = artName;
            art.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            art.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = artName;
            art.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = artSubtitle;
            art.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = artBio;
        }
    }
}