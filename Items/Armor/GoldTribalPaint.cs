using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class GoldTribalPaint : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tribal War Paint");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 18;
			base.item.rare = 3;
			base.item.vanity = true;
		}

		public override bool DrawHead()
		{
			return true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}
	}
}
