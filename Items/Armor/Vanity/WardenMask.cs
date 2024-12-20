using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class WardenMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Warden Mask");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 22;
			base.item.vanity = true;
			base.item.rare = 2;
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
