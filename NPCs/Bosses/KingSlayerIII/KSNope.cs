using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
					Color rarityCyan2 = Colors.RarityCyan;
					byte g = rarityCyan2.G;
					Color rarityCyan3 = Colors.RarityCyan;
					Main.NewText(text, r, g, rarityCyan3.B, false);
				}
				if (base.npc.ai[0] == 150f)
				{
					string text2 = "...";
					Color rarityCyan4 = Colors.RarityCyan;
					byte r2 = rarityCyan4.R;
					Color rarityCyan5 = Colors.RarityCyan;
					byte g2 = rarityCyan5.G;
					Color rarityCyan6 = Colors.RarityCyan;
					Main.NewText(text2, r2, g2, rarityCyan6.B, false);
				}
				if (base.npc.ai[0] >= 240f)
				{
					base.npc.ai[1] = 2f;
				}
			}
			else
			{
				string text3 = "...Nope.";
				Color rarityCyan7 = Colors.RarityCyan;
				byte r3 = rarityCyan7.R;
				Color rarityCyan8 = Colors.RarityCyan;
				byte g3 = rarityCyan8.G;
				Color rarityCyan9 = Colors.RarityCyan;
				Main.NewText(text3, r3, g3, rarityCyan9.B, false);
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 32f, base.npc.position.Y + 56f), new Vector2(0f, 0f), base.mod.ProjectileType("KSExitPro"), 0, 0f, 255, 0f, 0f);
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
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/KingSlayerIII/KSIdle1");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			if (base.npc.ai[1] == 0f)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			}
			else
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 4;
				int num2 = num * (int)base.npc.ai[2];
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			return false;
		}
	}
}
