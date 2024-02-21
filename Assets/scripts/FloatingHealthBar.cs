using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] Vector3 offset;

    [SerializeField] Transform target;
    private Slider slider;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        transform.SetPositionAndRotation(target.position + (offset * target.localScale.magnitude), _camera.transform.rotation);
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
