using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("alreadyPositioned", "number", "position", "fixedNumber")]
	public class ES3Type_CubeSide : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3Type_CubeSide() : base(typeof(CubeSide))
		{
			Instance = this;
		}

		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (CubeSide)obj;
			
			writer.WriteProperty("alreadyPositioned", instance.alreadyPositioned, ES3Type_bool.Instance);
			writer.WriteProperty("number", instance.number, ES3Type_int.Instance);
			writer.WriteProperty("position", instance.position, ES3Type_Vector2.Instance);
			writer.WriteProperty("fixedNumber", instance.fixedNumber, ES3Type_bool.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (CubeSide)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "alreadyPositioned":
						instance.alreadyPositioned = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "number":
						instance.number = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "position":
						instance.position = reader.Read<UnityEngine.Vector2>(ES3Type_Vector2.Instance);
						break;
					case "fixedNumber":
						instance.fixedNumber = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}

	public class ES3Type_CubeSideArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_CubeSideArray() : base(typeof(CubeSide[]), ES3Type_CubeSide.Instance)
		{
			Instance = this;
		}
	}
}