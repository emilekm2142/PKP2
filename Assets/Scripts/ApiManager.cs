using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class ApiManager : MonoBehaviour
{
    private const string baseUrl = "http://10.250.193.41:8080/";
    private int userId = 1;
    public User User { get; private set; }
    public TrainRide TrainRide { get; private set; }
    public List<User> users { get; private set; }
    public List<TrainRide> TrainRides { get; private set; }

    private void Awake()
    {
        FetchUser();
        FetchTrainRide();
        FetchTrainUsers();
        FetchTrainRides();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FetchUser()
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(baseUrl + "user/" + userId);
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();

        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(User));
        Stream stream = response.GetResponseStream();
        if (stream == null)
        {
            Debug.LogError("API Response is null");
            return;
        }
        User = (User) deserializer.ReadObject(stream);
    }
    
    public void FetchTrainRide()
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(baseUrl + "train-ride/" + User.currentTrainRide);
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();

        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(TrainRide));
        Stream stream = response.GetResponseStream();
        if (stream == null)
        {
            Debug.LogError("API Response is null");
        }
        TrainRide = (TrainRide) deserializer.ReadObject(stream);
    }
    
    public void FetchTrainUsers()
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(baseUrl + "train-ride/" + User.currentTrainRide + "/users");
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();

        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(List<User>));
        Stream stream = response.GetResponseStream();
        if (stream == null)
        {
            Debug.LogError("API Response is null");
        }
        users = (List<User>) deserializer.ReadObject(stream);
    }

    public void SubmitUserPosition(Vector3 position)
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(
            baseUrl + "user/" + User.currentTrainRide + "/locate?lat=" + position.x / 100.0f + "&lng=" + position.z
        );
        request.Method = "PUT";
        request.GetResponse();
    }

    public void FetchTrainRides()
    {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(baseUrl + "train-ride/");
        HttpWebResponse response = (HttpWebResponse) request.GetResponse();

        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(List<TrainRide>));
        Stream stream = response.GetResponseStream();
        if (stream == null)
        {
            Debug.LogError("API Response is null");
        }
        TrainRides = (List<TrainRide>) deserializer.ReadObject(stream);
    }

}
