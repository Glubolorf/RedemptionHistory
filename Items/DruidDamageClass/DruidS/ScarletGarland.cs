using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.DruidS
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class ScarletGarland : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scarlet Garland");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n5% increased druidic damage\n5% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 14;
			base.item.value = Item.sellPrice(0, 6, 0, 0);
			base.item.rare = 6;
			base.item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.05f;
			druidDamagePlayer.druidCrit += 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("ScarletChestguard") && legs.type == base.mod.ItemType("ScarletLeggings");
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Forms crimson thorns behind your feet";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.scarletBonus = true;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).scarletBonus && Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					if (Main.rand.Next(40) == 0)
					{
						Projectile.NewProjectile(new Vector2(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height)), new Vector2(0f, 0f), base.mod.ProjectileType("Seed25"), 50, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ScarletBar", 12);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
