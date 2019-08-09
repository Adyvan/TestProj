using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShutterBeh : MonoBehaviour
{
  [SerializeField]
  private GameObject bulletPrefab;

  [SerializeField]
  private float powerShoot = 20;

  private void Awake()
  {
    Observable.EveryUpdate()
   .Where(_ => Input.GetKeyDown(KeyCode.Space))
   .Subscribe(_ => Shoot())
   .AddTo(this);
  }

  private void Shoot()
  {
    var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

    var richBody = bullet.GetComponent<Rigidbody>();

    if (richBody != null)
    {
      richBody.velocity = transform.rotation * Vector3.forward * powerShoot;
    }
  }
}
