using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class KatanaChikyu : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chikyū Katana");
			base.Tooltip.SetDefault("'Imbued with Earth'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 21;
			base.item.melee = true;
			base.item.width = 46;
			base.item.height = 48;
			base.item.useTime = 23;
			base.item.useAnimation = 23;
			base.item.useStyle = 1;
			base.item.knockBack = 3.5f;
			base.item.crit = 19;
			base.item.value = Item.buyPrice(0, 0, 1, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 3, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(20, 600, false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2273, 1);
			modRecipe.AddIngredient(176, 25);
			modRecipe.AddIngredient(331, 5);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
