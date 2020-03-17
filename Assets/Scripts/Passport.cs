using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Passport : MonoBehaviour, IPointerDownHandler
{
    //public string username;
    List<string> allCountryList = new List<string>() { "EG", "US", "DE", "GR", "FR", "CN", "BR", "GB", "AU", "CA", "RU", "JP", "TR", "UK", "TH", "MX", "PH", "DZ", "AE", "ZA", "KR", "ES", "AR", "IN" };
    //List<string> collectedCountryList = new List<string> () {"AE", "ZA", "KR", "ES", "AR", "IN"};
    List<string> collectedCountryList = new List<string>();
    List<string> countries = new List<string>();

    public Image flagImage;
    private Texture2D texture;

    //public GameObject OpenedPassport;
    public GameObject UI;

    //retrieve the passport data of "User"
    public void start()
    {
        Debug.Log("START");
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://destinations-a0b80.firebaseio.com/");
        FirebaseDatabase.DefaultInstance
      .GetReference("Users")
      .Child("1")
      .Child("Countries")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              Debug.Log("error");
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              //List<string> countries = new List<string>();
              foreach (var country in snapshot.Children)
              {
                  countries.Add(country.Value.ToString());
              }

              Debug.Log("Suceed!");
              Debug.Log(countries);

          }
      });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("START");
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://destinations-a0b80.firebaseio.com/");
        FirebaseDatabase.DefaultInstance
      .GetReference("Users")
      .Child("1")
      .Child("Countries")
      .GetValueAsync().ContinueWith(task =>
      {
          if (task.IsFaulted)
          {
              Debug.Log("error");
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              //List<string> countries = new List<string>();
              foreach (var country in snapshot.Children)
              {
                  countries.Add(country.Value.ToString());
              }

              Debug.Log("Suceed!");
              Debug.Log(countries);

          }
      });

        UI = GameObject.Find("/UIComponents");
        //OpenedPassport = GameObject.Find("/UIComponents/OpenedPassport");
        Transform uiTr = UI.transform;

        foreach (Transform child in uiTr)
        {
            if (child.tag == "open")
            {
                foreach (Transform grandchild in child)
                {

                    if (grandchild.tag != "close")
                    {
                        if (countries.Contains(grandchild.tag))
                        {
                            //if (collectedCountryList.Contains(child.tag))
                            //{
                            grandchild.GetComponent<Image>().sprite = Resources.Load<Sprite>(grandchild.tag);
                        }
                        else
                        {
                            grandchild.GetComponent<Image>().sprite = Resources.Load<Sprite>("QMark");
                        }
                    }
                    else
                    {
                        grandchild.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
