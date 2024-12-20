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
					foreach (byte b in (byte[])obj)
					{
						packet.Write(b);
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
			int entType = (codable is NPC) ? 0 : ((codable is Projectile) ? 1 : -1);
			if (entType == -1)
			{
				return;
			}
			int id = (codable is NPC) ? ((NPC)codable).whoAmI : ((Projectile)codable).identity;
			BaseNet.SyncAI(entType, id, ai, aitype);
		}

		public static void SyncAI(int entType, int id, float[] ai, int aitype)
		{
			object[] ai2 = new object[ai.Length + 4];
			ai2[0] = (byte)entType;
			ai2[1] = (short)id;
			ai2[2] = (byte)aitype;
			ai2[3] = (byte)ai.Length;
			for (int i = 4; i < ai2.Length; i++)
			{
				ai2[i] = ai[i - 4];
			}
			MNet.SendBaseNetMessage(1, ai2);
		}

		public static object[] WriteVector2Array(Vector2[] array)
		{
			List<object> list = new List<object>();
			list.Add(array.Length);
			foreach (Vector2 vec in array)
			{
				list.Add(vec.X);
				list.Add(vec.Y);
			}
			return list.ToArray();
		}

		public static void WriteVector2Array(Vector2[] array, BinaryWriter writer)
		{
			writer.Write(array.Length);
			foreach (Vector2 vec in array)
			{
				writer.Write(vec.X);
				writer.Write(vec.Y);
			}
		}

		public static Vector2[] ReadVector2Array(BinaryReader reader)
		{
			int arrayLength = reader.ReadInt();
			Vector2[] array = new Vector2[arrayLength];
			for (int i = 0; i < arrayLength; i++)
			{
				array[i] = new Vector2(reader.ReadFloat(), reader.ReadFloat());
			}
			return array;
		}
	}
}
