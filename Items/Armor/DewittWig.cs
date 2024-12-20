using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class DewittWig : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dewitt's Wig");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 30;
			base.item.rare = 5;
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
