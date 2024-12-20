using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass.SeedBags;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	[AutoloadEquip(new EquipType[]
	{
		7
	})]
	public class TerrariasWill : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terraria's Will");
			base.Tooltip.SetDefault("'With every leaf born, the planet blooms'\nWalking about creates ancient spores that grows into powerful plants that fight for you\n7% increased druid damage\n7% increased druid critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 28;
			base.item.value = Item.buyPrice(3, 50, 0, 0);
			base.item.rare = 8;
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<DBLiquidFlames>(), 1);
			modRecipe.AddIngredient(ModContent.ItemType<DBSunAndMoon>(), 1);
			modRecipe.AddIngredient(ModContent.ItemType<SeedNade>(), 1);
			modRecipe.AddIngredient(ModContent.ItemType<PowerCellWristband>(), 1);
			modRecipe.AddIngredient(2766, 70);
			modRecipe.AddIngredient(ModContent.ItemType<SoulOfBloom>(), 70);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					if (Main.myPlayer == Main.projectile[this.p].owner && Main.rand.Next(90) == 0)
					{
						this.p = Projectile.NewProjectile(new Vector2(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height)), new Vector2(0f, 0f), ModContent.ProjectileType<AncientSporePro>(), 80, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.07f;
			druidDamagePlayer.druidCrit += 7;
		}

		public int p;
	}
}
