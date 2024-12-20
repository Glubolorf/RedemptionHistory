using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class VlitchCore1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Core");
		}

		public override void SetDefaults()
		{
			base.npc.width = 122;
			base.npc.height = 138;
			base.npc.friendly = false;
			base.npc.damage = 60;
			base.npc.defense = 0;
			base.npc.lifeMax = 20000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 5;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.boss = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossVlitch1");
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 82;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchCoreGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchCoreGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchCoreGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchCoreGore1"), 1f);
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			if (Main.rand.Next(6) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] == 200f)
			{
				int num = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, -8f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, 8f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-8f, 0f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(8f, 0f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, 6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, -6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, 6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, -6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				Main.projectile[num].netUpdate = true;
				base.npc.ai[1] = 0f;
			}
			if (base.npc.life < 10000 && base.npc.ai[1] == 100f)
			{
				int num2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, -8f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(0f, 8f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-8f, 0f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(8f, 0f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, 6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(6f, -6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, 6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				num2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 70f, base.npc.position.Y + 62f), new Vector2(-6f, -6f), base.mod.ProjectileType("VlitchLaserPro"), 60, 3f, 255, 0f, 0f);
				Main.projectile[num2].netUpdate = true;
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/VlitchCore_Glow");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			return false;
		}

		private Player player;
	}
}
