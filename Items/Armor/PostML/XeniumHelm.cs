using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class XeniumHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Helmet");
			base.Tooltip.SetDefault("25% increased melee damage and critical strike chance\n15% increased melee speed\nEnemies are more likely to target you");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 9, 50, 0);
			base.item.rare = 11;
			base.item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.25f;
			player.meleeCrit += 25;
			player.meleeSpeed += 0.15f;
			player.aggro += 10;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("XeniumBody") && legs.type == base.mod.ItemType("XeniumLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Grants immunity to the Infection, Radioactive Fallout, and infected waters\nWhen nearing death, a barrier will form around you, increasing damage reduction by a further 14%";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.35f)
			{
				redePlayer.xeniumBarrier = true;
				player.endurance += 0.14f;
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).xeniumBarrier && player.ownedProjectileCounts[base.mod.ProjectileType("XeniumShieldPro")] == 0)
				{
					Projectile.NewProjectile(player.position, Vector2.Zero, base.mod.ProjectileType("XeniumShieldPro"), 0, 0f, player.whoAmI, 0f, 0f);
				}
			}
			player.buffImmune[base.mod.BuffType("XenomiteDebuff")] = true;
			player.buffImmune[base.mod.BuffType("XenomiteDebuff2")] = true;
			player.buffImmune[base.mod.BuffType("RadioactiveFalloutDebuff")] = true;
			player.buffImmune[base.mod.BuffType("HeavyRadiationDebuff")] = true;
			redePlayer.labWaterImmune = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 12);
			modRecipe.AddIngredient(null, "ArtificalMuscle", 2);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
