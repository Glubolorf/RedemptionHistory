using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Costumes
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class JanitorOutfit : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Janitor Outfit");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 22;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 3, 0, 0);
			base.item.vanity = true;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = false;
			drawArms = false;
		}

		public override bool DrawBody()
		{
			return false;
		}
	}
}
