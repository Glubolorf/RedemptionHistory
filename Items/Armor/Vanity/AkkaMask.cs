using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class AkkaMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Akka Mask");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 24;
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
