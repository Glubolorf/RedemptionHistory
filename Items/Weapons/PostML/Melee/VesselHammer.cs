using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Buffs.NPCBuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class VesselHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vessel Smasher");
			base.Tooltip.SetDefault("Smashing an enemy will make it take 15% more damage for 5 seconds");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1200;
			base.item.melee = true;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 2;
			base.item.useAnimation = 26;
			base.item.hammer = 100;
			base.item.useStyle = 1;
			base.item.knockBack = 11f;
			base.item.value = Item.buyPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<SmashedDebuff>(), 300, false);
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.knockBack = 40f;
			}
			else
			{
				base.item.knockBack = 11f;
			}
			return true;
		}

		public override bool UseItem(Player player)
		{
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.altFunctionUse == 2 && modPlayer.shadowBinder && modPlayer.shadowBinderCharge >= 1 && !this.activate)
			{
				for (int i = 0; i < 5; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 261, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].velocity *= 2.4f;
				}
				modPlayer.shadowBinderCharge--;
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
				text = "Right-clicking will swing with extreme knockback (Consumes 1 Shadowbound Soul)";
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
