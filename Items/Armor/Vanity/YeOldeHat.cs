using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class YeOldeHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ye Olde Hat");
			base.Tooltip.SetDefault("'Hey, It might be old-fashioned, but atleast it looks cool!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 18;
			base.item.rare = 2;
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
