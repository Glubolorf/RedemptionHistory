using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
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
			base.item.shoot = base.mod.ProjectileType("XenomiteElementalPet");
			base.item.buffType = base.mod.BuffType("XenomiteElementalPetBuff");
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
