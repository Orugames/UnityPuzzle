using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("color", "position", "rotation")]
	public class ES3Type_Cube : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3Type_Cube() : base(typeof(Cube))
		{
			Instance = this;
		}

		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Cube)obj;
			
			writer.WriteProperty("color", instance.color, ES3Type_Color.Instance);
			writer.WriteProperty("position", instance.position, ES3Type_Vector2.Instance);
			writer.WriteProperty("rotation", instance.rotation, ES3Type_Quaternion.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Cube)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "color":
						instance.color = reader.Read<UnityEngine.Color>(ES3Type_Color.Instance);
						break;
					case "position":
						instance.position = reader.Read<UnityEngine.Vector2>(ES3Type_Vector2.Instance);
						break;
					case "rotation":
						instance.rotation = reader.Read<UnityEngine.Quaternion>(ES3Type_Quaternion.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}

	public class ES3Type_CubeArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_CubeArray() : base(typeof(Cube[]), ES3Type_Cube.Instance)
		{
			Instance = this;
		}
	}
}