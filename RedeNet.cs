using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption
{
	public class RedeNet
	{
		public static void HandlePacket(BinaryReader bb, int whoAmI)
		{
			byte msg = bb.ReadByte();
			if (RedeNet.DEBUG)
			{
				Redemption.Inst.Logger.Debug(((Main.netMode == 2) ? "--SERVER-- " : "--CLIENT-- ") + "HANDING MESSAGE: " + msg);
			}
			try
			{
				if (msg == 0 && Main.netMode == 2)
				{
					int playerID = (int)bb.ReadByte();
					int bossType = (int)bb.ReadShort();
					bool spawnMessage = bb.ReadBool();
					int npcCenterX = bb.ReadInt();
					int npcCenterY = bb.ReadInt();
					string overrideDisplayName = bb.ReadString();
					bool namePlural = bb.ReadBool();
					Redemption.SpawnBoss(Main.player[playerID], bossType, spawnMessage, new Vector2((float)npcCenterX, (float)npcCenterY), overrideDisplayName, namePlural);
				}
			}
			catch (Exception e)
			{
				Redemption.Inst.Logger.Debug(string.Concat(new string[]
				{
					(Main.netMode == 2) ? "--SERVER-- " : "--CLIENT-- ",
					"ERROR HANDLING MSG: ",
					msg.ToString(),
					": ",
					e.Message
				}));
				Redemption.Inst.Logger.Debug(e.StackTrace);
				Redemption.Inst.Logger.Debug("-------");
			}
		}

		public static void SyncPlayer(int toWho, int fromWho, bool newPlayer)
		{
			if (RedeNet.DEBUG)
			{
				Redemption.Inst.Logger.Debug(string.Concat(new object[]
				{
					(Main.netMode == 2) ? "--SERVER-- " : "--CLIENT-- ",
					"SYNC PLAYER CALLED! NEWPLAYER: ",
					newPlayer.ToString(),
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
				Redemption.Inst.Logger.Debug("--SERVER-- PLAYER JOINED!");
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
					BaseNet.WriteToPacket(Redemption.Inst.GetPacket(256), (byte)msg, param).Send(client, -1);
				}
			}
			catch (Exception e)
			{
				Redemption.Inst.Logger.Debug(string.Concat(new string[]
				{
					(Main.netMode == 2) ? "--SERVER-- " : "--CLIENT-- ",
					"ERROR SENDING MSG: ",
					msg.ToString(),
					": ",
					e.Message
				}));
				Redemption.Inst.Logger.Debug(e.StackTrace);
				Redemption.Inst.Logger.Debug("-------");
				string param2 = "";
				for (int i = 0; i < param.Length; i++)
				{
					param2 += param[i];
				}
				Redemption.Inst.Logger.Debug("PARAMS: " + param2);
				Redemption.Inst.Logger.Debug("-------");
			}
		}

		public const byte SummonNPCFromClient = 0;

		public const byte UpdateLovecraftianCount = 1;

		public const byte RabbitKillCounter = 1;

		public static bool DEBUG = true;
	}
}
