using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class HazmatSuit : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hazmat Suit");
			base.Tooltip.SetDefault("Grants immunity to the Abandoned Lab and Wasteland water\nGreatly extends underwater breathing\nGrants protection against low-level radiation");
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
			RedePlayer p = player.GetModPlayer<RedePlayer>();
			player.buffImmune[ModContent.BuffType<HeavyRadiationDebuff>()] = true;
			p.hazmatAccessory = true;
			if (hideVisual)
			{
				p.hazmatHideVanity = true;
			}
			player.accDivingHelm = true;
		}
	}
}
