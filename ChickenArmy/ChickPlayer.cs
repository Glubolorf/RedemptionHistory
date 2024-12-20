using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.ChickenArmy
{
	public class ChickPlayer : ModPlayer
	{
		public override void PostUpdate()
		{
			if (!ChickWorld.chickArmy)
			{
				ChickWorld.ChickPoints = 0;
			}
			if (RedeWorld.downedPatientZero)
			{
				if (ChickWorld.ChickPoints >= 200)
				{
					RedeWorld.downedChickenInv = true;
					RedeWorld.downedChickenInvPZ = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					Main.NewText("King Chicken's army retreats!", 250, 170, 50, false);
					ChickWorld.ChickPoints = 0;
					ChickWorld.chickArmy = false;
				}
			}
			else if (ChickWorld.ChickPoints >= 100)
			{
				RedeWorld.downedChickenInv = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				Main.NewText("King Chicken's army retreats!", 250, 170, 50, false);
				ChickWorld.ChickPoints = 0;
				ChickWorld.chickArmy = false;
			}
			if (!Main.dayTime)
			{
				ChickWorld.ChickPoints = 0;
				ChickWorld.chickArmy = false;
			}
		}
	}
}
