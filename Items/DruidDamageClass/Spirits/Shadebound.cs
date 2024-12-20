using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class Shadebound : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadebound");
			base.Tooltip.SetDefault("Release a bond of shadesouls\nHolding this while an enemy is slain has a chance for a Small Shadesoul to spawn from said enemy\nThe higher your Spirit Level, the greater chance of the Small Shadesoul spawning");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 17f;
			base.item.crit = 4;
			base.item.damage = 324;
			base.item.knockBack = 0f;
			base.item.useStyle = 1;
			base.item.useAnimation = 20;
			base.item.useTime = 20;
			base.item.width = 50;
			base.item.height = 50;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 55, 0, 0);
			base.item.shoot = base.mod.ProjectileType("ShadeboundPro");
			this.spiritWeapon = true;
			this.minSpiritLevel = 5;
			this.maxSpiritLevel = 7;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel <= 2)
			{
				flat -= 323f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 3)
			{
				flat -= 200f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 4)
			{
				flat -= 100f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 6)
			{
				flat += 10f;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 7)
			{
				flat += 20f;
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 6)
				{
					return 1.35f;
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 7)
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
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel == 6)
			{
				base.item.shootSpeed = 19f;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().spiritLevel >= 7)
			{
				base.item.shootSpeed = 23f;
			}
			else
			{
				base.item.shootSpeed = 17f;
			}
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("ShadeboundBuff"), 4, true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "MagSoulbound", 1);
			modRecipe.AddIngredient(null, "SmallShadesoul", 3);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
