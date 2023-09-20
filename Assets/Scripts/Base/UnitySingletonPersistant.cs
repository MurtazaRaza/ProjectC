using UnityEngine;

namespace Utils.BaseClasses
{
	/// <summary>
	/// Persistent Version of the Singleton Class.V1.0
	/// </summary>
	/// <typeparam name="T">Pass the name of this class so a proper typed static instance is created</typeparam>
	public class UnitySingletonPersistent<T> : MonoBehaviour where T : Component
	{
		private static T instance;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();
					if (instance == null)
					{
						var obj = new GameObject {hideFlags = HideFlags.HideAndDontSave};
						instance = obj.AddComponent<T>();
					}
				}

				return instance;
			}
		}

		public virtual void Awake()
		{
			DontDestroyOnLoad(gameObject);
			if (instance == null)
			{
				instance = this as T;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}