using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Vectrosity;
using System.Text;


// added
class DataItem
{
    public string username;
}
// end

public class ClientAPI : MonoBehaviour
{
    [SerializeField]
    Airplane[] prefabs;

    private string url = "//desti-nations-stage.herokuapp.com/";
    public static object planeData = "the data";
    public string result;
    public object data;
    public CountryFacts facts;
    public fullScreenFacts fullFacts;
    public GameObject globe;
    private Airplane instance;
    private GameObject departure;
    private GameObject arrival;
    private VectorLine spline;
    public int segments = 10;
    public bool loop = true;
    List<Airplane> airplanes;
    //List<string> countryCodes = new List<string> () {"EG", "US", "DE", "GR", "FR", "CN", "BR", "GB", "AU", "CA", "RU", "JP", "TR", "UK", "TH", "MX", "PH", "DZ", "AE", "ZA", "KR", "ES", "AR", "IN"};
    private int length;
    void Awake()
	{
        airplanes = new List<Airplane>();
    }

    void Start()
    {
        StartCoroutine(Get(url));
    }

    //void LateUpdate()
    //{
    //    spline.Draw3DAuto();
    //}


    public IEnumerator Get(string url)
    {
        using(UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    result = @System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
       

                    globe = GameObject.Find("/earth/Icosphere");


                    var N = JSON.Parse(result);
                    // assign the length of all the to-from Array into an integer (118, on 03/09 evening)
                    length = Int32.Parse(N["length"].Value);

                    for (int i = 0; i < length; i++)
                    {
                 
                        //bool hasCCto = countryCodes.Contains(N["data"]);
                        //bool hasCCfrom = countryCodes.Contains(N["data"][i]["from"]["country"]["cc"]);

                        //if (hasCCto && hasCCfrom) {
                            Airplane instance = Instantiate(prefabs[0]);
                            Transform p = instance.transform;
                            p.parent = globe.transform;
                            //p.localPosition = new Vector3(-2.999408f, -2.960107f - i, -.1357397f);
                            instance.ccTo = N["data"][i]["to"]["country"]["cc"];
                            instance.ccFrom = N["data"][i]["from"]["country"]["cc"];
                            instance.countryName = N["data"][i]["to"]["country"]["name"];
                            instance.flag = N["data"][i]["to"]["country"]["flag"];
                            instance.language = N["data"][i]["to"]["country"]["languages"][0]["name"];
                            instance.greeting = N["data"][i]["to"]["country"]["greeting"];
                            // instance.greeting = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes($"{greet}")));
                            

                        departure = GameObject.FindGameObjectWithTag($"{instance.ccFrom}");
                            arrival = GameObject.FindGameObjectWithTag($"{instance.ccTo}");
                            if (departure != null && arrival != null) 
                            {
                            Vector3 midpoint = (departure.transform.position + arrival.transform.position) * 0.5f;

                            p.position = Vector3.MoveTowards(midpoint, globe.transform.position, -2f);
                            p.LookAt(arrival.transform);
                            //p.localRotation = Quaternion.Euler(208.305f, -2.268005f, -48.89499f);
                            //p.localScale = new Vector3(0.3f, 0.3f, 0.3f);


                            var splinePoints = new List<Vector3>();
                            splinePoints.Add(departure.transform.localPosition);
                            splinePoints.Add(p.localPosition);
                            splinePoints.Add(arrival.transform.localPosition);
                            spline = new VectorLine("Spline", new List<Vector3>(segments + 1), 10.0f, LineType.Continuous); //LineType.Discrete
                            //spline.SetColor(new Color(204, 0, 82));
                            spline.MakeSpline(splinePoints.ToArray(), segments, loop);
                            //spline.MakeSpline(splinePoints.ToArray());
                            //spline.textureScale = 1.0f;
                            spline.drawTransform = globe.transform;
                            airplanes.Add(instance);
                            //Material lineMat = Resources.Load("ArcLine", typeof(Material)) as Material;
                            //spline.material = lineMat;
                            spline.Draw3DAuto();
                        //}

                            }
                    }
                    
                }
                else
                {
                    Debug.Log("Error: Could not get data");
                }
            }
        }
    }
}
