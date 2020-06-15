﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200058F RID: 1423
	public class ObjImporter
	{
		// Token: 0x06002668 RID: 9832 RVA: 0x001A9F7C File Offset: 0x001A817C
		public static Mesh ImportFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				Debug.LogError("No file was found at '" + filePath + "'");
				return null;
			}
			ObjImporter.meshStruct meshStruct = ObjImporter.createMeshStruct(filePath);
			ObjImporter.populateMeshStruct(ref meshStruct);
			Vector3[] array = new Vector3[meshStruct.faceData.Length];
			Vector2[] array2 = new Vector2[meshStruct.faceData.Length];
			Vector3[] array3 = new Vector3[meshStruct.faceData.Length];
			int num = 0;
			foreach (Vector3 vector in meshStruct.faceData)
			{
				array[num] = meshStruct.vertices[(int)vector.x - 1];
				if (vector.y >= 1f)
				{
					array2[num] = meshStruct.uv[(int)vector.y - 1];
				}
				if (vector.z >= 1f)
				{
					array3[num] = meshStruct.normals[(int)vector.z - 1];
				}
				num++;
			}
			Mesh mesh = new Mesh();
			mesh.vertices = array;
			mesh.uv = array2;
			mesh.normals = array3;
			mesh.triangles = meshStruct.triangles;
			mesh.RecalculateBounds();
			return mesh;
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x001AA0B4 File Offset: 0x001A82B4
		private static ObjImporter.meshStruct createMeshStruct(string filename)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			ObjImporter.meshStruct result = default(ObjImporter.meshStruct);
			result.fileName = filename;
			StreamReader streamReader = File.OpenText(filename);
			string s = streamReader.ReadToEnd();
			streamReader.Dispose();
			using (StringReader stringReader = new StringReader(s))
			{
				string text = stringReader.ReadLine();
				char[] separator = new char[]
				{
					' '
				};
				while (text != null)
				{
					if (!text.StartsWith("f ") && !text.StartsWith("v ") && !text.StartsWith("vt ") && !text.StartsWith("vn "))
					{
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
					else
					{
						text = text.Trim();
						string[] array = text.Split(separator, 50);
						string a = array[0];
						if (!(a == "v"))
						{
							if (!(a == "vt"))
							{
								if (!(a == "vn"))
								{
									if (a == "f")
									{
										num5 = num5 + array.Length - 1;
										num += 3 * (array.Length - 2);
									}
								}
								else
								{
									num4++;
								}
							}
							else
							{
								num3++;
							}
						}
						else
						{
							num2++;
						}
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
				}
			}
			result.triangles = new int[num];
			result.vertices = new Vector3[num2];
			result.uv = new Vector2[num3];
			result.normals = new Vector3[num4];
			result.faceData = new Vector3[num5];
			return result;
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x001AA288 File Offset: 0x001A8488
		private static void populateMeshStruct(ref ObjImporter.meshStruct mesh)
		{
			StreamReader streamReader = File.OpenText(mesh.fileName);
			string s = streamReader.ReadToEnd();
			streamReader.Close();
			using (StringReader stringReader = new StringReader(s))
			{
				string text = stringReader.ReadLine();
				char[] separator = new char[]
				{
					' '
				};
				char[] separator2 = new char[]
				{
					'/'
				};
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				while (text != null)
				{
					if (!text.StartsWith("f ") && !text.StartsWith("v ") && !text.StartsWith("vt ") && !text.StartsWith("vn ") && !text.StartsWith("g ") && !text.StartsWith("usemtl ") && !text.StartsWith("mtllib ") && !text.StartsWith("vt1 ") && !text.StartsWith("vt2 ") && !text.StartsWith("vc ") && !text.StartsWith("usemap "))
					{
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
					else
					{
						text = text.Trim();
						string[] array = text.Split(separator, 50);
						string text2 = array[0];
						uint num8 = <PrivateImplementationDetails>.ComputeStringHash(text2);
						if (num8 <= 1179241374U)
						{
							if (num8 <= 1128908517U)
							{
								if (num8 != 990293175U)
								{
									if (num8 == 1128908517U)
									{
										if (text2 == "vn")
										{
											mesh.normals[num4] = new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
											num4++;
										}
									}
								}
								else if (!(text2 == "mtllib"))
								{
								}
							}
							else if (num8 != 1146808303U)
							{
								if (num8 != 1163585922U)
								{
									if (num8 == 1179241374U)
									{
										if (!(text2 == "vc"))
										{
										}
									}
								}
								else if (text2 == "vt1")
								{
									mesh.uv[num6] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
									num6++;
								}
							}
							else if (text2 == "vt2")
							{
								mesh.uv[num7] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
								num7++;
							}
						}
						else if (num8 <= 1498016135U)
						{
							if (num8 != 1297068826U)
							{
								if (num8 != 1328799683U)
								{
									if (num8 == 1498016135U)
									{
										if (text2 == "vt")
										{
											mesh.uv[num5] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
											num5++;
										}
									}
								}
								else if (!(text2 == "usemtl"))
								{
								}
							}
							else if (!(text2 == "usemap"))
							{
							}
						}
						else if (num8 != 3792446982U)
						{
							if (num8 != 3809224601U)
							{
								if (num8 == 4077666505U)
								{
									if (text2 == "v")
									{
										mesh.vertices[num3] = new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
										num3++;
									}
								}
							}
							else if (text2 == "f")
							{
								int num9 = 1;
								List<int> list = new List<int>();
								while (num9 < array.Length && (array[num9] ?? "").Length > 0)
								{
									Vector3 vector = default(Vector3);
									string[] array2 = array[num9].Split(separator2, 3);
									vector.x = (float)Convert.ToInt32(array2[0]);
									if (array2.Length > 1)
									{
										if (array2[1] != "")
										{
											vector.y = (float)Convert.ToInt32(array2[1]);
										}
										vector.z = (float)Convert.ToInt32(array2[2]);
									}
									num9++;
									mesh.faceData[num2] = vector;
									list.Add(num2);
									num2++;
								}
								num9 = 1;
								while (num9 + 2 < array.Length)
								{
									mesh.triangles[num] = list[0];
									num++;
									mesh.triangles[num] = list[num9];
									num++;
									mesh.triangles[num] = list[num9 + 1];
									num++;
									num9++;
								}
							}
						}
						else if (!(text2 == "g"))
						{
						}
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
				}
			}
		}

		// Token: 0x02000763 RID: 1891
		private struct meshStruct
		{
			// Token: 0x04004A99 RID: 19097
			public Vector3[] vertices;

			// Token: 0x04004A9A RID: 19098
			public Vector3[] normals;

			// Token: 0x04004A9B RID: 19099
			public Vector2[] uv;

			// Token: 0x04004A9C RID: 19100
			public Vector2[] uv1;

			// Token: 0x04004A9D RID: 19101
			public Vector2[] uv2;

			// Token: 0x04004A9E RID: 19102
			public int[] triangles;

			// Token: 0x04004A9F RID: 19103
			public int[] faceVerts;

			// Token: 0x04004AA0 RID: 19104
			public int[] faceUVs;

			// Token: 0x04004AA1 RID: 19105
			public Vector3[] faceData;

			// Token: 0x04004AA2 RID: 19106
			public string name;

			// Token: 0x04004AA3 RID: 19107
			public string fileName;
		}
	}
}
