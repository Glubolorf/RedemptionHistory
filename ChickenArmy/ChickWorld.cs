﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.ChickenArmy
{
	public class ChickWorld : ModWorld
	{
		public override void Initialize()
		{
			ChickWorld.chickArmy = false;
			ChickWorld.ChickPoints2 = 0;
		}

		public override void PostUpdate()
		{
			if (RedeWorld.downedPatientZero)
			{
				if (ChickWorld.ChickPoints2 >= 180 || ChickWorld.ChickPoints >= 180)
				{
					RedeWorld.downedChickenInv = true;
					RedeWorld.downedChickenInvPZ = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					Main.NewText("King Chicken's army retreats!", 250, 170, 50, false);
					ChickWorld.ChickPoints2 = 0;
					ChickWorld.ChickPoints = 0;
					ChickWorld.chickArmy = false;
				}
			}
			else if (ChickWorld.ChickPoints2 >= 80 || ChickWorld.ChickPoints >= 80)
			{
				RedeWorld.downedChickenInv = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				Main.NewText("King Chicken's army retreats!", 250, 170, 50, false);
				ChickWorld.ChickPoints2 = 0;
				ChickWorld.ChickPoints = 0;
				ChickWorld.chickArmy = false;
			}
			if (!Main.dayTime)
			{
				ChickWorld.ChickPoints2 = 0;
				ChickWorld.ChickPoints = 0;
				ChickWorld.chickArmy = false;
			}
		}

		public static int ChickPoints = 0;

		public static int ChickPoints2;

		public static bool chickArmy;
	}
}