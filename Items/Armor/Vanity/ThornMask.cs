using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class ThornMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Thorn Mask");
			base.Tooltip.SetDefault("'Looks painful...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 28;
			base.item.vanity = true;
			base.item.rare = 2;
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
