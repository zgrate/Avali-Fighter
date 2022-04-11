using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{

    private readonly WaitForSeconds shotDuration = new WaitForSeconds(0.1f);   
    public AudioSource shootAudio;                                    
    public LineRenderer laserLineRenderer;                            
    private float fireCooldown;
    public float fireDelay = 0.25f;
    public Transform shootSource;

    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        laserLineRenderer.startWidth = 0.1f;
        laserLineRenderer.endWidth = 0.1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        Cursor.lockState = CursorLockMode.None;
    }


    private IEnumerator ShotLaser()
    {
        shootAudio.Play();
        laserLineRenderer.enabled = true;
        yield return shotDuration;
        laserLineRenderer.enabled = false;
    }
    public void SetPaused(bool paused)
    {
        this.paused = paused;
    }
    // Update is called once per frame
    void Update()
    {
        if (!paused && Input.GetButtonDown("Fire1") && Time.time > fireCooldown)
        {
            fireCooldown = Time.time + fireDelay;
            Vector3 rayStartOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            StartCoroutine(ShotLaser());
            laserLineRenderer.SetPosition(0, shootSource.position);

            if (Physics.Raycast(rayStartOrigin, Camera.main.transform.forward, out RaycastHit hit, 50))
            {
                laserLineRenderer.SetPosition(1, hit.point);
                Debug.Log(hit.collider.gameObject);
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<PlayerFollower>().GetShot();
                }

            }
            else
            {
                laserLineRenderer.SetPosition(1, rayStartOrigin + (Camera.main.transform.forward * 50));
            }
        }


    }
}
