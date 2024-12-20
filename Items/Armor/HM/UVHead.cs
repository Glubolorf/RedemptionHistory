using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class UVHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("UV Headgear");
			base.Tooltip.SetDefault("11% increased druidic damage\nStaves cast faster\n60+ max life");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.value = 59000;
			base.item.rare = 8;
			base.item.defense = 7;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.11f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).staveSpeed += 0.05f;
			player.statLifeMax2 += 60;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<UVBody>() && legs.type == ModContent.ItemType<UVLeggings>();
		}

		public override void ArmorSetShadows(Player player)
		{
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					int index = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[index].noGravity = true;
					Main.dust[index].noLight = true;
					Dust dust = Main.dust[index];
					dust.velocity.X = dust.velocity.X - player.velocity.X * 0.5f;
					dust.velocity.Y = dust.velocity.Y - player.velocity.Y * 0.5f;
				}
			}
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Goes into Defensive Mode at daytime - Greatly boosting defence and endurance\nGoes into Offensive Mode at nighttime - Greatly boosting druidic damage and crit";
			if (Main.dayTime)
			{
				player.statDefense += 10;
				player.endurance += 0.06f;
				return;
			}
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 15;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(181, 3);
			modRecipe.AddIngredient(null, "UltraVioletPlating", 6);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
