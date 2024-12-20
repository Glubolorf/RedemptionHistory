using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class NebuleusMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebuleus Mask");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 22;
			base.item.vanity = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
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
