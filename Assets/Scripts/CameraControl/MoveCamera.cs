using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class MoveCamera : MonoBehaviour
    {
        [SerializeField, Range(0.1f, 60)] private float _movespeedCamera = 20;
        [SerializeField, Range(0.01f, 1)] private float _speedScale = 0.1f;
        [SerializeField] private float _minValueX = 0f;
        [SerializeField] private float _maxValueX;
        [SerializeField] private float _minValueY = 2f;
        [SerializeField] private float _maxValueY = 5f;
        [SerializeField] private float _minValueZ = -2f;
        [SerializeField] private float _maxValueZ;
        private CameraController _cameraController;
        [SerializeField] private GameObject _ground;

        private void Awake()
        {
            _cameraController = new CameraController();
            _maxValueX = (_ground.transform.position.x * 2) / _ground.transform.localScale.x;
            _maxValueZ = (_ground.transform.position.z * 2) / _ground.transform.localScale.z;
        }

        private void OnEnable()
        {
            _cameraController.Controller.Enable();
        }
        private void Update()
        {
            Move();
            Scale();

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minValueX, _maxValueX), Mathf.Clamp(transform.position.y, _minValueY, _maxValueY),Mathf.Clamp(transform.position.z, _minValueZ, _maxValueZ));
        }
        private void Move()
        {
            var moveCamera = _cameraController.Controller.MoveCamera.ReadValue<Vector2>();
            transform.position += transform.TransformDirection(moveCamera.x, 0, moveCamera.y) * _movespeedCamera * Time.deltaTime;
        }
        private void Scale()
        {
            var scale = _cameraController.Controller.Scale.ReadValue<Vector2>();
            transform.position += transform.TransformDirection(0, scale.y, 0)* _speedScale * Time.deltaTime;
        }

    }
}

