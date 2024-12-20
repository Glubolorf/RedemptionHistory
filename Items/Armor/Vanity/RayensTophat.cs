using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class RayensTophat : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rayen's Tophat");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 16;
			base.item.rare = 5;
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
