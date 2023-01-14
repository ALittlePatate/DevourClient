using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;

namespace DevourClient.Render
{
	public class Render
	{
		public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

		public static Color Color
		{
			get { return GUI.color; }
			set { GUI.color = value; }
		}

		public static void DrawString(Vector2 position, Color color, string label, bool centered = true)
		{
			var content = new GUIContent(label);
			var size = StringStyle.CalcSize(content);
			var upperLeft = centered ? position - size / 2f : position;
			Color color2 = GUI.color;
			GUI.color = color;
			GUI.Label(new Rect(upperLeft, size), content);
			GUI.color = color2;
		}

		public static Texture2D lineTex = default!;
		public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
		{
			Matrix4x4 matrix = GUI.matrix;
			if (!lineTex)
				lineTex = new Texture2D(1, 1);

			Color color2 = GUI.color;
			GUI.color = color;
			float num = Vector3.Angle(pointB - pointA, Vector2.right);

			if (pointA.y > pointB.y)
				num = -num;

			GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
			GUIUtility.RotateAroundPivot(num, pointA);
			GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), lineTex);
			GUI.matrix = matrix;
			GUI.color = color2;
		}

		public static void DrawNameESP(Vector3 pos, string name, Color color)
        {
			if (Camera.main == null)
			{
				return;
			}

			Vector3 vector = Camera.main.WorldToScreenPoint(pos);
			if (vector.z > 0f)
			{
				vector.y = (float)Screen.height - (vector.y + 1f);
				GUI.color = color;
				GUI.DrawTexture(new Rect(new Vector2(vector.x, vector.y), new Vector2(5f, 5f)), Texture2D.whiteTexture, 0);
				GUI.Label(new Rect(new Vector2(vector.x, vector.y), new Vector2(100f, 100f)), name);
				GUI.color = Color.white;
			}
		}

		public static void RectFilled(float x, float y, float width, float height, Texture2D text)
		{
			GUI.DrawTexture(new Rect(x, y, width, height), text);
		}

		public static void RectOutlined(float x, float y, float width, float height, Texture2D text, float thickness = 1f)
		{
			RectFilled(x, y, thickness, height, text);
			RectFilled(x + width - thickness, y, thickness, height, text);
			RectFilled(x + thickness, y, width - thickness * 2f, thickness, text);
			RectFilled(x + thickness, y + height - thickness, width - thickness * 2f, thickness, text);
		}

		public static void Box(float x, float y, float width, float height, Texture2D text, float thickness = 1f)
		{
			RectOutlined(x - width / 2f, y - height, width, height, text, thickness);
		}

		public static Texture2D texture2 = new Texture2D(2, 2, TextureFormat.ARGB32, false);
		public static void DrawBoxESP(Vector3 pos, Transform child, Color color, string name, bool snapline = false, bool esp = false)
		{
			if (esp)
            {
				//ESP BOX
				if (texture2 == null) //this should fix the UI disappearing & game crash 
				{
					texture2 = new Texture2D(2, 2, TextureFormat.ARGB32, false);
				}

				texture2.SetPixel(0, 0, color);
				texture2.SetPixel(1, 0, color);
				texture2.SetPixel(0, 1, color);
				texture2.SetPixel(1, 1, color);
				texture2.Apply();


				child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + 2.0f, child.transform.position.z);

				Vector3 w2s = Camera.main.WorldToScreenPoint(pos);
				Vector3 w2s2 = Camera.main.WorldToScreenPoint(child.position);
				float num = Mathf.Abs(w2s.y - w2s2.y);
				Box(w2s.x, Screen.height - w2s.y, num / 1.8f, num, texture2, 1f);

				DrawString(new Vector2(w2s.x, Screen.height - w2s2.y + child.transform.position.y - 10.0f), color, name);
			}


			// Snapline
			if (snapline)
			{
				Vector3 pivotPos = pos;
				Vector3 playerFootPos; playerFootPos.x = pivotPos.x; playerFootPos.z = pivotPos.z; playerFootPos.y = pivotPos.y; //At the feet
				Vector3 w2s_footpos = Camera.main.WorldToScreenPoint(playerFootPos);

				if (w2s_footpos.z > 0f)
				{
					Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(w2s_footpos.x, (float)Screen.height - w2s_footpos.y), color, 2f);
				}
			}
		}
	}
}
