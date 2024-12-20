using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public class RedeProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public static int CountProjectiles(int type)
		{
			int num = 0;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == type)
				{
					num++;
				}
			}
			return num;
		}

		public static bool AnyProjectiles(int type)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == type)
				{
					return true;
				}
			}
			return false;
		}
	}
}
