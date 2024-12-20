using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class BladeOfTheMountain : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blade of the Mountain");
			base.Tooltip.SetDefault("Has a chance to freezes enemies no bigger than twice the player's size\nInflicts Frostburn");
		}

		public override void SetDefaults()
		{
			base.item.damage = 42;
			base.item.melee = true;
			base.item.width = 78;
			base.item.height = 78;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.useStyle = 1;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 3, 5, 0);
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.rare = 3;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			int targetSize = target.width + target.height;
			if (Main.rand.Next(6) == 0 && targetSize < 100 && !target.boss)
			{
				target.AddBuff(ModContent.BuffType<FrozenEnemyDebuff>(), 180, false);
			}
			target.AddBuff(44, 360, false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(724, 1);
			modRecipe.AddIngredient(ModContent.ItemType<PureIron>(), 14);
			modRecipe.AddIngredient(ModContent.ItemType<DarkShard>(), 2);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
