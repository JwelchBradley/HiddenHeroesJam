using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MechController : MonoBehaviour
{
    Rigidbody rb;
    DavidFace face;

    [SerializeField] private CinemachineImpulseSource shootCameraShake;

    private float timeOfLastShotSound = -Mathf.Infinity;
    [SerializeField] private float timeBetweenShotSounds = 0.1f;
    [SerializeField] private AudioClip shootSound;
    private AudioSource audioSource;

    public GameObject laserObj;
    LineRenderer[] lasers;

    int updatesLate = 10;
    int bacciCounter = 0;

    public Transform cam;

    List<Vector3> forwards, rights;

    public Gradient laserGradient;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        face = FindObjectOfType<DavidFace>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lasers = new LineRenderer[updatesLate * 2];
        for(int i = 0; i < updatesLate; i++)
        {
            LineRenderer lr = Instantiate(laserObj, transform).GetComponent<LineRenderer>();
            float c = ((float)1 / (float)updatesLate);
            lr.startColor = laserGradient.Evaluate(c * (float)i);
            lr.endColor = laserGradient.Evaluate(c * ((float)i + (float)1));
            lr.SetPositions(new Vector3[] { transform.position, transform.position + cam.transform.forward});
            lr.enabled = false;
            lasers[i] = lr;
        }
        for (int i = 0; i < updatesLate; i++)
        {
            LineRenderer lr = Instantiate(laserObj, transform).GetComponent<LineRenderer>();
            float c = ((float)1 / (float)updatesLate);
            lr.startColor = laserGradient.Evaluate(c * (float)i);
            lr.endColor = laserGradient.Evaluate(c * ((float)i + (float)1));
            lr.SetPositions(new Vector3[] { transform.position, transform.position + cam.transform.forward});
            lr.enabled = false;
            lasers[i + updatesLate] = lr;
        }

        forwards = new List<Vector3>();
        rights = new List<Vector3>();
        for(int i = 0; i < updatesLate; i++)
        {
            forwards.Add(cam.transform.forward);
            rights.Add(cam.transform.right);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if(timeBetweenShotSounds+timeOfLastShotSound < Time.time)
            {
                shootCameraShake.GenerateImpulse();
                timeOfLastShotSound = Time.time;
                audioSource.PlayOneShot(shootSound);
            }

            RaycastHit hit;
            if (Physics.Raycast(new Ray(cam.position, cam.forward), out hit, 100))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    hit.transform.GetComponent<Damageable>().UpdateHealth(-25);
                }
                else if (hit.transform.gameObject.tag == "Baccis" && enabled)
                {
                    bacciCounter++;

                    if (bacciCounter == 10)
                    {
                        FindObjectOfType<MenuBehavior>().LoadScene("BaccisBonus");
                        enabled = false;
                    }
                }
            }
            lasers[0].enabled = true;
            lasers[updatesLate].enabled = true;
            face.anim.SetBool("Attacking", true);
        }
        else
        {
            lasers[0].enabled = false;
            lasers[updatesLate].enabled = false;
            face.anim.SetBool("Attacking", false);
        }

        lasers[0].SetPositions(new Vector3[] { transform.position - cam.transform.right, transform.position + cam.transform.forward - cam.transform.right });
        lasers[updatesLate].SetPositions(new Vector3[] { transform.position + cam.transform.right, transform.position + cam.transform.forward + cam.transform.right });
        forwards.RemoveAt(forwards.Count - 1);
        forwards.Insert(0, cam.transform.forward);
        rights.RemoveAt(rights.Count - 1);
        rights.Insert(0, cam.transform.right);

        for(int i = updatesLate - 1; i > 0; i--)
        {
            lasers[i].SetPositions(new Vector3[] { lasers[i - 1].GetPosition(0) + forwards[i - 1] * (i - 1), lasers[i - 1].GetPosition(1) + forwards[i - 1] * i });
            lasers[i].enabled = lasers[i - 1].enabled;
        }

        for(int i = lasers.Length - 1; i >= updatesLate; i--)
        {
            lasers[i].SetPositions(new Vector3[] { lasers[i - 1].GetPosition(0) + forwards[i - updatesLate] * (i - updatesLate), lasers[i - 1].GetPosition(1) + forwards[i - updatesLate] * (i - updatesLate + 1) });
            lasers[i].enabled = lasers[i - 1].enabled;
        }

    }
}


