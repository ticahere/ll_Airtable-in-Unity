using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirtableUnity.PX.Model;

public class ListRecords : MonoBehaviour
{
    [Header("Table Name")] 
    public string TableName;
    public GetRecordExample _getRecord;

    private bool hasGet = false;

    private void Start()
    {
        //_getRecord = GetComponent<GetRecordExample>();
    }
    private void Update()
    {
        if (hasGet == false) {
            GetAirtableTableRecords();
            hasGet = true;
        }
    }
    /// <summary>
    /// Will call all records from a table using the TableName setted on Editor without custom callback
    /// </summary>
    [ContextMenu("Get Table Records")]
    public void GetAirtableTableRecords()
    {
        GetAirtableTableRecords<BaseField>(TableName, null);
    }

    /// <summary>
    /// Will call all records from a table using the TableName setted on Editor and
    /// the callback will be the one passed by parameter
    /// </summary>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    public void GetAirtableTableRecords<T>(Action<List<BaseRecord<T>>> callback)
    {
        GetAirtableTableRecords(TableName, callback);
    }

    /// <summary>
    /// Will call all records from a table using the paremeter
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="callback"></param>
    /// <typeparam name="T"></typeparam>
    private void GetAirtableTableRecords<T>(string tableName, Action<List<BaseRecord<T>>> callback)
    {
        StartCoroutine(GetTableRecordsCo(tableName, callback));
    }

    private IEnumerator GetTableRecordsCo<T>(string tableName, Action<List<BaseRecord<T>>> callback)
    {
        yield return StartCoroutine(AirtableUnity.PX.Proxy.ListRecordsCo<T>(tableName, (records) =>
        {
            OnResponseFinish(records);
            callback?.Invoke(records);
        }));
    }

    private void OnResponseFinish<T>(List<BaseRecord<T>> records)
    {
        Debug.Log("[Airtable Unity Example] - List Records: " + records?.Count);
        
        string[] recordId = new string[records.Count];
        for (int i = 0; i<records.Count; i++)
        {
            recordId[i] = records[i].id;
        }
        _getRecord.RecordIdList = recordId;
    }
}
