using System;
using Redemption.Buffs;
using Redemption.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GildedBonnet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gilded Bonnet");
			base.Tooltip.SetDefault("Summons Nebby to give you moral support!");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(669);
			base.item.rare = 0;
			base.item.shoot = ModContent.ProjectileType<NebPet>();
			base.item.buffType = ModContent.BuffType<NebPetBuff>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
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
