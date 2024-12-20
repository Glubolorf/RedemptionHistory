using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	public class Synthesizer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xeno Synthesizer");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 4, 50, 0);
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 32;
			base.item.useTime = 32;
			base.item.knockBack = 0f;
			base.item.rare = 7;
			base.item.damage = 52;
			base.item.shoot = ModContent.ProjectileType<XenoSynthPro>();
			base.item.shootSpeed = 11f;
			base.item.magic = true;
			base.item.mana = 4;
			base.item.autoReuse = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (!Main.dedServ)
			{
				float cursorPosFromPlayer = player.Distance(Main.MouseWorld) / (float)(Main.screenHeight / 2 / 24);
				if (cursorPosFromPlayer > 24f)
				{
					cursorPosFromPlayer = 1f;
				}
				else
				{
					cursorPosFromPlayer = cursorPosFromPlayer / 12f - 1f;
				}
				Main.PlaySound(2, (int)player.Center.X, (int)player.Center.Y, base.mod.GetSoundSlot(2, "Sounds/Item/SynthSound"), 1f, cursorPosFromPlayer);
			}
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<XenoSynthPro>(), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 20);
			modRecipe.AddIngredient(null, "Biohazard", 8);
			modRecipe.AddIngredient(549, 15);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
