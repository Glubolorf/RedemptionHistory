using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class InfectedMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Mask");
			base.Tooltip.SetDefault("'Become infected... Cosmetically'");
		}

		public override void SetDefaults()
		{
			base.item.width = 10;
			base.item.height = 12;
			base.item.rare = 2;
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
