using UnityEngine;
using System.Collections;

public static class ValueToVector3
{
    /// <summary>
    /// Créé un vecteur3 avec la valeur renseignée
    /// </summary>
    /// <param name="value"></param>
    /// <param name="axe"></param>
    /// <returns></returns>
	public static Vector3 ToVector3(float value, Axis3D axe) 
	{
		Vector3 temp = Vector3.zero;
		switch (axe) {
		case Axis3D.x:
			temp.x = value;
			break;
		case Axis3D.y:
			temp.y = value;
			break;
		case Axis3D.z:
			temp.z = value;
			break;
		}
		return temp;
	}

    /// <summary>
    /// Créé un vecteur3 avec la valeur renseignée
    /// </summary>
    /// <param name="value"></param>
    /// <param name="axe"></param>
    /// <returns></returns>
	public static Vector3 ToVector3(int value, Axis3D axe) 
	{
		return ToVector3 ((float)value, axe);
	}
}

