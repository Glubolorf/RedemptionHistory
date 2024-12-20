using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Redemption.ChickenArmy
{
	public class ChickWorld : ModWorld
	{
		public override void Initialize()
		{
			ChickWorld.chickArmy = false;
			ChickWorld.ChickPoints = 0;
		}

		public static ModPacket CreateProgressPacket()
		{
			ModPacket packet = ModContent.GetInstance<Redemption>().GetPacket(256);
			packet.Write(5);
			packet.Write(ChickWorld.ChickPoints);
			packet.Write(ChickWorld.chickArmy);
			return packet;
		}

		public static void SendInfoPacket()
		{
			if (Main.netMode == 0)
			{
				return;
			}
			ChickWorld.CreateProgressPacket().Send(-1, -1);
		}

		public static void HandlePacket(BinaryReader reader)
		{
			ChickWorld.ChickPoints = reader.ReadInt32();
			ChickWorld.chickArmy = reader.ReadBoolean();
			if (Main.netMode == 2)
			{
				ChickWorld.SendInfoPacket();
			}
		}

		public static void ChickArmyStart()
		{
			ChickWorld.SendInfoPacket();
		}

		public static void ChickArmyEnd()
		{
			if (RedeWorld.downedPatientZero)
			{
				if (ChickWorld.ChickPoints >= 200)
				{
					if (Main.netMode == 2)
					{
						NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("King Chicken's army retreats!"), new Color(250, 170, 50), -1);
					}
					else if (Main.netMode == 0)
					{
						Main.NewText("King Chicken's army retreats!", 250, 170, 50, false);
					}
					ChickWorld.chickArmy = false;
					ChickWorld.ChickPoints = 0;
					RedeWorld.downedChickenInv = true;
					RedeWorld.downedChickenInvPZ = true;
					if (!RedeWorld.downedChickenInv)
					{
						RedeWorld.downedChickenInv = true;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						}
					}
					if (!RedeWorld.downedChickenInvPZ)
					{
						RedeWorld.downedChickenInvPZ = true;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
			else if (ChickWorld.ChickPoints >= 100)
			{
				if (Main.netMode == 2)
				{
					NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("King Chicken's army retreats!"), new Color(250, 170, 50), -1);
				}
				else if (Main.netMode == 0)
				{
					Main.NewText("King Chicken's army retreats!", 250, 170, 50, false);
				}
				ChickWorld.chickArmy = false;
				ChickWorld.ChickPoints = 0;
				RedeWorld.downedChickenInv = true;
				if (!RedeWorld.downedChickenInv)
				{
					RedeWorld.downedChickenInv = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			ChickWorld.SendInfoPacket();
		}

		public override void PostUpdate()
		{
			if (!ChickWorld.chickArmy)
			{
				ChickWorld.ChickPoints = 0;
			}
			if (!Main.dayTime)
			{
				ChickWorld.ChickPoints = 0;
				ChickWorld.chickArmy = false;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		public static int ChickPoints;

		public static bool chickArmy;
	}
}
