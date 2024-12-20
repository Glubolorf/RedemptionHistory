using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class VesselScythe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vessel Shadescythe");
			base.Tooltip.SetDefault("Makes the target become soulless\nMelee swings deal double damage\nRight-clicking is a normal swing");
		}

		public override void SetDefaults()
		{
			base.item.damage = 520;
			base.item.melee = true;
			base.item.width = 84;
			base.item.height = 80;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.sellPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<VesselScythePro>();
			base.item.shootSpeed = 10f;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse != 2;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			target.AddBuff(ModContent.BuffType<BlackenedHeartDebuff>(), 120, false);
			if (target.life <= 0 && modPlayer.shadowBinder && modPlayer.shadowBinderCharge < 100)
			{
				modPlayer.shadowBinderCharge++;
			}
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			damage *= 2;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			RedePlayer redePlayer = (RedePlayer)Main.player[Main.myPlayer].GetModPlayer(base.mod, "RedePlayer");
			int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("Tooltip0"));
			string text;
			if (redePlayer.shadowBinder)
			{
				text = "Enemies slain by melee swings grant you an additional Shadowbound Soul";
			}
			else
			{
				text = "Has a special ability if Sielukaivo Shadowbinder is equipped";
			}
			TooltipLine line = new TooltipLine(base.mod, "text1", text)
			{
				overrideColor = new Color?(Color.DarkGray)
			};
			tooltips.Insert(tooltipLocation, line);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "VesselFrag", 34);
			modRecipe.AddIngredient(null, "SmallShadesoul", 4);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
