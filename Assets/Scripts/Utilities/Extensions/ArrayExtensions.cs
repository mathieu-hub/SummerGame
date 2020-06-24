using UnityEngine;
using System;
using System.Collections.Generic;

public static class ArrayExtensions
{
	/// <summary>
    /// Permet de return un objet aléatoire d'un array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
	public static T RandomItem<T> (this T[] array)
	{
		return array [UnityEngine.Random.Range (0, array.Length)];
	}

    /// <summary>
    /// Permet d'appliquer une fonction à chaque élément d'un array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="action"></param>
    /// <returns></returns>
	public static T[] ForEach<T> (this T[] array, Action<T> action)
	{
		for (int i = 0; i < array.Length; i++)
		{
			action (array [i]);
		}
		return array;
	}

	/// <summary>
	/// Return the next index of the array, if index is out of bounds, return 0.
	/// </summary>
	/// <returns>The index.</returns>
	/// <param name="array">Array.</param>
	/// <param name="actualIndex">Actual index.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static int NextIndex<T> (this T[] array, int actualIndex)
	{
		actualIndex++;
		return actualIndex < array.Length ? actualIndex : 0;
	}

	
	/// <summary>
    /// Permet de transférer tous les éléments d'un array dans une liste
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <returns></returns>
	public static List<T> ToList<T> (this T[] array)
	{
		
		return new List<T> (array);
	}


    /// <summary>
    /// Permet de supprimer un certain nombre d'éléments d'un array entre deux bornes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
	public static T[] RemoveRange<T> (this T[] array, int index, int count)
	{
		if (count < 0)
			throw new ArgumentOutOfRangeException ("count", " is out of range");
		if (index < 0 || index > array.Length - 1)
			throw new ArgumentOutOfRangeException ("index", " is out of range");

		if (array.Length - count - index < 0)
			throw new ArgumentException ("index and count do not denote a valid range of elements in the array", "");

		var newArray = new T[array.Length - count];

		for (int i = 0, ni = 0; i < array.Length; i++)
		{
			if (i < index || i >= index + count)
			{
				newArray [ni] = array [i];
				ni++;
			}
		}

		return newArray;
	}
}