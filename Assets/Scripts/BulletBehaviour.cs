using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BulletBehaviour : MonoBehaviour
{
    void Start()
    {
      //TODO create buffer for that, do not destroy
      Destroy(gameObject, 3);
    }
}
