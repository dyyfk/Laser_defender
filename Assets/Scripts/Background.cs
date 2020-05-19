using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
  [SerializeField] float backgroundFloatSpeed = 0.2f;
  Material material;
  Vector2 offSet;
  // Start is called before the first frame update
  void Start()
  {
    material = gameObject.GetComponent<Renderer>().material;
    offSet = new Vector2(0f, backgroundFloatSpeed);
  }

  // Update is called once per frame
  void Update()
  {
    material.mainTextureOffset += offSet * backgroundFloatSpeed * Time.deltaTime;
  }
}
