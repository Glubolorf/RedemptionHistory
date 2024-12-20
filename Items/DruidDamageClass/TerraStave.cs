using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class TerraStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terra Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nRight-clicking will summon a Terra Guardian [c/94c2ff:(Requires 300 Mana)]\n[c/71ee8d:-Guardian Info-]\n[c/a0db98:Type:] Offensive\n[c/98dbc3:Special Ability:] Rapid-Fire\n[c/98c1db:Buffs:] 12% druidic damage and crit, faster staves; seedbags & spirits, chance to throw an extra seed");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 98;
			base.item.width = 56;
			base.item.height = 56;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 20, 0, 30);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("TerraBallPro");
			base.item.shootSpeed = 17f;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.mana = 300;
				base.item.buffType = base.mod.BuffType("NatureGuardian19Buff");
				base.item.buffTime = 36000;
				base.item.shoot = base.mod.ProjectileType("NatureGuardian19");
				base.item.shootSpeed = 0f;
				return !player.HasBuff(base.mod.BuffType("NatureGuardian2Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardianBuff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian3Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian4Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian5Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian6Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian7Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian8Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian9Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian10Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian11Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian12Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian13Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian14Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian15Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian16Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian17Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian18Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian19Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian20Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian21Buff"));
			}
			base.item.mana = 0;
			base.item.buffType = 0;
			base.item.buffTime = 0;
			base.item.shoot = base.mod.ProjectileType("TerraBallPro");
			base.item.shootSpeed = 17f;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 15;
				base.item.useAnimation = 15;
			}
			else
			{
				base.item.useTime = 19;
				base.item.useAnimation = 19;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "TrueLunarCrescentStave", 1);
			modRecipe.AddIngredient(null, "TrueHallowedStave", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
