using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KingSlayerIII
{
	[AutoloadBossHead]
	public class KSNope : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III");
			Main.npcFrameCount[base.npc.type] = 12;
		}

		public override void SetDefaults()
		{
			base.npc.width = 64;
			base.npc.height = 112;
			base.npc.friendly = false;
			base.npc.damage = 80;
			base.npc.defense = 35;
			base.npc.lifeMax = 42000;
			base.npc.value = (float)Item.buyPrice(0, 20, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
			base.npc.aiStyle = 0;
			base.npc.netAlways = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.dontTakeDamage = true;
		}

		public override void AI()
		{
			if (!Redemption.AALoaded)
			{
				base.npc.active = false;
			}
			base.npc.ai[0] += 1f;
			base.npc.frameCounter += 1.0;
			if (base.npc.ai[1] == 0f)
			{
				if (base.npc.frameCounter >= 3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 150;
					if (base.npc.frame.Y > 1650)
					{
						base.npc.frame.Y = 1650;
					}
				}
				base.npc.netUpdate = true;
			}
			else if (base.npc.ai[1] == 1f)
			{
				if (base.npc.frameCounter > 3.0)
				{
					base.npc.ai[2] += 1f;
					base.npc.frameCounter = 0.0;
				}
				if (base.npc.ai[2] >= 4f)
				{
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[0] == 60f)
				{
					string text = "... Is that... Rajah Rabbit?";
					Color rarityCyan = Colors.RarityCyan;
					byte r = rarityCyan.R;
					rarityCyan = Colors.RarityCyan;
					byte g = rarityCyan.G;
					rarityCyan = Colors.RarityCyan;
					Main.NewText(text, r, g, rarityCyan.B, false);
				}
				if (base.npc.ai[0] == 150f)
				{
					string text2 = "...";
					Color rarityCyan = Colors.RarityCyan;
					byte r2 = rarityCyan.R;
					rarityCyan = Colors.RarityCyan;
					byte g2 = rarityCyan.G;
					rarityCyan = Colors.RarityCyan;
					Main.NewText(text2, r2, g2, rarityCyan.B, false);
				}
				if (base.npc.ai[0] >= 240f)
				{
					base.npc.ai[1] = 2f;
				}
			}
			else
			{
				string text3 = "...Nope.";
				Color rarityCyan = Colors.RarityCyan;
				byte r3 = rarityCyan.R;
				rarityCyan = Colors.RarityCyan;
				byte g3 = rarityCyan.G;
				rarityCyan = Colors.RarityCyan;
				Main.NewText(text3, r3, g3, rarityCyan.B, false);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 0f), ModContent.ProjectileType<KSExitPro>(), 0, 0f, 255, 0f, 0f);
				base.npc.active = false;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] >= 39f && base.npc.ai[1] == 0f)
			{
				base.npc.ai[1] = 1f;
				base.npc.frameCounter = 0.0;
				base.npc.netUpdate = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D idlesprite = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle1");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			if (base.npc.ai[1] == 0f)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			else
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = idlesprite.Height / 4;
				int y6 = num214 * (int)base.npc.ai[2];
				Main.spriteBatch.Draw(idlesprite, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, idlesprite.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)idlesprite.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}
	}
}
