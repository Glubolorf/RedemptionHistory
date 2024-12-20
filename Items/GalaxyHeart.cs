using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GalaxyHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Galaxy Heart");
			base.Tooltip.SetDefault("Permanently increases maximum life by 50\nCan only be used if the max amount of life fruit has been consumed");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 22;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item4;
			base.item.consumable = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.GetModPlayer<RedePlayer>().galaxyHeart && player.statLifeMax >= 500;
		}

		public override bool UseItem(Player player)
		{
			if (player.itemAnimation > 0 && player.itemTime == 0)
			{
				player.itemTime = base.item.useTime;
				if (Main.myPlayer == player.whoAmI)
				{
					player.HealEffect(50, true);
				}
				player.GetModPlayer<RedePlayer>().galaxyHeart = true;
			}
			return true;
		}
	}
}
