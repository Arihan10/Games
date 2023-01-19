using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;

    bool left, okNow = false;

    [SerializeField] GameObject player, launchPlayerPosReference, mesh; 

    [SerializeField] bool p1 = true;

    public GameObject car; 
    
    // Start is called before the first frame update
    void Start()
    {
        if (p1) {
            if (transform.parent.GetComponent<ChangeVehicleMaterial>().road.GetComponent<Road>().nextRoad.GetComponent<Road>().chosenCar1.transform.position.z < transform.position.z) {
                left = false;
            }
            else {
                left = true;
            }
        } else {
            if (transform.parent.GetComponent<ChangeVehicleMaterial>().road.GetComponent<Road>().nextRoad.GetComponent<Road>().chosenCar2.transform.position.z < transform.position.z) {
                left = false;
            }
            else {
                left = true;
            }
        }
    }

    private void OnEnable() {
        StartCoroutine(okNowWait()); 
    }

    IEnumerator okNowWait() {
        yield return new WaitForSeconds(0.2f);
        okNow = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerUI.instance.gameOver) gameObject.SetActive(false); 
        //Debug.Log(transform.eulerAngles.y); 
        if (transform.eulerAngles.y < 280f && transform.eulerAngles.y > 100f) {
            left = false; 
        } else if (transform.eulerAngles.y > 80f && transform.eulerAngles.y < 279f) {
            left = true; 
        }

        if (left) transform.Rotate(new Vector3(0f, -rotationSpeed * Time.deltaTime, 0f)); 
        else transform.Rotate(new Vector3(0f, rotationSpeed * Time.deltaTime, 0f)); 

        if (p1) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump(); 
            }
            PlayerUI.instance.p1Score += 0.002f; 
            //0.002f; 
        } else if (!p1) {
            if (Input.GetKeyDown(KeyCode.RightShift)) {
                Jump(); 
            }
            PlayerUI.instance.p2Score += 0.002f; 
        }

        if (!mesh.GetComponent<Renderer>().isVisible && okNow) {
            okNow = false; 
            StartCoroutine(almostOver()); 
        }

        if (PlayerUI.instance.p1Eligible && p1 && Input.GetKeyDown(KeyCode.A)) {
            //GameObject.Find("AimLinePivot (1)").transform.parent.GetComponent<ChangeVehicleMaterial>().road.GetComponent<Road>().nextRoad.GetComponent<Road>().chosenCar2.GetComponent<Rigidbody>().AddForce(transform. 1000f); 
            GameObject.Find("AimLinePivot (1)").transform.parent.GetComponent<ChangeVehicleMaterial>().road.GetComponent<Road>().nextRoad.GetComponent<Road>().chosenCar2.GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * 2000f); 
            //Destroy(GameObject.Find("AimLinePivot (1)").transform.parent.GetComponent<ChangeVehicleMaterial>().road.GetComponent<Road>().nextRoad.GetComponent<Road>().chosenCar2); 
            PlayerUI.instance.p1Eligible = false;
            PlayerUI.instance.p1Score = 0f; 
        } else if (PlayerUI.instance.p2Eligible && !p1 && Input.GetKeyDown(KeyCode.Slash)) {
            //GameObject.Find("AimLinePivot").transform.parent.GetComponent<ChangeVehicleMaterial>().road.GetComponent<Road>().nextRoad.GetComponent<Road>().chosenCar1.GetComponent<Rigidbody>().AddForce(transform.up * 10f);
            GameObject.Find("AimLinePivot").transform.parent.GetComponent<ChangeVehicleMaterial>().road.GetComponent<Road>().nextRoad.GetComponent<Road>().chosenCar1.GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * 2000f);
            PlayerUI.instance.p2Eligible = false;
            PlayerUI.instance.p2Score = 0f;
            PlayerUI.instance.used2 = false; 
        }
    }

    void Jump() {
        //Instantiate(player, launchPlayerPosReference.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y+90f,transform.eulerAngles.z)); 
        GameObject _player = Instantiate(player, launchPlayerPosReference.transform.position, transform.rotation);
        if (!p1) _player.GetComponent<Player>().p1 = false;
        _player.GetComponent<Player>().spawnedFrom = car; 
        Debug.Log(p1);
        GetComponent<AudioSource>().Play(); 
    }

    IEnumerator almostOver() {
        yield return new WaitForSeconds(1f);
        PlayerUI.instance.GameOver(!p1); 
    }
}
