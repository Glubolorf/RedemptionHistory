using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class OldTophat : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Old Tophat");
			base.Tooltip.SetDefault("'The last remnants...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 14;
			base.item.rare = 2;
			base.item.vanity = true;
		}

		public override bool DrawHead()
		{
			return true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
	}
}
