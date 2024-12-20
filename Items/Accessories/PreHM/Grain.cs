using System;
using Redemption.Buffs.Pets;
using Redemption.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
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
			base.item.value = 0;
			base.item.shoot = ModContent.ProjectileType<ChickenPet>();
			base.item.buffType = ModContent.BuffType<ChickenBuff>();
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
