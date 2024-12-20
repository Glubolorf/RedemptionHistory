using System;
using Redemption.Buffs;
using Redemption.Projectiles.Druid;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid
{
	public class MagSoulbound : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Magnetic Soulbond");
			base.Tooltip.SetDefault("Releases a bond of souls\nHolding this while an enemy is slain has a chance for a Small Lost Soul to spawn from said enemy\nThe higher your Spirit Level, the greater chance of the Small Lost Soul spawning");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 11f;
			base.item.crit = 4;
			base.item.damage = 10;
			base.item.knockBack = 0f;
			base.item.useStyle = 1;
			base.item.useAnimation = 24;
			base.item.useTime = 24;
			base.item.width = 42;
			base.item.height = 38;
			base.item.rare = 1;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 90, 50);
			base.item.shoot = ModContent.ProjectileType<MagSoulboundPro>();
			this.spiritWeapon = true;
			this.minSpiritLevel = 0;
			this.maxSpiritLevel = 3;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 1)
			{
				flat += 2f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 2)
			{
				flat += 3f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 3)
			{
				flat += 5f;
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 2)
				{
					return 1.35f;
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 3)
				{
					return 1.55f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
				{
					return 1.25f;
				}
				return 1f;
			}
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 2)
			{
				base.item.autoReuse = true;
				base.item.shootSpeed = 16f;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 3)
			{
				base.item.autoReuse = true;
				base.item.shootSpeed = 18f;
			}
			else
			{
				base.item.autoReuse = false;
			}
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<SoulboundBuff>(), 4, true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(21, 5);
			modRecipe.AddIngredient(null, "SmallLostSoul", 3);
			modRecipe.AddIngredient(177, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(705, 5);
			modRecipe2.AddIngredient(null, "SmallLostSoul", 3);
			modRecipe2.AddIngredient(177, 1);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
