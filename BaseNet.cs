using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Redemption
{
	public class BaseNet
	{
		public static void SendData(int dataType, int dataA, int dataB, string text, int playerID, float dataC, float dataD, float dataE, int clientType)
		{
			NetMessage.SendData(dataType, dataA, dataB, NetworkText.FromLiteral(text), playerID, dataC, dataD, dataE, clientType, 0, 0);
		}

		public static ModPacket WriteToPacket(ModPacket packet, byte msg, params object[] param)
		{
			packet.Write(msg);
			foreach (object obj in param)
			{
				if (obj is byte[])
				{
					byte[] array = (byte[])obj;
					foreach (byte value in array)
					{
						packet.Write(value);
					}
				}
				else if (obj is bool)
				{
					packet.Write((bool)obj);
				}
				else if (obj is byte)
				{
					packet.Write((byte)obj);
				}
				else if (obj is short)
				{
					packet.Write((short)obj);
				}
				else if (obj is int)
				{
					packet.Write((int)obj);
				}
				else if (obj is float)
				{
					packet.Write((float)obj);
				}
			}
			return packet;
		}

		public static void SyncAI(Entity codable, float[] ai, int aitype)
		{
			int num = (codable is NPC) ? 0 : ((codable is Projectile) ? 1 : -1);
			if (num == -1)
			{
				return;
			}
			int id = (codable is NPC) ? ((NPC)codable).whoAmI : ((Projectile)codable).identity;
			BaseNet.SyncAI(num, id, ai, aitype);
		}

		public static void SyncAI(int entType, int id, float[] ai, int aitype)
		{
			object[] array = new object[ai.Length + 4];
			array[0] = (byte)entType;
			array[1] = (short)id;
			array[2] = (byte)aitype;
			array[3] = (byte)ai.Length;
			for (int i = 4; i < array.Length; i++)
			{
				array[i] = ai[i - 4];
			}
			MNet.SendBaseNetMessage(1, array);
		}

		public static object[] WriteVector2Array(Vector2[] array)
		{
			List<object> list = new List<object>();
			list.Add(array.Length);
			foreach (Vector2 vector in array)
			{
				list.Add(vector.X);
				list.Add(vector.Y);
			}
			return list.ToArray();
		}

		public static void WriteVector2Array(Vector2[] array, BinaryWriter writer)
		{
			writer.Write(array.Length);
			foreach (Vector2 vector in array)
			{
				writer.Write(vector.X);
				writer.Write(vector.Y);
			}
		}

		public static Vector2[] ReadVector2Array(BinaryReader reader)
		{
			int num = reader.ReadInt();
			Vector2[] array = new Vector2[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new Vector2(reader.ReadFloat(), reader.ReadFloat());
			}
			return array;
		}
	}
}
