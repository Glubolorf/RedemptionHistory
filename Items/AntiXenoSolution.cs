using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class AntiXenoSolution : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Anti-Xeno Solution");
			base.Tooltip.SetDefault("Used by the Clentaminator\nRemoves the Wasteland");
		}

		public override void SetDefaults()
		{
			base.item.shoot = ModContent.ProjectileType<AntiXenoSolutionPro>() - 145;
			base.item.ammo = AmmoID.Solution;
			base.item.width = 10;
			base.item.height = 12;
			base.item.value = Item.buyPrice(0, 0, 10, 0);
			base.item.rare = 7;
			base.item.maxStack = 999;
			base.item.consumable = true;
		}
	}
}
