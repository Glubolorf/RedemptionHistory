using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	[AutoloadEquip(new EquipType[]
	{
		12
	})]
	public class GasMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gas Mask");
			base.Tooltip.SetDefault("Hudda hudda! Grants immunity to Radioactive Fallout");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 32;
			base.item.value = Item.buyPrice(0, 35, 0, 0);
			base.item.rare = 5;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[ModContent.BuffType<RadioactiveFalloutDebuff>()] = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
	}
}
