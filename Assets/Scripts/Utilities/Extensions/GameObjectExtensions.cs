using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class GameObjectExtensions
{
    /// <summary>
    /// Informe si l'objet possède un rb ou pas
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
	public static bool HasRigidbody (this GameObject go)
	{
		return (go.GetComponent<Rigidbody> () != null);
	}

    /// <summary>
    /// Informe si l'objet possède une animation ou pas
    /// </summary>
    /// <param name="gobj"></param>
    /// <returns></returns>
	public static bool HasAnimation (this GameObject gobj)
	{
		return (gobj.GetComponent<Animation> () != null);
	}


	/// <summary>
    /// Change le layer du gameObject et de tous ses enfants
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="layer"></param>
	public static void SetLayerRecursively (this GameObject gameObject, int layer)
	{
		gameObject.layer = layer;
		foreach (Transform t in gameObject.transform)
			t.gameObject.SetLayerRecursively (layer);
	}

	/// <summary>
    /// Active les colliders du gameObject et de tous ses enfants
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="tf"></param>
	public static void SetCollisionRecursively (this GameObject gameObject, bool tf)
	{
		Collider[] colliders = gameObject.GetComponentsInChildren<Collider> ();
		foreach (Collider collider in colliders)
			collider.enabled = tf;
	}


	/// <summary>
    /// Renvoie tous les components souhaités des enfants du gameObject ayant un tag spécifique
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObject"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
	public static T[] GetComponentsInChildrenWithTag<T> (this GameObject gameObject, string tag) where T: Component
	{
		List<T> results = new List<T> ();

		if (gameObject.CompareTag (tag))
			results.Add (gameObject.GetComponent<T> ());

		foreach (Transform t in gameObject.transform)
			results.AddRange (t.gameObject.GetComponentsInChildrenWithTag<T> (tag));

		return results.ToArray ();
	}

	/// <summary>
	/// Counts the components of type T in children with tag.
	/// </summary>
	/// <returns>The components in children with tag.</returns>
	/// <param name="gameObject">Game object.</param>
	/// <param name="tag">Tag.</param>
	/// <typeparam name="T">The type of component.</typeparam>
	public static int CountComponentsInChildrenWithTag<T> (this GameObject gameObject, string tag)
        where T: Component
	{
		int compteur = 0;

		foreach (Transform t in gameObject.transform)
		{
			if (t.gameObject.GetComponent<T> () != null && t.gameObject.CompareTag (tag))
				compteur++;

			compteur += t.gameObject.CountComponentsInChildrenWithTag<T> (tag);

		}
		return compteur;
	}


	/// <summary>
	/// Get one Component In Parents
	/// </summary>
	/// <returns>The component in parents.</returns>
	/// <param name="gameObject">Game object.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T GetComponentInParents<T> (this GameObject gameObject)
        where T : Component
	{
		for (Transform t = gameObject.transform; t != null; t = t.parent)
		{
			T result = t.GetComponent<T> ();
			if (result != null)
				return result;
		}

		return null;
	}

	/// <summary>
	/// Gets the components in parents.
	/// </summary>
	/// <returns>The components in parents.</returns>
	/// <param name="gameObject">Game object.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T[] GetComponentsInParents<T> (this GameObject gameObject)
        where T: Component
	{
		List<T> results = new List<T> ();
		for (Transform t = gameObject.transform; t != null; t = t.parent)
		{
			T result = t.GetComponent<T> ();
			if (result != null)
				results.Add (result);
		}

		return results.ToArray ();
	}

	/// <summary>
	/// Get An Object’s Collision Mask
	/// </summary>
	/// <returns>The collision mask.</returns>
	/// <param name="gameObject">Game object.</param>
	/// <param name="layer">Layer.</param>
	public static int GetCollisionMask (this GameObject gameObject, int layer = -1)
	{
		if (layer == -1)
			layer = gameObject.layer;

		int mask = 0;
		for (int i = 0; i < 32; i++)
			mask |= (Physics.GetIgnoreLayerCollision (layer, i) ? 0 : 1) << i;

		return mask;
	}

	/// <summary>
	/// Gets the child with tag.
	/// </summary>
	/// <returns>The child with tag.</returns>
	/// <param name="self">Self.</param>
	/// <param name="tag">Tag.</param>
	public static GameObject GetChildWithTag (this GameObject self, string tag)
	{
		foreach (Transform t in self.transform)
		{
			if (t.gameObject.CompareTag (tag))
			{
				return t.gameObject;
			}
			GameObject temp = t.gameObject.GetChildWithTag (tag);
			if (temp != null)
				return temp;
		}

		return null;
	}


	/// <summary>
	/// Gets the child with tag.
	/// </summary>
	/// <returns>The child with tag.</returns>
	/// <param name="self">Self.</param>
	/// <param name="tag">Tag.</param>
	public static GameObject GetChildNamed (this GameObject self, string name)
	{
		foreach (Transform t in self.transform)
		{
			if (t.gameObject.name == name)
			{
				return t.gameObject;
			}
			GameObject temp = t.gameObject.GetChildNamed (name);
			if (temp != null)
				return temp;
		}

		return null;
	}


	/// <summary>
	/// Gets the children with tag.
	/// </summary>
	/// <returns>The children with tag.</returns>
	/// <param name="self">Self.</param>
	/// <param name="tag">Tag.</param>
	public static GameObject[] GetChildrenWithTag (this GameObject self, string tag)
	{
		List<GameObject> results = new List<GameObject> ();

		foreach (Transform t in self.transform)
		{
			if (t.gameObject.CompareTag (tag))
			{
				results.Add (t.gameObject);
			}
			results.AddRange (t.gameObject.GetChildrenWithTag (tag));
		}

		return results.ToArray ();
	}


    /// <summary>
    /// Renvoie les enfants du gameObject ayant un certain nom
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <returns></returns>
	public static GameObject[] GetChildrenNamed (this GameObject self, string name)
	{
		List<GameObject> results = new List<GameObject> ();

		foreach (Transform t in self.transform)
		{
			if (t.gameObject.name == name)
			{
				results.Add (t.gameObject);
			}
			results.AddRange (t.gameObject.GetChildrenNamed (name));
		}

		return results.ToArray ();
	}

	/// <summary>
	/// Gets the parent with tag.
	/// </summary>
	/// <returns>The parent with tag.</returns>
	/// <param name="self">Self.</param>
	/// <param name="tag">Tag.</param>
	public static GameObject GetParentWithTag (this GameObject self, string tag)
	{
		Transform t = self.transform;
		for (; t != null; t = t.parent)
		{
			if (t.CompareTag (tag))
			{
				return t.gameObject;
			}
		}

		return null;
	}


    /// <summary>
    /// Gets the parent with name.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="name"></param>
    /// <returns></returns>
	public static GameObject GetParentWithName (this GameObject self, string name)
	{
		Transform t = self.transform;
		for (; t != null; t = t.parent)
		{
			if (t.name == name)
			{
				return t.gameObject;
			}
		}

		return null;
	}

	/// <summary>
	/// is the object's layer in the specified layermask
	/// </summary>
	/// <param name="gameObject"></param>
	/// <param name="mask"></param>
	/// <returns></returns>
	public static bool IsInLayerMask (this GameObject gameObject, LayerMask mask)
	{
		return ((mask.value & (1 << gameObject.layer)) > 0);
	}

	/// <summary>
	/// Returns all monobehaviours that are of type T, as T. Works for interfaces
	/// </summary>
	/// <typeparam name="T">class type</typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T[] GetClasses<T> (this GameObject gObj) where T : class
	{
		var ts = gObj.GetComponents (typeof(T));

		var ret = new T[ts.Length];
		for (int i = 0; i < ts.Length; i++)
		{
			ret [i] = ts [i] as T;
		}
		return ret;
	}

	/// <summary>
	/// Returns all classes of type T (casted to T)
	/// works with interfaces
	/// </summary>
	/// <typeparam name="T">interface type</typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T[] GetClasses<T> (this Transform gObj) where T : class
	{
		return gObj.gameObject.GetClasses<T> ();
	}

	/// <summary>
	/// Returns the first monobehaviour that is of the class Type, as T
	/// works with interfaces
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T GetClass<T> (this GameObject gObj) where T : class
	{
		return gObj.GetComponent (typeof(T)) as T;
	}

	/// <summary>
	/// Gets all monobehaviours in children that implement the class of type T (casted to T)
	/// works with interfaces
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T[] GetClassesInChildren<T> (this GameObject gObj) where T : class
	{
		var ts = gObj.GetComponentsInChildren (typeof(T));

		var ret = new T[ts.Length];
		for (int i = 0; i < ts.Length; i++)
		{
			ret [i] = ts [i] as T;
		}
		return ret;
	}

	/// <summary>
	/// 
	/// Returns the first instance of the monobehaviour that is of the class type T (casted to T)
	/// works with interfaces
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T GetClassInChildren<T> (this GameObject gObj) where T : class
	{
		return gObj.GetComponentInChildren (typeof(T)) as T;
	}

	/// <summary>
	/// executes message with the component of type TI if it exists in gameobject's heirarchy. this executes on all behaviours that implement TI.
	/// parm is included in the action, to help reduce closures
	/// </summary>
	/// <typeparam name="TI">component type to get</typeparam>
	/// <typeparam name="TParm">type of the parameter to pass into the message</typeparam>
	/// <param name="gobj"></param>
	/// <param name="message">action to run on each component that matches TI</param>
	/// <param name="parm">the object to pass into the message. this reduces closures.</param>
	public static void DoMessage<TI, TParm> (this GameObject gobj, System.Action<TI, TParm> message, TParm parm) where TI : class
	{
		var ts = gobj.GetComponentsInChildren (typeof(TI));
		for (int i = 0; i < ts.Length; i++)
		{
			var comp = ts [i] as TI;
			if (comp != null)
			{
				message (comp, parm);
			}
		}
	}


	/// <summary>
	/// executes message with the component of type TI if it exists in gameobject's heirarchy. this executes for all behaviours that implement TI.
	/// It is recommended that you use the other DoMessage if you need to pass a variable into the message, to reduce garbage pressure due to lambdas.
	/// </summary>
	/// <typeparam name="TI"></typeparam>
	/// <param name="gobj"></param>
	/// <param name="message"></param>
	public static void DoMessage<TI> (this GameObject gobj, System.Action<TI> message) where TI : class
	{
		var ts = gobj.GetComponentsInChildren (typeof(TI));
		for (int i = 0; i < ts.Length; i++)
		{
			var comp = ts [i] as TI;
			if (comp != null)
			{
				message (comp);
			}
		}
	}

}