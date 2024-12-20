using System;
using Redemption.Buffs.Minions;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class GirusHeavyHead2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Tactical Visor");
			base.Tooltip.SetDefault("6% increased ranged damage\n6% increased ranged crit\n20% chance not consume ammo");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 40, 0, 0);
			base.item.rare = 10;
			base.item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage *= 1.06f;
			player.rangedCrit += 6;
			player.ammoCost80 = true;
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
			player.setBonus = "Enemies are more likely to target you, gained stealth and 8% damage reduction.\nSummons a tiny Sniper Drone that'll fire bullets from your inventory in the direction of your cursor when an enemy is near";
			player.AddBuff(11, 2, true);
			player.shroomiteStealth = true;
			player.endurance += 0.08f;
			player.aggro += 10;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).girusSniperDrone = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(ModContent.BuffType<SniperDroneBuff>()) == -1)
				{
					player.AddBuff(ModContent.BuffType<SniperDroneBuff>(), 3600, true);
				}
				if (player.ownedProjectileCounts[ModContent.ProjectileType<SniperDrone>()] < 1)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<SniperDrone>(), 100, 2f, Main.myPlayer, 0f, 0f);
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
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
