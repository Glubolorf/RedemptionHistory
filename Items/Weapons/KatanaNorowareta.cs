using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class KatanaNorowareta : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Norowareta Katana");
			base.Tooltip.SetDefault("'Cursed with the Elements'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 29;
			base.item.melee = true;
			base.item.width = 48;
			base.item.height = 50;
			base.item.useTime = 17;
			base.item.useAnimation = 17;
			base.item.useStyle = 1;
			base.item.knockBack = 3.5f;
			base.item.crit = 19;
			base.item.value = Item.buyPrice(0, 0, 2, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 6, 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 3, 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 226, 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 80, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(24, 600, false);
			target.AddBuff(31, 100, false);
			target.AddBuff(103, 600, false);
			target.AddBuff(20, 600, false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KatanaChikyu", 1);
			modRecipe.AddIngredient(null, "KatanaMizu", 1);
			modRecipe.AddIngredient(null, "KatanaKasai", 1);
			modRecipe.AddIngredient(null, "KatanaKuki", 1);
			modRecipe.AddIngredient(154, 25);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
