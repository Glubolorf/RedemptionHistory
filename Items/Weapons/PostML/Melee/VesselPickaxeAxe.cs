using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class VesselPickaxeAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vessel Pickaxe Axe");
			base.Tooltip.SetDefault("Hitting an enemy will make them bleed heavily for a long period of time");
		}

		public override void SetDefaults()
		{
			base.item.damage = 650;
			base.item.melee = true;
			base.item.width = 60;
			base.item.height = 62;
			base.item.useTime = 7;
			base.item.useAnimation = 10;
			base.item.pick = 320;
			base.item.axe = 35;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<LaceratedDebuff>(), 1200, false);
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.HasBuff(ModContent.BuffType<VesselPickBuff>()))
			{
				base.item.useTime = 3;
			}
			else
			{
				base.item.useTime = 7;
			}
			return true;
		}

		public override bool UseItem(Player player)
		{
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.altFunctionUse == 2 && modPlayer.shadowBinder && modPlayer.shadowBinderCharge >= 4 && !this.activate)
			{
				for (int i = 0; i < 15; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 261, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].velocity *= 2.4f;
				}
				Main.PlaySound(SoundID.Item74, player.position);
				modPlayer.shadowBinderCharge -= 4;
				player.AddBuff(ModContent.BuffType<VesselPickBuff>(), 600, true);
				this.activate = true;
			}
			return true;
		}

		public override void HoldItem(Player player)
		{
			if (player.itemAnimation == 0)
			{
				this.activate = false;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			RedePlayer redePlayer = (RedePlayer)Main.player[Main.myPlayer].GetModPlayer(base.mod, "RedePlayer");
			int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("Tooltip0"));
			string text;
			if (redePlayer.shadowBinder)
			{
				text = "Right-clicking will give this tool a boost in mining speed for 10 seconds (Consumes 4 Shadowbound Souls)";
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
			modRecipe.AddIngredient(null, "VesselFrag", 24);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public bool activate;
	}
}
