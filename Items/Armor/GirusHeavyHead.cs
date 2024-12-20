using System;
using Redemption.Buffs;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class GirusHeavyHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Heavy Headgear");
			base.Tooltip.SetDefault("6% increased melee damage\n6% increased melee crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 40, 0, 0);
			base.item.rare = 10;
			base.item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.06f;
			player.meleeCrit += 6;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<GirusHeavyBody>() && legs.type == ModContent.ItemType<GirusHeavyLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Enemies are more likely to target you, reduced movement speed and 8% damage reduction\nSummons a tiny Shield Drone that appears whenever a hostile projectile is shot at the player\nWhen a projectile hits the shield, it will release a discharge and reflect it\nThe shield has 500 max life, once destroyed, it will take 10 seconds to reactivate";
			player.AddBuff(11, 2, true);
			player.moveSpeed -= 0.02f;
			player.endurance += 0.08f;
			player.aggro += 10;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(ModContent.BuffType<ShieldDroneBuff>()) == -1)
				{
					player.AddBuff(ModContent.BuffType<ShieldDroneBuff>(), 3600, true);
				}
				if (player.ownedProjectileCounts[ModContent.ProjectileType<ShieldDrone>()] < 1)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<ShieldDrone>(), 0, 0f, Main.myPlayer, 0f, 0f);
				}
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomiteHead", 1);
			modRecipe.AddIngredient(null, "VlitchScale", 10);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
