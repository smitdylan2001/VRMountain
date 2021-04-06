using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform _target;

	private void Awake()
	{
        transform.position = _target.position;
	}
	void Update()
    {
        transform.position = _target.position;
    }
}
