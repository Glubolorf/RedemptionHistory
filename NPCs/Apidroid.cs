using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class Apidroid : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Apidroid");
			Main.npcFrameCount[base.npc.type] = 15;
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 56;
			base.npc.damage = 45;
			base.npc.defense = 9999;
			base.npc.lifeMax = 50;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 1, 50, 0);
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 3;
			this.aiType = 140;
			this.animationType = 140;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<AndroidBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Main.rand.Next(5) == 0 && !projectile.minion)
			{
				if (projectile.penetrate == 1)
				{
					projectile.penetrate = 2;
				}
				if (damage > 200)
				{
					damage = 200;
				}
				projectile.damage = damage / 4;
				projectile.velocity.X = -projectile.velocity.X;
				projectile.velocity.Y = -projectile.velocity.Y;
				projectile.friendly = false;
				projectile.hostile = true;
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (base.npc.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			spriteBatch.Draw(base.mod.GetTexture("NPCs/Apidroid_Glow"), new Vector2(base.npc.Center.X - Main.screenPosition.X, base.npc.Center.Y - Main.screenPosition.Y), new Rectangle?(base.npc.frame), Color.White, base.npc.rotation, new Vector2((float)base.npc.width * 0.5f, (float)base.npc.height * 0.5f), 1f, spriteEffects, 0f);
		}
	}
}
