using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpinnyWinny : MonoBehaviour
{
    [SerializeField] GameObject[] vehicles;

    GameObject vehicle; 

    List<float> yRots = new List<float>(); 

    [SerializeField] float movementSpeed = 0.1f; 
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject _vehicle in vehicles) {
            yRots.Add(_vehicle.transform.eulerAngles.y); 
        }
        StartCoroutine(SpinnyWinny()); 
    }

    IEnumerator SpinnyWinny() {
        while (true) {
            yield return new WaitForSeconds(0.05f); 
            for (int i = 0; i < vehicles.Length; ++i) {
                /* 
                vehicle = vehicles[i];
                vehicle.SetActive(true); 
                while (Mathf.Abs(vehicles[i].transform.eulerAngles.y - yRots[i]) > 2f) {
                    yield return new WaitForSeconds(0.1f); 
                }
                vehicle.SetActive(false); 
                */
                vehicles[i].SetActive(true); 
                for (int j = 0; j < 360; ++j) {
                    vehicles[i].transform.Rotate(0f, 1f, 0f, Space.World);
                    yield return new WaitForSeconds(movementSpeed / 50f); 
                }
                vehicles[i].SetActive(false); 
            }
        }
    }

    private void Update() {
        //vehicle.transform.Rotate(0f, movementSpeed, 0f); 
    }
}
