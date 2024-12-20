using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class SkullDiggerMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skull Digger's Mask");
			base.Tooltip.SetDefault("'Made of bone'");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 18;
			base.item.rare = 1;
			base.item.vanity = true;
		}

		public override bool DrawHead()
		{
			return true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = false;
		}
	}
}
