using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class HeroSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hero Sword");
			base.Tooltip.SetDefault("'A masterfully forged blade, worthy of a Hero'\nCritical strikes deal x3 damage");
		}

		public override void SetDefaults()
		{
			base.item.damage = 56;
			base.item.melee = true;
			base.item.width = 50;
			base.item.height = 50;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.crit = 65;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (crit)
			{
				damage += damage / 2;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(368, 1);
			modRecipe.AddIngredient(548, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
