using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    public bool p1 = true;

    GameObject endScreen;

    public GameObject spawnedFrom; 

    private void Start() {
        transform.Rotate(new Vector3(0f, 90f, 0f)); 
    }

    private void Awake() {
        endScreen = PlayerUI.instance.endScreen; 
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(transform.forward * movementSpeed * Time.deltaTime); 
        transform.position += transform.forward * movementSpeed * Time.deltaTime; 
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject == spawnedFrom) return; 
        if (p1) {
            if (collision.gameObject == collision.gameObject.GetComponent<ChangeVehicleMaterial>().road.GetComponent<Road>().chosenCar1) {
                collision.gameObject.GetComponent<ChangeVehicleMaterial>().Hit1(); 
                Debug.Log("done wtf");
            }
            else {
                Debug.Log("YOU LOSE");
                //SceneManager.LoadScene(1); 
                PlayerUI.instance.GameOver(false); 
            }
        } else {
            if (collision.gameObject == collision.gameObject.GetComponent<ChangeVehicleMaterial>().road.GetComponent<Road>().chosenCar2) {
                collision.gameObject.GetComponent<ChangeVehicleMaterial>().Hit2(); 
                Debug.Log("done wtf");
            }
            else {
                Debug.Log("YOU LOSE");
                //SceneManager.LoadScene(1); 
                PlayerUI.instance.GameOver(true); 
            }
        }
        Destroy(gameObject); 
    }
}
