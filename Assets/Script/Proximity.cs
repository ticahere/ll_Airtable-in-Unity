using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Proximity : MonoBehaviour
{
    public string newTitle;
    public string newSubtitle;
    public string newDesc;
    public int distThreshold = 5;

    private Transform other;
    private TextMeshProUGUI artTitle;
    private TextMeshProUGUI artSubtitle;
    private TextMeshProUGUI artDesc;
    private float dist;
    private GameObject player;
    private GameObject bg;
    private GameObject titleObj;
    private GameObject subtitleObj;
    private GameObject descObject;
    private bool check;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        other = player.GetComponent<Transform>();
        titleObj = GameObject.FindWithTag("ArtTitle");
        subtitleObj = GameObject.FindWithTag("Subtitle");
        descObject = GameObject.FindWithTag("Description");
        bg = GameObject.FindWithTag("Background");
        bg.GetComponent<Image>().color = Color.clear;
        resetText();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (other)
        {
            dist = Vector3.Distance(transform.position, other.position);


            if (dist < distThreshold)
            {
                bg = GameObject.FindWithTag("Background");
                artTitle.text = newTitle;
                artSubtitle.text = newSubtitle;
                artDesc.text = newDesc;
                check = true;
                if (newTitle.Length > 0)
                    bg.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
            if (dist > distThreshold && check == true)
            {
                bg.GetComponent<Image>().color = Color.clear;
                resetText();
            }
        }
    }
    void resetText()
    {
        artTitle = titleObj.GetComponent<TextMeshProUGUI>();
        artTitle.text = "";
        artSubtitle = subtitleObj.GetComponent<TextMeshProUGUI>();
        artSubtitle.text = "";
        artDesc = descObject.GetComponent<TextMeshProUGUI>();
        artDesc.text = "";
        check = false;

    }
}
