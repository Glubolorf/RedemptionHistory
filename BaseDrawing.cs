using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.UI;

namespace Redemption
{
	public class BaseDrawing
	{
		public static void DrawInvasionProgressBar(SpriteBatch sb, int progress, int progressMax, bool forceDisplay, ref int displayCount, ref float displayAlpha, Texture2D iconTex, string displayText, string percentText = null, Color backgroundColor = default(Color), Vector2 offset = default(Vector2))
		{
			if (Main.invasionProgressMode == 2 && forceDisplay && displayCount < 160)
			{
				displayCount = 160;
			}
			if (!Main.gamePaused && displayCount > 0)
			{
				displayCount = Math.Max(0, displayCount - 1);
			}
			if (displayCount > 0)
			{
				displayAlpha += 0.05f;
			}
			else
			{
				displayAlpha -= 0.05f;
			}
			if (displayAlpha < 0f)
			{
				displayAlpha = 0f;
			}
			if (displayAlpha > 1f)
			{
				displayAlpha = 1f;
			}
			if (displayAlpha <= 0f)
			{
				return;
			}
			float num = 0.5f + displayAlpha * 0.5f;
			int num2 = (int)(200f * num);
			int num3 = (int)(45f * num);
			Vector2 vector = new Vector2((float)(Main.screenWidth - 120), (float)(Main.screenHeight - 40)) + offset;
			Rectangle rectangle;
			rectangle..ctor((int)vector.X - num2 / 2, (int)vector.Y - num3 / 2, num2, num3);
			Utils.DrawInvBG(Main.spriteBatch, rectangle, new Color(63, 65, 151, 255) * 0.785f);
			string text;
			if (progressMax == 0)
			{
				text = progress.ToString();
			}
			else
			{
				text = ((int)((float)progress * 100f / (float)progressMax)).ToString() + "%";
			}
			if (percentText != null)
			{
				text = percentText;
			}
			Texture2D colorBarTexture = Main.colorBarTexture;
			if (progressMax != 0)
			{
				Main.spriteBatch.Draw(colorBarTexture, vector, null, Color.White * displayAlpha, 0f, new Vector2((float)(colorBarTexture.Width / 2), 0f), num, 0, 0f);
				float num4 = MathHelper.Clamp((float)progress / (float)progressMax, 0f, 1f);
				float num5 = 169f * num;
				float num6 = 8f * num;
				Vector2 vector2 = vector + Vector2.UnitY * num6 + Vector2.UnitX * 1f;
				Utils.DrawBorderString(Main.spriteBatch, text, vector2, Color.White * displayAlpha, num, 0.5f, 1f, -1);
				vector2 += Vector2.UnitX * (num4 - 0.5f) * num5;
				Main.spriteBatch.Draw(Main.magicPixel, vector2, new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(255, 241, 51) * displayAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(num5 * num4, num6), 0, 0f);
				Main.spriteBatch.Draw(Main.magicPixel, vector2, new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(255, 165, 0, 127) * displayAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(2f, num6), 0, 0f);
				Main.spriteBatch.Draw(Main.magicPixel, vector2, new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Black * displayAlpha, 0f, new Vector2(0f, 0.5f), new Vector2(num5 * (1f - num4), num6), 0, 0f);
			}
			Vector2 vector3 = new Vector2((float)(Main.screenWidth - 120), (float)(Main.screenHeight - 80)) + offset;
			Vector2 vector4 = Main.fontItemStack.MeasureString(displayText);
			Rectangle rectangle2 = Utils.CenteredRectangle(vector3, (vector4 + new Vector2((float)(iconTex.Width + 20), 10f)) * num);
			Utils.DrawInvBG(Main.spriteBatch, rectangle2, backgroundColor);
			Main.spriteBatch.Draw(iconTex, Utils.Left(rectangle2) + Vector2.UnitX * num * 8f, null, Color.White * displayAlpha, 0f, new Vector2(0f, (float)(iconTex.Height / 2)), num * 0.8f, 0, 0f);
			Utils.DrawBorderString(Main.spriteBatch, displayText, Utils.Right(rectangle2) + Vector2.UnitX * num * -8f, Color.White * displayAlpha, num * 0.9f, 1f, 0.4f, -1);
		}

		public static void AddInterfaceLayer(Mod mod, List<GameInterfaceLayer> list, InterfaceLayer layer, string parent, bool first)
		{
			GameInterfaceLayer gameInterfaceLayer = new LegacyGameInterfaceLayer(mod.Name + ":" + layer.name, delegate()
			{
				layer.Draw();
				return true;
			}, 1);
			layer.listItem = gameInterfaceLayer;
			int num = -1;
			for (int i = 0; i < list.Count; i++)
			{
				GameInterfaceLayer gameInterfaceLayer2 = list[i];
				if (gameInterfaceLayer2.Name.Contains(parent))
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				list.Add(gameInterfaceLayer);
				return;
			}
			list.Insert(first ? num : (num + 1), gameInterfaceLayer);
		}

		public static Texture2D StitchTogetherTileTex(Texture2D tex, int tileType, int width = -1, int[] heights = null)
		{
			TileObjectData tileData = TileObjectData.GetTileData(tileType, 0, 0);
			if (width == -1)
			{
				width = tileData.CoordinateWidth;
			}
			if (heights == null)
			{
				heights = tileData.CoordinateHeights;
			}
			int coordinatePadding = tileData.CoordinatePadding;
			List<Texture2D> list = new List<Texture2D>();
			for (int i = 0; i < tileData.Width; i++)
			{
				for (int j = 0; j < tileData.Height; j++)
				{
					int num = 0;
					for (int k = j; k > 0; k--)
					{
						num += heights[k] + coordinatePadding;
					}
					list.Add(BaseDrawing.GetCroppedTex(tex, new Rectangle(i * (width + coordinatePadding), num, width, heights[j])));
				}
			}
			int num2 = 0;
			for (int l = tileData.Height - 1; l > 0; l--)
			{
				num2 += heights[l];
			}
			Rectangle rectangle;
			rectangle..ctor(0, 0, tileData.Width * width, num2);
			Texture2D toDrawTo = new Texture2D(Main.instance.GraphicsDevice, rectangle.Width, rectangle.Height);
			List<Vector2> list2 = new List<Vector2>();
			for (int m = 0; m < list.Count; m++)
			{
				list2.Add(new Vector2((float)(width * m), 0f));
			}
			return BaseDrawing.DrawTextureToTexture(toDrawTo, list.ToArray(), list2.ToArray());
		}

		public static Texture2D DrawTextureToTexture(Texture2D toDrawTo, Texture2D[] toDraws, Vector2[] drawPos)
		{
			RenderTarget2D renderTarget2D = new RenderTarget2D(Main.instance.GraphicsDevice, toDrawTo.Width, toDrawTo.Height, false, Main.instance.GraphicsDevice.PresentationParameters.BackBufferFormat, 2);
			Main.instance.GraphicsDevice.SetRenderTarget(renderTarget2D);
			Main.instance.GraphicsDevice.Clear(Color.Black);
			Main.spriteBatch.Begin(1, BlendState.AlphaBlend);
			for (int i = 0; i < toDraws.Length; i++)
			{
				Texture2D texture2D = toDraws[i];
				BaseDrawing.DrawTexture(Main.spriteBatch, texture2D, 0, drawPos[i], texture2D.Width, texture2D.Height, 1f, 0f, 0, 1, texture2D.Bounds, null, false, default(Vector2));
			}
			Main.spriteBatch.End();
			Main.instance.GraphicsDevice.SetRenderTarget(null);
			return renderTarget2D;
		}

		public static Texture2D GetCroppedTex(Texture2D texture, Rectangle rect)
		{
			return BaseDrawing.GetCroppedTex(texture, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public static Texture2D GetCroppedTex(Texture2D texture, int startX, int startY, int newWidth, int newHeight)
		{
			Rectangle bounds = texture.Bounds;
			bounds.X += startX;
			bounds.Y += startY;
			bounds.Width = newWidth;
			bounds.Height = newHeight;
			Texture2D texture2D = new Texture2D(Main.instance.GraphicsDevice, bounds.Width, bounds.Height);
			Color[] array = new Color[bounds.Width * bounds.Height];
			texture.GetData<Color>(0, new Rectangle?(bounds), array, 0, bounds.Width * bounds.Height);
			texture2D.SetData<Color>(array);
			return texture2D;
		}

		public static Texture2D GetPlayerTex(Player p, string name)
		{
			return BaseDrawing.GetPlayerTex(p.skinVariant, name, p.Male);
		}

		public static Texture2D GetPlayerTex(int skinVariant, string name, bool male = true)
		{
			switch (name)
			{
			case "Head":
				return Main.playerTextures[skinVariant, 0];
			case "EyeWhite":
				return Main.playerTextures[skinVariant, 1];
			case "Eye":
				return Main.playerTextures[skinVariant, 2];
			case "Body":
				if (!male)
				{
					return Main.playerTextures[skinVariant, 6];
				}
				return Main.playerTextures[skinVariant, 4];
			case "Hand":
				return Main.playerTextures[skinVariant, 5];
			case "Arms":
				return Main.playerTextures[skinVariant, 7];
			case "Legs":
				return Main.playerTextures[skinVariant, 10];
			}
			return null;
		}

		public static void AddPlayerLayer(List<PlayerLayer> list, PlayerLayer layer, PlayerLayer parent, bool first)
		{
			int num = -1;
			for (int i = 0; i < list.Count; i++)
			{
				PlayerLayer playerLayer = list[i];
				if (playerLayer.Name.Equals(parent.Name))
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				list.Add(layer);
				return;
			}
			list.Insert(first ? num : (num + 1), layer);
		}

		public static void AddPlayerHeadLayer(List<PlayerHeadLayer> list, PlayerHeadLayer layer, PlayerHeadLayer parent, bool first)
		{
			int num = -1;
			for (int i = 0; i < list.Count; i++)
			{
				PlayerHeadLayer playerHeadLayer = list[i];
				if (playerHeadLayer.Name.Equals(parent.Name))
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				list.Add(layer);
				return;
			}
			list.Insert(first ? num : (num + 1), layer);
		}

		public static Rectangle GetAdvancedFrame(int currentFrame, int frameOffsetX, int frameWidth, int frameHeight, int pixelSpaceX = 0, int pixelSpaceY = 2)
		{
			int num = currentFrame / frameOffsetX;
			currentFrame -= num * frameOffsetX;
			pixelSpaceY *= currentFrame;
			int num2 = (frameOffsetX == 0) ? 0 : (num * (frameWidth + pixelSpaceX));
			int num3 = frameHeight * currentFrame + pixelSpaceY;
			return new Rectangle(num2, num3, frameWidth, frameHeight);
		}

		public static Rectangle GetFrame(int currentFrame, int frameWidth, int frameHeight, int pixelSpaceX = 0, int pixelSpaceY = 2)
		{
			pixelSpaceY *= currentFrame;
			int num = frameHeight * currentFrame + pixelSpaceY;
			return new Rectangle(0, num, frameWidth - pixelSpaceX, frameHeight);
		}

		public static bool IsNormalDrawPass(Player player, PlayerDrawInfo pdi = default(PlayerDrawInfo))
		{
			return player.ghostFade == 0f && player.shadow == 0f && (pdi.Equals(default(PlayerDrawInfo)) || pdi.shadow == 0f);
		}

		public static int GetDye(Player drawPlayer, int accSlot, bool social = false, bool wings = false)
		{
			int num = accSlot % 10;
			if (!wings && accSlot < 10 && drawPlayer.hideVisual[num])
			{
				return -1;
			}
			return GameShaders.Armor.GetShaderIdFromItemId(drawPlayer.dye[num].type);
		}

		public static Color? GetDyeColor(int dye)
		{
			Color? result = null;
			float num = 1f;
			if (dye >= 13 && dye <= 24)
			{
				num = 0.7f;
				dye -= 12;
			}
			if (dye >= 45 && dye <= 56)
			{
				num = 1.3f;
				dye -= 44;
			}
			if (dye >= 32 && dye <= 43)
			{
				num = 1.5f;
				dye -= 31;
			}
			int num2 = dye;
			if (num2 <= 31)
			{
				switch (num2)
				{
				case 1:
					result = new Color?(new Color(248, 63, 63));
					break;
				case 2:
					result = new Color?(new Color(248, 148, 63));
					break;
				case 3:
					result = new Color?(new Color(248, 242, 62));
					break;
				case 4:
					result = new Color?(new Color(157, 248, 70));
					break;
				case 5:
					result = new Color?(new Color(48, 248, 70));
					break;
				case 6:
					result = new Color?(new Color(60, 248, 70));
					break;
				case 7:
					result = new Color?(new Color(62, 242, 248));
					break;
				case 8:
					result = new Color?(new Color(64, 181, 247));
					break;
				case 9:
					result = new Color?(new Color(66, 95, 247));
					break;
				case 10:
					result = new Color?(new Color(159, 65, 247));
					break;
				case 11:
					result = new Color?(new Color(212, 65, 247));
					break;
				case 12:
					result = new Color?(new Color(242, 63, 131));
					break;
				default:
					if (num2 == 31)
					{
						result = new Color?(new Color(226, 226, 226));
					}
					break;
				}
			}
			else if (num2 != 44)
			{
				switch (num2)
				{
				case 62:
					result = new Color?(new Color(157, 248, 70));
					break;
				case 63:
					result = new Color?(new Color(64, 181, 247));
					break;
				case 64:
					result = new Color?(new Color(212, 65, 247));
					break;
				}
			}
			else
			{
				result = new Color?(new Color(40, 40, 40));
			}
			if (result != null && num != 1f)
			{
				result = new Color?(BaseUtility.ColorMult(result.Value, num));
			}
			return result;
		}

		public static Color GetGemColor(int type)
		{
			if (type == 181)
			{
				return Color.MediumOrchid;
			}
			if (type == 180)
			{
				return Color.Gold;
			}
			if (type == 177)
			{
				return Color.DeepSkyBlue;
			}
			if (type == 178)
			{
				return Color.Crimson;
			}
			if (type == 179)
			{
				return Color.LimeGreen;
			}
			if (type == 182)
			{
				return Color.GhostWhite;
			}
			if (type == 999)
			{
				return Color.Orange;
			}
			return Color.Black;
		}

		public static Color GetNPCColor(NPC npc, Vector2? position = null, bool effects = true, float shadowOverride = 0f)
		{
			return npc.GetAlpha(BaseDrawing.BuffEffects(npc, BaseDrawing.GetLightColor((position != null) ? position.Value : npc.Center), (shadowOverride != 0f) ? shadowOverride : 0f, effects, npc.poisoned, npc.onFire, npc.onFire2, Main.player[Main.myPlayer].detectCreature, false, false, false, npc.venom, npc.midas, npc.ichor, npc.onFrostBurn, false, false, npc.dripping, npc.drippingSlime, npc.loveStruck, npc.stinky));
		}

		public static Color GetPlayerColor(Player p, Vector2? position = null, bool effects = false, float shadowOverride = 0f)
		{
			return p.GetImmuneAlpha(BaseDrawing.BuffEffects(p, BaseDrawing.GetLightColor((position != null) ? position.Value : p.Center), (shadowOverride != 0f) ? shadowOverride : p.shadow, effects, p.poisoned, p.onFire, p.onFire2, false, p.noItems, p.blind, p.bleed, p.venom, false, p.ichor, p.onFrostBurn, p.burned, p.honey, p.dripping, p.drippingSlime, p.loveStruck, p.stinky), p.shadow);
		}

		public static Color GetLightColor(Vector2 position)
		{
			return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
		}

		public static void AddLight(Vector2 position, Color color, float brightnessDivider = 1f)
		{
			BaseDrawing.AddLight(position, (float)color.R / 255f, (float)color.G / 255f, (float)color.B / 255f, brightnessDivider);
		}

		public static void AddLight(Vector2 position, float colorR, float colorG, float colorB, float brightnessDivider = 1f)
		{
			Lighting.AddLight((int)(position.X / 16f), (int)(position.Y / 16f), colorR / brightnessDivider, colorG / brightnessDivider, colorB / brightnessDivider);
		}

		public static Color BuffEffects(Entity codable, Color lightColor, float shadow = 0f, bool effects = true, bool poisoned = false, bool onFire = false, bool onFire2 = false, bool hunter = false, bool noItems = false, bool blind = false, bool bleed = false, bool venom = false, bool midas = false, bool ichor = false, bool onFrostBurn = false, bool burned = false, bool honey = false, bool dripping = false, bool drippingSlime = false, bool loveStruck = false, bool stinky = false)
		{
			float num = 1f;
			float num2 = 1f;
			float num3 = 1f;
			float num4 = 1f;
			if (effects && honey && Main.rand.Next(30) == 0)
			{
				int num5 = Dust.NewDust(codable.position, codable.width, codable.height, 152, 0f, 0f, 150, default(Color), 1f);
				Main.dust[num5].velocity.Y = 0.3f;
				Dust dust = Main.dust[num5];
				dust.velocity.X = dust.velocity.X * 0.1f;
				Main.dust[num5].scale += (float)Main.rand.Next(3, 4) * 0.1f;
				Main.dust[num5].alpha = 100;
				Main.dust[num5].noGravity = true;
				Main.dust[num5].velocity += codable.velocity * 0.1f;
				if (codable is Player)
				{
					Main.playerDrawDust.Add(num5);
				}
			}
			if (poisoned)
			{
				if (effects && Main.rand.Next(30) == 0)
				{
					int num6 = Dust.NewDust(codable.position, codable.width, codable.height, 46, 0f, 0f, 120, default(Color), 0.2f);
					Main.dust[num6].noGravity = true;
					Main.dust[num6].fadeIn = 1.9f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(num6);
					}
				}
				num *= 0.65f;
				num3 *= 0.75f;
			}
			if (venom)
			{
				if (effects && Main.rand.Next(10) == 0)
				{
					int num7 = Dust.NewDust(codable.position, codable.width, codable.height, 171, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[num7].noGravity = true;
					Main.dust[num7].fadeIn = 1.5f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(num7);
					}
				}
				num2 *= 0.45f;
				num *= 0.75f;
			}
			if (midas)
			{
				num3 *= 0.3f;
				num *= 0.85f;
			}
			if (ichor)
			{
				if (codable is NPC)
				{
					lightColor = new Color(255, 255, 0, 255);
				}
				else
				{
					num3 = 0f;
				}
			}
			if (burned)
			{
				if (effects)
				{
					int num8 = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 2f);
					Main.dust[num8].noGravity = true;
					Main.dust[num8].velocity *= 1.8f;
					Dust dust2 = Main.dust[num8];
					dust2.velocity.Y = dust2.velocity.Y - 0.75f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(num8);
					}
				}
				if (codable is Player)
				{
					num = 1f;
					num3 *= 0.6f;
					num2 *= 0.7f;
				}
			}
			if (onFrostBurn)
			{
				if (effects)
				{
					if (Main.rand.Next(4) < 3)
					{
						int num9 = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 135, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[num9].noGravity = true;
						Main.dust[num9].velocity *= 1.8f;
						Dust dust3 = Main.dust[num9];
						dust3.velocity.Y = dust3.velocity.Y - 0.5f;
						if (Main.rand.Next(4) == 0)
						{
							Main.dust[num9].noGravity = false;
							Main.dust[num9].scale *= 0.5f;
						}
						if (codable is Player)
						{
							Main.playerDrawDust.Add(num9);
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 0.1f, 0.6f, 1f);
				}
				if (codable is Player)
				{
					num *= 0.5f;
					num2 *= 0.7f;
				}
			}
			if (onFire)
			{
				if (effects)
				{
					if (Main.rand.Next(4) != 0)
					{
						int num10 = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[num10].noGravity = true;
						Main.dust[num10].velocity *= 1.8f;
						Dust dust4 = Main.dust[num10];
						dust4.velocity.Y = dust4.velocity.Y - 0.5f;
						if (Main.rand.Next(4) == 0)
						{
							Main.dust[num10].noGravity = false;
							Main.dust[num10].scale *= 0.5f;
						}
						if (codable is Player)
						{
							Main.playerDrawDust.Add(num10);
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
				}
				if (codable is Player)
				{
					num3 *= 0.6f;
					num2 *= 0.7f;
				}
			}
			if (dripping && shadow == 0f && Main.rand.Next(4) != 0)
			{
				Vector2 position = codable.position;
				position.X -= 2f;
				position.Y -= 2f;
				if (Main.rand.Next(2) == 0)
				{
					int num11 = Dust.NewDust(position, codable.width + 4, codable.height + 2, 211, 0f, 0f, 50, default(Color), 0.8f);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num11].alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num11].alpha += 25;
					}
					Main.dust[num11].noLight = true;
					Main.dust[num11].velocity *= 0.2f;
					Dust dust5 = Main.dust[num11];
					dust5.velocity.Y = dust5.velocity.Y + 0.2f;
					Main.dust[num11].velocity += codable.velocity;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(num11);
					}
				}
				else
				{
					int num12 = Dust.NewDust(position, codable.width + 8, codable.height + 8, 211, 0f, 0f, 50, default(Color), 1.1f);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num12].alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num12].alpha += 25;
					}
					Main.dust[num12].noLight = true;
					Main.dust[num12].noGravity = true;
					Main.dust[num12].velocity *= 0.2f;
					Dust dust6 = Main.dust[num12];
					dust6.velocity.Y = dust6.velocity.Y + 1f;
					Main.dust[num12].velocity += codable.velocity;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(num12);
					}
				}
			}
			if (drippingSlime && shadow == 0f)
			{
				int num13 = 175;
				Color color;
				color..ctor(0, 80, 255, 100);
				if (Main.rand.Next(4) != 0 && Main.rand.Next(2) == 0)
				{
					Vector2 position2 = codable.position;
					position2.X -= 2f;
					position2.Y -= 2f;
					int num14 = Dust.NewDust(position2, codable.width + 4, codable.height + 2, 4, 0f, 0f, num13, color, 1.4f);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num14].alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num14].alpha += 25;
					}
					Main.dust[num14].noLight = true;
					Main.dust[num14].velocity *= 0.2f;
					Dust dust7 = Main.dust[num14];
					dust7.velocity.Y = dust7.velocity.Y + 0.2f;
					Main.dust[num14].velocity += codable.velocity;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(num14);
					}
				}
				num *= 0.8f;
				num2 *= 0.8f;
			}
			if (onFire2)
			{
				if (effects)
				{
					if (Main.rand.Next(4) != 0)
					{
						int num15 = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, 75, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[num15].noGravity = true;
						Main.dust[num15].velocity *= 1.8f;
						Dust dust8 = Main.dust[num15];
						dust8.velocity.Y = dust8.velocity.Y - 0.5f;
						if (Main.rand.Next(4) == 0)
						{
							Main.dust[num15].noGravity = false;
							Main.dust[num15].scale *= 0.5f;
						}
						if (codable is Player)
						{
							Main.playerDrawDust.Add(num15);
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
				}
				if (codable is Player)
				{
					num3 *= 0.6f;
					num2 *= 0.7f;
				}
			}
			if (noItems)
			{
				num *= 0.65f;
				num2 *= 0.8f;
			}
			if (blind)
			{
				num *= 0.7f;
				num2 *= 0.65f;
			}
			if (bleed)
			{
				bool flag = (codable is Player) ? ((Player)codable).dead : (codable is NPC && ((NPC)codable).life <= 0);
				if (effects && !flag && Main.rand.Next(30) == 0)
				{
					int num16 = Dust.NewDust(codable.position, codable.width, codable.height, 5, 0f, 0f, 0, default(Color), 1f);
					Dust dust9 = Main.dust[num16];
					dust9.velocity.Y = dust9.velocity.Y + 0.5f;
					Main.dust[num16].velocity *= 0.25f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(num16);
					}
				}
				num2 *= 0.9f;
				num3 *= 0.9f;
			}
			if (loveStruck && effects && shadow == 0f && Main.instance.IsActive && !Main.gamePaused && Main.rand.Next(5) == 0)
			{
				Vector2 vector;
				vector..ctor((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
				vector.Normalize();
				vector.X *= 0.66f;
				int num17 = Gore.NewGore(codable.position + new Vector2((float)Main.rand.Next(codable.width + 1), (float)Main.rand.Next(codable.height + 1)), vector * (float)Main.rand.Next(3, 6) * 0.33f, 331, (float)Main.rand.Next(40, 121) * 0.01f);
				Main.gore[num17].sticky = false;
				Main.gore[num17].velocity *= 0.4f;
				Gore gore = Main.gore[num17];
				gore.velocity.Y = gore.velocity.Y - 0.6f;
				if (codable is Player)
				{
					Main.playerDrawGore.Add(num17);
				}
			}
			if (stinky && shadow == 0f)
			{
				num *= 0.7f;
				num3 *= 0.55f;
				if (effects && Main.rand.Next(5) == 0 && Main.instance.IsActive && !Main.gamePaused)
				{
					Vector2 vector2;
					vector2..ctor((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
					vector2.Normalize();
					vector2.X *= 0.66f;
					vector2.Y = Math.Abs(vector2.Y);
					Vector2 vector3 = vector2 * (float)Main.rand.Next(3, 5) * 0.25f;
					int num18 = Dust.NewDust(codable.position, codable.width, codable.height, 188, vector3.X, vector3.Y * 0.5f, 100, default(Color), 1.5f);
					Main.dust[num18].velocity *= 0.1f;
					Dust dust10 = Main.dust[num18];
					dust10.velocity.Y = dust10.velocity.Y - 0.5f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(num18);
					}
				}
			}
			lightColor.R = (byte)((float)lightColor.R * num);
			lightColor.G = (byte)((float)lightColor.G * num2);
			lightColor.B = (byte)((float)lightColor.B * num3);
			lightColor.A = (byte)((float)lightColor.A * num4);
			if (codable is NPC)
			{
				NPCLoader.DrawEffects((NPC)codable, ref lightColor);
			}
			if (hunter && (!(codable is NPC) || ((NPC)codable).lifeMax > 1))
			{
				if (effects && !Main.gamePaused && Main.instance.IsActive && Main.rand.Next(50) == 0)
				{
					int num19 = Dust.NewDust(codable.position, codable.width, codable.height, 15, 0f, 0f, 150, default(Color), 0.8f);
					Main.dust[num19].velocity *= 0.1f;
					Main.dust[num19].noLight = true;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(num19);
					}
				}
				byte b = 50;
				byte b2 = byte.MaxValue;
				byte b3 = 50;
				if (codable is NPC && !((NPC)codable).friendly && ((NPC)codable).catchItem <= 0 && (((NPC)codable).damage != 0 || ((NPC)codable).lifeMax != 5))
				{
					b = byte.MaxValue;
					b2 = 50;
				}
				if (!(codable is NPC) && lightColor.R < 150)
				{
					lightColor.A = Main.mouseTextColor;
				}
				if (lightColor.R < b)
				{
					lightColor.R = b;
				}
				if (lightColor.G < b2)
				{
					lightColor.G = b2;
				}
				if (lightColor.B < b3)
				{
					lightColor.B = b3;
				}
			}
			return lightColor;
		}

		public static bool ShouldDrawHelmet(Player drawPlayer, int itemType = -1)
		{
			return drawPlayer.head > 0 && BaseDrawing.ShouldDrawArmor(drawPlayer, 0, itemType);
		}

		public static bool ShouldDrawChestplate(Player drawPlayer, int itemType = -1)
		{
			return drawPlayer.body > 0 && BaseDrawing.ShouldDrawArmor(drawPlayer, 1, itemType);
		}

		public static bool ShouldDrawLeggings(Player drawPlayer, int itemType = -1)
		{
			return drawPlayer.legs > 0 && BaseDrawing.ShouldDrawArmor(drawPlayer, 2, itemType);
		}

		public static bool ShouldDrawArmor(Player drawPlayer, int armorType, int itemType = -1)
		{
			if (drawPlayer.merman || drawPlayer.wereWolf)
			{
				return false;
			}
			if (itemType == -1)
			{
				return drawPlayer.armor[10 + armorType].type > 0 || (drawPlayer.armor[10 + armorType].IsBlank() && drawPlayer.armor[armorType].type > 0);
			}
			return drawPlayer.armor[10 + armorType].type == itemType || (drawPlayer.armor[10 + armorType].IsBlank() && drawPlayer.armor[armorType].type == itemType);
		}

		public static bool ShouldDrawAccessory(Player drawPlayer, int itemType)
		{
			for (int i = 3; i < 8 + drawPlayer.extraAccessorySlots; i++)
			{
				if (drawPlayer.armor[i + 10].type == itemType)
				{
					return true;
				}
				if (drawPlayer.armor[i + 10].IsBlank() && !drawPlayer.hideVisual[i] && drawPlayer.armor[i].type == itemType)
				{
					return true;
				}
			}
			return false;
		}

		public static bool ShouldDrawHeldItem(Player drawPlayer)
		{
			return BaseDrawing.ShouldDrawHeldItem(drawPlayer.inventory[drawPlayer.selectedItem], drawPlayer.itemAnimation, drawPlayer.wet, drawPlayer.dead);
		}

		public static bool ShouldDrawHeldItem(Item item, int itemAnimation, bool isWet, bool isDead = false)
		{
			return (itemAnimation > 0 || item.holdStyle > 0) && item.type > 0 && !isDead && !item.noUseGraphic && (!isWet || !item.noWet);
		}

		public static bool DrawHeldSword(object sb, int shader, Player drawPlayer, Color lightColor = default(Color), float scale = 0f, float xOffset = 0f, float yOffset = 0f, Rectangle? frame = null, int frameCount = 1, Texture2D overrideTex = null)
		{
			if (BaseDrawing.ShouldDrawHeldItem(drawPlayer))
			{
				Item item = drawPlayer.inventory[drawPlayer.selectedItem];
				BaseDrawing.DrawHeldSword(sb, (overrideTex != null) ? overrideTex : Main.itemTexture[item.type], shader, drawPlayer.itemLocation, item, drawPlayer.direction, drawPlayer.itemRotation, (scale <= 0f) ? item.scale : scale, lightColor, item.color, xOffset, yOffset, drawPlayer.gravDir, drawPlayer, frame, frameCount);
				return false;
			}
			return true;
		}

		public static void DrawHeldSword(object sb, Texture2D tex, int shader, Vector2 position, Item item, int direction, float itemRotation, float itemScale, Color lightColor = default(Color), Color wepColor = default(Color), float xOffset = 0f, float yOffset = 0f, float gravDir = -1f, Entity entity = null, Rectangle? frame = null, int frameCount = 1)
		{
			if (frame == null)
			{
				frame = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
			}
			if (lightColor == default(Color))
			{
				lightColor = BaseDrawing.GetLightColor(position);
			}
			xOffset *= (float)direction;
			SpriteEffects spriteEffects = (direction == -1) ? 1 : 0;
			if (gravDir == -1f)
			{
				yOffset *= -1f;
				spriteEffects |= 2;
			}
			if (entity is Player)
			{
				Player player = (Player)entity;
				yOffset -= player.gfxOffY;
			}
			else if (entity is NPC)
			{
				NPC npc = (NPC)entity;
				yOffset -= npc.gfxOffY;
			}
			int type = item.type;
			Vector2 vector = position - Main.screenPosition;
			Vector2 vector2;
			vector2..ctor((float)tex.Width * 0.5f, (float)tex.Height * 0.5f / (float)frameCount);
			Vector2 vector3 = new Vector2(vector2.X - vector2.X * (float)direction, (float)((gravDir == -1f) ? 0 : tex.Height)) + new Vector2(xOffset, -yOffset);
			if (gravDir == -1f)
			{
				if (sb is List<DrawData>)
				{
					DrawData item2;
					item2..ctor(tex, vector, frame, item.GetAlpha(lightColor), itemRotation, vector3, itemScale, spriteEffects, 0);
					item2.shader = shader;
					((List<DrawData>)sb).Add(item2);
				}
				else if (sb is SpriteBatch)
				{
					((SpriteBatch)sb).Draw(tex, vector, frame, item.GetAlpha(lightColor), itemRotation, vector3, itemScale, spriteEffects, 0f);
				}
				if (wepColor != default(Color))
				{
					if (sb is List<DrawData>)
					{
						DrawData item3;
						item3..ctor(tex, vector, frame, item.GetColor(wepColor), itemRotation, vector3, itemScale, spriteEffects, 0);
						item3.shader = shader;
						((List<DrawData>)sb).Add(item3);
						return;
					}
					if (sb is SpriteBatch)
					{
						((SpriteBatch)sb).Draw(tex, vector, frame, item.GetColor(wepColor), itemRotation, vector3, itemScale, spriteEffects, 0f);
						return;
					}
				}
			}
			else
			{
				if (type == 425 || type == 507)
				{
					if (direction == 1)
					{
						spriteEffects = 2;
					}
					else
					{
						spriteEffects = 3;
					}
				}
				if (sb is List<DrawData>)
				{
					DrawData item4;
					item4..ctor(tex, vector, frame, item.GetAlpha(lightColor), itemRotation, vector3, itemScale, spriteEffects, 0);
					item4.shader = shader;
					((List<DrawData>)sb).Add(item4);
				}
				else if (sb is SpriteBatch)
				{
					((SpriteBatch)sb).Draw(tex, vector, frame, item.GetAlpha(lightColor), itemRotation, vector3, itemScale, spriteEffects, 0f);
				}
				if (wepColor != default(Color))
				{
					if (sb is List<DrawData>)
					{
						DrawData item5;
						item5..ctor(tex, vector, frame, item.GetColor(wepColor), itemRotation, vector3, itemScale, spriteEffects, 0);
						item5.shader = shader;
						((List<DrawData>)sb).Add(item5);
						return;
					}
					if (sb is SpriteBatch)
					{
						((SpriteBatch)sb).Draw(tex, vector, frame, item.GetColor(wepColor), itemRotation, vector3, itemScale, spriteEffects, 0f);
					}
				}
			}
		}

		public static bool DrawHeldGun(object sb, int shader, Player drawPlayer, Color lightColor = default(Color), float scale = 0f, float xOffset = 0f, float yOffset = 0f, bool shakeX = false, bool shakeY = false, float shakeScalarX = 1f, float shakeScalarY = 1f, Rectangle? frame = null, int frameCount = 1, Texture2D overrideTex = null)
		{
			if (BaseDrawing.ShouldDrawHeldItem(drawPlayer))
			{
				Item item = drawPlayer.inventory[drawPlayer.selectedItem];
				BaseDrawing.DrawHeldGun(sb, (overrideTex != null) ? overrideTex : Main.itemTexture[item.type], shader, drawPlayer.itemLocation, item, drawPlayer.direction, drawPlayer.itemRotation, (scale <= 0f) ? item.scale : scale, lightColor, item.color, xOffset, yOffset, shakeX, shakeY, shakeScalarX, shakeScalarY, drawPlayer.gravDir, drawPlayer, frame, frameCount);
				return false;
			}
			return true;
		}

		public static void DrawHeldGun(object sb, Texture2D tex, int shader, Vector2 position, Item item, int direction, float itemRotation, float itemScale, Color lightColor = default(Color), Color wepColor = default(Color), float xOffset = 0f, float yOffset = 0f, bool shakeX = false, bool shakeY = false, float shakeScalarX = 1f, float shakeScalarY = 1f, float gravDir = 1f, Entity entity = null, Rectangle? frame = null, int frameCount = 1)
		{
			if (frame == null)
			{
				frame = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
			}
			if (lightColor == default(Color))
			{
				lightColor = BaseDrawing.GetLightColor(position);
			}
			SpriteEffects spriteEffects = (direction == -1) ? 1 : 0;
			if (gravDir == -1f)
			{
				yOffset *= -1f;
				spriteEffects |= 2;
			}
			int type = item.type;
			int num = type;
			Vector2 vector;
			vector..ctor((float)(tex.Width / 2), (float)(tex.Height / 2) / (float)frameCount);
			if (entity is Player)
			{
				Player player = (Player)entity;
				yOffset += player.gfxOffY;
			}
			else if (entity is NPC)
			{
				NPC npc = (NPC)entity;
				yOffset += npc.gfxOffY;
			}
			Vector2 vector2;
			vector2..ctor(-xOffset, (float)(tex.Height / 2) / (float)frameCount - yOffset);
			if (direction == -1)
			{
				vector2..ctor((float)tex.Width + xOffset, (float)(tex.Height / 2) / (float)frameCount - yOffset);
			}
			Vector2 vector3;
			vector3..ctor((float)((int)(position.X - Main.screenPosition.X + vector.X)), (float)((int)(position.Y - Main.screenPosition.Y + vector.Y)));
			if (shakeX)
			{
				vector3.X += shakeScalarX * ((float)Main.rand.Next(-5, 6) / 9f);
			}
			if (shakeY)
			{
				vector3.Y += shakeScalarY * ((float)Main.rand.Next(-5, 6) / 9f);
			}
			if (sb is List<DrawData>)
			{
				DrawData item2;
				item2..ctor(tex, vector3, frame, item.GetAlpha(lightColor), itemRotation, vector2, itemScale, spriteEffects, 0);
				item2.shader = shader;
				((List<DrawData>)sb).Add(item2);
			}
			else if (sb is SpriteBatch)
			{
				((SpriteBatch)sb).Draw(tex, vector3, frame, item.GetAlpha(lightColor), itemRotation, vector2, itemScale, spriteEffects, 0f);
			}
			if (wepColor != default(Color))
			{
				if (sb is List<DrawData>)
				{
					DrawData item3;
					item3..ctor(tex, vector3, frame, item.GetColor(wepColor), itemRotation, vector2, itemScale, spriteEffects, 0);
					item3.shader = shader;
					((List<DrawData>)sb).Add(item3);
				}
				else if (sb is SpriteBatch)
				{
					((SpriteBatch)sb).Draw(tex, vector3, frame, item.GetColor(wepColor), itemRotation, vector2, itemScale, spriteEffects, 0f);
				}
			}
			try
			{
				if (type != num)
				{
					item.type = type;
				}
			}
			catch
			{
			}
		}

		public static void DrawProjectileSpear(object sb, Texture2D texture, int shader, Projectile p, Color? overrideColor = null, float offsetX = 0f, float offsetY = 0f)
		{
			offsetX += (float)(-(float)texture.Width) * 0.5f;
			Color color = (overrideColor != null) ? overrideColor.Value : p.GetAlpha(BaseDrawing.GetLightColor(Main.player[p.owner].Center));
			Vector2 vector;
			vector..ctor((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			offsetY -= Main.player[p.owner].gfxOffY;
			Vector2 vector2 = BaseUtility.RotateVector(p.Center, p.Center + new Vector2((p.direction == -1) ? offsetX : offsetY, (p.direction == 1) ? offsetX : offsetY), p.rotation - 2.355f) - p.Center;
			if (sb is List<DrawData>)
			{
				DrawData item;
				item..ctor(texture, p.Center - Main.screenPosition + vector2, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), color, p.rotation, vector, p.scale, (p.direction == -1) ? 1 : 0, 0);
				item.shader = shader;
				((List<DrawData>)sb).Add(item);
				return;
			}
			if (sb is SpriteBatch)
			{
				((SpriteBatch)sb).Draw(texture, p.Center - Main.screenPosition + vector2, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), color, p.rotation, vector, p.scale, (p.direction == -1) ? 1 : 0, 0f);
			}
		}

		public static void DrawAura(object sb, Texture2D texture, int shader, Entity codable, float auraPercent, float distanceScalar = 1f, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
		{
			int framecount = (codable is NPC) ? Main.npcFrameCount[((NPC)codable).type] : 1;
			Rectangle frame = (codable is NPC) ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Height, texture.Width);
			float scale = (codable is NPC) ? ((NPC)codable).scale : ((Projectile)codable).scale;
			float rotation = (codable is NPC) ? ((NPC)codable).rotation : ((Projectile)codable).rotation;
			int direction = (codable is NPC) ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection;
			float num = (codable is NPC) ? ((NPC)codable).gfxOffY : 0f;
			BaseDrawing.DrawAura(sb, texture, shader, codable.position + new Vector2(0f, num), codable.width, codable.height, auraPercent, distanceScalar, scale, rotation, direction, framecount, frame, offsetX, offsetY, overrideColor);
		}

		public static void DrawAura(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float auraPercent, float distanceScalar = 1f, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default(Rectangle), float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
		{
			Color value = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(position + new Vector2((float)width * 0.5f, (float)height * 0.5f));
			float num = auraPercent * 5f * distanceScalar;
			float num2 = MathHelper.Lerp(0.8f, 0.2f, auraPercent);
			value.R = (byte)((float)value.R * num2);
			value.G = (byte)((float)value.G * num2);
			value.B = (byte)((float)value.B * num2);
			value.A = (byte)((float)value.A * num2);
			Vector2 position2 = position;
			for (int i = 0; i < 4; i++)
			{
				float num3 = offsetX;
				float num4 = offsetY;
				switch (i)
				{
				case 0:
					num3 += num;
					break;
				case 1:
					num3 -= num;
					break;
				case 2:
					num4 += num;
					break;
				case 3:
					num4 -= num;
					break;
				}
				position2..ctor(position.X + num3, position.Y + num4);
				BaseDrawing.DrawTexture(sb, texture, shader, position2, width, height, scale, rotation, direction, framecount, frame, new Color?(value), false, default(Vector2));
			}
		}

		public static void DrawYoyoLine(SpriteBatch sb, Projectile projectile, Texture2D overrideTex = null, Color? overrideColor = null)
		{
			BaseDrawing.DrawYoyoLine(sb, projectile, Main.player[projectile.owner], projectile.Center, Main.player[projectile.owner].MountedCenter, overrideTex, overrideColor);
		}

		public static void DrawYoyoLine(SpriteBatch sb, Projectile projectile, Entity owner, Vector2 yoyoLoc, Vector2 connectionLoc, Texture2D overrideTex = null, Color? overrideColor = null)
		{
			Vector2 vector = connectionLoc;
			if (owner is Player)
			{
				vector.Y += Main.player[projectile.owner].gfxOffY;
			}
			float num = yoyoLoc.X - vector.X;
			float num2 = yoyoLoc.Y - vector.Y;
			Math.Sqrt((double)(num * num + num2 * num2));
			float num3 = (float)Math.Atan2((double)num2, (double)num) - 1.57f;
			if (owner is Player && !projectile.counterweight)
			{
				int num4 = -1;
				if (projectile.position.X + (float)(projectile.width / 2) < Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
				{
					num4 = 1;
				}
				num4 *= -1;
				((Player)owner).itemRotation = (float)Math.Atan2((double)(num2 * (float)num4), (double)(num * (float)num4));
			}
			bool flag = true;
			if (num == 0f && num2 == 0f)
			{
				flag = false;
			}
			else
			{
				float num5 = (float)Math.Sqrt((double)(num * num + num2 * num2));
				num5 = 12f / num5;
				num *= num5;
				num2 *= num5;
				vector.X -= num * 0.1f;
				vector.Y -= num2 * 0.1f;
				num = yoyoLoc.X - vector.X;
				num2 = yoyoLoc.Y - vector.Y;
			}
			while (flag)
			{
				float num6 = 12f;
				float num7 = (float)Math.Sqrt((double)(num * num + num2 * num2));
				float num8 = num7;
				if (float.IsNaN(num7) || float.IsNaN(num8))
				{
					flag = false;
				}
				else
				{
					if (num7 < 20f)
					{
						num6 = num7 - 8f;
						flag = false;
					}
					num7 = 12f / num7;
					num *= num7;
					num2 *= num7;
					vector.X += num;
					vector.Y += num2;
					num = yoyoLoc.X - vector.X;
					num2 = yoyoLoc.Y - vector.Y;
					if (num8 > 12f)
					{
						float num9 = 0.3f;
						float num10 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
						if (num10 > 16f)
						{
							num10 = 16f;
						}
						num10 = 1f - num10 / 16f;
						num9 *= num10;
						num10 = num8 / 80f;
						if (num10 > 1f)
						{
							num10 = 1f;
						}
						num9 *= num10;
						if (num9 < 0f)
						{
							num9 = 0f;
						}
						num9 *= num10;
						num9 *= 0.5f;
						if (num2 > 0f)
						{
							num2 *= 1f + num9;
							num *= 1f - num9;
						}
						else
						{
							num10 = Math.Abs(projectile.velocity.X) / 3f;
							if (num10 > 1f)
							{
								num10 = 1f;
							}
							num10 -= 0.5f;
							num9 *= num10;
							if (num9 > 0f)
							{
								num9 *= 2f;
							}
							num2 *= 1f + num9;
							num *= 1f - num9;
						}
					}
					num3 = (float)Math.Atan2((double)num2, (double)num) - 1.57f;
					int stringColor = Main.player[projectile.owner].stringColor;
					Color color = (overrideColor != null && stringColor <= 0) ? overrideColor.Value : WorldGen.paintColor(stringColor);
					if (color.R < 75)
					{
						color.R = 75;
					}
					if (color.G < 75)
					{
						color.G = 75;
					}
					if (color.B < 75)
					{
						color.B = 75;
					}
					if (stringColor == 13)
					{
						color..ctor(20, 20, 20);
					}
					else if (stringColor == 14 || stringColor == 0)
					{
						color..ctor(200, 200, 200);
					}
					else if (stringColor == 28)
					{
						color..ctor(163, 116, 91);
					}
					else if (stringColor == 27)
					{
						color..ctor(Main.DiscoR, Main.DiscoG, Main.DiscoB);
					}
					color.A = (byte)((float)color.A * 0.4f);
					float num11 = 0.5f;
					if (overrideColor == null)
					{
						color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f), color);
						color..ctor((int)((byte)((float)color.R * num11)), (int)((byte)((float)color.G * num11)), (int)((byte)((float)color.B * num11)), (int)((byte)((float)color.A * num11)));
					}
					Texture2D texture2D = (overrideTex != null) ? overrideTex : Main.fishingLineTexture;
					Vector2 vector2;
					vector2..ctor((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.5f);
					Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(vector.X - Main.screenPosition.X + vector2.X, vector.Y - Main.screenPosition.Y + vector2.Y) - new Vector2(6f, 0f), new Rectangle?(new Rectangle(0, 0, texture2D.Width, (int)num6)), color, num3, new Vector2((float)texture2D.Width * 0.5f, 0f), 1f, 0, 0f);
				}
			}
		}

		public static void DrawFishingLine(SpriteBatch sb, Projectile projectile, Vector2 rodLoc, Vector2 bobberLoc, Texture2D overrideTex = null, Color? overrideColor = null)
		{
			Player player = Main.player[projectile.owner];
			if (projectile.bobber && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].holdStyle > 0)
			{
				float num = player.MountedCenter.X;
				float num2 = player.MountedCenter.Y;
				num2 += Main.player[projectile.owner].gfxOffY;
				int type = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type;
				float gravDir = Main.player[projectile.owner].gravDir;
				num += rodLoc.X * (float)Main.player[projectile.owner].direction;
				if (Main.player[projectile.owner].direction < 0)
				{
					num -= 13f;
				}
				num2 -= rodLoc.Y * gravDir;
				if (gravDir == -1f)
				{
					num2 -= 12f;
				}
				Vector2 vector;
				vector..ctor(num, num2);
				vector = Main.player[projectile.owner].RotatedRelativePoint(vector + new Vector2(8f), true) - new Vector2(8f);
				float num3 = projectile.position.X + (float)projectile.width * 0.5f - vector.X;
				float num4 = projectile.position.Y + (float)projectile.height * 0.5f - vector.Y;
				num3 += bobberLoc.X;
				num4 += bobberLoc.Y;
				Math.Sqrt((double)(num3 * num3 + num4 * num4));
				float num5 = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
				bool flag = true;
				if (num3 == 0f && num4 == 0f)
				{
					flag = false;
				}
				else
				{
					float num6 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
					num6 = 12f / num6;
					num3 *= num6;
					num4 *= num6;
					vector.X -= num3;
					vector.Y -= num4;
					num3 = projectile.position.X + (float)projectile.width * 0.5f - vector.X;
					num4 = projectile.position.Y + (float)projectile.height * 0.5f - vector.Y;
				}
				while (flag)
				{
					float num7 = 12f;
					float num8 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
					float num9 = num8;
					if (float.IsNaN(num8) || float.IsNaN(num9))
					{
						flag = false;
					}
					else
					{
						if (num8 < 20f)
						{
							num7 = num8 - 8f;
							flag = false;
						}
						num8 = 12f / num8;
						num3 *= num8;
						num4 *= num8;
						vector.X += num3;
						vector.Y += num4;
						num3 = projectile.position.X + (float)projectile.width * 0.5f - vector.X;
						num4 = projectile.position.Y + (float)projectile.height * 0.1f - vector.Y;
						if (num9 > 12f)
						{
							float num10 = 0.3f;
							float num11 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
							if (num11 > 16f)
							{
								num11 = 16f;
							}
							num11 = 1f - num11 / 16f;
							num10 *= num11;
							num11 = num9 / 80f;
							if (num11 > 1f)
							{
								num11 = 1f;
							}
							num10 *= num11;
							if (num10 < 0f)
							{
								num10 = 0f;
							}
							num11 = 1f - projectile.localAI[0] / 100f;
							num10 *= num11;
							if (num4 > 0f)
							{
								num4 *= 1f + num10;
								num3 *= 1f - num10;
							}
							else
							{
								num11 = Math.Abs(projectile.velocity.X) / 3f;
								if (num11 > 1f)
								{
									num11 = 1f;
								}
								num11 -= 0.5f;
								num10 *= num11;
								if (num10 > 0f)
								{
									num10 *= 2f;
								}
								num4 *= 1f + num10;
								num3 *= 1f - num10;
							}
						}
						num5 = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
						Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f), (overrideColor != null) ? overrideColor.Value : new Color(200, 200, 200, 100));
						Texture2D texture2D = (overrideTex != null) ? overrideTex : Main.fishingLineTexture;
						Vector2 vector2;
						vector2..ctor((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.5f);
						sb.Draw(texture2D, new Vector2(vector.X - Main.screenPosition.X + vector2.X * 0.5f, vector.Y - Main.screenPosition.Y + vector2.Y * 0.5f), new Rectangle?(new Rectangle(0, 0, texture2D.Width, (int)num7)), color, num5, new Vector2((float)texture2D.Width * 0.5f, 0f), 1f, 0, 0f);
					}
				}
			}
		}

		public static void DrawAfterimage(object sb, Texture2D texture, int shader, Entity codable, float distanceScalar = 1f, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null, Rectangle? overrideFrame = null, int overrideFrameCount = 0)
		{
			int framecount = (overrideFrameCount > 0) ? overrideFrameCount : ((codable is NPC) ? Main.npcFrameCount[((NPC)codable).type] : 1);
			Rectangle frame = (overrideFrame != null) ? overrideFrame.Value : ((codable is NPC) ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height));
			float scale = (codable is NPC) ? ((NPC)codable).scale : ((Projectile)codable).scale;
			float rotation = (codable is NPC) ? ((NPC)codable).rotation : ((Projectile)codable).rotation;
			int direction = (codable is NPC) ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection;
			Vector2[] oldPoints = new Vector2[]
			{
				codable.velocity
			};
			if (useOldPos)
			{
				oldPoints = ((codable is NPC) ? ((NPC)codable).oldPos : ((Projectile)codable).oldPos);
			}
			float num = (codable is NPC) ? ((NPC)codable).gfxOffY : 0f;
			BaseDrawing.DrawAfterimage(sb, texture, shader, codable.position + new Vector2(0f, num), codable.width, codable.height, oldPoints, scale, rotation, direction, framecount, frame, distanceScalar, sizeScalar, imageCount, useOldPos, offsetX, offsetY, overrideColor);
		}

		public static void DrawAfterimage(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, Vector2[] oldPoints, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default(Rectangle), float distanceScalar = 1f, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
		{
			new Vector2((float)(texture.Width / 2), (float)(texture.Height / framecount / 2));
			Color color = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(position + new Vector2((float)width * 0.5f, (float)height * 0.5f));
			Vector2 vector = default(Vector2);
			Vector2 vector2 = position;
			Vector2 vector3;
			vector3..ctor(offsetX, offsetY);
			for (int i = 1; i <= imageCount; i++)
			{
				scale *= sizeScalar;
				Color value = color;
				value.R = (byte)((int)value.R * (imageCount + 3 - i) / (imageCount + 9));
				value.G = (byte)((int)value.G * (imageCount + 3 - i) / (imageCount + 9));
				value.B = (byte)((int)value.B * (imageCount + 3 - i) / (imageCount + 9));
				value.A = (byte)((int)value.A * (imageCount + 3 - i) / (imageCount + 9));
				if (useOldPos)
				{
					position = Vector2.Lerp(vector2, (i - 1 >= oldPoints.Length) ? oldPoints[oldPoints.Length - 1] : oldPoints[i - 1], distanceScalar);
					BaseDrawing.DrawTexture(sb, texture, shader, position + vector3, width, height, scale, rotation, direction, framecount, frame, new Color?(value), false, default(Vector2));
				}
				else
				{
					Vector2 vector4 = (i - 1 >= oldPoints.Length) ? oldPoints[oldPoints.Length - 1] : oldPoints[i - 1];
					vector += vector4 * distanceScalar;
					BaseDrawing.DrawTexture(sb, texture, shader, position + vector3 - vector, width, height, scale, rotation, direction, framecount, frame, new Color?(value), false, default(Vector2));
				}
			}
		}

		public static void DrawChain(object sb, Texture2D texture, int shader, Vector2 start, Vector2 end, float Jump = 0f, Color? overrideColor = null, float scale = 1f, bool drawEndsUnder = false, Func<Texture2D, Vector2, Vector2, Vector2, Rectangle, Color, float, float, int, bool> OnDrawTex = null)
		{
			BaseDrawing.DrawChain(sb, new Texture2D[]
			{
				texture,
				texture,
				texture
			}, shader, start, end, Jump, overrideColor, scale, drawEndsUnder, OnDrawTex);
		}

		public static void DrawChain(object sb, Texture2D[] textures, int shader, Vector2 start, Vector2 end, float Jump = 0f, Color? overrideColor = null, float scale = 1f, bool drawEndsUnder = false, Func<Texture2D, Vector2, Vector2, Vector2, Rectangle, Color, float, float, int, bool> OnDrawTex = null)
		{
			if (Jump <= 0f)
			{
				Jump = ((float)textures[1].Height - 2f) * scale;
			}
			Vector2 vector = end - start;
			vector.Normalize();
			float length = Vector2.Distance(start, end);
			float Way = 0f;
			float rotation = BaseUtility.RotationTo(start, end) - 1.57f;
			int num = 0;
			int maxTextures = textures.Length - 2;
			int num2 = 0;
			while (Way < length)
			{
				Action action = delegate()
				{
					if (textures[0] != null && Way == 0f)
					{
						float num5 = (float)textures[0].Width;
						float num6 = (float)textures[0].Height;
						Vector2 vector4 = new Vector2(num5 / 2f, num6 / 2f) * scale;
						Vector2 vector5 = start - Main.screenPosition + vector4;
						Color color2 = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(start + vector4);
						if (OnDrawTex == null || OnDrawTex.Invoke(textures[0], start + vector4, vector5 - vector4, vector4, new Rectangle(0, 0, (int)num5, (int)num6), color2, rotation, scale, -1))
						{
							if (sb is List<DrawData>)
							{
								DrawData item2;
								item2..ctor(textures[0], vector5 - vector4, new Rectangle?(new Rectangle(0, 0, (int)num5, (int)num6)), color2, rotation, vector4, scale, 0, 0);
								item2.shader = shader;
								((List<DrawData>)sb).Add(item2);
							}
							else if (sb is SpriteBatch)
							{
								((SpriteBatch)sb).Draw(textures[0], vector5 - vector4, new Rectangle?(new Rectangle(0, 0, (int)num5, (int)num6)), color2, rotation, vector4, scale, 0, 0f);
							}
						}
					}
					if (textures[maxTextures + 1] != null && Way + Jump >= length)
					{
						float num7 = (float)textures[maxTextures + 1].Width;
						float num8 = (float)textures[maxTextures + 1].Height;
						Vector2 vector6 = new Vector2(num7 / 2f, num8 / 2f) * scale;
						Vector2 vector7 = end - Main.screenPosition + vector6;
						Color color3 = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(end + vector6);
						if (OnDrawTex != null && !OnDrawTex.Invoke(textures[maxTextures + 1], end + vector6, vector7 - vector6, vector6, new Rectangle(0, 0, (int)num7, (int)num8), color3, rotation, scale, -2))
						{
							return;
						}
						if (sb is List<DrawData>)
						{
							DrawData item3;
							item3..ctor(textures[maxTextures + 1], vector7 - vector6, new Rectangle?(new Rectangle(0, 0, (int)num7, (int)num8)), color3, rotation, vector6, scale, 0, 0);
							item3.shader = shader;
							((List<DrawData>)sb).Add(item3);
							return;
						}
						if (sb is SpriteBatch)
						{
							((SpriteBatch)sb).Draw(textures[maxTextures + 1], vector7 - vector6, new Rectangle?(new Rectangle(0, 0, (int)num7, (int)num8)), color3, rotation, vector6, scale, 0, 0f);
						}
					}
				};
				float num3 = (float)textures[1].Width;
				float num4 = (float)textures[1].Height;
				Vector2 vector2 = new Vector2(num3 / 2f, num4 / 2f) * scale;
				Vector2 vector3 = start + vector * Way + vector2;
				if (BaseDrawing.InDrawZone(vector3, false))
				{
					vector3 -= Main.screenPosition;
					if ((Way == 0f || Way + Jump >= length) && drawEndsUnder)
					{
						action();
					}
					Color color = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(start + vector * Way + vector2);
					num++;
					if (num >= maxTextures)
					{
						num = 0;
					}
					if (OnDrawTex == null || OnDrawTex.Invoke(textures[num + 1], start + vector * Way + vector2, vector3 - vector2, vector2, new Rectangle(0, 0, (int)num3, (int)num4), color, rotation, scale, num2))
					{
						if (sb is List<DrawData>)
						{
							DrawData item;
							item..ctor(textures[num + 1], vector3 - vector2, new Rectangle?(new Rectangle(0, 0, (int)num3, (int)num4)), color, rotation, vector2, scale, 0, 0);
							item.shader = shader;
							((List<DrawData>)sb).Add(item);
						}
						else if (sb is SpriteBatch)
						{
							((SpriteBatch)sb).Draw(textures[num + 1], vector3 - vector2, new Rectangle?(new Rectangle(0, 0, (int)num3, (int)num4)), color, rotation, vector2, scale, 0, 0f);
						}
					}
					num2++;
					if ((Way == 0f || Way + Jump >= length) && !drawEndsUnder)
					{
						action();
					}
				}
				Way += Jump;
			}
		}

		public static void DrawVectorChain(object sb, Texture2D[] textures, int shader, Vector2[] chain, float Jump = 0f, Color? overrideColor = null, float scale = 1f, bool drawEndsUnder = false, Func<Texture2D, Vector2, Vector2, Vector2, Rectangle, Color, float, float, int, bool> OnDrawTex = null)
		{
			if (Jump <= 0f)
			{
				Jump = ((float)textures[1].Height - 2f) * scale;
			}
			float length = 0f;
			for (int i = 0; i < chain.Length - 1; i++)
			{
				length += Vector2.Distance(chain[i], chain[i + 1]);
			}
			Vector2 start = chain[0];
			Vector2 end = chain[chain.Length - 1];
			Vector2 vector = end - start;
			vector.Normalize();
			float Way = 0f;
			float rotation = BaseUtility.RotationTo(chain[0], chain[1]) - 1.57f;
			int num = 0;
			int maxTextures = textures.Length - 2;
			int num2 = 0;
			Vector2 vector2 = chain[0];
			while (Way < length)
			{
				Action action = delegate()
				{
					if (textures[0] != null && Way == 0f)
					{
						float num5 = (float)textures[0].Width;
						float num6 = (float)textures[0].Height;
						Vector2 vector6 = new Vector2(num5 / 2f, num6 / 2f) * scale;
						Vector2 vector7 = start - Main.screenPosition + vector6;
						Color color2 = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(start + vector6);
						if (OnDrawTex == null || OnDrawTex.Invoke(textures[0], start + vector6, vector7 - vector6, vector6, new Rectangle(0, 0, (int)num5, (int)num6), color2, rotation, scale, -1))
						{
							if (sb is List<DrawData>)
							{
								DrawData item2;
								item2..ctor(textures[0], vector7 - vector6, new Rectangle?(new Rectangle(0, 0, (int)num5, (int)num6)), color2, rotation, vector6, scale, 0, 0);
								item2.shader = shader;
								((List<DrawData>)sb).Add(item2);
							}
							else if (sb is SpriteBatch)
							{
								((SpriteBatch)sb).Draw(textures[0], vector7 - vector6, new Rectangle?(new Rectangle(0, 0, (int)num5, (int)num6)), color2, rotation, vector6, scale, 0, 0f);
							}
						}
					}
					if (textures[maxTextures + 1] != null && Way + Jump >= length)
					{
						float num7 = (float)textures[maxTextures + 1].Width;
						float num8 = (float)textures[maxTextures + 1].Height;
						Vector2 vector8 = new Vector2(num7 / 2f, num8 / 2f) * scale;
						Vector2 vector9 = end - Main.screenPosition + vector8;
						Color color3 = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(end + vector8);
						if (OnDrawTex != null && !OnDrawTex.Invoke(textures[maxTextures + 1], end + vector8, vector9 - vector8, vector8, new Rectangle(0, 0, (int)num7, (int)num8), color3, rotation, scale, -2))
						{
							return;
						}
						if (sb is List<DrawData>)
						{
							DrawData item3;
							item3..ctor(textures[maxTextures + 1], vector9 - vector8, new Rectangle?(new Rectangle(0, 0, (int)num7, (int)num8)), color3, rotation, vector8, scale, 0, 0);
							item3.shader = shader;
							((List<DrawData>)sb).Add(item3);
							return;
						}
						if (sb is SpriteBatch)
						{
							((SpriteBatch)sb).Draw(textures[maxTextures + 1], vector9 - vector8, new Rectangle?(new Rectangle(0, 0, (int)num7, (int)num8)), color3, rotation, vector8, scale, 0, 0f);
						}
					}
				};
				float num3 = (float)textures[1].Width;
				float num4 = (float)textures[1].Height;
				Vector2 vector3 = new Vector2(num3 / 2f, num4 / 2f) * scale;
				Vector2 vector4 = BaseUtility.MultiLerpVector(Way / length, chain) + vector3;
				Vector2 vector5 = BaseUtility.MultiLerpVector(Math.Max(length - 1f, Way + 1f) / length, chain) + vector3;
				if (vector4 != vector5)
				{
					rotation = BaseUtility.RotationTo(vector4, vector5) - 1.57f;
				}
				if (BaseDrawing.InDrawZone(vector4, false))
				{
					vector4 -= Main.screenPosition;
					if ((Way == 0f || Way + Jump >= length) && drawEndsUnder)
					{
						action();
					}
					Color color = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(start + vector * Way + vector3);
					num++;
					if (num >= maxTextures)
					{
						num = 0;
					}
					if (OnDrawTex == null || OnDrawTex.Invoke(textures[num + 1], start + vector * Way + vector3, vector4 - vector3, vector3, new Rectangle(0, 0, (int)num3, (int)num4), color, rotation, scale, num2))
					{
						if (sb is List<DrawData>)
						{
							DrawData item;
							item..ctor(textures[num + 1], vector4 - vector3, new Rectangle?(new Rectangle(0, 0, (int)num3, (int)num4)), color, rotation, vector3, scale, 0, 0);
							item.shader = shader;
							((List<DrawData>)sb).Add(item);
						}
						else if (sb is SpriteBatch)
						{
							((SpriteBatch)sb).Draw(textures[num + 1], vector4 - vector3, new Rectangle?(new Rectangle(0, 0, (int)num3, (int)num4)), color, rotation, vector3, scale, 0, 0f);
						}
					}
					num2++;
					if ((Way == 0f || Way + Jump >= length) && !drawEndsUnder)
					{
						action();
					}
				}
				Way += Jump;
			}
		}

		public static void DrawTexture(object sb, Texture2D texture, int shader, Entity codable, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
		{
			BaseDrawing.DrawTexture(sb, texture, shader, codable, 1, overrideColor, drawCentered, overrideOrigin);
		}

		public static void DrawTexture(object sb, Texture2D texture, int shader, Entity codable, int framecountX, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
		{
			Color value = (overrideColor != null) ? overrideColor.Value : ((codable is Item) ? ((Item)codable).GetAlpha(BaseDrawing.GetLightColor(codable.Center)) : ((codable is NPC) ? BaseDrawing.GetNPCColor((NPC)codable, new Vector2?(codable.Center), false, 0f) : ((codable is Projectile) ? ((Projectile)codable).GetAlpha(BaseDrawing.GetLightColor(codable.Center)) : BaseDrawing.GetLightColor(codable.Center))));
			int framecount = (codable is Item) ? 1 : ((codable is NPC) ? Main.npcFrameCount[((NPC)codable).type] : 1);
			Rectangle frame = (codable is NPC) ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height);
			float scale = (codable is Item) ? ((Item)codable).scale : ((codable is NPC) ? ((NPC)codable).scale : ((Projectile)codable).scale);
			float rotation = (codable is Item) ? 0f : ((codable is NPC) ? ((NPC)codable).rotation : ((Projectile)codable).rotation);
			int direction = (codable is Item) ? 1 : ((codable is NPC) ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection);
			float num = (codable is NPC) ? ((NPC)codable).gfxOffY : 0f;
			BaseDrawing.DrawTexture(sb, texture, shader, codable.position + new Vector2(0f, num), codable.width, codable.height, scale, rotation, direction, framecount, framecountX, frame, new Color?(value), drawCentered, overrideOrigin);
		}

		public static void DrawTexture(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float scale, float rotation, int direction, int framecount, Rectangle frame, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
		{
			BaseDrawing.DrawTexture(sb, texture, shader, position, width, height, scale, rotation, direction, framecount, 1, frame, overrideColor, drawCentered, overrideOrigin);
		}

		public static void DrawTexture(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float scale, float rotation, int direction, int framecount, int framecountX, Rectangle frame, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
		{
			Vector2 vector = (overrideOrigin != default(Vector2)) ? overrideOrigin : new Vector2((float)(frame.Width / framecountX / 2), (float)(texture.Height / framecount / 2));
			Color color = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(position + new Vector2((float)width * 0.5f, (float)height * 0.5f));
			if (sb is List<DrawData>)
			{
				DrawData item;
				item..ctor(texture, BaseDrawing.GetDrawPosition(position, vector, width, height, texture.Width, texture.Height, frame, framecount, framecountX, scale, drawCentered), new Rectangle?(frame), color, rotation, vector, scale, (direction == 1) ? 1 : 0, 0);
				item.shader = shader;
				((List<DrawData>)sb).Add(item);
				return;
			}
			if (sb is SpriteBatch)
			{
				bool flag = shader > 0;
				if (flag)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(1, BlendState.AlphaBlend);
					GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);
				}
				((SpriteBatch)sb).Draw(texture, BaseDrawing.GetDrawPosition(position, vector, width, height, texture.Width, texture.Height, frame, framecount, framecountX, scale, drawCentered), new Rectangle?(frame), color, rotation, vector, scale, (direction == 1) ? 1 : 0, 0f);
				if (flag)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(0, BlendState.AlphaBlend);
				}
			}
		}

		public static void DrawHitbox(SpriteBatch sb, Rectangle hitbox, Color? overrideColor = null)
		{
			Vector2 vector = default(Vector2);
			Color color = (overrideColor != null) ? overrideColor.Value : Color.White;
			Vector2 vector2 = new Vector2((float)hitbox.Left, (float)hitbox.Top) - Main.screenPosition;
			sb.Draw(Main.magicPixel, vector2, new Rectangle?(hitbox), color, 0f, vector, 1f, 0, 0f);
		}

		public static void DrawTileTexture(SpriteBatch sb, Texture2D texture, int x, int y, bool slopeDraw = true, bool flipTex = false, bool ignoreHalfBricks = false, bool? overrideHalfBrick = null, Func<Color, Color> overrideColor = null, Vector2 offset = default(Vector2))
		{
			Tile tile = Main.tile[x, y];
			int frameX = (int)((tile != null && tile.active()) ? tile.frameX : 0);
			int frameY = (int)((tile != null && tile.active()) ? tile.frameY : 0);
			BaseDrawing.DrawTileTexture(sb, texture, x, y, 16, 16, frameX, frameY, slopeDraw, flipTex, ignoreHalfBricks, overrideHalfBrick, overrideColor, offset);
		}

		public static void DrawTileTexture(SpriteBatch sb, Texture2D texture, int x, int y, int fwidth = 16, int fheight = 16, int frameX = 0, int frameY = 0, bool slopeDraw = true, bool flipTex = false, bool ignoreHalfBricks = false, bool? overrideHalfBrick = null, Func<Color, Color> overrideColor = null, Vector2 offset = default(Vector2))
		{
			Tile tile = Main.tile[x, y];
			bool flag = (overrideHalfBrick != null) ? overrideHalfBrick.Value : tile.halfBrick();
			int num = flag ? 8 : 0;
			Color color = Lighting.GetColor(x, y);
			Vector2 drawOffset = (Main.drawToScreen ? default(Vector2) : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange)) + offset;
			if (tile.inActive())
			{
				color = tile.actColor(color);
			}
			SpriteEffects spriteEffects = flipTex ? 1 : 0;
			Vector2 tileDrawPosition = BaseDrawing.GetTileDrawPosition(x, y, fwidth, fheight, drawOffset);
			int num2 = (int)(255f * (1f - Main.gfxQuality) + 30f * Main.gfxQuality);
			int num3 = (int)(50f * (1f - Main.gfxQuality) + 2f * Main.gfxQuality);
			if (slopeDraw && tile.slope() > 0)
			{
				bool flag2 = tile.rightSlope();
				bool flag3 = tile.topSlope();
				for (int i = 0; i < 8; i++)
				{
					int num4 = flag2 ? (i * 2) : (16 - i * 2 - 2);
					int num5 = flag3 ? (i * 2) : 0;
					int num6 = num4;
					int num7 = 14 - i * 2;
					sb.Draw(texture, tileDrawPosition + new Vector2((float)num4, (float)num5), new Rectangle?(new Rectangle(frameX + num6, frameY, 2, num7)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
				}
				if (flag3)
				{
					sb.Draw(texture, tileDrawPosition + new Vector2(0f, 14f), new Rectangle?(new Rectangle(frameX, frameY + 14, 16, 2)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					return;
				}
				sb.Draw(texture, tileDrawPosition, new Rectangle?(new Rectangle(frameX, frameY, 16, 2)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
				return;
			}
			else if (!ignoreHalfBricks && Main.tileSolid[(int)tile.type] && !flag && (Main.tile[x - 1, y].halfBrick() || Main.tile[x + 1, y].halfBrick()))
			{
				if (Main.tile[x - 1, y].halfBrick() && Main.tile[x + 1, y].halfBrick())
				{
					sb.Draw(texture, tileDrawPosition + new Vector2(0f, 8f), new Rectangle?(new Rectangle(frameX, frameY + 8, fwidth, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					sb.Draw(texture, tileDrawPosition, new Rectangle?(new Rectangle(126, 0, 16, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					return;
				}
				if (Main.tile[x - 1, y].halfBrick())
				{
					sb.Draw(texture, tileDrawPosition + new Vector2(0f, 8f), new Rectangle?(new Rectangle(frameX, frameY + 8, fwidth, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					sb.Draw(texture, tileDrawPosition + new Vector2(4f, 0f), new Rectangle?(new Rectangle(frameX + 4, frameY, fwidth - 4, fheight)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					sb.Draw(texture, tileDrawPosition, new Rectangle?(new Rectangle(126, 0, 4, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					return;
				}
				if (Main.tile[x + 1, y].halfBrick())
				{
					sb.Draw(texture, tileDrawPosition + new Vector2(0f, 8f), new Rectangle?(new Rectangle(frameX, frameY + 8, fwidth, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					sb.Draw(texture, tileDrawPosition, new Rectangle?(new Rectangle(frameX, frameY, fwidth - 4, fheight)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					sb.Draw(texture, tileDrawPosition + new Vector2(12f, 0f), new Rectangle?(new Rectangle(138, 0, 4, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					return;
				}
				sb.Draw(texture, tileDrawPosition, new Rectangle?(new Rectangle(frameX, frameY, fwidth, fheight)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
				return;
			}
			else if (Lighting.lightMode < 2 && Main.tileSolid[(int)tile.type] && !flag && !tile.inActive())
			{
				if ((int)color.R > num2 || (double)color.G > (double)num2 * 1.1 || (double)color.B > (double)num2 * 1.2)
				{
					Color[] array = new Color[9];
					Lighting.GetColor9Slice(x, y, ref array);
					for (int j = 0; j < 9; j++)
					{
						int num8 = 0;
						int num9 = 0;
						int num10 = 4;
						int num11 = 4;
						Color color2 = color;
						Color color3 = array[j];
						if (j == 1)
						{
							num10 = 8;
							num8 = 4;
						}
						else if (j == 2)
						{
							num8 = 12;
						}
						else if (j == 3)
						{
							num11 = 8;
							num9 = 4;
						}
						else if (j == 4)
						{
							num10 = 8;
							num11 = 8;
							num8 = 4;
							num9 = 4;
						}
						else if (j == 5)
						{
							num8 = 12;
							num9 = 4;
							num11 = 8;
						}
						else if (j == 6)
						{
							num9 = 12;
						}
						else if (j == 7)
						{
							num10 = 8;
							num11 = 4;
							num8 = 4;
							num9 = 12;
						}
						else if (j == 8)
						{
							num8 = 12;
							num9 = 12;
						}
						color2.R = (color.R + color3.R) / 2;
						color2.G = (color.G + color3.G) / 2;
						color2.B = (color.B + color3.B) / 2;
						sb.Draw(texture, tileDrawPosition + new Vector2((float)num8, (float)num9), new Rectangle?(new Rectangle(frameX + num8, frameY + num9, num10, num11)), (overrideColor != null) ? overrideColor(color2) : color2, 0f, default(Vector2), 1f, spriteEffects, 0f);
					}
					return;
				}
				if ((int)color.R > num3 || (double)color.G > (double)num3 * 1.1 || (double)color.B > (double)num3 * 1.2)
				{
					Color[] array2 = new Color[4];
					Lighting.GetColor4Slice(x, y, ref array2);
					for (int k = 0; k < 4; k++)
					{
						int num12 = 0;
						int num13 = 0;
						Color color4 = color;
						Color color5 = array2[k];
						if (k == 1)
						{
							num12 = 8;
						}
						if (k == 2)
						{
							num13 = 8;
						}
						if (k == 3)
						{
							num12 = 8;
							num13 = 8;
						}
						color4.R = (color.R + color5.R) / 2;
						color4.G = (color.G + color5.G) / 2;
						color4.B = (color.B + color5.B) / 2;
						sb.Draw(texture, tileDrawPosition + new Vector2((float)num12, (float)num13), new Rectangle?(new Rectangle(frameX + num12, frameY + num13, 8, 8)), (overrideColor != null) ? overrideColor(color4) : color4, 0f, default(Vector2), 1f, spriteEffects, 0f);
					}
					return;
				}
				sb.Draw(texture, tileDrawPosition, new Rectangle?(new Rectangle(frameX, frameY, fwidth, fheight)), color, 0f, default(Vector2), 1f, spriteEffects, 0f);
				return;
			}
			else
			{
				if (num == 8 && (!Main.tile[x, y + 1].active() || !Main.tileSolid[(int)Main.tile[x, y + 1].type] || Main.tile[x, y + 1].halfBrick()))
				{
					sb.Draw(texture, tileDrawPosition + new Vector2(0f, (float)num), new Rectangle?(new Rectangle(frameX, frameY, fwidth, fheight - num - 4)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					sb.Draw(texture, tileDrawPosition + new Vector2(0f, 12f), new Rectangle?(new Rectangle(144, 66, fwidth, 4)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
					return;
				}
				sb.Draw(texture, tileDrawPosition + new Vector2(0f, (float)num), new Rectangle?(new Rectangle(frameX, frameY, fwidth, fheight)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, spriteEffects, 0f);
				return;
			}
		}

		public static void DrawWallTexture(SpriteBatch sb, Texture2D texture, int x, int y, bool drawOutline = false, Func<Color, Color> overrideColor = null, Vector2 offset = default(Vector2))
		{
			Tile tile = Main.tile[x, y];
			bool flag = tile != null && tile.wall > 0;
			int wallFrameX = flag ? tile.wallFrameX() : 0;
			int wallFrameY = flag ? tile.wallFrameY() : 0;
			int frameOffsetY = (int)(flag ? (Main.wallFrame[(int)tile.wall] * 180) : 0);
			BaseDrawing.DrawWallTexture(sb, texture, x, y, wallFrameX, wallFrameY, frameOffsetY, drawOutline, overrideColor, offset);
		}

		public static void DrawWallTexture(SpriteBatch sb, Texture2D texture, int x, int y, int wallFrameX, int wallFrameY, int frameOffsetY, bool drawOutline = false, Func<Color, Color> overrideColor = null, Vector2 offset = default(Vector2))
		{
			int num = (int)(255f * (1f - Main.gfxQuality) + 100f * Main.gfxQuality);
			int num2 = (int)(120f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
			Vector2 vector = (Main.drawToScreen ? default(Vector2) : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange)) + offset;
			int num3 = (int)((Main.tileColor.R + Main.tileColor.G + Main.tileColor.B) / 3);
			float num4 = (float)((double)num3 * 0.53) / 255f;
			if (Lighting.lightMode == 2)
			{
				float num5 = (float)(Main.tileColor.R - 12) / 255f;
			}
			else if (Lighting.lightMode == 3)
			{
				float num6 = (float)(num3 - 12) / 255f;
			}
			Color color = (overrideColor != null) ? overrideColor(default(Color)) : Lighting.GetColor(x, y);
			if (Lighting.lightMode < 2)
			{
				if ((int)color.R > num || (double)color.G > (double)num * 1.1 || (double)color.B > (double)num * 1.2)
				{
					Color[] array = new Color[9];
					Lighting.GetColor9Slice(x, y, ref array);
					for (int i = 0; i < 9; i++)
					{
						int num7 = 0;
						int num8 = 0;
						int num9 = 12;
						int num10 = 12;
						Color color2 = color;
						Color color3 = array[i];
						if (i == 1)
						{
							num9 = 8;
							num7 = 12;
						}
						if (i == 2)
						{
							num7 = 20;
						}
						if (i == 3)
						{
							num10 = 8;
							num8 = 12;
						}
						if (i == 4)
						{
							num9 = 8;
							num10 = 8;
							num7 = 12;
							num8 = 12;
						}
						if (i == 5)
						{
							num7 = 20;
							num8 = 12;
							num10 = 8;
						}
						if (i == 6)
						{
							num8 = 20;
						}
						if (i == 7)
						{
							num9 = 12;
							num7 = 12;
							num8 = 20;
						}
						if (i == 8)
						{
							num7 = 20;
							num8 = 20;
						}
						color2.R = (color.R + color3.R) / 2;
						color2.G = (color.G + color3.G) / 2;
						color2.B = (color.B + color3.B) / 2;
						sb.Draw(texture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8 + num7), (float)(y * 16 - (int)Main.screenPosition.Y - 8 + num8)) + vector, new Rectangle?(new Rectangle(wallFrameX + num7, wallFrameY + num8 + frameOffsetY, num9, num10)), (overrideColor != null) ? overrideColor(color2) : color2, 0f, default(Vector2), 1f, 0, 0f);
					}
				}
				else if ((int)color.R > num2 || (double)color.G > (double)num2 * 1.1 || (double)color.B > (double)num2 * 1.2)
				{
					Color[] array2 = new Color[4];
					Lighting.GetColor4Slice(x, y, ref array2);
					for (int j = 0; j < 4; j++)
					{
						int num11 = 0;
						int num12 = 0;
						Color color4 = color;
						Color color5 = array2[j];
						if (j == 1)
						{
							num11 = 16;
						}
						if (j == 2)
						{
							num12 = 16;
						}
						if (j == 3)
						{
							num11 = 16;
							num12 = 16;
						}
						color4.R = (color.R + color5.R) / 2;
						color4.G = (color.G + color5.G) / 2;
						color4.B = (color.B + color5.B) / 2;
						sb.Draw(texture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8 + num11), (float)(y * 16 - (int)Main.screenPosition.Y - 8 + num12)) + vector, new Rectangle?(new Rectangle(wallFrameX + num11, wallFrameY + num12 + frameOffsetY, 16, 16)), (overrideColor != null) ? overrideColor(color4) : color4, 0f, default(Vector2), 1f, 0, 0f);
					}
				}
				else
				{
					Rectangle value;
					value..ctor(wallFrameX, wallFrameY + frameOffsetY, 32, 32);
					sb.Draw(texture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8), (float)(y * 16 - (int)Main.screenPosition.Y - 8)) + vector, new Rectangle?(value), color, 0f, default(Vector2), 1f, 0, 0f);
				}
			}
			if (drawOutline && ((double)color.R > (double)num2 * 0.4 || (double)color.G > (double)num2 * 0.35 || (double)color.B > (double)num2 * 0.3))
			{
				bool flag = Main.tile[x - 1, y].wall > 0 && Main.wallBlend[(int)Main.tile[x - 1, y].wall] != Main.wallBlend[(int)Main.tile[x, y].wall];
				bool flag2 = Main.tile[x + 1, y].wall > 0 && Main.wallBlend[(int)Main.tile[x + 1, y].wall] != Main.wallBlend[(int)Main.tile[x, y].wall];
				bool flag3 = Main.tile[x, y - 1].wall > 0 && Main.wallBlend[(int)Main.tile[x, y - 1].wall] != Main.wallBlend[(int)Main.tile[x, y].wall];
				bool flag4 = Main.tile[x, y + 1].wall > 0 && Main.wallBlend[(int)Main.tile[x, y + 1].wall] != Main.wallBlend[(int)Main.tile[x, y].wall];
				if (flag)
				{
					sb.Draw(Main.wallOutlineTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y)) + vector, new Rectangle?(new Rectangle(0, 0, 2, 16)), color, 0f, default(Vector2), 1f, 0, 0f);
				}
				if (flag2)
				{
					sb.Draw(Main.wallOutlineTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X + 14), (float)(y * 16 - (int)Main.screenPosition.Y)) + vector, new Rectangle?(new Rectangle(14, 0, 2, 16)), color, 0f, default(Vector2), 1f, 0, 0f);
				}
				if (flag3)
				{
					sb.Draw(Main.wallOutlineTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y)) + vector, new Rectangle?(new Rectangle(0, 0, 16, 2)), color, 0f, default(Vector2), 1f, 0, 0f);
				}
				if (flag4)
				{
					sb.Draw(Main.wallOutlineTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y + 14)) + vector, new Rectangle?(new Rectangle(0, 14, 16, 2)), color, 0f, default(Vector2), 1f, 0, 0f);
				}
			}
		}

		public static Vector2 GetTileDrawPosition(int x, int y, int width, int height, Vector2 drawOffset)
		{
			return new Vector2((float)(x * 16 - (int)Main.screenPosition.X) - ((float)width - 16f) / 2f, (float)(y * 16 - (int)Main.screenPosition.Y)) + drawOffset;
		}

		public static Vector2 GetDrawPosition(Vector2 position, Vector2 origin, int width, int height, int texWidth, int texHeight, Rectangle frame, int framecount, float scale, bool drawCentered = false)
		{
			return BaseDrawing.GetDrawPosition(position, origin, width, height, texWidth, texHeight, frame, framecount, 1, scale, drawCentered);
		}

		public static Vector2 GetDrawPosition(Vector2 position, Vector2 origin, int width, int height, int texWidth, int texHeight, Rectangle frame, int framecount, int framecountX, float scale, bool drawCentered = false)
		{
			Vector2 vector;
			vector..ctor((float)((int)Main.screenPosition.X), (float)((int)Main.screenPosition.Y));
			if (drawCentered)
			{
				Vector2 vector2;
				vector2..ctor((float)(texWidth / framecountX / 2), (float)(texHeight / framecount / 2));
				return position + new Vector2((float)(width / 2), (float)(height / 2)) - vector2 * scale + origin * scale - vector;
			}
			return position - vector + new Vector2((float)(width / 2), (float)height) - new Vector2((float)(texWidth / framecountX / 2), (float)(texHeight / framecount)) * scale + origin * scale + new Vector2(0f, 5f);
		}

		public static void DrawPlayerTexture(object sb, Texture2D texture, int shader, Player drawPlayer, Vector2 ediPos, int locationType, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null, Rectangle? frameRect = null, float scaleOverride = 0f)
		{
			Vector2 locationPos = (locationType == 0) ? drawPlayer.headPosition : ((locationType == 1) ? drawPlayer.bodyPosition : drawPlayer.legPosition);
			float locationRot = (locationType == 0) ? drawPlayer.headRotation : ((locationType == 1) ? drawPlayer.bodyRotation : drawPlayer.legRotation);
			Rectangle locationFrame = (locationType == 0) ? drawPlayer.headFrame : ((locationType == 1) ? drawPlayer.bodyFrame : drawPlayer.legFrame);
			BaseDrawing.DrawPlayerTexture(sb, texture, shader, drawPlayer, ediPos, locationPos, locationRot, locationFrame, offsetX, offsetY, overrideColor, frameRect, scaleOverride);
		}

		public static void DrawPlayerTexture(object sb, Texture2D texture, int shader, Player drawPlayer, Vector2 ediPos, Vector2 locationPos, float locationRot, Rectangle locationFrame, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null, Rectangle? frameRect = null, float scaleOverride = 0f)
		{
			offsetX = ((drawPlayer.direction == -1) ? (-offsetX) : offsetX);
			offsetY += 4f;
			Vector2 vector;
			vector..ctor((float)locationFrame.Width * 0.5f, (float)locationFrame.Height * 0.5f);
			Color color = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetPlayerColor(drawPlayer, new Vector2?(drawPlayer.Center), false, 0f);
			Rectangle value = (frameRect != null) ? frameRect.Value : drawPlayer.bodyFrame;
			SpriteEffects spriteEffects = (drawPlayer.direction == -1) ? 1 : 0;
			if (drawPlayer.gravDir == -1f)
			{
				spriteEffects |= 2;
			}
			float num = (scaleOverride > 0f) ? scaleOverride : 1f;
			if (sb is List<DrawData>)
			{
				DrawData item;
				item..ctor(texture, new Vector2((float)((int)(ediPos.X - (float)((int)Main.screenPosition.X) - (float)(value.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(ediPos.Y - (float)((int)Main.screenPosition.Y) + (float)drawPlayer.height - (float)value.Height))) + new Vector2(offsetX * num, offsetY * num) + locationPos + vector, new Rectangle?(value), color, locationRot, vector, num, spriteEffects, 0);
				item.shader = shader;
				((List<DrawData>)sb).Add(item);
				return;
			}
			if (sb is SpriteBatch)
			{
				bool flag = shader > 0;
				if (flag)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(1, BlendState.AlphaBlend);
					GameShaders.Armor.ApplySecondary(shader, drawPlayer, null);
				}
				((SpriteBatch)sb).Draw(texture, new Vector2((float)((int)(ediPos.X - (float)((int)Main.screenPosition.X) - (float)(value.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(ediPos.Y - (float)((int)Main.screenPosition.Y) + (float)drawPlayer.height - (float)value.Height))) + new Vector2(offsetX * num, offsetY * num) + locationPos + vector, new Rectangle?(value), color, locationRot, vector, num, spriteEffects, 0f);
				if (flag)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(0, BlendState.AlphaBlend);
				}
			}
		}

		public static float GetYOffset(Player player)
		{
			return BaseDrawing.GetYOffset(player.bodyFrame, player.gravDir);
		}

		public static float GetYOffset(Rectangle frame, float gravDir = 0f)
		{
			int num = frame.Y / frame.Height;
			if (num != 7 && num != 8 && num != 9 && num != 14 && num != 15 && num != 16)
			{
				return 0f;
			}
			if (gravDir >= 0f)
			{
				return -2f;
			}
			return 2f;
		}

		public static bool InDrawZone(Vector2 vec, bool noScreenPos = false)
		{
			if ((int)Main.screenPosition.X - 300 != BaseDrawing.drawZoneRect.X || (int)Main.screenPosition.Y - 300 != BaseDrawing.drawZoneRect.Y)
			{
				BaseDrawing.drawZoneRect = new Rectangle((int)Main.screenPosition.X - 300, (int)Main.screenPosition.Y - 300, Main.screenWidth + 600, Main.screenHeight + 600);
			}
			if (noScreenPos)
			{
				vec += Main.screenPosition;
			}
			return BaseDrawing.drawZoneRect.Contains((int)vec.X, (int)vec.Y);
		}

		public static bool InDrawZone(Rectangle rect)
		{
			if ((int)Main.screenPosition.X - 300 != BaseDrawing.drawZoneRect.X || (int)Main.screenPosition.Y - 300 != BaseDrawing.drawZoneRect.Y)
			{
				BaseDrawing.drawZoneRect = new Rectangle((int)Main.screenPosition.X - 300, (int)Main.screenPosition.Y - 300, Main.screenWidth + 600, Main.screenHeight + 600);
			}
			return BaseDrawing.drawZoneRect.Intersects(rect);
		}

		private static Rectangle drawZoneRect = default(Rectangle);
	}
}
