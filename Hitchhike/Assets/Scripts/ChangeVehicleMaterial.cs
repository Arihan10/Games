using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVehicleMaterial : MonoBehaviour {
    [SerializeField] GameObject car;

    Material[] actualMaterials;

    public GameObject aimLine1, aimLine2, road;

    [SerializeField] bool starter;

    private void Awake() {
        actualMaterials = car.GetComponent<MeshRenderer>().materials;
        aimLine1.GetComponent<Aiming>().car = gameObject; 
        aimLine2.GetComponent<Aiming>().car = gameObject; 
    }

    public void ChangeMat(Material _mat) {
        //car.GetComponent<MeshRenderer>().material = _mat; 
        Material[] materials = new Material[car.GetComponent<MeshRenderer>().materials.Length];
        //actualMaterials = car.GetComponent<MeshRenderer>().materials; 
        for (int i = 0; i < materials.Length; ++i) {
            materials[i] = _mat;
        }
        car.GetComponent<MeshRenderer>().materials = materials;
        //Debug.Log(name); 
    }

    public void ChangeMat(Material[] _mat) {
        car.GetComponent<MeshRenderer>().materials = _mat;
        //Debug.Log(name); 
    }

    public void RestoreMat() {
        ChangeMat(actualMaterials);
    }

    public void Hit1() {
        road.GetComponent<Road>().Land1();
    }

    public void Hit2() {
        road.GetComponent<Road>().Land2();
    }
}
