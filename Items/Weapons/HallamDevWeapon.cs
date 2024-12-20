using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class HallamDevWeapon : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				HallamDevWeapon.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = HallamDevWeapon.customGlowMask;
			base.DisplayName.SetDefault("Hallam's Legendary Staff");
			base.Tooltip.SetDefault("'Godly'\nSummons a Legendary Rainbow Cat at cursor point\nShoots Rainbow Bolts that move in the direction of your cursor\nCan only be used if Moonlord is defeated");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 200;
			base.item.magic = true;
			base.item.mana = 200;
			base.item.width = 52;
			base.item.height = 52;
			base.item.useTime = 60;
			base.item.useAnimation = 60;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.sellPrice(0, 50, 0, 0);
			base.item.rare = 9;
			base.item.expert = true;
			base.item.UseSound = SoundID.Item44;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("RainbowCatPro");
			base.item.shootSpeed = 0f;
			base.item.glowMask = HallamDevWeapon.customGlowMask;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedMoonlord && player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			return true;
		}

		public static short customGlowMask;
	}
}
