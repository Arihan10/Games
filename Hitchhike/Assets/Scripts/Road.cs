using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject nextRoad, chosenCar1, chosenCar2, lastRoad, destroyVehicleLeft, destroyVehicleRight; 

    [SerializeField] Material p1Mat, p2Mat, neutralMat; 

    [SerializeField] bool isOn = false, starter = false, same = false, autoChooseDirLeft = false; 

    int landed = 0; 

    // Start is called before the first frame update
    void Start()
    {
        //SelectRandomCar(); 
        if (isOn) {
            SelectRandomCar(true); 
            SelectRandomCar(false); 
        }
        if (starter) {
            Land1();
            Land2(); 
        }

        if (Random.Range(0,2) == 0 || autoChooseDirLeft) {
            destroyVehicleRight.GetComponent<DestroyVehicle>().Disable(); 
        } else {
            destroyVehicleLeft.GetComponent<DestroyVehicle>().Disable(); 
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isOn) {
            //Land();
            isOn = false; 
        }
    }

    IEnumerator setOn() {
        yield return new WaitForSeconds(0.05f);
        isOn = true; 
    }

    public void SelectRandomCar(bool p1) {
        if (p1) {
            chosenCar1 = GetComponentInChildren<SpawnVehicle>().ChooseRandomCar(); 
            chosenCar1.GetComponent<ChangeVehicleMaterial>().ChangeMat(p1Mat); 
        } else {
            chosenCar2 = GetComponentInChildren<SpawnVehicle>().ChooseRandomCar();
            chosenCar2.GetComponent<ChangeVehicleMaterial>().ChangeMat(p2Mat);
        }
        if (chosenCar1 == chosenCar2) {
            chosenCar1.GetComponent<ChangeVehicleMaterial>().ChangeMat(neutralMat);
            same = true; 
        }
        StartCoroutine(setOn()); 
    }

    public void Land1() {
        isOn = false;
        ++landed; 
        nextRoad.GetComponent<Road>().SelectRandomCar(true); 
        nextRoad.GetComponent<Road>().lastRoad = gameObject; 
        if (!same || landed == 2) chosenCar1.GetComponent<ChangeVehicleMaterial>().RestoreMat(); 
        else chosenCar1.GetComponent<ChangeVehicleMaterial>().ChangeMat(p2Mat); 
        chosenCar1.GetComponent<ChangeVehicleMaterial>().aimLine1.SetActive(true);
        //Debug.Log(nextRoad.name); 
        if (starter) return; 
        lastRoad.GetComponent<Road>().chosenCar1.GetComponent<ChangeVehicleMaterial>().aimLine1.SetActive(false);
        destroyVehicleLeft.GetComponent<DestroyVehicle>().spawnVehicle.GetComponent<SpawnVehicle>().isSecond = true; 
    }

    public void Land2() {
        isOn = false;
        ++landed; 
        nextRoad.GetComponent<Road>().SelectRandomCar(false); 
        nextRoad.GetComponent<Road>().lastRoad = gameObject; 
        if (!same || landed == 2) chosenCar2.GetComponent<ChangeVehicleMaterial>().RestoreMat(); 
        else chosenCar2.GetComponent<ChangeVehicleMaterial>().ChangeMat(p1Mat); 
        chosenCar2.GetComponent<ChangeVehicleMaterial>().aimLine2.SetActive(true);
        //Debug.Log(nextRoad.name); 
        if (starter) return; 
        lastRoad.GetComponent<Road>().chosenCar2.GetComponent<ChangeVehicleMaterial>().aimLine2.SetActive(false); 
    }
}
