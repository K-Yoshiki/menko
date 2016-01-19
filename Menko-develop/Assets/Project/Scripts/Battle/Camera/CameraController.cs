using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] Camera _camera;
	Transform _transform;
	Transform _target;
	Vector3 _defPos;

	public void SetTarget(Transform target)
	{
		_target = target;
	}

	public void ResetTarget()
	{
		_target = null;
	}

	public Ray ScreenPointToRay(Vector3 pos)
	{
		return _camera.ScreenPointToRay(pos);
	}

	void Awake()
	{
		_transform = this.transform;
		_defPos = _transform.position;
	}

	void Update()
	{
		if (_target == null)
		{
			UpdatePos(_defPos);
		}
		else
		{
			UpdatePos(_target.position);
		}
	}

	void UpdatePos(Vector3 _pos)
	{
		if (_pos == _transform.position)
			return;

		var dirPos = _pos - _transform.position;
		_transform.position += dirPos * 0.25f;
	}
}