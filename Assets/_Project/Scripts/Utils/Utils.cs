using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MathUtils
{
    public static class Utils 
    {
        public static Vector2 GetRotated(this Vector2 vector, float degrees)
		{
			if (degrees == 0)
				return vector;
			if (vector.magnitude >= 0.99 && vector.magnitude <= 1.01f)
				return (Quaternion.Euler(0, 0, -degrees) * vector).normalized;
			return Quaternion.Euler(0, 0, -degrees) * vector;
		}

		public static Vector2 GetVector(int index, float degreesStep) =>
			GetVector(index * degreesStep);
		public static Vector2 GetVector(float degrees) =>
			Vector2.up.GetRotated(degrees);

		public static void Shuffle<T>(this T[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					var r = Random.Range(0, array.Length);
					var t = array[r];
					array[r] = array[i];
					array[i] = t;
				}
			}
		}
	}
}