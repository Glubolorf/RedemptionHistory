using System;
using Redemption.Buffs.Pets;
using Redemption.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	public class HamSandwich : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ham Sandwich");
			base.Tooltip.SetDefault("'Unleash doomsday upon this fragile universe'\nSummons !!??");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(669);
			base.item.width = 24;
			base.item.height = 20;
			base.item.rare = -12;
			base.item.shoot = ModContent.ProjectileType<HalPet>();
			base.item.buffType = ModContent.BuffType<HalPetBuff>();
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
