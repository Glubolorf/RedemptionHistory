using System;
using Redemption.Buffs.Pets;
using Redemption.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	public class SuspiciousXenomiteShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mysterious Xenomite Fragment");
			base.Tooltip.SetDefault("Summons a lil Xenomite Elemental to light your way!");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(669);
			base.item.rare = 7;
			base.item.shoot = ModContent.ProjectileType<XenomiteElementalPet>();
			base.item.buffType = ModContent.BuffType<XenomiteElementalPetBuff>();
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
