using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.ChickenArmy
{
	public class ChickNPC : GlobalNPC
	{
		public override void NPCLoot(NPC npc)
		{
			if (ChickWorld.chickArmy)
			{
				if (RedeWorld.downedPatientZero)
				{
					if (ChickWorld.ChickPoints >= 200)
					{
						ChickWorld.ChickArmyEnd();
					}
				}
				else if (ChickWorld.ChickPoints >= 100)
				{
					ChickWorld.ChickArmyEnd();
				}
				ChickWorld.SendInfoPacket();
			}
		}
	}
}
