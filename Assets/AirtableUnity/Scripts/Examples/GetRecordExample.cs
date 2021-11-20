using System.Collections;
using System.Collections.Generic;
using AirtableUnity.PX.Model;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class GetRecordExample : MonoBehaviour
{
    [Header("Record Id")] 
    public string TableName;
    private bool hasGet = false;
    public string[] RecordIdList;
    private string RecordId;
    private int currentID = 0;
    public GameObject artObject;

    private void Update()
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
        //Debug.Log(record?.fields.Name);
        //Debug.Log(record?.fields.url);
        
        StartCoroutine(DownloadImage(record?.fields.url, record?.fields.Name, currentID));
        currentID += 1;
    }

    IEnumerator DownloadImage(string MediaUrl, string artName, int artID)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //GameObject art = Instantiate(artObject, new Vector3(artID * 3, 0, 0), Quaternion.identity);
            GameObject art = GameObject.FindGameObjectsWithTag("Art")[artID];
            Debug.Log("art ID " + artID);
            art.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            art.transform.GetChild(0).GetComponent<TextMesh>().text = artName;
            art.name = artName;
            art.GetComponent<Proximity>().newTitle = artName;
        }
    }
    public void CreateArt()
    {

    }
}
