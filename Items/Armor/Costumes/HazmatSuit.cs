using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Costumes
{
	public class HazmatSuit : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hazmat Suit");
			base.Tooltip.SetDefault("Grants immunity to the Abandoned Lab and Wasteland water\nGreatly extends underwater breathing");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 30;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.rare = 9;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			player.buffImmune[base.mod.BuffType("HeavyRadiationDebuff")] = true;
			modPlayer.hazmatAccessory = true;
			if (hideVisual)
			{
				modPlayer.hazmatHideVanity = true;
			}
			player.accDivingHelm = true;
		}
	}
}
