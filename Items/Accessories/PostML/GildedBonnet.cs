using System;
using Redemption.Buffs.Pets;
using Redemption.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
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
			base.item.shoot = ModContent.ProjectileType<NebPet>();
			base.item.buffType = ModContent.BuffType<NebPetBuff>();
			base.item.rare = 11;
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
