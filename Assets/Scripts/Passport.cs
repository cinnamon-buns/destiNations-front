using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Passport : MonoBehaviour, IPointerDownHandler
{
    public string username;
    List<string> allCountryList = new List<string> () {"EG", "US", "DE", "GR", "FR", "CN", "BR", "GB", "AU", "CA", "RU", "JP", "TR", "UK", "TH", "MX", "PH", "DZ", "AE", "ZA", "KR", "ES", "AR", "IN"};
    List<string> collectedCountryList = new List<string> () {"AE", "ZA", "KR", "ES", "AR", "IN"};

    public Image flagImage;
    private Texture2D texture;

    //public int ShapeID { get; set; } = int.MinValue;
    //public GameObject CountryCanvas;
    public GameObject OpenedPassport;



    public void start()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OpenedPassport = GameObject.Find("/UIComponents/OpenedPassport");
        Transform ppTr = OpenedPassport.transform;
        foreach (Transform child in ppTr) {
            if (child.tag != "close") {
                if (collectedCountryList.Contains(child.tag)) {
                    child.GetComponent<Image>().sprite = Resources.Load<Sprite>(child.tag);
                } else {
                    child.GetComponent<Image>().sprite = Resources.Load<Sprite>("QMark");
                }
            } else {
                child.gameObject.SetActive(true);
            }
        }
    }
}

