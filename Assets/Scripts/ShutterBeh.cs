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
    if (!Input.touchSupported)
    {
      Observable.EveryUpdate()
        .Where(_ => Input.GetMouseButtonDown(0))
        .Select(_ => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)))
        .Subscribe(positon => RotateAndShoot(positon))
        .AddTo(this);
    }
    else
    {
      Observable.EveryUpdate()
        .Where(_ => Input.touchCount > 0)
        .Select(_ => Input.GetTouch(0))
        .Where(touch => touch.phase == TouchPhase.Began)
        .Select(touch => Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane)))
        .Subscribe(positon => RotateAndShoot(positon))
        .AddTo(this);
    }
  }

  private void Shoot()
  {
    if (GameManager.Instance.countBullet <= 0) return;

    var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

    var richBody = bullet.GetComponent<Rigidbody>();
    if (richBody != null)
    {
      richBody.velocity = transform.rotation * Vector3.forward * powerShoot;
    }

    GameManager.Instance.MakedShoot();
  }

  private void RotateAndShoot(Vector3 pos)
  {
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
