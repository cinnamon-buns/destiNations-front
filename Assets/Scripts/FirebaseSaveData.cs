using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

public class FirebaseSaveData : MonoBehaviour
{
    // DatabaseReference reference;
    public Text text;
    public InputField inputField;

    public static string countryAdded;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     // Set up the Editor before calling into the realtime database.
    //     FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://destinations-a0b80.firebaseio.com/");

    //     // Get the root reference location of the database.
    //     reference = FirebaseDatabase.DefaultInstance.RootReference;
    // }

    // Update is called once per frame
    public void OnSubmit()
    {
        Debug.Log("submit!");
        countryAdded = inputField.text;
        PostToDatabase();
    }

    private void PostToDatabase()
    {
        Debug.Log("posting to database");
        User user = new User();
        Debug.Log(user);
        RestClient.Post("https://destinations-a0b80.firebaseio.com/.json", user);
        Debug.Log("after calling restClient");
    }
}
