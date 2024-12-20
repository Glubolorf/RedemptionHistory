using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class OOMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Obliterator Mask");
			base.Tooltip.SetDefault("'Yo.'");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 18;
			base.item.rare = 10;
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
