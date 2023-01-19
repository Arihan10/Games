using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class SpawnVehicle : MonoBehaviour
{
    [SerializeField] GameObject[] vehicles, boats; 

    public float movementSpeed = -2f; 

    public List<GameObject> activeVehicles;

    string prev;

    [SerializeField] int startingVehicles = 6;

    float spawnVehicleTimeOffset = 2.5f, spawnVehicleDistanceOffset;

    public int destroyed, spawned;

    GameObject lastVehicleSpawned;

    public bool dirLeft, isSecond = false;

    [SerializeField] bool spawnBoat = false; 

    int GetRandomVehicleIndex() {
        return UnityEngine.Random.Range(0, vehicles.Length); 
    }

    int GetRandomBoatIndex() {
        return UnityEngine.Random.Range(0, boats.Length); 
    }

    // Start is called before the first frame update
    void Start()
    {
        //SpawnVehicleMethod(GetRandomVehicleIndex()); 
        //StartCoroutine(StartVehicles(startingVehicles, Random.Range(1.5f,2.5f))); 
        if (!spawnBoat) {
            spawnVehicleTimeOffset /= Mathf.Abs(movementSpeed / 2f); 
            StartCoroutine(StartVehicles(startingVehicles, spawnVehicleTimeOffset)); 
            StartCoroutine(SpawnVehiclesCoroutine(0f));
            return; 
        }

        StartCoroutine(SpawnBoat()); 
    }

    IEnumerator SpawnBoat() {
        GameObject _boat; 
        while (true) {
            if (UnityEngine.Random.Range(0,15) == 0) {
                _boat = Instantiate(boats[GetRandomBoatIndex()], transform.position, transform.rotation);
                activeVehicles.Add(_boat); 
            }
            yield return new WaitForSeconds(5f); 
        }
    }

    IEnumerator SpawnVehiclesCoroutine(float time) {
        while (true) {
            if (destroyed != spawned) {
                int rand = GetRandomVehicleIndex(); 
                //float _time = time; 
                if (vehicles[rand].name == "Truck1 Variant" || prev == "Truck1 Variant") {
                    spawnVehicleDistanceOffset = 4f; 
                    Debug.Log("TRUCK");
                }
                else spawnVehicleDistanceOffset = 3f; 

                if (lastVehicleSpawned != null) {
                    while (Mathf.Abs(transform.position.z - lastVehicleSpawned.transform.position.z) < spawnVehicleDistanceOffset) {
                        Debug.Log("waiting"); 
                        yield return new WaitForSeconds(0.015f); 
                    }
                }
                lastVehicleSpawned = SpawnVehicleMethod(rand);
                prev = vehicles[rand].name; 
                //yield return new WaitForSeconds(_time); 
                //SpawnVehicleMethod(rand); 
                ++spawned; 
            }
            yield return new WaitForSeconds(0.015f); 
        }
    }

    public GameObject SpawnVehicleMethod(int rand) {
        //int rand = Random.Range(0, vehicles.Length); 
        GameObject _vehicle = Instantiate(vehicles[rand], transform.position, transform.rotation);
        _vehicle.GetComponent<ChangeVehicleMaterial>().road = transform.parent.gameObject; 
        prev = vehicles[rand].name; 
        activeVehicles.Add(_vehicle); 
        return _vehicle; 
    }

    IEnumerator StartVehicles(int n, float time) {
        for (int i = 0; i < n; ++i) {
            int rand = GetRandomVehicleIndex();
            //rand = 3; 
            float _time = time; 
            /* 
            if (prev == "Truck1 Variant") {
                _time += 2; 
                Debug.Log("TRUCK"); 
            }
            if (vehicles[rand].name == "Truck1 Variant") {
                _time += 2; 
                Debug.Log("TRUCK");
                --n; 
            }
            */
            yield return new WaitForSeconds(_time);
            SpawnVehicleMethod(rand);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerUI.instance.isTutorial && !isSecond) return; 
        foreach(GameObject _vehicle in activeVehicles) {
            //_vehicle.GetComponent<Rigidbody>().AddForce(_vehicle.transform.forward * movementSpeed * Time.deltaTime); 
            if (dirLeft) _vehicle.transform.Translate(_vehicle.transform.forward * movementSpeed * Time.deltaTime); 
            else _vehicle.transform.Translate(_vehicle.transform.forward * -movementSpeed * Time.deltaTime); 
            //_vehicle.transform.Translate(new Vector3(0f,0f,1f) * movementSpeed * Time.deltaTime); 
        }
    }

    public GameObject ChooseRandomCar() {
        if (activeVehicles.Count < 4) return activeVehicles[UnityEngine.Random.Range(1, activeVehicles.Count)]; 
        else return activeVehicles[UnityEngine.Random.Range(2, activeVehicles.Count)]; 
    }
}
