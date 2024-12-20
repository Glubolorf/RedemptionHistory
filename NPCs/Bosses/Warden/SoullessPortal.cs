using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.NPCs.Soulless;
using Redemption.WorldGeneration.Soulless;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Redemption.NPCs.Bosses.Warden
{
	[AutoloadBossHead]
	public class SoullessPortal : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadesoul Gateway");
			NPCID.Sets.TownCritter[base.npc.type] = true;
		}

		public override void SetDefaults()
		{
			base.npc.townNPC = true;
			base.npc.friendly = true;
			base.npc.width = 188;
			base.npc.height = 188;
			base.npc.dontTakeDamage = true;
			base.npc.immortal = true;
			base.npc.noGravity = true;
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 999;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.noTileCollide = true;
			base.npc.alpha = 255;
			base.npc.npcSlots = 0f;
		}

		public override bool UsesPartyHat()
		{
			return false;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			float obj = base.npc.ai[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (!2f.Equals(obj))
					{
						if (3f.Equals(obj))
						{
							if (base.npc.scale > 0f)
							{
								base.npc.scale -= 0.02f;
							}
							base.npc.alpha += 5;
							base.npc.velocity.Y = 0.1f;
							if (base.npc.alpha >= 255 || base.npc.scale <= 0f)
							{
								base.npc.active = false;
							}
						}
					}
					else
					{
						base.npc.ai[1] += 1f;
						if (base.npc.ai[1] > 18000f)
						{
							base.npc.ai[0] = 3f;
							base.npc.ai[1] = 0f;
							Main.NewText("A Shadesoul Gateway has faded by itself...", Color.LightSlateGray, false);
						}
					}
				}
				else
				{
					if (base.npc.scale < 1f)
					{
						base.npc.scale += 0.02f;
					}
					base.npc.alpha -= 3;
					base.npc.velocity.Y = -0.3f;
					if (base.npc.alpha <= 0)
					{
						base.npc.ai[0] = 2f;
						base.npc.scale = 1f;
						base.npc.alpha = 0;
					}
				}
			}
			else
			{
				base.npc.scale = 0.1f;
				base.npc.ai[0] = 1f;
			}
			if (((base.npc.ai[1] < 3600f) ? Utils.NextBool(Main.rand, 2000) : ((base.npc.ai[1] < 7600f) ? Utils.NextBool(Main.rand, 1000) : Utils.NextBool(Main.rand, 500))) && base.npc.Distance(player.Center) > 200f && base.npc.Distance(player.Center) < 1600f && !Subworld.IsActive<SoullessSub>())
			{
				for (int i = 0; i < 40; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 4f);
					Main.dust[dustIndex2].velocity *= 6f;
				}
				switch (Main.rand.Next(7))
				{
				case 0:
					base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<SoullessWanderer>(), 0f, 0f, 0f, 0f);
					break;
				case 1:
					base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<LaughingMaskBig>(), 0f, 0f, 0f, 0f);
					break;
				case 2:
					base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<LaughingMaskMedium>(), 0f, 0f, 0f, 0f);
					break;
				case 3:
					base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<LaughingMaskSmall>(), 0f, 0f, 0f, 0f);
					break;
				case 4:
					base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<Shadebug>(), 0f, 0f, 0f, 0f);
					break;
				case 5:
					base.npc.SpawnNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<ShadesoulNPC>(), 0f, 0f, 0f, 0f);
					break;
				case 6:
					base.npc.SpawnNPC((int)base.npc.Center.X + Main.rand.Next(-800, 800), (int)base.npc.Center.Y + Main.rand.Next(-800, 800), ModContent.NPCType<SpookyEyes>(), 0f, 0f, 0f, 0f);
					break;
				}
			}
			base.npc.wet = false;
			base.npc.lavaWet = false;
			base.npc.honeyWet = false;
			base.npc.velocity.X = (base.npc.velocity.Y = 0f);
			base.npc.dontTakeDamage = true;
			base.npc.rotation += 0.02f;
			base.npc.immune[255] = 30;
			if (Main.netMode != 1)
			{
				base.npc.homeless = false;
				base.npc.homeTileX = -1;
				base.npc.homeTileY = -1;
				base.npc.netUpdate = true;
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Enter Gateway";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				Main.PlaySound(SoundID.NPCDeath52, base.npc.position);
				if (!Subworld.AnyActive<Redemption>())
				{
					Main.rand = new UnifiedRandom();
					Redemption.cachedata = true;
					Subworld.Enter<SoullessSub>(false);
				}
				if (Subworld.IsActive<SoullessSub>())
				{
					Subworld.Exit(false);
				}
			}
		}

		public override string GetChat()
		{
			if (Subworld.IsActive<SoullessSub>())
			{
				return "You wish to escape this cursed place...";
			}
			return "You hear an ominous hum from the portal...";
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Utils.Size(base.npc.frame) / 2f;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), -base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale * 1.2f, effects, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}
	}
}
