using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class InfectionShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Thornshield");
			base.Tooltip.SetDefault("-2 defense\nDouble tap a direction to dash\n14% increased druidic critical strike chance\nInflicts Infection upon dashing into an enemy\nReleases acid-like sparks as you move");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 32;
			base.item.rare = 7;
			base.item.value = 80000;
			base.item.damage = 40;
			base.item.accessory = true;
			base.item.crit = 4;
			base.item.knockBack = 10f;
			base.item.expert = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3097, 1);
			modRecipe.AddIngredient(null, "Xenomite", 10);
			modRecipe.AddIngredient(null, "StarliteBar", 6);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<PlasmaShield>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			DashPlayer modPlayer = player.GetModPlayer<DashPlayer>();
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame && Main.rand.Next(10) == 0)
			{
				Projectile.NewProjectile(new Vector2(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height)), new Vector2(0f, 0f), ModContent.ProjectileType<AcidSpark>(), 0, 0f, Main.myPlayer, 0f, 0f);
			}
			modPlayer.infectedThornshield = true;
			player.statDefense -= 2;
			druidDamagePlayer.druidCrit += 14;
		}
	}
}
