using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShotFactory : MonoBehaviour
{
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private Image shotReloadTimer;

    [Header("Variables")]
    [SerializeField]
    private float shotForce;
    [SerializeField]
    private float reloadTime;

    private float timer = 0f;
    private bool ableToShoot = true;

    [Header("DEBUG")]
    [SerializeField]
    private List<GameObject> shotInstances = new List<GameObject>();

    public void CreateShot()
    {
        if (ableToShoot)
        {
            Vector3 aimTarget = Mouse.current.position.value;
            var direction = shootOrigin.position - aimTarget;

            GameObject nextShot = null;
            var force = direction * shotForce;
            foreach (var shot in shotInstances)
            {
                if (shot != null && !shot.activeSelf)
                {
                    shot.SetActive(true);
                    nextShot = shot;
                }
            }
            if (nextShot == null)
            {
                nextShot = Instantiate(shotPrefab);
                shotInstances.Add(nextShot);
            }

            nextShot.transform.position = shootOrigin.position;
            var body = nextShot.GetComponent<Rigidbody2D>();
            body.velocity = force;

            ableToShoot = false;
            timer = reloadTime;
        }
    }


    public void Update()
    {
        if (timer > 0) { 
            timer -= Time.deltaTime;
            shotReloadTimer.fillAmount = 1 - timer / reloadTime;
        } else
        {
            ableToShoot = true;
            shotReloadTimer.fillAmount = 1;
        }
    }

}
