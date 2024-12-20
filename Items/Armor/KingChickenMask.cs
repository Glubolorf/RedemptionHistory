using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class KingChickenMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Chicken Mask");
			base.Tooltip.SetDefault("'... Uh...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 18;
			base.item.rare = 2;
			base.item.vanity = true;
		}

		public override bool DrawHead()
		{
			return false;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
