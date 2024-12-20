using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Grain : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pile o' Grain");
			base.Tooltip.SetDefault("Summons the legendary Chicken");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(2420);
			base.item.shoot = base.mod.ProjectileType("ChickenPet");
			base.item.buffType = base.mod.BuffType("ChickenBuff");
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(base.item.buffType, 3600, true);
			}
		}
	}
}
