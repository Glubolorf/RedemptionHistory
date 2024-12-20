using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class LivingWoodHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood Helmet");
			base.Tooltip.SetDefault("Increases your max number of minions\nIncreases minion damage by 3%");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.value = 0;
			base.item.rare = 0;
			base.item.defense = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions++;
			player.minionDamage *= 1.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("LivingWoodBody") && legs.type == base.mod.ItemType("LivingWoodLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Immune to poison, A bird is summoned to fight for you";
			player.buffImmune[20] = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(base.mod.BuffType("BirdMinionBuff")) == -1)
				{
					player.AddBuff(base.mod.BuffType("BirdMinionBuff"), 3600, true);
				}
				if (player.ownedProjectileCounts[base.mod.ProjectileType("BirdMinion1")] < 1 && player.ownedProjectileCounts[base.mod.ProjectileType("BirdMinion2")] < 1 && player.ownedProjectileCounts[base.mod.ProjectileType("BirdMinion3")] < 1)
				{
					if (Main.rand.Next(3) == 0)
					{
						Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, base.mod.ProjectileType("BirdMinion3"), 10, 4f, Main.myPlayer, 0f, 0f);
						return;
					}
					if (Main.rand.Next(3) == 1)
					{
						Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, base.mod.ProjectileType("BirdMinion2"), 10, 4f, Main.myPlayer, 0f, 0f);
						return;
					}
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, base.mod.ProjectileType("BirdMinion1"), 10, 4f, Main.myPlayer, 0f, 0f);
				}
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LivingTwig", 24);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
