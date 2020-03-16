using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Airplane : MonoBehaviour, IPointerDownHandler
{
    public string ccTo;
    public string ccFrom;
    public string countryName;
    public string language;
    public string greeting;
    public string flag;
    public string animal;
    public Image flagImage;
    private Texture2D texture;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public int ShapeID { get; set; } = int.MinValue;
    public GameObject CountryCanvas;
    public GameObject UI;


    public void playClip()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playClip();
        UI = GameObject.Find("/UIComponents");
        Transform uiTr = UI.transform;
        foreach (Transform child in uiTr)
        {
            if (child.tag == "CountryFacts")
            {
                foreach (Transform grandchild in child)
                {
                    if (countryName == "United States of America" || countryName == "Philippines")
                    {
                        countryName = "The " + countryName;
                    }
                    if (countryName == "United Kingdom of Great Britain and Northern Ireland")
                    {
                        countryName = "The United Kingdom";
                    }
                    if (countryName == "Russian Federation")
                    {
                        countryName = "Russia";
                    }
                    if (countryName == "Korea (Republic of)")
                    {
                        countryName = "South Korea";
                    }
                    if (grandchild.tag == "mainText")
                    {
                        // name of the country
                        grandchild.GetComponent<UnityEngine.UI.Text>().text = $"This plane is on its way to...\r\n{countryName}!";
                    }

                    if (grandchild.tag == "someFacts")
                    {
                        foreach (Transform ggrandchild in grandchild)
                        {
                            // some facts about COUNTRYNAME
                            // if (ggrandchild.tag == "someFacts")
                            // {
                            ggrandchild.GetComponent<UnityEngine.UI.Text>().text = $"Learn more about\r\n{countryName}?";
                        }
                        //}
                    }

                    if (grandchild.tag == "flagImg")
                    {
                        grandchild.GetComponent<Image>().sprite = Resources.Load<Sprite>($"{ccTo}");
                    }
                    child.gameObject.SetActive(true);
                    if (grandchild.tag == "close")
                    {
                        grandchild.gameObject.SetActive(true);
                    }
                }
            }
            if (child.tag == "fullScreenFacts")
            {
                foreach (Transform grandchild in child)
                {
                    if (grandchild.tag == "titleLarge")
                    {
                        grandchild.GetComponent<UnityEngine.UI.Text>().text = $"Learn about {countryName}:";
                    }
                    if (grandchild.tag == "funFact")
                    {
                        grandchild.GetComponent<UnityEngine.UI.Text>().text = $"The national animal of {countryName} is the {animal}.";
                    }
                    if (grandchild.tag == "flagImg")
                    {
                        grandchild.GetComponent<Image>().sprite = Resources.Load<Sprite>($"{ccTo}");
                    }
                    if (grandchild.tag == "flagImg")
                    {
                        grandchild.GetComponent<Image>().sprite = Resources.Load<Sprite>($"{ccTo}");
                    }
                    if (grandchild.tag == "languagesSpoken")
                    {
                        grandchild.GetComponent<UnityEngine.UI.Text>().text = $"In {countryName}, we speak {language}.";
                    }
                    foreach (Transform ggrandchild in grandchild)
                    {
                        if (ggrandchild.tag == "greeting")
                        {
                            ggrandchild.GetComponent<UnityEngine.UI.Text>().text = $"{greeting}!";
                        }
                    }
                }
            }
        }
    }
}