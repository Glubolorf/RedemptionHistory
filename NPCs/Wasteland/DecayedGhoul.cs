using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Placeable.Banners;
using Redemption.Projectiles.Hostile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Wasteland
{
	public class DecayedGhoul : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Decayed Ghoul");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.width = 36;
			base.npc.height = 48;
			base.npc.damage = 75;
			base.npc.friendly = false;
			base.npc.defense = 34;
			base.npc.lifeMax = 200;
			base.npc.HitSound = SoundID.NPCHit37;
			base.npc.DeathSound = SoundID.NPCDeath40;
			base.npc.value = 90f;
			base.npc.knockBackResist = 0.3f;
			base.npc.aiStyle = 3;
			this.aiType = 524;
			this.animationType = 524;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<DecayedGhoulBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/DecayingGhoulGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/DecayingGhoulGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/DecayingGhoulGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/DecayingGhoulGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(250, 500), true);
			}
		}

		public override void AI()
		{
			if (this.explode)
			{
				this.explodeCounter++;
				if (this.explodeCounter > 20)
				{
					this.explodeFrame++;
					this.explodeCounter = 0;
				}
				if (this.explodeFrame >= 2)
				{
					this.explodeFrame = 0;
				}
			}
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 80f && Main.rand.Next(50) == 0 && !this.explode)
			{
				this.explode = true;
			}
			if (!this.explode)
			{
				base.npc.aiStyle = 3;
			}
			if (this.explode)
			{
				this.explodeTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.explodeTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.DarkGreen, "Hnng!", true, true);
				}
				if (this.explodeTimer >= 40)
				{
					Main.PlaySound(SoundID.Item14, base.npc.position);
					for (int i = 0; i < 25; i++)
					{
						int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 2.2f);
						Main.dust[dustIndex].velocity *= 1.9f;
					}
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/DecayingGhoulGore1"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/DecayingGhoulGore2"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/DecayingGhoulGore3"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/DecayingGhoulGore4"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
					for (int j = 0; j < 8; j++)
					{
						int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), ModContent.ProjectileType<GloopBallPro1>(), 30, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
					}
					base.npc.active = false;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D explodeAni = base.mod.GetTexture("NPCs/Wasteland/DecayedGhoulBoom");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.explode)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.explode)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = explodeAni.Height / 2;
				int y6 = num214 * this.explodeFrame;
				Main.spriteBatch.Draw(explodeAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, explodeAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)explodeAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private bool explode;

		private int explodeFrame;

		private int explodeCounter;

		private int explodeTimer;
	}
}
