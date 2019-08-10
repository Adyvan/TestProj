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

    Observable.EveryUpdate()
      .Where(_ => Input.GetMouseButtonDown(0))
      .Subscribe(_ => RotateAndShoot())
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

  private void RotateAndShoot()
  {
    var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    var delta_position = transform.position - Camera.main.gameObject.transform.position;
    pos += delta_position;
    var vector = (pos - transform.position).normalized;
    var currentVector = (transform.rotation * Vector3.forward).normalized;

    var axis = Vector3.Cross(vector, currentVector);
    var angle = Vector3.Angle(vector, currentVector);

    transform.rotation *= Quaternion.AngleAxis(angle, axis);

    Shoot();
  }
}
