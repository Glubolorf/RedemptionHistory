using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DragonLeadGreatsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragon Slayer's Greatsword");
			base.Tooltip.SetDefault("'Does actually slay dragons... and wyverns... and pig-fish'\nBurns the enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 56;
			base.item.melee = true;
			base.item.width = 54;
			base.item.height = 54;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 8, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DragonLeadBar", 10);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (target.type == 551)
			{
				damage *= 50;
			}
			if (target.type >= 87 && target.type <= 92)
			{
				damage *= 50;
			}
			if (target.type == 558)
			{
				damage *= 50;
			}
			if (target.type == 559)
			{
				damage *= 50;
			}
			if (target.type == 560)
			{
				damage *= 50;
			}
			if (target.type >= 454 && target.type <= 459)
			{
				damage *= 50;
			}
			if (target.type == 170)
			{
				damage *= 50;
			}
			if (target.type == 180)
			{
				damage *= 50;
			}
			if (target.type == 171)
			{
				damage *= 50;
			}
			if (target.type == 370)
			{
				damage *= 50;
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(24, 600, false);
		}
	}
}
