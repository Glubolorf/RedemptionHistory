using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class ZoroShusui : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shusui");
			base.Tooltip.SetDefault("'Wielded by the greatest Swordsman...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 250;
			base.item.melee = true;
			base.item.width = 68;
			base.item.height = 70;
			base.item.useTime = 9;
			base.item.useAnimation = 9;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 27, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KatanaNorowareta", 1);
			modRecipe.AddIngredient(3457, 15);
			modRecipe.AddIngredient(3467, 15);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
