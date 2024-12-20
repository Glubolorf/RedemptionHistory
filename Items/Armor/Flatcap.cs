using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class Flatcap : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flatcap");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 8;
			base.item.rare = 1;
			base.item.vanity = true;
		}

		public override bool DrawHead()
		{
			return true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = true);
		}
	}
}
