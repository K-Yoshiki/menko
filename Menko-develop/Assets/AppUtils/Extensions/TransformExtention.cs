using UnityEngine;

public static class TransformExtention
{
	#region Set position

	/// <summary>
	/// Sets the position x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 SetPosX(this Transform transform, float x)
	{
		Vector3 pos = transform.position;
		pos.x = x;
		return transform.position = pos;
	}

	/// <summary>
	/// Sets the position y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetPosY(this Transform transform, float y)
	{
		Vector3 pos = transform.position;
		pos.y = y;
		return transform.position = pos;
	}

	/// <summary>
	/// Sets the position z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetPosZ(this Transform transform, float z)
	{
		Vector3 pos = transform.position;
		pos.z = z;
		return transform.position = pos;
	}

	/// <summary>
	/// Sets the position X and Y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetPosXY(this Transform transform, float x, float y)
	{
		Vector3 pos = transform.position;
		pos.x = x;
		pos.y = y;
		return transform.position = pos;
	}

	/// <summary>
	/// Sets the position Y and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetPosYZ(this Transform transform, float y, float z)
	{
		Vector3 pos = transform.position;
		pos.y = y;
		pos.z = z;
		return transform.position = pos;
	}

	/// <summary>
	/// Sets the position X and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetPosXZ(this Transform transform, float x, float z)
	{
		Vector3 pos = transform.position;
		pos.x = x;
		pos.z = z;
		return transform.position = pos;
	}

	#endregion


	#region Set local position

	/// <summary>
	/// Sets the local position x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 SetLocalPosX(this Transform transform, float x)
	{
		Vector3 localPos = transform.localPosition;
		localPos.x = x;
		return transform.localPosition = localPos;
	}

	/// <summary>
	/// Sets the local position y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetLocalPosY(this Transform transform, float y)
	{
		Vector3 localPos = transform.localPosition;
		localPos.y = y;
		return transform.localPosition = localPos;
	}

	/// <summary>
	/// Sets the local position z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetLocalPosZ(this Transform transform, float z)
	{
		Vector3 localPos = transform.localPosition;
		localPos.z = z;
		return transform.localPosition = localPos;
	}

	/// <summary>
	/// Sets the local position X and Y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetLocalPosXY(this Transform transform, float x, float y)
	{
		Vector3 pos = transform.localPosition;
		pos.x = x;
		pos.y = y;
		return transform.localPosition = pos;
	}

	/// <summary>
	/// Sets the local position Y and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetLocalPosYZ(this Transform transform, float y, float z)
	{
		Vector3 pos = transform.localPosition;
		pos.y = y;
		pos.z = z;
		return transform.localPosition = pos;
	}

	/// <summary>
	/// Sets the local position X and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetLocalPosXZ(this Transform transform, float x, float z)
	{
		Vector3 pos = transform.localPosition;
		pos.x = x;
		pos.z = z;
		return transform.localPosition = pos;
	}

	#endregion


	#region Add position

	/// <summary>
	/// Adds the position x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 AddPosX(this Transform transform, float x)
	{
		Vector3 pos = new Vector3(x, 0, 0);
		return transform.position += pos;
	}

	/// <summary>
	/// Adds the position y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 AddPosY(this Transform transform, float y)
	{
		Vector3 pos = new Vector3(0, y, 0);
		return transform.position += pos;
	}

	/// <summary>
	/// Adds the position z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 AddPosZ(this Transform transform, float z)
	{
		Vector3 pos = new Vector3(0, 0, z);
		return transform.position += pos;
	}

	#endregion


	#region Add local position

	/// <summary>
	/// Adds the local position x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 AddLocalPosX(this Transform transform, float x)
	{
		Vector3 pos = new Vector3(x, 0, 0);
		return transform.localPosition += pos;
	}

	/// <summary>
	/// Adds the local position y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 AddLocalPosY(this Transform transform, float y)
	{
		Vector3 pos = new Vector3(0, y, 0);
		return transform.localPosition += pos;
	}

	/// <summary>
	/// Adds the local position z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 AddLocalPosZ(this Transform transform, float z)
	{
		Vector3 pos = new Vector3(0, 0, z);
		return transform.localPosition += pos; 
	}

	#endregion


	#region Set rotation

	/// <summary>
	/// Sets the rotation x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 SetRotX(this Transform transform, float x)
	{
		Vector3 rot = transform.eulerAngles;
		rot.x = x;
		return transform.eulerAngles = rot;
	}

	/// <summary>
	/// Sets the rotation y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetRotY(this Transform transform, float y)
	{
		Vector3 rot = transform.eulerAngles;
		rot.y = y;
		return transform.eulerAngles = rot;
	}

	/// <summary>
	/// Sets the rotation z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetRotZ(this Transform transform, float z)
	{
		Vector3 rot = transform.eulerAngles;
		rot.z = z;
		return transform.eulerAngles = rot;
	}

	/// <summary>
	/// Sets the rotation X and Y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetRotXY(this Transform transform, float x, float y)
	{
		Vector3 rot = transform.eulerAngles;
		rot.x = x;
		rot.y = y;
		return transform.eulerAngles = rot;
	}

	/// <summary>
	/// Sets the rotation Y and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetRotYZ(this Transform transform, float y, float z)
	{
		Vector3 rot = transform.eulerAngles;
		rot.y = y;
		rot.z = z;
		return transform.eulerAngles = rot;
	}

	/// <summary>
	/// Sets the rotation X and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetRotXZ(this Transform transform, float x, float z)
	{
		Vector3 rot = transform.eulerAngles;
		rot.x = x;
		rot.z = z;
		return transform.eulerAngles = rot;
	}

	#endregion


	#region Set local rotation

	/// <summary>
	/// Sets the local rotation x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 SetLocalRotX(this Transform transform, float x)
	{
		Vector3 localRot = transform.localEulerAngles;
		localRot.x = x;
		return transform.localEulerAngles = localRot;
	}

	/// <summary>
	/// Sets the local rotation y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetLocalRotY(this Transform transform, float y)
	{
		Vector3 localRot = transform.localEulerAngles;
		localRot.y = y;
		return transform.localEulerAngles = localRot;
	}

	/// <summary>
	/// Sets the local rotation z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetLocalRotZ(this Transform transform, float z)
	{
		Vector3 localRot = transform.localEulerAngles;
		localRot.z = z;
		return transform.localEulerAngles = localRot;
	}

	/// <summary>
	/// Sets the local rotation X and Y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetLocalRotXY(this Transform transform, float x, float y)
	{
		Vector3 localRot = transform.localEulerAngles;
		localRot.x = x;
		localRot.y = y;
		return transform.localEulerAngles = localRot;
	}

	/// <summary>
	/// Sets the local rotation Y and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetLocalRotYZ(this Transform transform, float y, float z)
	{
		Vector3 localRot = transform.localEulerAngles;
		localRot.y = y;
		localRot.z = z;
		return transform.localEulerAngles = localRot;
	}

	/// <summary>
	/// Sets the local rotation X and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetLocalRotXZ(this Transform transform, float x, float z)
	{
		Vector3 localRot = transform.localEulerAngles;
		localRot.x = x;
		localRot.z = z;
		return transform.localEulerAngles = localRot;
	}

	#endregion


	#region Add rotation

	/// <summary>
	/// Adds the rotation x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 AddRotX(this Transform transform, float x)
	{
		Vector3 rot = new Vector3(x, 0, 0);
		return transform.eulerAngles += rot;
	}

	/// <summary>
	/// Adds the rotation y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 AddRotY(this Transform transform, float y)
	{
		Vector3 rot = new Vector3(0, y, 0);
		return transform.eulerAngles += rot;
	}

	/// <summary>
	/// Adds the rotation z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 AddRotZ(this Transform transform, float z)
	{
		Vector3 rot = new Vector3(0, 0, z);
		return transform.eulerAngles += rot;
	}

	#endregion


	#region Add local rotation

	/// <summary>
	/// Adds the local rotation x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 AddLocalRotX(this Transform transform, float x)
	{
		Vector3 rot = new Vector3(x, 0, 0);
		return transform.localEulerAngles += rot;
	}

	/// <summary>
	/// Adds the local rotation y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 AddLocalRotY(this Transform transform, float y)
	{
		Vector3 rot = new Vector3(0, y, 0);
		return transform.localEulerAngles += rot;
	}

	/// <summary>
	/// Adds the local rotation z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 AddLocalRotZ(this Transform transform, float z)
	{
		Vector3 rot = new Vector3(0, 0, z);
		return transform.localEulerAngles += rot;
	}

	#endregion


	#region Set scale

	/// <summary>
	/// Sets the scale x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 SetScaleX(this Transform transform, float x)
	{
		Vector3 scale = transform.localScale;
		scale.x = x;
		return transform.localScale = scale;
	}

	/// <summary>
	/// Sets the scale y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetScaleY(this Transform transform, float y)
	{
		Vector3 scale = transform.localScale;
		scale.y = y;
		return transform.localScale = scale;
	}

	/// <summary>
	/// Sets the scale z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetScaleZ(this Transform transform, float z)
	{
		Vector3 scale = transform.localScale;
		scale.z = z;
		return transform.localScale = scale;
	}

	/// <summary>
	/// Sets the scale X and Y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 SetScaleXY(this Transform transform, float x, float y)
	{
		Vector3 scale = transform.localScale;
		scale.x = x;
		scale.y = y;
		return transform.localScale = scale;
	}

	/// <summary>
	/// Sets the scale Y and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetScaleYZ(this Transform transform, float y, float z)
	{
		Vector3 scale = transform.localScale;
		scale.y = y;
		scale.z = z;
		return transform.localScale = scale;
	}

	/// <summary>
	/// Sets the scale X and Z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 SetScaleXZ(this Transform transform, float x, float z)
	{
		Vector3 scale = transform.localScale;
		scale.x = x;
		scale.z = z;
		return transform.localScale = scale;
	}

	#endregion


	#region Add scale

	/// <summary>
	/// Adds the scale x.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="x">The x coordinate.</param>
	public static Vector3 AddScaleX(this Transform transform, float x)
	{
		Vector3 scale = new Vector3(x, 0, 0);
		return transform.localScale += scale;
	}

	/// <summary>
	/// Adds the scale y.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector3 AddScaleY(this Transform transform, float y)
	{
		Vector3 scale = new Vector3(0, y, 0);
		return transform.localScale += scale;
	}

	/// <summary>
	/// Adds the scale z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 AddScaleZ(this Transform transform, float z)
	{
		Vector3 scale = new Vector3(0, 0, z);
		return transform.localScale += scale;
	}

	#endregion
}