using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DefaultNamespace;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
	
	
	public GameObject wagonPrefab;
	public GameObject railSegmentPrefab;
    public GameObject cityPrefab;
    public GameObject trainPrefab;
    public GameObject thomasPrefab;
    public GameObject pendolinoPrefab;
    public GameObject enemyPrefab;
    public List<TrainPath> trainPaths  = new List<TrainPath>();
    public CustomizationManager customizationManager;
    public CameraDrag cameraDrag;
    public ApiManager apiManager;
    public List<Train> trains;
    public List<Vector3> beziers = new List<Vector3>();
    public const float cityHeight = 1.276719f;

    public int EnemiesNum = 10;
    // Start is called before the first frame update
    private void Awake()
    {
	    customizationManager = GameObject.FindObjectOfType<CustomizationManager>();
        apiManager = GameObject.FindObjectOfType<ApiManager>();
        cameraDrag = GameObject.FindObjectOfType<CameraDrag>();
    }

    GameObject DisplayRails(List<Vector3> points, float offset)
    { 
	    var go = Instantiate(railSegmentPrefab, new Vector3(0, cityHeight	, 0), Quaternion.identity);
    var x = go.GetComponent<LineRenderer>();
    x.positionCount = points.Count;
    for (int i = 0; i < points.Count; i++)
    {
	    x.SetPosition(i, points[i] + new Vector3(offset,cityHeight,0));
    }

    return go;
    }
    void Start()
    {
	    LoadCities();
	    LoadTrains();
	    StartMinigame(trains[0]);
    }

   public  Wagon getMyWagon()
    {
	    foreach (var i in trains)
	    {
		    foreach (var j in i.wagons)
		    {
			    if (Utils.HasComponent	<Player>(j.gameObject)){return j;}
		    }
	    }

	    return null;
    }
    Train MakeTrain(string name, TrainTypes type, Vector3 position)
    {
	    var a = Instantiate(trainPrefab, position, Quaternion.identity);
        var train = a.GetComponent<Train>();
        train.name = name;
        GameObject lokomotywa = null;
        if (type == TrainTypes.Thomans)
        {
            lokomotywa = Instantiate(thomasPrefab, train.gameObject.transform);
        }
        else if(type == TrainTypes.Pendolino)
        {
            lokomotywa = Instantiate(pendolinoPrefab, train.gameObject.transform);
        }
        return train;
    }

    City MakeCity(string name, Vector3 position)
    {
        var a = Instantiate(cityPrefab, position + new Vector3(UnityEngine.Random.Range	(8,12),cityHeight	 ,UnityEngine.Random.Range(8,12)), Quaternion.identity);
        a.gameObject.name = name;
        a.GetComponent<City>().name = name;
        return a.GetComponent<City>();
    }

    TrainPath MakePath(string name, Vector3 start, Vector3 end)
    {
	    var a =new TrainPath();
	    a.name = name;
	    a.points = MakeBezierBetweenTwoPoints(start, end, 25).Item1;
	    return a;
    }
    public Tuple<List<Vector3>, List<Vector3>> MakeBezierBetweenTwoPoints(Vector3 A, Vector3 D, float distance = 1.0f)
    {
	    float angle1 = UnityEngine.Random.Range(0.0f, 6.28f);
	    float angle2 = UnityEngine.Random.Range(0.0f, 6.28f);
	    angle1 = 0;
	    angle2 = 0;
	    Vector3 B = A + new Vector3(Mathf.Cos(angle1)*distance, 0, 0) + new Vector3(Mathf.Sin(angle1), 0, 0);
	    Vector3 C = D + new Vector3(Mathf.Cos(angle2)*distance, 0, 0) + new Vector3(Mathf.Sin(angle2), 0, 0);
	    
		int interpolationPoints = 1000;
		float dt = 1.0f / interpolationPoints;
		var curve = new List<Vector3>();
		var direction = new List<Vector3>();
		//Calculating the total length of the path to know what the interpolation distance should be
		float totalLength = 0;
		for (int i=1; i<interpolationPoints; i++){
			float t = i/interpolationPoints;
			Vector3 derivative = (-A*3*Mathf.Pow((1-t),2) + 3*B*Mathf.Pow((1-t),2) - 6.0f*B*t*(1-t) +6.0f*C*t*(1-t)-3*C*t*t + 3*D*t*t);
			float speed = Mathf.Sqrt(derivative[0]*derivative[0] + derivative[1]*derivative[1]+ derivative[2]*derivative[2]);
			totalLength += speed * dt;
		}
		
	    float t2 = 0;
	    float interpolationLength = 0.1f;
	    //calculating the number of samples in the final path
	    interpolationPoints = (int)(totalLength / interpolationLength);
		while (t2<1){
			Vector3 curvePoint = A*Mathf.Pow((1-t2),3) + 3*B*t2*(1-t2)*(1-t2) + 3*C*t2*t2*(1-t2) + D*t2*t2*t2;
			Vector3 derivative = (-A*3*Mathf.Pow((1-t2),2) + 3*B*Mathf.Pow((1-t2),2) - 6*B*t2*(1-t2) +6*C*t2*(1-t2)-3*C*t2*t2 + 3*D*t2*t2);
			float speed = Mathf.Sqrt(derivative[0]*derivative[0] + derivative[1]*derivative[1]+ derivative[2]*derivative[2]);
			curve.Add(curvePoint);
			direction.Add(derivative/speed);
			t2 += interpolationLength / speed;
		}
		var tuple = new Tuple<List<Vector3>, List<Vector3>>(curve,direction);

		return tuple;
    }
	public Tuple<List<Vector3>, List<Vector3>> GenerateRails(Tuple<List<Vector3>, List<Vector3>> path, float trackWidth=1.0f)
    {
	    var left = new List<Vector3>();
	    var right = new List<Vector3>();
	    for (int i = 0; i < path.Item1.Count; i++)
	    {
		    float x = path.Item1[i][0];
		    float y = path.Item1[i][1];
		    float z = path.Item1[i][2];
		    
		    float dirX = path.Item2[i][0]*trackWidth/2;
		    float dirY = path.Item2[i][1]*trackWidth/2;
		    float dirZ = path.Item2[i][2]*trackWidth/2;
		    left.Add(new Vector3(x+dirZ, 0, z-dirX));
		    right.Add(new Vector3(x-dirZ, 0, z+dirX));
	    }
	    return new Tuple<List<Vector3>, List<Vector3>>(left, right);
    }

	private void LoadCities()
    {
	    int stationsToMake = Math.Min(10, apiManager.TrainRide.points.Count);
	    for (int i = 0; i < stationsToMake; i++)
	    {
		    Point point = apiManager.TrainRide.points[i];
		    MakeCity(point.stationName, new Vector3(100.0f * (float) point.lat, cityHeight, 100.0f * (float) point.lng));

		    if (i < stationsToMake - 1)
		    {
			    Point nextPoint = apiManager.TrainRide.points[i + 1];
			    Tuple<List<Vector3>, List<Vector3>> bezier = MakeBezierBetweenTwoPoints(
				    new Vector3(100.0f * (float) point.lat, cityHeight, 100.0f * (float) point.lng),
				    new Vector3(100.0f * (float) nextPoint.lat, cityHeight, 100.0f * (float) nextPoint.lng),1
			    );
			    foreach (var b in bezier.Item1)
			    {
				    beziers.Add(b);
			    }
			    Tuple<List<Vector3>, List<Vector3>> rails = GenerateRails(bezier,1.2f);
			    DisplayRails(rails.Item1, 0);
			    DisplayRails(rails.Item2, 0);
		    }
	    }
    }

	private void LoadTrains()
	{
		foreach (var trainRide in apiManager.TrainRides)
		{
			Point firstPoint = trainRide.points[0];
			
			Train train = MakeTrain(
				trainRide.trainRideId,
				TrainTypes.Thomans,
				new Vector3(100.0f * (float) firstPoint.lat, cityHeight, 100.0f * (float) firstPoint.lng)
			);
			
			foreach (var user in apiManager.GetTrainUsers(trainRide.trainRideId))
			{
				bool isPlayer = user.userId == apiManager.userId;
				Wagon wagon = train.AddWagon(isPlayer);
				if (isPlayer)
				{
					Camera.main.transform.SetParent(wagon.transform);
					Camera.main.transform.position = new Vector3(-10, 10, -6);
					Camera.main.transform.rotation = Quaternion.Euler(20, 50, 0);
					cameraDrag.target = train;
					customizationManager.myWagon = wagon;
					customizationManager.Fetch();
				}
			}
			
			TrainPath path = new TrainPath();
			path.points = beziers;
			train.FollowPath(path);

			trains.Add(train);
		}
	}

	private void StartMinigame(Train train)
	{
		List<Enemy> enemies = new List<Enemy>();
		Run.EachFrame(() =>
		{
			if (enemies.Count < EnemiesNum && UnityEngine.Random.Range(0.0f, 1.0f) < 0.01)
			{
				float angle = UnityEngine.Random.Range(0.0f, 6.28f);
				Vector3 offset = new Vector3(Mathf.Sin(angle)*50.0f, 0, Mathf.Cos(angle)*50.0f);
				var enemyGameObject = Instantiate(enemyPrefab, train.gameObject.transform.position+offset, Quaternion.identity);
				var enemy = enemyGameObject.GetComponent<Enemy>();
				enemies.Add(enemy);
			}

			for (int i = 0; i < enemies.Count; i++)
			{
				if (enemies[i].StepTowardsTrain(train) < 1)
				{
					Destroy(enemies[i].gameObject);
					enemies.Remove(enemies[i]);
				}
			}

		});
	}

	// Update is called once per frame
    void Update()
    {
        
    }
}
