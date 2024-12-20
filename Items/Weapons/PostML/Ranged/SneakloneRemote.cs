using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class SneakloneRemote : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PostML/Ranged/" + base.GetType().Name + "_Glow");
				SneakloneRemote.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Blast Battery");
			base.Tooltip.SetDefault("'Prepare for obliteration'\nLeft-Click to mark a single enemy and fire a stream of missiles at their position\nRight-Click to mark your cursor position with a barrage of missiles\nUses rockets as ammo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 210;
			base.item.ranged = true;
			base.item.width = 24;
			base.item.height = 26;
			base.item.useTime = 5;
			base.item.useAnimation = 30;
			base.item.reuseDelay = 60;
			base.item.knockBack = 7f;
			base.item.useStyle = 4;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 1;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/AlarmItem");
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.noUseGraphic = false;
			base.item.shoot = ModContent.ProjectileType<SOOCrosshair>();
			base.item.useAmmo = AmmoID.Rocket;
			base.item.glowMask = SneakloneRemote.customGlowMask;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			Projectile projectile = Main.projectile[base.item.shoot];
			if (player.altFunctionUse == 2)
			{
				base.item.useTime = 5;
			}
			else
			{
				base.item.useTime = 30;
			}
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = ModContent.ProjectileType<SOOCrosshair>();
			position = Main.MouseWorld;
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(position + RedeHelper.Spread(80f), Vector2.Zero, type, damage, knockBack, Main.myPlayer, 1f, 0f);
				return false;
			}
			return true;
		}

		public static short customGlowMask;
	}
}
