using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Cooldowns;
using Redemption.Projectiles.Druid.Stave;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Staves
{
	public class LihzWorldStave : DruidDamageItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Druid/Staves/" + base.GetType().Name + "_Glow");
				LihzWorldStave.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Lihzahrd World Stave");
			base.Tooltip.SetDefault("'Holds the power of the sun'\nSummon a Golem Statue\nThe statue emits a great aura that increases player's attack and life regen when near\nCan only place one at a time");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.mana = 100;
			base.item.value = Item.buyPrice(0, 6, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<WorldTree7>();
			base.item.shootSpeed = 0f;
			base.item.glowMask = LihzWorldStave.customGlowMask;
			base.item.potion = false;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<WorldStaveCooldownDebuff>());
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(ModContent.BuffType<WorldStaveCooldownDebuff>(), 1200, true);
			position = Main.MouseWorld;
			return true;
		}

		public static short customGlowMask;
	}
}
