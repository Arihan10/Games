using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVehicle : MonoBehaviour
{
    public SpawnVehicle spawnVehicle; 
    
    // Start is called before the first frame update
    void Start()
    {
        /* 
        for (int i = 0; i < 5; ++i) {
            spawnVehicle.StartCoroutine(spawnVehicle.SpawnVehicleMethod(0.5f)); 
        }
        */
    }

    void DestroyVehicleMethod(GameObject _gameObject) {
        spawnVehicle.activeVehicles.Remove(_gameObject); 
        Destroy(_gameObject);
        //spawnVehicle.StartCoroutine(spawnVehicle.SpawnVehicleOffset(0f)); 
        ++spawnVehicle.destroyed; 
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.parent != null) DestroyVehicleMethod(other.transform.parent.gameObject);
        else DestroyVehicleMethod(other.gameObject); 
    }

    public void Disable() {
        spawnVehicle.gameObject.SetActive(false);
        gameObject.SetActive(false); 
    }
}
