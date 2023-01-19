using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f, accelerationFactor = 0.008f, vehicleSpeedAccelerationFactor = 0.5f, vehicleMovementSpeed; 

    float posBeforeRoad = -16.6f;

    [SerializeField] GameObject road, lastRoad, ground, scoreText;

    float startPos;

    int score = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        vehicleMovementSpeed = lastRoad.GetComponentInChildren<SpawnVehicle>().movementSpeed;
        startPos = transform.position.x; 
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(0f, 1f, 0f) * movementSpeed * Time.deltaTime);
        transform.Translate(new Vector3(1f, 0f, 0f) * movementSpeed * Time.deltaTime, Space.World); 
        movementSpeed += accelerationFactor; 
        if (Mathf.Abs(transform.position.x - posBeforeRoad) >= 6f) {
            ++score; 
            posBeforeRoad = transform.position.x; 
            lastRoad.GetComponent<Road>().nextRoad = Instantiate(road, new Vector3(lastRoad.transform.position.x + 6.5f, 0.57f, 0f), Quaternion.identity);
            lastRoad = lastRoad.GetComponent<Road>().nextRoad; 
            vehicleMovementSpeed -= vehicleSpeedAccelerationFactor; 
            //Debug.Log(vehicleMovementSpeed + " " + accelerationFactor); 
            lastRoad.GetComponent<Road>().destroyVehicleLeft.GetComponent<DestroyVehicle>().spawnVehicle.GetComponentInChildren<SpawnVehicle>().movementSpeed = Mathf.Clamp(vehicleMovementSpeed, -4f, -2f); lastRoad.GetComponent<Road>().destroyVehicleRight.GetComponent<DestroyVehicle>().spawnVehicle.GetComponentInChildren<SpawnVehicle>().movementSpeed = Mathf.Clamp(vehicleMovementSpeed, -4f, -2f); 
            //ground.transform.localScale += new Vector3(2f,0f,0f); 
            if (!PlayerUI.instance.gameOver) scoreText.GetComponent<Text>().text = score.ToString(); 
        }

        if (PlayerUI.instance.gameOver) {
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.None;
            return; 
        }; 

        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;

        //scoreText.GetComponent<Text>().text = ((int)(transform.position.x - startPos)).ToString(); 
    }
}
