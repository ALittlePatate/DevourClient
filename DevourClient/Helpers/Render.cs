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
		public static void DrawBones(Transform bone1, Transform bone2, Color c)
		{
			if (!Camera.main) //fix the crash maybe
            {
				return;
            }

			if (!bone1 || !bone2)
            {
				return;
			}
			
			Vector3 w1 = Camera.main.WorldToScreenPoint(bone1.position);
			Vector3 w2 = Camera.main.WorldToScreenPoint(bone2.position);
			if (w1.z > 0.0f && w2.z > 0.0f)
            {
				DrawLine(new Vector2(w1.x, Screen.height - w1.y), new Vector2(w2.x, Screen.height - w2.y), c, 2f);
			}
		}

		public static void DrawAllBones(List<Transform> b, Color c)
		{
			DrawBones(b[0], b[1], c);
			DrawBones(b[1], b[2], c);
			DrawBones(b[2], b[3], c);

			DrawBones(b[1], b[4], c);
			DrawBones(b[4], b[5], c);
			DrawBones(b[5], b[6], c);
			DrawBones(b[6], b[7], c);

			DrawBones(b[1], b[8], c);
			DrawBones(b[8], b[9], c);
			DrawBones(b[9], b[10], c);
			DrawBones(b[10], b[11], c);

			DrawBones(b[3], b[12], c);
			DrawBones(b[12], b[13], c);
			DrawBones(b[13], b[14], c);

			DrawBones(b[3], b[15], c);
			DrawBones(b[15], b[16], c);
			DrawBones(b[16], b[17], c);
		}

		static void DrawBox(float x, float y, float w, float h, Color color, float thickness)
		{
			Render.DrawLine(new Vector2(x, y), new Vector2(x + w, y), color, thickness);
			Render.DrawLine(new Vector2(x, y), new Vector2(x, y + h), color, thickness);
			Render.DrawLine(new Vector2(x + w, y), new Vector2(x + w, y + h), color, thickness);
			Render.DrawLine(new Vector2(x, y + h), new Vector2(x + w, y + h), color, thickness);
		}

		public static void DrawBoxESP(GameObject it, float footOffset, float headOffset, string name, Color color, bool snapline = false, bool esp = false, float nameOffset = -0.5f, float widthOffset = 2.0f)
        {
			if (!it || !Camera.main)
            {
				return;
            }

			Vector3 footpos = Camera.main.WorldToScreenPoint(new Vector3(it.transform.position.x, it.transform.position.y + footOffset, it.transform.position.z));
			Vector3 headpos = Camera.main.WorldToScreenPoint(new Vector3(it.transform.position.x, it.transform.position.y + headOffset, it.transform.position.z));
			Vector3 namepos = Camera.main.WorldToScreenPoint(new Vector3(it.transform.position.x, it.transform.position.y + nameOffset, it.transform.position.z));

			if (esp && footpos.z > 0.0f)
			{
				float height = (headpos.y - footpos.y);
				float width = height / widthOffset;

				DrawBox(footpos.x - (width / 2), (float)Screen.height - footpos.y - height, width, height, color, 2.0f);
				DrawString(new Vector2(namepos.x, (float)Screen.height - namepos.y), color, name);
			}

			if (snapline && footpos.z > 0f)
			{
				Render.DrawLine(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), new Vector2(footpos.x, (float)Screen.height - footpos.y), color, 2f);
			}
		}
	}
}
