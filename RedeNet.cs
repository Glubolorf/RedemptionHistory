using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class RedeNet
	{
		public static void HandlePacket(BinaryReader bb, int whoAmI)
		{
			byte b = bb.ReadByte();
			if (RedeNet.DEBUG)
			{
				ErrorLogger.Log(((Main.netMode == 2) ? "--SERVER-- " : "--CLIENT-- ") + "HANDING MESSAGE: " + b);
			}
			try
			{
				if (b == 0 && Main.netMode == 2)
				{
					int num = (int)bb.ReadByte();
					int bossType = (int)bb.ReadShort();
					bool spawnMessage = bb.ReadBool();
					int num2 = bb.ReadInt();
					int num3 = bb.ReadInt();
					string overrideDisplayName = bb.ReadString();
					bool namePlural = bb.ReadBool();
					Redemption.SpawnBoss(Main.player[num], bossType, spawnMessage, new Vector2((float)num2, (float)num3), overrideDisplayName, namePlural);
				}
			}
			catch (Exception ex)
			{
				ErrorLogger.Log(string.Concat(new string[]
				{
					(Main.netMode == 2) ? "--SERVER-- " : "--CLIENT-- ",
					"ERROR HANDLING MSG: ",
					b.ToString(),
					": ",
					ex.Message
				}));
				ErrorLogger.Log(ex.StackTrace);
				ErrorLogger.Log("-------");
			}
		}

		public static void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			if (RedeNet.DEBUG)
			{
				ErrorLogger.Log(string.Concat(new object[]
				{
					(Main.netMode == 2) ? "--SERVER-- " : "--CLIENT-- ",
					"SYNC PLAYER CALLED! NEWPLAYER: ",
					newPlayer,
					". TOWHO: ",
					toWho,
					". FROMWHO:",
					fromWho
				}));
			}
			if (Main.netMode == 2 && (toWho > -1 || fromWho > -1))
			{
				RedeNet.PlayerConnected((toWho == -1) ? fromWho : toWho);
			}
		}

		public static void PlayerConnected(int playerID)
		{
			if (RedeNet.DEBUG)
			{
				ErrorLogger.Log("--SERVER-- PLAYER JOINED!");
			}
		}

		public static void SendNetMessage(int msg, params object[] param)
		{
			RedeNet.SendNetMessageClient(msg, -1, param);
		}

		public static void SendNetMessageClient(int msg, int client, params object[] param)
		{
			try
			{
				if (Main.netMode != 0)
				{
					BaseNet.WriteToPacket(Redemption.inst.GetPacket(256), (byte)msg, param).Send(client, -1);
				}
			}
			catch (Exception ex)
			{
				ErrorLogger.Log(string.Concat(new string[]
				{
					(Main.netMode == 2) ? "--SERVER-- " : "--CLIENT-- ",
					"ERROR SENDING MSG: ",
					msg.ToString(),
					": ",
					ex.Message
				}));
				ErrorLogger.Log(ex.StackTrace);
				ErrorLogger.Log("-------");
				string text = "";
				for (int i = 0; i < param.Length; i++)
				{
					text += param[i];
				}
				ErrorLogger.Log("PARAMS: " + text);
				ErrorLogger.Log("-------");
			}
		}

		public const byte SummonNPCFromClient = 0;

		public const byte UpdateLovecraftianCount = 1;

		public const byte RabbitKillCounter = 1;

		public static bool DEBUG = true;
	}
}
