using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CorruptedXenomiteHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Xenomite Hamaxe");
		}

		public override void SetDefaults()
		{
			base.item.damage = 95;
			base.item.melee = true;
			base.item.width = 54;
			base.item.height = 56;
			base.item.useTime = 9;
			base.item.useAnimation = 16;
			base.item.axe = 40;
			base.item.hammer = 110;
			base.item.useStyle = 1;
			base.item.knockBack = 7f;
			base.item.value = 775000;
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 30);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 5);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
