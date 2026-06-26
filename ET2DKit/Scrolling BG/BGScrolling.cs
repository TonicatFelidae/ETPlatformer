using System;
using UnityEngine;


public class BGScrolling : MonoBehaviour
{

	//use with mesh renderer or anything that have lengh equivate to size
	public float scrollSpeed;
	float tileSizeZ = 3;
	public enum ScroolType
	{
		up, down, left, right
	}
	[SerializeField] private ScroolType scrollType;
	Vector3 startPosition;

	void OnEnable()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		Scrolling();
	}
	void Scrolling()
	{
		Vector3 VectorDir = GetVectorDir(scrollType);
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + VectorDir * newPosition;
	}
	Vector3 GetVectorDir(ScroolType type)
	{
		switch (type)
		{
			case ScroolType.up:
				return Vector3.forward;
			case ScroolType.down:
				return Vector3.back;
			case ScroolType.left:
				return Vector3.left;
			case ScroolType.right:
				return Vector3.right;
		}
		return Vector3.forward;
	}

}