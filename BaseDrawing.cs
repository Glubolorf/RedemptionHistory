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
			float displayScalar = 0.5f + displayAlpha * 0.5f;
			int displayWidth = (int)(200f * displayScalar);
			int displayHeight = (int)(45f * displayScalar);
			Vector2 basePosition = new Vector2((float)(Main.screenWidth - 120), (float)(Main.screenHeight - 40)) + offset;
			Rectangle displayRect = new Rectangle((int)basePosition.X - displayWidth / 2, (int)basePosition.Y - displayHeight / 2, displayWidth, displayHeight);
			Utils.DrawInvBG(Main.spriteBatch, displayRect, new Color(63, 65, 151, 255) * 0.785f);
			string displayText2;
			if (progressMax == 0)
			{
				displayText2 = progress.ToString();
			}
			else
			{
				displayText2 = ((int)((float)progress * 100f / (float)progressMax)).ToString() + "%";
			}
			if (percentText != null)
			{
				displayText2 = percentText;
			}
			Texture2D barTex = Main.colorBarTexture;
			if (progressMax != 0)
			{
				Main.spriteBatch.Draw(barTex, basePosition, null, Color.White * displayAlpha, 0f, new Vector2((float)(barTex.Width / 2), 0f), displayScalar, SpriteEffects.None, 0f);
				float progressPercent = MathHelper.Clamp((float)progress / (float)progressMax, 0f, 1f);
				float scalarX = 169f * displayScalar;
				float scalarY = 8f * displayScalar;
				Vector2 vector4 = basePosition + Vector2.UnitY * scalarY + Vector2.UnitX * 1f;
				Utils.DrawBorderString(Main.spriteBatch, displayText2, vector4, Color.White * displayAlpha, displayScalar, 0.5f, 1f, -1);
				vector4 += Vector2.UnitX * (progressPercent - 0.5f) * scalarX;
				Main.spriteBatch.Draw(Main.magicPixel, vector4, new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(255, 241, 51) * displayAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(scalarX * progressPercent, scalarY), SpriteEffects.None, 0f);
				Main.spriteBatch.Draw(Main.magicPixel, vector4, new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(255, 165, 0, 127) * displayAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(2f, scalarY), SpriteEffects.None, 0f);
				Main.spriteBatch.Draw(Main.magicPixel, vector4, new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Black * displayAlpha, 0f, new Vector2(0f, 0.5f), new Vector2(scalarX * (1f - progressPercent), scalarY), SpriteEffects.None, 0f);
			}
			Vector2 vector5 = new Vector2((float)(Main.screenWidth - 120), (float)(Main.screenHeight - 80)) + offset;
			Vector2 stringLength = Main.fontItemStack.MeasureString(displayText);
			Rectangle textRect = Utils.CenteredRectangle(vector5, (stringLength + new Vector2((float)(iconTex.Width + 20), 10f)) * displayScalar);
			Utils.DrawInvBG(Main.spriteBatch, textRect, backgroundColor);
			Main.spriteBatch.Draw(iconTex, Utils.Left(textRect) + Vector2.UnitX * displayScalar * 8f, null, Color.White * displayAlpha, 0f, new Vector2(0f, (float)(iconTex.Height / 2)), displayScalar * 0.8f, SpriteEffects.None, 0f);
			Utils.DrawBorderString(Main.spriteBatch, displayText, Utils.Right(textRect) + Vector2.UnitX * displayScalar * -8f, Color.White * displayAlpha, displayScalar * 0.9f, 1f, 0.4f, -1);
		}

		public static void AddInterfaceLayer(Mod mod, List<GameInterfaceLayer> list, InterfaceLayer layer, string parent, bool first)
		{
			GameInterfaceLayer item = new LegacyGameInterfaceLayer(mod.Name + ":" + layer.name, delegate()
			{
				layer.Draw();
				return true;
			}, 1);
			layer.listItem = item;
			int insertAt = -1;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Name.Contains(parent))
				{
					insertAt = i;
					break;
				}
			}
			if (insertAt == -1)
			{
				list.Add(item);
				return;
			}
			list.Insert(first ? insertAt : (insertAt + 1), item);
		}

		public static Texture2D StitchTogetherTileTex(Texture2D tex, int tileType, int width = -1, int[] heights = null)
		{
			TileObjectData data = TileObjectData.GetTileData(tileType, 0, 0);
			if (width == -1)
			{
				width = data.CoordinateWidth;
			}
			if (heights == null)
			{
				heights = data.CoordinateHeights;
			}
			int padding = data.CoordinatePadding;
			List<Texture2D> subTexs = new List<Texture2D>();
			for (int w = 0; w < data.Width; w++)
			{
				for (int h = 0; h < data.Height; h++)
				{
					int currentHeight = 0;
					for (int tempH = h; tempH > 0; tempH--)
					{
						currentHeight += heights[tempH] + padding;
					}
					subTexs.Add(BaseDrawing.GetCroppedTex(tex, new Rectangle(w * (width + padding), currentHeight, width, heights[h])));
				}
			}
			int newHeight = 0;
			for (int tempH2 = data.Height - 1; tempH2 > 0; tempH2--)
			{
				newHeight += heights[tempH2];
			}
			Rectangle newBounds = new Rectangle(0, 0, data.Width * width, newHeight);
			Texture2D tex2 = new Texture2D(Main.instance.GraphicsDevice, newBounds.Width, newBounds.Height);
			List<Vector2> drawPos = new List<Vector2>();
			for (int i = 0; i < subTexs.Count; i++)
			{
				drawPos.Add(new Vector2((float)(width * i), 0f));
			}
			return BaseDrawing.DrawTextureToTexture(tex2, subTexs.ToArray(), drawPos.ToArray());
		}

		public static Texture2D DrawTextureToTexture(Texture2D toDrawTo, Texture2D[] toDraws, Vector2[] drawPos)
		{
			RenderTarget2D renderTarget = new RenderTarget2D(Main.instance.GraphicsDevice, toDrawTo.Width, toDrawTo.Height, false, Main.instance.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
			Main.instance.GraphicsDevice.SetRenderTarget(renderTarget);
			Main.instance.GraphicsDevice.Clear(Color.Black);
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			for (int i = 0; i < toDraws.Length; i++)
			{
				Texture2D toDraw = toDraws[i];
				BaseDrawing.DrawTexture(Main.spriteBatch, toDraw, 0, drawPos[i], toDraw.Width, toDraw.Height, 1f, 0f, 0, 1, toDraw.Bounds, null, false, default(Vector2));
			}
			Main.spriteBatch.End();
			Main.instance.GraphicsDevice.SetRenderTarget(null);
			return renderTarget;
		}

		public static Texture2D GetCroppedTex(Texture2D texture, Rectangle rect)
		{
			return BaseDrawing.GetCroppedTex(texture, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public static Texture2D GetCroppedTex(Texture2D texture, int startX, int startY, int newWidth, int newHeight)
		{
			Rectangle newBounds = texture.Bounds;
			newBounds.X += startX;
			newBounds.Y += startY;
			newBounds.Width = newWidth;
			newBounds.Height = newHeight;
			Texture2D texture2D = new Texture2D(Main.instance.GraphicsDevice, newBounds.Width, newBounds.Height);
			Color[] data = new Color[newBounds.Width * newBounds.Height];
			texture.GetData<Color>(0, new Rectangle?(newBounds), data, 0, newBounds.Width * newBounds.Height);
			texture2D.SetData<Color>(data);
			return texture2D;
		}

		public static Texture2D GetPlayerTex(Player p, string name)
		{
			return BaseDrawing.GetPlayerTex(p.skinVariant, name, p.Male);
		}

		public static Texture2D GetPlayerTex(int skinVariant, string name, bool male = true)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1155955520U)
			{
				if (num != 530731474U)
				{
					if (num != 1116344469U)
					{
						if (num == 1155955520U)
						{
							if (name == "Hand")
							{
								return Main.playerTextures[skinVariant, 5];
							}
						}
					}
					else if (name == "Body")
					{
						if (!male)
						{
							return Main.playerTextures[skinVariant, 6];
						}
						return Main.playerTextures[skinVariant, 4];
					}
				}
				else if (name == "Arms")
				{
					return Main.playerTextures[skinVariant, 7];
				}
			}
			else if (num <= 2574877228U)
			{
				if (num != 2268902839U)
				{
					if (num == 2574877228U)
					{
						if (name == "Legs")
						{
							return Main.playerTextures[skinVariant, 10];
						}
					}
				}
				else if (name == "EyeWhite")
				{
					return Main.playerTextures[skinVariant, 1];
				}
			}
			else if (num != 2996251363U)
			{
				if (num == 4037930426U)
				{
					if (name == "Eye")
					{
						return Main.playerTextures[skinVariant, 2];
					}
				}
			}
			else if (name == "Head")
			{
				return Main.playerTextures[skinVariant, 0];
			}
			return null;
		}

		public static void AddPlayerLayer(List<PlayerLayer> list, PlayerLayer layer, PlayerLayer parent, bool first)
		{
			int insertAt = -1;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Name.Equals(parent.Name))
				{
					insertAt = i;
					break;
				}
			}
			if (insertAt == -1)
			{
				list.Add(layer);
				return;
			}
			list.Insert(first ? insertAt : (insertAt + 1), layer);
		}

		public static void AddPlayerHeadLayer(List<PlayerHeadLayer> list, PlayerHeadLayer layer, PlayerHeadLayer parent, bool first)
		{
			int insertAt = -1;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Name.Equals(parent.Name))
				{
					insertAt = i;
					break;
				}
			}
			if (insertAt == -1)
			{
				list.Add(layer);
				return;
			}
			list.Insert(first ? insertAt : (insertAt + 1), layer);
		}

		public static Rectangle GetAdvancedFrame(int currentFrame, int frameOffsetX, int frameWidth, int frameHeight, int pixelSpaceX = 0, int pixelSpaceY = 2)
		{
			int column = currentFrame / frameOffsetX;
			currentFrame -= column * frameOffsetX;
			pixelSpaceY *= currentFrame;
			int x = (frameOffsetX == 0) ? 0 : (column * (frameWidth + pixelSpaceX));
			int startY = frameHeight * currentFrame + pixelSpaceY;
			return new Rectangle(x, startY, frameWidth, frameHeight);
		}

		public static Rectangle GetFrame(int currentFrame, int frameWidth, int frameHeight, int pixelSpaceX = 0, int pixelSpaceY = 2)
		{
			pixelSpaceY *= currentFrame;
			int startY = frameHeight * currentFrame + pixelSpaceY;
			return new Rectangle(0, startY, frameWidth - pixelSpaceX, frameHeight);
		}

		public static bool IsNormalDrawPass(Player player, PlayerDrawInfo pdi = default(PlayerDrawInfo))
		{
			return player.ghostFade == 0f && player.shadow == 0f && (pdi.Equals(default(PlayerDrawInfo)) || pdi.shadow == 0f);
		}

		public static int GetDye(Player drawPlayer, int accSlot, bool social = false, bool wings = false)
		{
			int dye = accSlot % 10;
			if (!wings && accSlot < 10 && drawPlayer.hideVisual[dye])
			{
				return -1;
			}
			return GameShaders.Armor.GetShaderIdFromItemId(drawPlayer.dye[dye].type);
		}

		public static Color? GetDyeColor(int dye)
		{
			Color? returnColor = null;
			float brightness = 1f;
			if (dye >= 13 && dye <= 24)
			{
				brightness = 0.7f;
				dye -= 12;
			}
			if (dye >= 45 && dye <= 56)
			{
				brightness = 1.3f;
				dye -= 44;
			}
			if (dye >= 32 && dye <= 43)
			{
				brightness = 1.5f;
				dye -= 31;
			}
			if (dye <= 31)
			{
				switch (dye)
				{
				case 1:
					returnColor = new Color?(new Color(248, 63, 63));
					break;
				case 2:
					returnColor = new Color?(new Color(248, 148, 63));
					break;
				case 3:
					returnColor = new Color?(new Color(248, 242, 62));
					break;
				case 4:
					returnColor = new Color?(new Color(157, 248, 70));
					break;
				case 5:
					returnColor = new Color?(new Color(48, 248, 70));
					break;
				case 6:
					returnColor = new Color?(new Color(60, 248, 70));
					break;
				case 7:
					returnColor = new Color?(new Color(62, 242, 248));
					break;
				case 8:
					returnColor = new Color?(new Color(64, 181, 247));
					break;
				case 9:
					returnColor = new Color?(new Color(66, 95, 247));
					break;
				case 10:
					returnColor = new Color?(new Color(159, 65, 247));
					break;
				case 11:
					returnColor = new Color?(new Color(212, 65, 247));
					break;
				case 12:
					returnColor = new Color?(new Color(242, 63, 131));
					break;
				default:
					if (dye == 31)
					{
						returnColor = new Color?(new Color(226, 226, 226));
					}
					break;
				}
			}
			else if (dye != 44)
			{
				switch (dye)
				{
				case 62:
					returnColor = new Color?(new Color(157, 248, 70));
					break;
				case 63:
					returnColor = new Color?(new Color(64, 181, 247));
					break;
				case 64:
					returnColor = new Color?(new Color(212, 65, 247));
					break;
				}
			}
			else
			{
				returnColor = new Color?(new Color(40, 40, 40));
			}
			if (returnColor != null && brightness != 1f)
			{
				returnColor = new Color?(BaseUtility.ColorMult(returnColor.Value, brightness));
			}
			return returnColor;
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
			float cr = 1f;
			float cg = 1f;
			float cb = 1f;
			float ca = 1f;
			if (effects && honey && Main.rand.Next(30) == 0)
			{
				int dustID = Dust.NewDust(codable.position, codable.width, codable.height, 152, 0f, 0f, 150, default(Color), 1f);
				Main.dust[dustID].velocity.Y = 0.3f;
				Dust dust = Main.dust[dustID];
				dust.velocity.X = dust.velocity.X * 0.1f;
				Main.dust[dustID].scale += (float)Main.rand.Next(3, 5) * 0.1f;
				Main.dust[dustID].alpha = 100;
				Main.dust[dustID].noGravity = true;
				Main.dust[dustID].velocity += codable.velocity * 0.1f;
				if (codable is Player)
				{
					Main.playerDrawDust.Add(dustID);
				}
			}
			if (poisoned)
			{
				if (effects && Main.rand.Next(30) == 0)
				{
					int dustID2 = Dust.NewDust(codable.position, codable.width, codable.height, 46, 0f, 0f, 120, default(Color), 0.2f);
					Main.dust[dustID2].noGravity = true;
					Main.dust[dustID2].fadeIn = 1.9f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(dustID2);
					}
				}
				cr *= 0.65f;
				cb *= 0.75f;
			}
			if (venom)
			{
				if (effects && Main.rand.Next(10) == 0)
				{
					int dustID3 = Dust.NewDust(codable.position, codable.width, codable.height, 171, 0f, 0f, 100, default(Color), 0.5f);
					Main.dust[dustID3].noGravity = true;
					Main.dust[dustID3].fadeIn = 1.5f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(dustID3);
					}
				}
				cg *= 0.45f;
				cr *= 0.75f;
			}
			if (midas)
			{
				cb *= 0.3f;
				cr *= 0.85f;
			}
			if (ichor)
			{
				if (codable is NPC)
				{
					lightColor = new Color(255, 255, 0, 255);
				}
				else
				{
					cb = 0f;
				}
			}
			if (burned)
			{
				if (effects)
				{
					int dustID4 = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 2f);
					Main.dust[dustID4].noGravity = true;
					Main.dust[dustID4].velocity *= 1.8f;
					Dust dust2 = Main.dust[dustID4];
					dust2.velocity.Y = dust2.velocity.Y - 0.75f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(dustID4);
					}
				}
				if (codable is Player)
				{
					cr = 1f;
					cb *= 0.6f;
					cg *= 0.7f;
				}
			}
			if (onFrostBurn)
			{
				if (effects)
				{
					if (Main.rand.Next(4) < 3)
					{
						int dustID5 = Dust.NewDust(new Vector2(codable.position.X - 2f, codable.position.Y - 2f), codable.width + 4, codable.height + 4, 135, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[dustID5].noGravity = true;
						Main.dust[dustID5].velocity *= 1.8f;
						Dust dust3 = Main.dust[dustID5];
						dust3.velocity.Y = dust3.velocity.Y - 0.5f;
						if (Main.rand.Next(4) == 0)
						{
							Main.dust[dustID5].noGravity = false;
							Main.dust[dustID5].scale *= 0.5f;
						}
						if (codable is Player)
						{
							Main.playerDrawDust.Add(dustID5);
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 0.1f, 0.6f, 1f);
				}
				if (codable is Player)
				{
					cr *= 0.5f;
					cg *= 0.7f;
				}
			}
			if (onFire)
			{
				if (effects)
				{
					if (Main.rand.Next(4) != 0)
					{
						int dustID6 = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, 6, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[dustID6].noGravity = true;
						Main.dust[dustID6].velocity *= 1.8f;
						Dust dust4 = Main.dust[dustID6];
						dust4.velocity.Y = dust4.velocity.Y - 0.5f;
						if (Main.rand.Next(4) == 0)
						{
							Main.dust[dustID6].noGravity = false;
							Main.dust[dustID6].scale *= 0.5f;
						}
						if (codable is Player)
						{
							Main.playerDrawDust.Add(dustID6);
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
				}
				if (codable is Player)
				{
					cb *= 0.6f;
					cg *= 0.7f;
				}
			}
			if (dripping && shadow == 0f && Main.rand.Next(4) != 0)
			{
				Vector2 position = codable.position;
				position.X -= 2f;
				position.Y -= 2f;
				if (Main.rand.Next(2) == 0)
				{
					int dustID7 = Dust.NewDust(position, codable.width + 4, codable.height + 2, 211, 0f, 0f, 50, default(Color), 0.8f);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID7].alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID7].alpha += 25;
					}
					Main.dust[dustID7].noLight = true;
					Main.dust[dustID7].velocity *= 0.2f;
					Dust dust5 = Main.dust[dustID7];
					dust5.velocity.Y = dust5.velocity.Y + 0.2f;
					Main.dust[dustID7].velocity += codable.velocity;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(dustID7);
					}
				}
				else
				{
					int dustID8 = Dust.NewDust(position, codable.width + 8, codable.height + 8, 211, 0f, 0f, 50, default(Color), 1.1f);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID8].alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID8].alpha += 25;
					}
					Main.dust[dustID8].noLight = true;
					Main.dust[dustID8].noGravity = true;
					Main.dust[dustID8].velocity *= 0.2f;
					Dust dust6 = Main.dust[dustID8];
					dust6.velocity.Y = dust6.velocity.Y + 1f;
					Main.dust[dustID8].velocity += codable.velocity;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(dustID8);
					}
				}
			}
			if (drippingSlime && shadow == 0f)
			{
				int alpha = 175;
				Color newColor = new Color(0, 80, 255, 100);
				if (Main.rand.Next(4) != 0 && Main.rand.Next(2) == 0)
				{
					Vector2 position2 = codable.position;
					position2.X -= 2f;
					position2.Y -= 2f;
					int dustID9 = Dust.NewDust(position2, codable.width + 4, codable.height + 2, 4, 0f, 0f, alpha, newColor, 1.4f);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID9].alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[dustID9].alpha += 25;
					}
					Main.dust[dustID9].noLight = true;
					Main.dust[dustID9].velocity *= 0.2f;
					Dust dust7 = Main.dust[dustID9];
					dust7.velocity.Y = dust7.velocity.Y + 0.2f;
					Main.dust[dustID9].velocity += codable.velocity;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(dustID9);
					}
				}
				cr *= 0.8f;
				cg *= 0.8f;
			}
			if (onFire2)
			{
				if (effects)
				{
					if (Main.rand.Next(4) != 0)
					{
						int dustID10 = Dust.NewDust(codable.position - new Vector2(2f, 2f), codable.width + 4, codable.height + 4, 75, codable.velocity.X * 0.4f, codable.velocity.Y * 0.4f, 100, default(Color), 3.5f);
						Main.dust[dustID10].noGravity = true;
						Main.dust[dustID10].velocity *= 1.8f;
						Dust dust8 = Main.dust[dustID10];
						dust8.velocity.Y = dust8.velocity.Y - 0.5f;
						if (Main.rand.Next(4) == 0)
						{
							Main.dust[dustID10].noGravity = false;
							Main.dust[dustID10].scale *= 0.5f;
						}
						if (codable is Player)
						{
							Main.playerDrawDust.Add(dustID10);
						}
					}
					Lighting.AddLight((int)(codable.position.X / 16f), (int)(codable.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
				}
				if (codable is Player)
				{
					cb *= 0.6f;
					cg *= 0.7f;
				}
			}
			if (noItems)
			{
				cr *= 0.65f;
				cg *= 0.8f;
			}
			if (blind)
			{
				cr *= 0.7f;
				cg *= 0.65f;
			}
			if (bleed)
			{
				bool dead = (codable is Player) ? ((Player)codable).dead : (codable is NPC && ((NPC)codable).life <= 0);
				if (effects && !dead && Main.rand.Next(30) == 0)
				{
					int dustID11 = Dust.NewDust(codable.position, codable.width, codable.height, 5, 0f, 0f, 0, default(Color), 1f);
					Dust dust9 = Main.dust[dustID11];
					dust9.velocity.Y = dust9.velocity.Y + 0.5f;
					Main.dust[dustID11].velocity *= 0.25f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(dustID11);
					}
				}
				cg *= 0.9f;
				cb *= 0.9f;
			}
			if (loveStruck && effects && shadow == 0f && Main.instance.IsActive && !Main.gamePaused && Main.rand.Next(5) == 0)
			{
				Vector2 value = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
				value.Normalize();
				value.X *= 0.66f;
				int goreID = Gore.NewGore(codable.position + new Vector2((float)Main.rand.Next(codable.width + 1), (float)Main.rand.Next(codable.height + 1)), value * (float)Main.rand.Next(3, 6) * 0.33f, 331, (float)Main.rand.Next(40, 121) * 0.01f);
				Main.gore[goreID].sticky = false;
				Main.gore[goreID].velocity *= 0.4f;
				Gore gore = Main.gore[goreID];
				gore.velocity.Y = gore.velocity.Y - 0.6f;
				if (codable is Player)
				{
					Main.playerDrawGore.Add(goreID);
				}
			}
			if (stinky && shadow == 0f)
			{
				cr *= 0.7f;
				cb *= 0.55f;
				if (effects && Main.rand.Next(5) == 0 && Main.instance.IsActive && !Main.gamePaused)
				{
					Vector2 value2 = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
					value2.Normalize();
					value2.X *= 0.66f;
					value2.Y = Math.Abs(value2.Y);
					Vector2 vector = value2 * (float)Main.rand.Next(3, 5) * 0.25f;
					int dustID12 = Dust.NewDust(codable.position, codable.width, codable.height, 188, vector.X, vector.Y * 0.5f, 100, default(Color), 1.5f);
					Main.dust[dustID12].velocity *= 0.1f;
					Dust dust10 = Main.dust[dustID12];
					dust10.velocity.Y = dust10.velocity.Y - 0.5f;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(dustID12);
					}
				}
			}
			lightColor.R = (byte)((float)lightColor.R * cr);
			lightColor.G = (byte)((float)lightColor.G * cg);
			lightColor.B = (byte)((float)lightColor.B * cb);
			lightColor.A = (byte)((float)lightColor.A * ca);
			if (codable is NPC)
			{
				NPCLoader.DrawEffects((NPC)codable, ref lightColor);
			}
			if (hunter && (!(codable is NPC) || ((NPC)codable).lifeMax > 1))
			{
				if (effects && !Main.gamePaused && Main.instance.IsActive && Main.rand.Next(50) == 0)
				{
					int dustID13 = Dust.NewDust(codable.position, codable.width, codable.height, 15, 0f, 0f, 150, default(Color), 0.8f);
					Main.dust[dustID13].velocity *= 0.1f;
					Main.dust[dustID13].noLight = true;
					if (codable is Player)
					{
						Main.playerDrawDust.Add(dustID13);
					}
				}
				byte colorR = 50;
				byte colorG = byte.MaxValue;
				byte colorB = 50;
				if (codable is NPC && !((NPC)codable).friendly && ((NPC)codable).catchItem <= 0 && (((NPC)codable).damage != 0 || ((NPC)codable).lifeMax != 5))
				{
					colorR = byte.MaxValue;
					colorG = 50;
				}
				if (!(codable is NPC) && lightColor.R < 150)
				{
					lightColor.A = Main.mouseTextColor;
				}
				if (lightColor.R < colorR)
				{
					lightColor.R = colorR;
				}
				if (lightColor.G < colorG)
				{
					lightColor.G = colorG;
				}
				if (lightColor.B < colorB)
				{
					lightColor.B = colorB;
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
			SpriteEffects spriteEffect = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			if (gravDir == -1f)
			{
				yOffset *= -1f;
				spriteEffect |= SpriteEffects.FlipVertically;
			}
			if (entity is Player)
			{
				Player drawPlayer = (Player)entity;
				yOffset -= drawPlayer.gfxOffY;
			}
			else if (entity is NPC)
			{
				NPC drawNPC = (NPC)entity;
				yOffset -= drawNPC.gfxOffY;
			}
			int drawType = item.type;
			Vector2 drawPos = position - Main.screenPosition;
			Vector2 texOrigin = new Vector2((float)tex.Width * 0.5f, (float)tex.Height * 0.5f / (float)frameCount);
			Vector2 rotOrigin = new Vector2(texOrigin.X - texOrigin.X * (float)direction, (float)((gravDir == -1f) ? 0 : tex.Height)) + new Vector2(xOffset, -yOffset);
			if (gravDir == -1f)
			{
				DrawData drawData;
				if (sb is List<DrawData>)
				{
					drawData..ctor(tex, drawPos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
					drawData.shader = shader;
					DrawData dd = drawData;
					((List<DrawData>)sb).Add(dd);
				}
				else if (sb is SpriteBatch)
				{
					((SpriteBatch)sb).Draw(tex, drawPos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0f);
				}
				if (wepColor != default(Color))
				{
					if (sb is List<DrawData>)
					{
						drawData..ctor(tex, drawPos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
						drawData.shader = shader;
						DrawData dd2 = drawData;
						((List<DrawData>)sb).Add(dd2);
						return;
					}
					if (sb is SpriteBatch)
					{
						((SpriteBatch)sb).Draw(tex, drawPos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0f);
						return;
					}
				}
			}
			else
			{
				if (drawType == 425 || drawType == 507)
				{
					if (direction == 1)
					{
						spriteEffect = SpriteEffects.FlipVertically;
					}
					else
					{
						spriteEffect = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
					}
				}
				DrawData drawData;
				if (sb is List<DrawData>)
				{
					drawData..ctor(tex, drawPos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
					drawData.shader = shader;
					DrawData dd3 = drawData;
					((List<DrawData>)sb).Add(dd3);
				}
				else if (sb is SpriteBatch)
				{
					((SpriteBatch)sb).Draw(tex, drawPos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0f);
				}
				if (wepColor != default(Color))
				{
					if (sb is List<DrawData>)
					{
						drawData..ctor(tex, drawPos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
						drawData.shader = shader;
						DrawData dd4 = drawData;
						((List<DrawData>)sb).Add(dd4);
						return;
					}
					if (sb is SpriteBatch)
					{
						((SpriteBatch)sb).Draw(tex, drawPos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0f);
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
			SpriteEffects spriteEffect = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			if (gravDir == -1f)
			{
				yOffset *= -1f;
				spriteEffect |= SpriteEffects.FlipVertically;
			}
			int type = item.type;
			int fakeType = type;
			Vector2 texOrigin = new Vector2((float)(tex.Width / 2), (float)(tex.Height / 2) / (float)frameCount);
			if (entity is Player)
			{
				Player drawPlayer = (Player)entity;
				yOffset += drawPlayer.gfxOffY;
			}
			else if (entity is NPC)
			{
				NPC drawNPC = (NPC)entity;
				yOffset += drawNPC.gfxOffY;
			}
			Vector2 rotOrigin = new Vector2(-xOffset, (float)(tex.Height / 2) / (float)frameCount - yOffset);
			if (direction == -1)
			{
				rotOrigin = new Vector2((float)tex.Width + xOffset, (float)(tex.Height / 2) / (float)frameCount - yOffset);
			}
			Vector2 pos = new Vector2((float)((int)(position.X - Main.screenPosition.X + texOrigin.X)), (float)((int)(position.Y - Main.screenPosition.Y + texOrigin.Y)));
			if (shakeX)
			{
				pos.X += shakeScalarX * ((float)Main.rand.Next(-5, 6) / 9f);
			}
			if (shakeY)
			{
				pos.Y += shakeScalarY * ((float)Main.rand.Next(-5, 6) / 9f);
			}
			DrawData drawData;
			if (sb is List<DrawData>)
			{
				drawData..ctor(tex, pos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
				drawData.shader = shader;
				DrawData dd = drawData;
				((List<DrawData>)sb).Add(dd);
			}
			else if (sb is SpriteBatch)
			{
				((SpriteBatch)sb).Draw(tex, pos, frame, item.GetAlpha(lightColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0f);
			}
			if (wepColor != default(Color))
			{
				if (sb is List<DrawData>)
				{
					drawData..ctor(tex, pos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0);
					drawData.shader = shader;
					DrawData dd2 = drawData;
					((List<DrawData>)sb).Add(dd2);
				}
				else if (sb is SpriteBatch)
				{
					((SpriteBatch)sb).Draw(tex, pos, frame, item.GetColor(wepColor), itemRotation, rotOrigin, itemScale, spriteEffect, 0f);
				}
			}
			try
			{
				if (type != fakeType)
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
			Color lightColor = (overrideColor != null) ? overrideColor.Value : p.GetAlpha(BaseDrawing.GetLightColor(Main.player[p.owner].Center));
			Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			offsetY -= Main.player[p.owner].gfxOffY;
			Vector2 offset = BaseUtility.RotateVector(p.Center, p.Center + new Vector2((p.direction == -1) ? offsetX : offsetY, (p.direction == 1) ? offsetX : offsetY), p.rotation - 2.355f) - p.Center;
			if (sb is List<DrawData>)
			{
				DrawData drawData;
				drawData..ctor(texture, p.Center - Main.screenPosition + offset, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), lightColor, p.rotation, origin, p.scale, (p.direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
				drawData.shader = shader;
				DrawData dd = drawData;
				((List<DrawData>)sb).Add(dd);
				return;
			}
			if (sb is SpriteBatch)
			{
				((SpriteBatch)sb).Draw(texture, p.Center - Main.screenPosition + offset, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), lightColor, p.rotation, origin, p.scale, (p.direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
			}
		}

		public static void DrawAura(object sb, Texture2D texture, int shader, Entity codable, float auraPercent, float distanceScalar = 1f, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
		{
			int frameCount = (codable is NPC) ? Main.npcFrameCount[((NPC)codable).type] : 1;
			Rectangle frame = (codable is NPC) ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Height, texture.Width);
			float scale = (codable is NPC) ? ((NPC)codable).scale : ((Projectile)codable).scale;
			float rotation = (codable is NPC) ? ((NPC)codable).rotation : ((Projectile)codable).rotation;
			int spriteDirection = (codable is NPC) ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection;
			float offsetY2 = (codable is NPC) ? ((NPC)codable).gfxOffY : 0f;
			BaseDrawing.DrawAura(sb, texture, shader, codable.position + new Vector2(0f, offsetY2), codable.width, codable.height, auraPercent, distanceScalar, scale, rotation, spriteDirection, frameCount, frame, offsetX, offsetY, overrideColor);
		}

		public static void DrawAura(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float auraPercent, float distanceScalar = 1f, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default(Rectangle), float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
		{
			Color lightColor = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(position + new Vector2((float)width * 0.5f, (float)height * 0.5f));
			float percentHalf = auraPercent * 5f * distanceScalar;
			float percentLight = MathHelper.Lerp(0.8f, 0.2f, auraPercent);
			lightColor.R = (byte)((float)lightColor.R * percentLight);
			lightColor.G = (byte)((float)lightColor.G * percentLight);
			lightColor.B = (byte)((float)lightColor.B * percentLight);
			lightColor.A = (byte)((float)lightColor.A * percentLight);
			Vector2 position2 = position;
			for (int i = 0; i < 4; i++)
			{
				float offX = offsetX;
				float offY = offsetY;
				switch (i)
				{
				case 0:
					offX += percentHalf;
					break;
				case 1:
					offX -= percentHalf;
					break;
				case 2:
					offY += percentHalf;
					break;
				case 3:
					offY -= percentHalf;
					break;
				}
				position2 = new Vector2(position.X + offX, position.Y + offY);
				BaseDrawing.DrawTexture(sb, texture, shader, position2, width, height, scale, rotation, direction, framecount, frame, new Color?(lightColor), false, default(Vector2));
			}
		}

		public static void DrawYoyoLine(SpriteBatch sb, Projectile projectile, Texture2D overrideTex = null, Color? overrideColor = null)
		{
			BaseDrawing.DrawYoyoLine(sb, projectile, Main.player[projectile.owner], projectile.Center, Main.player[projectile.owner].MountedCenter, overrideTex, overrideColor);
		}

		public static void DrawYoyoLine(SpriteBatch sb, Projectile projectile, Entity owner, Vector2 yoyoLoc, Vector2 connectionLoc, Texture2D overrideTex = null, Color? overrideColor = null)
		{
			Vector2 mountedCenter = connectionLoc;
			if (owner is Player)
			{
				mountedCenter.Y += Main.player[projectile.owner].gfxOffY;
			}
			float centerDistX = yoyoLoc.X - mountedCenter.X;
			float centerDistY = yoyoLoc.Y - mountedCenter.Y;
			Math.Sqrt((double)(centerDistX * centerDistX + centerDistY * centerDistY));
			float rotation = (float)Math.Atan2((double)centerDistY, (double)centerDistX) - 1.57f;
			if (owner is Player && !projectile.counterweight)
			{
				int projDir = -1;
				if (projectile.position.X + (float)(projectile.width / 2) < Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
				{
					projDir = 1;
				}
				projDir *= -1;
				((Player)owner).itemRotation = (float)Math.Atan2((double)(centerDistY * (float)projDir), (double)(centerDistX * (float)projDir));
			}
			bool flag = true;
			if (centerDistX == 0f && centerDistY == 0f)
			{
				flag = false;
			}
			else
			{
				float sqrtCenter = (float)Math.Sqrt((double)(centerDistX * centerDistX + centerDistY * centerDistY));
				sqrtCenter = 12f / sqrtCenter;
				centerDistX *= sqrtCenter;
				centerDistY *= sqrtCenter;
				mountedCenter.X -= centerDistX * 0.1f;
				mountedCenter.Y -= centerDistY * 0.1f;
				centerDistX = yoyoLoc.X - mountedCenter.X;
				centerDistY = yoyoLoc.Y - mountedCenter.Y;
			}
			while (flag)
			{
				float textureHeight = 12f;
				float sqrtCenter2 = (float)Math.Sqrt((double)(centerDistX * centerDistX + centerDistY * centerDistY));
				float sqrtCenter3 = sqrtCenter2;
				if (float.IsNaN(sqrtCenter2) || float.IsNaN(sqrtCenter3))
				{
					flag = false;
				}
				else
				{
					if (sqrtCenter2 < 20f)
					{
						textureHeight = sqrtCenter2 - 8f;
						flag = false;
					}
					sqrtCenter2 = 12f / sqrtCenter2;
					centerDistX *= sqrtCenter2;
					centerDistY *= sqrtCenter2;
					mountedCenter.X += centerDistX;
					mountedCenter.Y += centerDistY;
					centerDistX = yoyoLoc.X - mountedCenter.X;
					centerDistY = yoyoLoc.Y - mountedCenter.Y;
					if (sqrtCenter3 > 12f)
					{
						float scalar = 0.3f;
						float velocityAverage = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
						if (velocityAverage > 16f)
						{
							velocityAverage = 16f;
						}
						velocityAverage = 1f - velocityAverage / 16f;
						scalar *= velocityAverage;
						velocityAverage = sqrtCenter3 / 80f;
						if (velocityAverage > 1f)
						{
							velocityAverage = 1f;
						}
						scalar *= velocityAverage;
						if (scalar < 0f)
						{
							scalar = 0f;
						}
						scalar *= velocityAverage;
						scalar *= 0.5f;
						if (centerDistY > 0f)
						{
							centerDistY *= 1f + scalar;
							centerDistX *= 1f - scalar;
						}
						else
						{
							velocityAverage = Math.Abs(projectile.velocity.X) / 3f;
							if (velocityAverage > 1f)
							{
								velocityAverage = 1f;
							}
							velocityAverage -= 0.5f;
							scalar *= velocityAverage;
							if (scalar > 0f)
							{
								scalar *= 2f;
							}
							centerDistY *= 1f + scalar;
							centerDistX *= 1f - scalar;
						}
					}
					rotation = (float)Math.Atan2((double)centerDistY, (double)centerDistX) - 1.57f;
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
						color = new Color(20, 20, 20);
					}
					else if (stringColor == 14 || stringColor == 0)
					{
						color = new Color(200, 200, 200);
					}
					else if (stringColor == 28)
					{
						color = new Color(163, 116, 91);
					}
					else if (stringColor == 27)
					{
						color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
					}
					color.A = (byte)((float)color.A * 0.4f);
					float colorScalar = 0.5f;
					if (overrideColor == null)
					{
						color = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f), color);
						color = new Color((int)((byte)((float)color.R * colorScalar)), (int)((byte)((float)color.G * colorScalar)), (int)((byte)((float)color.B * colorScalar)), (int)((byte)((float)color.A * colorScalar)));
					}
					Texture2D tex = (overrideTex != null) ? overrideTex : Main.fishingLineTexture;
					Vector2 texCenter = new Vector2((float)tex.Width * 0.5f, (float)tex.Height * 0.5f);
					Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(mountedCenter.X - Main.screenPosition.X + texCenter.X, mountedCenter.Y - Main.screenPosition.Y + texCenter.Y) - new Vector2(6f, 0f), new Rectangle?(new Rectangle(0, 0, tex.Width, (int)textureHeight)), color, rotation, new Vector2((float)tex.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
				}
			}
		}

		public static void DrawFishingLine(SpriteBatch sb, Projectile projectile, Vector2 rodLoc, Vector2 bobberLoc, Texture2D overrideTex = null, Color? overrideColor = null)
		{
			Player player = Main.player[projectile.owner];
			if (projectile.bobber && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].holdStyle > 0)
			{
				float mountedCenterX = player.MountedCenter.X;
				float mountedCenterY = player.MountedCenter.Y;
				mountedCenterY += Main.player[projectile.owner].gfxOffY;
				int type = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type;
				float gravDir = Main.player[projectile.owner].gravDir;
				mountedCenterX += rodLoc.X * (float)Main.player[projectile.owner].direction;
				if (Main.player[projectile.owner].direction < 0)
				{
					mountedCenterX -= 13f;
				}
				mountedCenterY -= rodLoc.Y * gravDir;
				if (gravDir == -1f)
				{
					mountedCenterY -= 12f;
				}
				Vector2 mountedCenter = new Vector2(mountedCenterX, mountedCenterY);
				mountedCenter = Main.player[projectile.owner].RotatedRelativePoint(mountedCenter + new Vector2(8f), true) - new Vector2(8f);
				float projLineCenterX = projectile.position.X + (float)projectile.width * 0.5f - mountedCenter.X;
				float projLineCenterY = projectile.position.Y + (float)projectile.height * 0.5f - mountedCenter.Y;
				projLineCenterX += bobberLoc.X;
				projLineCenterY += bobberLoc.Y;
				Math.Sqrt((double)(projLineCenterX * projLineCenterX + projLineCenterY * projLineCenterY));
				float rotation2 = (float)Math.Atan2((double)projLineCenterY, (double)projLineCenterX) - 1.57f;
				bool flag2 = true;
				if (projLineCenterX == 0f && projLineCenterY == 0f)
				{
					flag2 = false;
				}
				else
				{
					float num15 = (float)Math.Sqrt((double)(projLineCenterX * projLineCenterX + projLineCenterY * projLineCenterY));
					num15 = 12f / num15;
					projLineCenterX *= num15;
					projLineCenterY *= num15;
					mountedCenter.X -= projLineCenterX;
					mountedCenter.Y -= projLineCenterY;
					projLineCenterX = projectile.position.X + (float)projectile.width * 0.5f - mountedCenter.X;
					projLineCenterY = projectile.position.Y + (float)projectile.height * 0.5f - mountedCenter.Y;
				}
				while (flag2)
				{
					float num16 = 12f;
					float num17 = (float)Math.Sqrt((double)(projLineCenterX * projLineCenterX + projLineCenterY * projLineCenterY));
					float num18 = num17;
					if (float.IsNaN(num17) || float.IsNaN(num18))
					{
						flag2 = false;
					}
					else
					{
						if (num17 < 20f)
						{
							num16 = num17 - 8f;
							flag2 = false;
						}
						num17 = 12f / num17;
						projLineCenterX *= num17;
						projLineCenterY *= num17;
						mountedCenter.X += projLineCenterX;
						mountedCenter.Y += projLineCenterY;
						projLineCenterX = projectile.position.X + (float)projectile.width * 0.5f - mountedCenter.X;
						projLineCenterY = projectile.position.Y + (float)projectile.height * 0.1f - mountedCenter.Y;
						if (num18 > 12f)
						{
							float num19 = 0.3f;
							float num20 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
							if (num20 > 16f)
							{
								num20 = 16f;
							}
							num20 = 1f - num20 / 16f;
							num19 *= num20;
							num20 = num18 / 80f;
							if (num20 > 1f)
							{
								num20 = 1f;
							}
							num19 *= num20;
							if (num19 < 0f)
							{
								num19 = 0f;
							}
							num20 = 1f - projectile.localAI[0] / 100f;
							num19 *= num20;
							if (projLineCenterY > 0f)
							{
								projLineCenterY *= 1f + num19;
								projLineCenterX *= 1f - num19;
							}
							else
							{
								num20 = Math.Abs(projectile.velocity.X) / 3f;
								if (num20 > 1f)
								{
									num20 = 1f;
								}
								num20 -= 0.5f;
								num19 *= num20;
								if (num19 > 0f)
								{
									num19 *= 2f;
								}
								projLineCenterY *= 1f + num19;
								projLineCenterX *= 1f - num19;
							}
						}
						rotation2 = (float)Math.Atan2((double)projLineCenterY, (double)projLineCenterX) - 1.57f;
						Color color2 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f), (overrideColor != null) ? overrideColor.Value : new Color(200, 200, 200, 100));
						Texture2D tex = (overrideTex != null) ? overrideTex : Main.fishingLineTexture;
						Vector2 texCenter = new Vector2((float)tex.Width * 0.5f, (float)tex.Height * 0.5f);
						sb.Draw(tex, new Vector2(mountedCenter.X - Main.screenPosition.X + texCenter.X * 0.5f, mountedCenter.Y - Main.screenPosition.Y + texCenter.Y * 0.5f), new Rectangle?(new Rectangle(0, 0, tex.Width, (int)num16)), color2, rotation2, new Vector2((float)tex.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
		}

		public static void DrawAfterimage(object sb, Texture2D texture, int shader, Entity codable, float distanceScalar = 1f, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null, Rectangle? overrideFrame = null, int overrideFrameCount = 0)
		{
			int frameCount = (overrideFrameCount > 0) ? overrideFrameCount : ((codable is NPC) ? Main.npcFrameCount[((NPC)codable).type] : 1);
			Rectangle frame = (overrideFrame != null) ? overrideFrame.Value : ((codable is NPC) ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height));
			float scale = (codable is NPC) ? ((NPC)codable).scale : ((Projectile)codable).scale;
			float rotation = (codable is NPC) ? ((NPC)codable).rotation : ((Projectile)codable).rotation;
			int spriteDirection = (codable is NPC) ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection;
			Vector2[] velocities = new Vector2[]
			{
				codable.velocity
			};
			if (useOldPos)
			{
				velocities = ((codable is NPC) ? ((NPC)codable).oldPos : ((Projectile)codable).oldPos);
			}
			float offsetY2 = (codable is NPC) ? ((NPC)codable).gfxOffY : 0f;
			BaseDrawing.DrawAfterimage(sb, texture, shader, codable.position + new Vector2(0f, offsetY2), codable.width, codable.height, velocities, scale, rotation, spriteDirection, frameCount, frame, distanceScalar, sizeScalar, imageCount, useOldPos, offsetX, offsetY, overrideColor);
		}

		public static void DrawAfterimage(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, Vector2[] oldPoints, float scale = 1f, float rotation = 0f, int direction = 0, int framecount = 1, Rectangle frame = default(Rectangle), float distanceScalar = 1f, float sizeScalar = 1f, int imageCount = 7, bool useOldPos = true, float offsetX = 0f, float offsetY = 0f, Color? overrideColor = null)
		{
			new Vector2((float)(texture.Width / 2), (float)(texture.Height / framecount / 2));
			Color lightColor = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(position + new Vector2((float)width * 0.5f, (float)height * 0.5f));
			Vector2 velAddon = default(Vector2);
			Vector2 originalpos = position;
			Vector2 offset = new Vector2(offsetX, offsetY);
			for (int i = 1; i <= imageCount; i++)
			{
				scale *= sizeScalar;
				Color newLightColor = lightColor;
				newLightColor.R = (byte)((int)newLightColor.R * (imageCount + 3 - i) / (imageCount + 9));
				newLightColor.G = (byte)((int)newLightColor.G * (imageCount + 3 - i) / (imageCount + 9));
				newLightColor.B = (byte)((int)newLightColor.B * (imageCount + 3 - i) / (imageCount + 9));
				newLightColor.A = (byte)((int)newLightColor.A * (imageCount + 3 - i) / (imageCount + 9));
				if (useOldPos)
				{
					position = Vector2.Lerp(originalpos, (i - 1 >= oldPoints.Length) ? oldPoints[oldPoints.Length - 1] : oldPoints[i - 1], distanceScalar);
					BaseDrawing.DrawTexture(sb, texture, shader, position + offset, width, height, scale, rotation, direction, framecount, frame, new Color?(newLightColor), false, default(Vector2));
				}
				else
				{
					Vector2 velocity = (i - 1 >= oldPoints.Length) ? oldPoints[oldPoints.Length - 1] : oldPoints[i - 1];
					velAddon += velocity * distanceScalar;
					BaseDrawing.DrawTexture(sb, texture, shader, position + offset - velAddon, width, height, scale, rotation, direction, framecount, frame, new Color?(newLightColor), false, default(Vector2));
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
			Vector2 dir = end - start;
			dir.Normalize();
			float length = Vector2.Distance(start, end);
			float Way = 0f;
			float rotation = BaseUtility.RotationTo(start, end) - 1.57f;
			int texID = 0;
			int maxTextures = textures.Length - 2;
			int currentChain = 0;
			Action <>9__0;
			while (Way < length)
			{
				Action action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate()
					{
						DrawData drawData2;
						if (textures[0] != null && Way == 0f)
						{
							float texWidth2 = (float)textures[0].Width;
							float texHeight2 = (float)textures[0].Height;
							Vector2 texCenter2 = new Vector2(texWidth2 / 2f, texHeight2 / 2f) * scale;
							Vector2 v2 = start - Main.screenPosition + texCenter2;
							Color lightColor2 = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(start + texCenter2);
							if (OnDrawTex == null || OnDrawTex.Invoke(textures[0], start + texCenter2, v2 - texCenter2, texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, scale, -1))
							{
								if (sb is List<DrawData>)
								{
									drawData2..ctor(textures[0], v2 - texCenter2, new Rectangle?(new Rectangle(0, 0, (int)texWidth2, (int)texHeight2)), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0);
									drawData2.shader = shader;
									DrawData dd2 = drawData2;
									((List<DrawData>)sb).Add(dd2);
								}
								else if (sb is SpriteBatch)
								{
									((SpriteBatch)sb).Draw(textures[0], v2 - texCenter2, new Rectangle?(new Rectangle(0, 0, (int)texWidth2, (int)texHeight2)), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0f);
								}
							}
						}
						if (textures[maxTextures + 1] != null && Way + Jump >= length)
						{
							float texWidth3 = (float)textures[maxTextures + 1].Width;
							float texHeight3 = (float)textures[maxTextures + 1].Height;
							Vector2 texCenter3 = new Vector2(texWidth3 / 2f, texHeight3 / 2f) * scale;
							Vector2 v3 = end - Main.screenPosition + texCenter3;
							Color lightColor3 = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(end + texCenter3);
							if (OnDrawTex == null || OnDrawTex.Invoke(textures[maxTextures + 1], end + texCenter3, v3 - texCenter3, texCenter3, new Rectangle(0, 0, (int)texWidth3, (int)texHeight3), lightColor3, rotation, scale, -2))
							{
								if (sb is List<DrawData>)
								{
									drawData2..ctor(textures[maxTextures + 1], v3 - texCenter3, new Rectangle?(new Rectangle(0, 0, (int)texWidth3, (int)texHeight3)), lightColor3, rotation, texCenter3, scale, SpriteEffects.None, 0);
									drawData2.shader = shader;
									DrawData dd3 = drawData2;
									((List<DrawData>)sb).Add(dd3);
									return;
								}
								if (sb is SpriteBatch)
								{
									((SpriteBatch)sb).Draw(textures[maxTextures + 1], v3 - texCenter3, new Rectangle?(new Rectangle(0, 0, (int)texWidth3, (int)texHeight3)), lightColor3, rotation, texCenter3, scale, SpriteEffects.None, 0f);
								}
							}
						}
					});
				}
				Action drawEnds = action;
				float texWidth = (float)textures[1].Width;
				float texHeight = (float)textures[1].Height;
				Vector2 texCenter = new Vector2(texWidth / 2f, texHeight / 2f) * scale;
				Vector2 v = start + dir * Way + texCenter;
				if (BaseDrawing.InDrawZone(v, false))
				{
					v -= Main.screenPosition;
					if ((Way == 0f || Way + Jump >= length) && drawEndsUnder)
					{
						drawEnds();
					}
					Color lightColor = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(start + dir * Way + texCenter);
					texID++;
					if (texID >= maxTextures)
					{
						texID = 0;
					}
					if (OnDrawTex == null || OnDrawTex.Invoke(textures[texID + 1], start + dir * Way + texCenter, v - texCenter, texCenter, new Rectangle(0, 0, (int)texWidth, (int)texHeight), lightColor, rotation, scale, currentChain))
					{
						if (sb is List<DrawData>)
						{
							DrawData drawData;
							drawData..ctor(textures[texID + 1], v - texCenter, new Rectangle?(new Rectangle(0, 0, (int)texWidth, (int)texHeight)), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0);
							drawData.shader = shader;
							DrawData dd = drawData;
							((List<DrawData>)sb).Add(dd);
						}
						else if (sb is SpriteBatch)
						{
							((SpriteBatch)sb).Draw(textures[texID + 1], v - texCenter, new Rectangle?(new Rectangle(0, 0, (int)texWidth, (int)texHeight)), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0f);
						}
					}
					currentChain++;
					if ((Way == 0f || Way + Jump >= length) && !drawEndsUnder)
					{
						drawEnds();
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
			Vector2 dir = end - start;
			dir.Normalize();
			float Way = 0f;
			float rotation = BaseUtility.RotationTo(chain[0], chain[1]) - 1.57f;
			int texID = 0;
			int maxTextures = textures.Length - 2;
			int currentChain = 0;
			Action <>9__0;
			while (Way < length)
			{
				Action action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate()
					{
						DrawData drawData2;
						if (textures[0] != null && Way == 0f)
						{
							float texWidth2 = (float)textures[0].Width;
							float texHeight2 = (float)textures[0].Height;
							Vector2 texCenter2 = new Vector2(texWidth2 / 2f, texHeight2 / 2f) * scale;
							Vector2 v2 = start - Main.screenPosition + texCenter2;
							Color lightColor2 = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(start + texCenter2);
							if (OnDrawTex == null || OnDrawTex.Invoke(textures[0], start + texCenter2, v2 - texCenter2, texCenter2, new Rectangle(0, 0, (int)texWidth2, (int)texHeight2), lightColor2, rotation, scale, -1))
							{
								if (sb is List<DrawData>)
								{
									drawData2..ctor(textures[0], v2 - texCenter2, new Rectangle?(new Rectangle(0, 0, (int)texWidth2, (int)texHeight2)), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0);
									drawData2.shader = shader;
									DrawData dd2 = drawData2;
									((List<DrawData>)sb).Add(dd2);
								}
								else if (sb is SpriteBatch)
								{
									((SpriteBatch)sb).Draw(textures[0], v2 - texCenter2, new Rectangle?(new Rectangle(0, 0, (int)texWidth2, (int)texHeight2)), lightColor2, rotation, texCenter2, scale, SpriteEffects.None, 0f);
								}
							}
						}
						if (textures[maxTextures + 1] != null && Way + Jump >= length)
						{
							float texWidth3 = (float)textures[maxTextures + 1].Width;
							float texHeight3 = (float)textures[maxTextures + 1].Height;
							Vector2 texCenter3 = new Vector2(texWidth3 / 2f, texHeight3 / 2f) * scale;
							Vector2 v3 = end - Main.screenPosition + texCenter3;
							Color lightColor3 = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(end + texCenter3);
							if (OnDrawTex == null || OnDrawTex.Invoke(textures[maxTextures + 1], end + texCenter3, v3 - texCenter3, texCenter3, new Rectangle(0, 0, (int)texWidth3, (int)texHeight3), lightColor3, rotation, scale, -2))
							{
								if (sb is List<DrawData>)
								{
									drawData2..ctor(textures[maxTextures + 1], v3 - texCenter3, new Rectangle?(new Rectangle(0, 0, (int)texWidth3, (int)texHeight3)), lightColor3, rotation, texCenter3, scale, SpriteEffects.None, 0);
									drawData2.shader = shader;
									DrawData dd3 = drawData2;
									((List<DrawData>)sb).Add(dd3);
									return;
								}
								if (sb is SpriteBatch)
								{
									((SpriteBatch)sb).Draw(textures[maxTextures + 1], v3 - texCenter3, new Rectangle?(new Rectangle(0, 0, (int)texWidth3, (int)texHeight3)), lightColor3, rotation, texCenter3, scale, SpriteEffects.None, 0f);
								}
							}
						}
					});
				}
				Action drawEnds = action;
				float texWidth = (float)textures[1].Width;
				float texHeight = (float)textures[1].Height;
				Vector2 texCenter = new Vector2(texWidth / 2f, texHeight / 2f) * scale;
				Vector2 v = BaseUtility.MultiLerpVector(Way / length, chain) + texCenter;
				Vector2 nextV = BaseUtility.MultiLerpVector(Math.Max(length - 1f, Way + 1f) / length, chain) + texCenter;
				if (v != nextV)
				{
					rotation = BaseUtility.RotationTo(v, nextV) - 1.57f;
				}
				if (BaseDrawing.InDrawZone(v, false))
				{
					v -= Main.screenPosition;
					if ((Way == 0f || Way + Jump >= length) && drawEndsUnder)
					{
						drawEnds();
					}
					Color lightColor = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(start + dir * Way + texCenter);
					texID++;
					if (texID >= maxTextures)
					{
						texID = 0;
					}
					if (OnDrawTex == null || OnDrawTex.Invoke(textures[texID + 1], start + dir * Way + texCenter, v - texCenter, texCenter, new Rectangle(0, 0, (int)texWidth, (int)texHeight), lightColor, rotation, scale, currentChain))
					{
						if (sb is List<DrawData>)
						{
							DrawData drawData;
							drawData..ctor(textures[texID + 1], v - texCenter, new Rectangle?(new Rectangle(0, 0, (int)texWidth, (int)texHeight)), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0);
							drawData.shader = shader;
							DrawData dd = drawData;
							((List<DrawData>)sb).Add(dd);
						}
						else if (sb is SpriteBatch)
						{
							((SpriteBatch)sb).Draw(textures[texID + 1], v - texCenter, new Rectangle?(new Rectangle(0, 0, (int)texWidth, (int)texHeight)), lightColor, rotation, texCenter, scale, SpriteEffects.None, 0f);
						}
					}
					currentChain++;
					if ((Way == 0f || Way + Jump >= length) && !drawEndsUnder)
					{
						drawEnds();
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
			Color lightColor = (overrideColor != null) ? overrideColor.Value : ((codable is Item) ? ((Item)codable).GetAlpha(BaseDrawing.GetLightColor(codable.Center)) : ((codable is NPC) ? BaseDrawing.GetNPCColor((NPC)codable, new Vector2?(codable.Center), false, 0f) : ((codable is Projectile) ? ((Projectile)codable).GetAlpha(BaseDrawing.GetLightColor(codable.Center)) : BaseDrawing.GetLightColor(codable.Center))));
			int frameCount = (codable is Item) ? 1 : ((codable is NPC) ? Main.npcFrameCount[((NPC)codable).type] : 1);
			Rectangle frame = (codable is NPC) ? ((NPC)codable).frame : new Rectangle(0, 0, texture.Width, texture.Height);
			float scale = (codable is Item) ? ((Item)codable).scale : ((codable is NPC) ? ((NPC)codable).scale : ((Projectile)codable).scale);
			float rotation = (codable is Item) ? 0f : ((codable is NPC) ? ((NPC)codable).rotation : ((Projectile)codable).rotation);
			int spriteDirection = (codable is Item) ? 1 : ((codable is NPC) ? ((NPC)codable).spriteDirection : ((Projectile)codable).spriteDirection);
			float offsetY = (codable is NPC) ? ((NPC)codable).gfxOffY : 0f;
			BaseDrawing.DrawTexture(sb, texture, shader, codable.position + new Vector2(0f, offsetY), codable.width, codable.height, scale, rotation, spriteDirection, frameCount, framecountX, frame, new Color?(lightColor), drawCentered, overrideOrigin);
		}

		public static void DrawTexture(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float scale, float rotation, int direction, int framecount, Rectangle frame, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
		{
			BaseDrawing.DrawTexture(sb, texture, shader, position, width, height, scale, rotation, direction, framecount, 1, frame, overrideColor, drawCentered, overrideOrigin);
		}

		public static void DrawTexture(object sb, Texture2D texture, int shader, Vector2 position, int width, int height, float scale, float rotation, int direction, int framecount, int framecountX, Rectangle frame, Color? overrideColor = null, bool drawCentered = false, Vector2 overrideOrigin = default(Vector2))
		{
			Vector2 origin = (overrideOrigin != default(Vector2)) ? overrideOrigin : new Vector2((float)(frame.Width / framecountX / 2), (float)(texture.Height / framecount / 2));
			Color lightColor = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetLightColor(position + new Vector2((float)width * 0.5f, (float)height * 0.5f));
			if (sb is List<DrawData>)
			{
				DrawData drawData;
				drawData..ctor(texture, BaseDrawing.GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, frame, framecount, framecountX, scale, drawCentered), new Rectangle?(frame), lightColor, rotation, origin, scale, (direction == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
				drawData.shader = shader;
				DrawData dd = drawData;
				((List<DrawData>)sb).Add(dd);
				return;
			}
			if (sb is SpriteBatch)
			{
				bool flag = shader > 0;
				if (flag)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
					GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);
				}
				((SpriteBatch)sb).Draw(texture, BaseDrawing.GetDrawPosition(position, origin, width, height, texture.Width, texture.Height, frame, framecount, framecountX, scale, drawCentered), new Rectangle?(frame), lightColor, rotation, origin, scale, (direction == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
				if (flag)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
				}
			}
		}

		public static void DrawHitbox(SpriteBatch sb, Rectangle hitbox, Color? overrideColor = null)
		{
			Vector2 origin = default(Vector2);
			Color lightColor = (overrideColor != null) ? overrideColor.Value : Color.White;
			Vector2 position = new Vector2((float)hitbox.Left, (float)hitbox.Top) - Main.screenPosition;
			sb.Draw(Main.magicPixel, position, new Rectangle?(hitbox), lightColor, 0f, origin, 1f, SpriteEffects.None, 0f);
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
			bool halfBrick = (overrideHalfBrick != null) ? overrideHalfBrick.Value : tile.halfBrick();
			int halfBrickOffset = halfBrick ? 8 : 0;
			Color color = Lighting.GetColor(x, y);
			Vector2 drawOffset = (Main.drawToScreen ? default(Vector2) : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange)) + offset;
			if (tile.inActive())
			{
				color = tile.actColor(color);
			}
			SpriteEffects effects = flipTex ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Vector2 drawPos = BaseDrawing.GetTileDrawPosition(x, y, fwidth, fheight, drawOffset);
			int gfxCheck = (int)(255f * (1f - Main.gfxQuality) + 30f * Main.gfxQuality);
			int gfxCheck2 = (int)(50f * (1f - Main.gfxQuality) + 2f * Main.gfxQuality);
			if (slopeDraw && tile.slope() > 0)
			{
				bool rightSlope = tile.rightSlope();
				bool topSlope = tile.topSlope();
				for (int i = 0; i < 8; i++)
				{
					int xOffset = rightSlope ? (i * 2) : (16 - i * 2 - 2);
					int yOffset = topSlope ? (i * 2) : 0;
					int frameOffsetX = xOffset;
					int height = 14 - i * 2;
					sb.Draw(texture, drawPos + new Vector2((float)xOffset, (float)yOffset), new Rectangle?(new Rectangle(frameX + frameOffsetX, frameY, 2, height)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
				}
				if (topSlope)
				{
					sb.Draw(texture, drawPos + new Vector2(0f, 14f), new Rectangle?(new Rectangle(frameX, frameY + 14, 16, 2)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					return;
				}
				sb.Draw(texture, drawPos, new Rectangle?(new Rectangle(frameX, frameY, 16, 2)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
				return;
			}
			else if (!ignoreHalfBricks && Main.tileSolid[(int)tile.type] && !halfBrick && (Main.tile[x - 1, y].halfBrick() || Main.tile[x + 1, y].halfBrick()))
			{
				if (Main.tile[x - 1, y].halfBrick() && Main.tile[x + 1, y].halfBrick())
				{
					sb.Draw(texture, drawPos + new Vector2(0f, 8f), new Rectangle?(new Rectangle(frameX, frameY + 8, fwidth, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					sb.Draw(texture, drawPos, new Rectangle?(new Rectangle(126, 0, 16, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					return;
				}
				if (Main.tile[x - 1, y].halfBrick())
				{
					sb.Draw(texture, drawPos + new Vector2(0f, 8f), new Rectangle?(new Rectangle(frameX, frameY + 8, fwidth, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					sb.Draw(texture, drawPos + new Vector2(4f, 0f), new Rectangle?(new Rectangle(frameX + 4, frameY, fwidth - 4, fheight)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					sb.Draw(texture, drawPos, new Rectangle?(new Rectangle(126, 0, 4, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					return;
				}
				if (Main.tile[x + 1, y].halfBrick())
				{
					sb.Draw(texture, drawPos + new Vector2(0f, 8f), new Rectangle?(new Rectangle(frameX, frameY + 8, fwidth, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					sb.Draw(texture, drawPos, new Rectangle?(new Rectangle(frameX, frameY, fwidth - 4, fheight)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					sb.Draw(texture, drawPos + new Vector2(12f, 0f), new Rectangle?(new Rectangle(138, 0, 4, 8)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					return;
				}
				sb.Draw(texture, drawPos, new Rectangle?(new Rectangle(frameX, frameY, fwidth, fheight)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
				return;
			}
			else if (Lighting.lightMode < 2 && Main.tileSolid[(int)tile.type] && !halfBrick && !tile.inActive())
			{
				if ((int)color.R > gfxCheck || (double)color.G > (double)gfxCheck * 1.1 || (double)color.B > (double)gfxCheck * 1.2)
				{
					Color[] lightArray = new Color[9];
					Lighting.GetColor9Slice(x, y, ref lightArray);
					for (int j = 0; j < 9; j++)
					{
						int offsetX = 0;
						int offsetY = 0;
						int width = 4;
						int height2 = 4;
						Color mixedColor = color;
						Color lightColor = lightArray[j];
						if (j == 1)
						{
							width = 8;
							offsetX = 4;
						}
						else if (j == 2)
						{
							offsetX = 12;
						}
						else if (j == 3)
						{
							height2 = 8;
							offsetY = 4;
						}
						else if (j == 4)
						{
							width = 8;
							height2 = 8;
							offsetX = 4;
							offsetY = 4;
						}
						else if (j == 5)
						{
							offsetX = 12;
							offsetY = 4;
							height2 = 8;
						}
						else if (j == 6)
						{
							offsetY = 12;
						}
						else if (j == 7)
						{
							width = 8;
							height2 = 4;
							offsetX = 4;
							offsetY = 12;
						}
						else if (j == 8)
						{
							offsetX = 12;
							offsetY = 12;
						}
						mixedColor.R = (color.R + lightColor.R) / 2;
						mixedColor.G = (color.G + lightColor.G) / 2;
						mixedColor.B = (color.B + lightColor.B) / 2;
						sb.Draw(texture, drawPos + new Vector2((float)offsetX, (float)offsetY), new Rectangle?(new Rectangle(frameX + offsetX, frameY + offsetY, width, height2)), (overrideColor != null) ? overrideColor(mixedColor) : mixedColor, 0f, default(Vector2), 1f, effects, 0f);
					}
					return;
				}
				if ((int)color.R > gfxCheck2 || (double)color.G > (double)gfxCheck2 * 1.1 || (double)color.B > (double)gfxCheck2 * 1.2)
				{
					Color[] lightArray2 = new Color[4];
					Lighting.GetColor4Slice(x, y, ref lightArray2);
					for (int k = 0; k < 4; k++)
					{
						int offsetX2 = 0;
						int offsetY2 = 0;
						Color mixedColor2 = color;
						Color lightColor2 = lightArray2[k];
						if (k == 1)
						{
							offsetX2 = 8;
						}
						if (k == 2)
						{
							offsetY2 = 8;
						}
						if (k == 3)
						{
							offsetX2 = 8;
							offsetY2 = 8;
						}
						mixedColor2.R = (color.R + lightColor2.R) / 2;
						mixedColor2.G = (color.G + lightColor2.G) / 2;
						mixedColor2.B = (color.B + lightColor2.B) / 2;
						sb.Draw(texture, drawPos + new Vector2((float)offsetX2, (float)offsetY2), new Rectangle?(new Rectangle(frameX + offsetX2, frameY + offsetY2, 8, 8)), (overrideColor != null) ? overrideColor(mixedColor2) : mixedColor2, 0f, default(Vector2), 1f, effects, 0f);
					}
					return;
				}
				sb.Draw(texture, drawPos, new Rectangle?(new Rectangle(frameX, frameY, fwidth, fheight)), color, 0f, default(Vector2), 1f, effects, 0f);
				return;
			}
			else
			{
				if (halfBrickOffset == 8 && (!Main.tile[x, y + 1].active() || !Main.tileSolid[(int)Main.tile[x, y + 1].type] || Main.tile[x, y + 1].halfBrick()))
				{
					sb.Draw(texture, drawPos + new Vector2(0f, (float)halfBrickOffset), new Rectangle?(new Rectangle(frameX, frameY, fwidth, fheight - halfBrickOffset - 4)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					sb.Draw(texture, drawPos + new Vector2(0f, 12f), new Rectangle?(new Rectangle(144, 66, fwidth, 4)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
					return;
				}
				sb.Draw(texture, drawPos + new Vector2(0f, (float)halfBrickOffset), new Rectangle?(new Rectangle(frameX, frameY, fwidth, fheight)), (overrideColor != null) ? overrideColor(color) : color, 0f, default(Vector2), 1f, effects, 0f);
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
			int gfxCheck = (int)(255f * (1f - Main.gfxQuality) + 100f * Main.gfxQuality);
			int gfxCheck2 = (int)(120f * (1f - Main.gfxQuality) + 40f * Main.gfxQuality);
			Vector2 drawOffset = (Main.drawToScreen ? default(Vector2) : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange)) + offset;
			int tileColor = (int)((Main.tileColor.R + Main.tileColor.G + Main.tileColor.B) / 3);
			float num = (float)((double)tileColor * 0.53) / 255f;
			if (Lighting.lightMode == 2)
			{
				float num2 = (float)(Main.tileColor.R - 12) / 255f;
			}
			else if (Lighting.lightMode == 3)
			{
				float num3 = (float)(tileColor - 12) / 255f;
			}
			Color color = (overrideColor != null) ? overrideColor(default(Color)) : Lighting.GetColor(x, y);
			if (Lighting.lightMode < 2)
			{
				if ((int)color.R > gfxCheck || (double)color.G > (double)gfxCheck * 1.1 || (double)color.B > (double)gfxCheck * 1.2)
				{
					Color[] lightArray = new Color[9];
					Lighting.GetColor9Slice(x, y, ref lightArray);
					for (int i = 0; i < 9; i++)
					{
						int offsetX = 0;
						int offsetY = 0;
						int width = 12;
						int height = 12;
						Color color2 = color;
						Color color3 = lightArray[i];
						if (i == 1)
						{
							width = 8;
							offsetX = 12;
						}
						if (i == 2)
						{
							offsetX = 20;
						}
						if (i == 3)
						{
							height = 8;
							offsetY = 12;
						}
						if (i == 4)
						{
							width = 8;
							height = 8;
							offsetX = 12;
							offsetY = 12;
						}
						if (i == 5)
						{
							offsetX = 20;
							offsetY = 12;
							height = 8;
						}
						if (i == 6)
						{
							offsetY = 20;
						}
						if (i == 7)
						{
							width = 12;
							offsetX = 12;
							offsetY = 20;
						}
						if (i == 8)
						{
							offsetX = 20;
							offsetY = 20;
						}
						color2.R = (color.R + color3.R) / 2;
						color2.G = (color.G + color3.G) / 2;
						color2.B = (color.B + color3.B) / 2;
						sb.Draw(texture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8 + offsetX), (float)(y * 16 - (int)Main.screenPosition.Y - 8 + offsetY)) + drawOffset, new Rectangle?(new Rectangle(wallFrameX + offsetX, wallFrameY + offsetY + frameOffsetY, width, height)), (overrideColor != null) ? overrideColor(color2) : color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
				else if ((int)color.R > gfxCheck2 || (double)color.G > (double)gfxCheck2 * 1.1 || (double)color.B > (double)gfxCheck2 * 1.2)
				{
					Color[] lightArray2 = new Color[4];
					Lighting.GetColor4Slice(x, y, ref lightArray2);
					for (int j = 0; j < 4; j++)
					{
						int offsetX2 = 0;
						int offsetY2 = 0;
						Color color4 = color;
						Color color5 = lightArray2[j];
						if (j == 1)
						{
							offsetX2 = 16;
						}
						if (j == 2)
						{
							offsetY2 = 16;
						}
						if (j == 3)
						{
							offsetX2 = 16;
							offsetY2 = 16;
						}
						color4.R = (color.R + color5.R) / 2;
						color4.G = (color.G + color5.G) / 2;
						color4.B = (color.B + color5.B) / 2;
						sb.Draw(texture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8 + offsetX2), (float)(y * 16 - (int)Main.screenPosition.Y - 8 + offsetY2)) + drawOffset, new Rectangle?(new Rectangle(wallFrameX + offsetX2, wallFrameY + offsetY2 + frameOffsetY, 16, 16)), (overrideColor != null) ? overrideColor(color4) : color4, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
				else
				{
					Rectangle rect = new Rectangle(wallFrameX, wallFrameY + frameOffsetY, 32, 32);
					sb.Draw(texture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X - 8), (float)(y * 16 - (int)Main.screenPosition.Y - 8)) + drawOffset, new Rectangle?(rect), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			if (drawOutline && ((double)color.R > (double)gfxCheck2 * 0.4 || (double)color.G > (double)gfxCheck2 * 0.35 || (double)color.B > (double)gfxCheck2 * 0.3))
			{
				bool flag = Main.tile[x - 1, y].wall > 0 && Main.wallBlend[(int)Main.tile[x - 1, y].wall] != Main.wallBlend[(int)Main.tile[x, y].wall];
				bool outlineRight = Main.tile[x + 1, y].wall > 0 && Main.wallBlend[(int)Main.tile[x + 1, y].wall] != Main.wallBlend[(int)Main.tile[x, y].wall];
				bool outlineUp = Main.tile[x, y - 1].wall > 0 && Main.wallBlend[(int)Main.tile[x, y - 1].wall] != Main.wallBlend[(int)Main.tile[x, y].wall];
				bool outlineDown = Main.tile[x, y + 1].wall > 0 && Main.wallBlend[(int)Main.tile[x, y + 1].wall] != Main.wallBlend[(int)Main.tile[x, y].wall];
				if (flag)
				{
					sb.Draw(Main.wallOutlineTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y)) + drawOffset, new Rectangle?(new Rectangle(0, 0, 2, 16)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				if (outlineRight)
				{
					sb.Draw(Main.wallOutlineTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X + 14), (float)(y * 16 - (int)Main.screenPosition.Y)) + drawOffset, new Rectangle?(new Rectangle(14, 0, 2, 16)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				if (outlineUp)
				{
					sb.Draw(Main.wallOutlineTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y)) + drawOffset, new Rectangle?(new Rectangle(0, 0, 16, 2)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				if (outlineDown)
				{
					sb.Draw(Main.wallOutlineTexture, new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y + 14)) + drawOffset, new Rectangle?(new Rectangle(0, 14, 16, 2)), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
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
			Vector2 screenPos = new Vector2((float)((int)Main.screenPosition.X), (float)((int)Main.screenPosition.Y));
			if (drawCentered)
			{
				Vector2 texHalf = new Vector2((float)(texWidth / framecountX / 2), (float)(texHeight / framecount / 2));
				return position + new Vector2((float)(width / 2), (float)(height / 2)) - texHalf * scale + origin * scale - screenPos;
			}
			return position - screenPos + new Vector2((float)(width / 2), (float)height) - new Vector2((float)(texWidth / framecountX / 2), (float)(texHeight / framecount)) * scale + origin * scale + new Vector2(0f, 5f);
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
			Vector2 frameCenter = new Vector2((float)locationFrame.Width * 0.5f, (float)locationFrame.Height * 0.5f);
			Color color = (overrideColor != null) ? overrideColor.Value : BaseDrawing.GetPlayerColor(drawPlayer, new Vector2?(drawPlayer.Center), false, 0f);
			Rectangle frame = (frameRect != null) ? frameRect.Value : drawPlayer.bodyFrame;
			SpriteEffects effect = (drawPlayer.direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			if (drawPlayer.gravDir == -1f)
			{
				effect |= SpriteEffects.FlipVertically;
			}
			float scale = (scaleOverride > 0f) ? scaleOverride : 1f;
			if (sb is List<DrawData>)
			{
				DrawData drawData;
				drawData..ctor(texture, new Vector2((float)((int)(ediPos.X - (float)((int)Main.screenPosition.X) - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(ediPos.Y - (float)((int)Main.screenPosition.Y) + (float)drawPlayer.height - (float)frame.Height))) + new Vector2(offsetX * scale, offsetY * scale) + locationPos + frameCenter, new Rectangle?(frame), color, locationRot, frameCenter, scale, effect, 0);
				drawData.shader = shader;
				DrawData dd = drawData;
				((List<DrawData>)sb).Add(dd);
				return;
			}
			if (sb is SpriteBatch)
			{
				bool flag = shader > 0;
				if (flag)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
					GameShaders.Armor.ApplySecondary(shader, drawPlayer, null);
				}
				((SpriteBatch)sb).Draw(texture, new Vector2((float)((int)(ediPos.X - (float)((int)Main.screenPosition.X) - (float)(frame.Width / 2) + (float)(drawPlayer.width / 2))), (float)((int)(ediPos.Y - (float)((int)Main.screenPosition.Y) + (float)drawPlayer.height - (float)frame.Height))) + new Vector2(offsetX * scale, offsetY * scale) + locationPos + frameCenter, new Rectangle?(frame), color, locationRot, frameCenter, scale, effect, 0f);
				if (flag)
				{
					((SpriteBatch)sb).End();
					((SpriteBatch)sb).Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
				}
			}
		}

		public static float GetYOffset(Player player)
		{
			return BaseDrawing.GetYOffset(player.bodyFrame, player.gravDir);
		}

		public static float GetYOffset(Rectangle frame, float gravDir = 0f)
		{
			int frameID = frame.Y / frame.Height;
			if (frameID != 7 && frameID != 8 && frameID != 9 && frameID != 14 && frameID != 15 && frameID != 16)
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

		private static Rectangle drawZoneRect;
	}
}
