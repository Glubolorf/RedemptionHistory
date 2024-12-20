using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Usable.Potions;
using Redemption.Tiles.Furniture.Shade;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	public class CelestineDreamsong : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Celestine Dreamsong");
			base.Tooltip.SetDefault("'Those of us who hide a darkness see a different kind of light'\nIncreases vision within the Soulless Caverns\nImmunity to the 'Soulless' debuff\nAn aura of light surrounds you, damaging soulless enemies");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 15, 50, 0);
			base.item.accessory = true;
			base.item.rare = 11;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<CelestineDreamsongTile>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			Lighting.AddLight(player.Center, 1.1f, 1.1f, 1f);
			player.buffImmune[ModContent.BuffType<BlackenedHeartDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<BlackenedHeartBuff>()] = true;
			player.buffImmune[ModContent.BuffType<BlackenedHeartBuff2>()] = true;
			player.AddBuff(ModContent.BuffType<DreamsongBuff>(), 10, true);
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					int index = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 261, 0f, 0f, 100, default(Color), 2f);
					Main.dust[index].noGravity = true;
					Dust dust = Main.dust[index];
					dust.velocity.X = dust.velocity.X - player.velocity.X * 0.5f;
					dust.velocity.Y = dust.velocity.Y - player.velocity.Y * 0.5f;
				}
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DreamsongLightPro>()] == 0)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<DreamsongLightPro>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).dreamsong = true;
		}
	}
}
