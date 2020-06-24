using UnityEngine;
using System.Collections;

/// <summary>
/// Les noms des fonctions sont explicites et j'ai la flemme de les commenter
/// </summary>
public static class Vector2Extensions
{

    public static Vector2 Norm (this Vector2 vector)
    {
        float ratio = Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2);
        ratio = Mathf.Sqrt(ratio);
        return new Vector2(vector.x / ratio, vector.y / ratio);
    }

	public static Vector2 withX (this Vector2 vector, float x)
	{
		return new Vector2 (x, vector.y);
	}

	public static Vector2 withY (this Vector2 vector, float y)
	{
		return new Vector2 (vector.x, y);
	}

	public static Vector2 plusX (this Vector2 vector, float plusX)
	{
		return new Vector2 (vector.x + plusX, vector.y);
	}

	public static Vector2 plusY (this Vector2 vector, float plusY)
	{
		return new Vector2 (vector.x, vector.y + plusY);
	}

	public static Vector2 timesX (this Vector2 vector, float timesX)
	{
		return new Vector2 (vector.x * timesX, vector.y);
	}

	public static Vector2 timesY (this Vector2 vector, float timesY)
	{
		return new Vector2 (vector.x, vector.y * timesY);
	}

	public static Vector2 Rotate (this Vector2 vector, float degrees)
	{
		float sin = Mathf.Sin (degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos (degrees * Mathf.Deg2Rad);
		
		float tx = vector.x;
		float ty = vector.y;
		vector.x = (cos * tx) - (sin * ty);
		vector.y = (sin * tx) + (cos * ty);
		return vector;
	}




	public static Vector2 mulComponents (this Vector2 a, Vector2 d)
	{
		return new Vector2 (a.x * d.x, a.y * d.y);
	}


	public static Vector2 addComponents (this Vector2 a, Vector2 d)
	{
		return new Vector2 (a.x + d.x, a.y + d.y);
	}


	public static Vector2 subComponents (this Vector2 a, Vector2 d)
	{
		return new Vector2 (a.x - d.x, a.y - d.y);
	}


	public static Vector2 divComponents (this Vector2 a, Vector2 d)
	{
		return new Vector2 (a.x / d.x, a.y / d.y);
	}



	public static Vector3 ToVector3 (this Vector2 aVector, float zValue = 0.0f)
	{
		return new Vector3 (aVector.x, aVector.y, zValue);
	}




	public static Vector2 NearestPointStrict (Vector2 lineStart, Vector2 lineEnd, Vector2 point)
	{
		var fullDirection = lineEnd - lineStart;
		var lineDirection = fullDirection.normalized;
		var closestPoint = Vector2.Dot ((point - lineStart), lineDirection) / Vector2.Dot (lineDirection, lineDirection);
		return lineStart + (Mathf.Clamp (closestPoint, 0, fullDirection.magnitude) * lineDirection);
	}
}
