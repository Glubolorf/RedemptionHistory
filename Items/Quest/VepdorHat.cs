using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class VepdorHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vepdor's Headgear");
			base.Tooltip.SetDefault("'Came from a mysterious man...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 30;
			base.item.questItem = true;
			base.item.rare = -11;
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
