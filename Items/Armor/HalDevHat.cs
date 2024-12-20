using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class HalDevHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Servant's Godly Tophat");
			base.Tooltip.SetDefault("'Flubbergasting!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 14;
			base.item.rare = 11;
			base.item.vanity = true;
		}

		public override bool DrawHead()
		{
			return true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
