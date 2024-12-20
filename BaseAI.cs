using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;

namespace Redemption
{
	public class BaseAI
	{
		public static void AIDive(Projectile projectile, ref int chargeTime, int chargeTimeMax, Vector2 targetCenter)
		{
			chargeTime = Math.Max(0, chargeTime - 1);
			if (chargeTime > 0)
			{
				Vector2 positionOld = projectile.position - projectile.velocity;
				float percent = (float)chargeTime / (float)chargeTimeMax;
				float place = (float)Math.Sin((double)(3.1415927f * percent));
				float distX = Math.Abs(targetCenter.X - projectile.Center.X);
				float num = Math.Abs(targetCenter.Y - projectile.Center.Y);
				float distPercentX = distX / (float)chargeTimeMax;
				float distPercentY = num / (float)chargeTimeMax;
				projectile.velocity = new Vector2(distPercentX * (float)chargeTime * (float)projectile.direction, place * distPercentY);
				projectile.position = positionOld;
			}
		}

		public static void AIMinionPlant(Projectile projectile, ref float[] ai, Entity owner, Vector2 endPoint, bool setTime = true, float vineLength = 150f, float vineLengthLong = 200f, int vineTimeExtend = 300, int vineTimeMax = 450, float moveInterval = 0.035f, float speedMax = 2f, Vector2 targetOffset = default(Vector2), Func<Entity, Entity, Entity> GetTarget = null, Func<Entity, Entity, Entity, bool> ShootTarget = null)
		{
			if (setTime)
			{
				projectile.timeLeft = 10;
			}
			Entity target = (GetTarget == null) ? null : GetTarget(projectile, owner);
			if (target == null)
			{
				target = owner;
			}
			bool flag = target == owner;
			ai[0] += 1f;
			if (ai[0] > (float)vineTimeExtend)
			{
				vineLength = vineLengthLong;
				if (ai[0] > (float)vineTimeMax)
				{
					ai[0] = 0f;
				}
			}
			Vector2 targetCenter = target.Center + targetOffset + ((target == owner) ? new Vector2(0f, (owner is Player) ? ((Player)owner).gfxOffY : ((owner is NPC) ? ((NPC)owner).gfxOffY : ((Projectile)owner).gfxOffY)) : default(Vector2));
			if (!flag)
			{
				float distTargetX = targetCenter.X - endPoint.X;
				float distTargetY = targetCenter.Y - endPoint.Y;
				float distTarget = (float)Math.Sqrt((double)(distTargetX * distTargetX + distTargetY * distTargetY));
				if (distTarget > vineLength)
				{
					projectile.velocity *= 0.85f;
					projectile.velocity += owner.velocity;
					distTarget = vineLength / distTarget;
					distTargetX *= distTarget;
					distTargetY *= distTarget;
				}
				if (ShootTarget == null || !ShootTarget(projectile, owner, target))
				{
					if (projectile.position.X < endPoint.X + distTargetX)
					{
						projectile.velocity.X = projectile.velocity.X + moveInterval;
						if (projectile.velocity.X < 0f && distTargetX > 0f)
						{
							projectile.velocity.X = projectile.velocity.X + moveInterval * 1.5f;
						}
					}
					else if (projectile.position.X > endPoint.X + distTargetX)
					{
						projectile.velocity.X = projectile.velocity.X - moveInterval;
						if (projectile.velocity.X > 0f && distTargetX < 0f)
						{
							projectile.velocity.X = projectile.velocity.X - moveInterval * 1.5f;
						}
					}
					if (projectile.position.Y < endPoint.Y + distTargetY)
					{
						projectile.velocity.Y = projectile.velocity.Y + moveInterval;
						if (projectile.velocity.Y < 0f && distTargetY > 0f)
						{
							projectile.velocity.Y = projectile.velocity.Y + moveInterval * 1.5f;
						}
					}
					else if (projectile.position.Y > endPoint.Y + distTargetY)
					{
						projectile.velocity.Y = projectile.velocity.Y - moveInterval;
						if (projectile.velocity.Y > 0f && distTargetY < 0f)
						{
							projectile.velocity.Y = projectile.velocity.Y - moveInterval * 1.5f;
						}
					}
				}
				else
				{
					projectile.velocity *= 0.85f;
					if (Math.Abs(projectile.velocity.X) < moveInterval + 0.01f)
					{
						projectile.velocity.X = 0f;
					}
					if (Math.Abs(projectile.velocity.Y) < moveInterval + 0.01f)
					{
						projectile.velocity.Y = 0f;
					}
				}
				projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -speedMax, speedMax);
				projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -speedMax, speedMax);
				if (distTargetX > 0f)
				{
					projectile.spriteDirection = 1;
					projectile.rotation = (float)Math.Atan2((double)distTargetY, (double)distTargetX);
				}
				else if (distTargetX < 0f)
				{
					projectile.spriteDirection = -1;
					projectile.rotation = (float)Math.Atan2((double)distTargetY, (double)distTargetX) + 3.14f;
				}
				if (projectile.tileCollide)
				{
					Vector4 slopeVec = Collision.SlopeCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, 0f, false);
					projectile.position = new Vector2(slopeVec.X, slopeVec.Y);
					projectile.velocity = new Vector2(slopeVec.Z, slopeVec.W);
				}
				projectile.position += owner.position - owner.oldPosition;
				return;
			}
			projectile.position += owner.position - owner.oldPosition;
			projectile.spriteDirection = ((owner.Center.X > projectile.Center.X) ? -1 : 1);
			projectile.velocity = BaseAI.AIVelocityLinear(projectile, targetCenter, moveInterval, speedMax, true);
			if (Vector2.Distance(projectile.Center, targetCenter) < speedMax * 1.1f)
			{
				projectile.rotation = 0f;
				projectile.velocity *= 0f;
				projectile.Center = targetCenter;
				return;
			}
			projectile.rotation = BaseUtility.RotationTo(targetCenter, projectile.Center) + ((projectile.spriteDirection == -1) ? 3.14f : 0f);
		}

		public static void TileCollidePlant(Projectile projectile, ref Vector2 velocity, float speedMax)
		{
			if (projectile.velocity.X != velocity.X)
			{
				projectile.netUpdate = true;
				projectile.velocity.X = projectile.velocity.X * -0.7f;
				projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -speedMax, speedMax);
			}
			if (projectile.velocity.Y != velocity.Y)
			{
				projectile.netUpdate = true;
				projectile.velocity.Y = projectile.velocity.Y * -0.7f;
				projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -speedMax, speedMax);
			}
		}

		public static void AIMinionFlier(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, bool movementFixed = false, bool hover = false, int hoverHeight = 40, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = -1f, float maxSpeed = -1f, float maxSpeedFlying = -1f, bool autoSpriteDir = true, bool dummyTileCollide = false, Func<Entity, Entity, Entity> GetTarget = null, Func<Entity, Entity, Entity, bool> ShootTarget = null)
		{
			if (moveInterval == -1f)
			{
				moveInterval = 0.08f * Main.player[projectile.owner].moveSpeed;
			}
			if (maxSpeed == -1f)
			{
				maxSpeed = Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed);
			}
			if (maxSpeedFlying == -1f)
			{
				maxSpeedFlying = Math.Max(maxSpeed, Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed));
			}
			projectile.timeLeft = 10;
			bool tileCollide = projectile.tileCollide;
			BaseAI.AIMinionFlier(projectile, ref ai, owner, ref tileCollide, ref projectile.netUpdate, pet ? 0 : projectile.minionPos, movementFixed, hover, hoverHeight, lineDist, returnDist, teleportDist, moveInterval, maxSpeed, maxSpeedFlying, GetTarget, ShootTarget);
			if (!dummyTileCollide)
			{
				projectile.tileCollide = tileCollide;
			}
			if (autoSpriteDir)
			{
				projectile.spriteDirection = projectile.direction;
			}
			Vector2.Distance(projectile.Center, owner.Center);
			if (ai[0] == 1f)
			{
				projectile.spriteDirection = ((owner.velocity.X == 0f) ? projectile.spriteDirection : ((owner.velocity.X > 0f) ? 1 : -1));
			}
			if ((GetTarget == null || GetTarget(projectile, owner) == null || GetTarget(projectile, owner) == owner) && Math.Abs(projectile.velocity.X + projectile.velocity.Y) <= 0.025f)
			{
				projectile.spriteDirection = ((owner.Center.X > projectile.Center.X) ? 1 : -1);
			}
		}

		public static void AIMinionFlier(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, int minionPos, bool movementFixed, bool hover = false, int hoverHeight = 40, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = 0.2f, float maxSpeed = 4.5f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> GetTarget = null, Func<Entity, Entity, Entity, bool> ShootTarget = null)
		{
			float dist = Vector2.Distance(codable.Center, owner.Center);
			if (dist > (float)teleportDist)
			{
				codable.Center = owner.Center;
			}
			int tileX = (int)(codable.Center.X / 16f);
			int tileY = (int)(codable.Center.Y / 16f);
			Tile tile = Main.tile[tileX, tileY];
			bool inTile = tile != null && tile.nactive() && Main.tileSolid[(int)tile.type];
			float prevAI = ai[0];
			ai[0] = (float)(((ai[0] == 1f && (dist > Math.Max((float)lineDist, (float)returnDist / 2f) || !BaseUtility.CanHit(codable.Hitbox, owner.Hitbox))) || dist > (float)returnDist || inTile) ? 1 : 0);
			if (ai[0] != prevAI)
			{
				netUpdate = true;
			}
			if (ai[0] == 0f || ai[0] == 1f)
			{
				if (ai[0] == 1f)
				{
					moveInterval *= 1.5f;
					maxSpeedFlying *= 1.5f;
				}
				tileCollide = (ai[0] == 0f);
				Entity target = (GetTarget == null) ? owner : GetTarget(codable, owner);
				if (target == null)
				{
					target = owner;
				}
				Vector2 targetCenter = target.Center;
				bool isOwner = target == owner;
				object obj = ai[0] == 0f && ShootTarget != null && ShootTarget(codable, owner, target);
				if (isOwner)
				{
					targetCenter.Y -= (float)hoverHeight;
					if (hover)
					{
						targetCenter.X += (float)((lineDist + lineDist * minionPos) * -(float)target.direction);
					}
				}
				if (!hover || !isOwner)
				{
					float dirDist = hover ? 1.2f : 1.8f;
					float dir = (dist < (float)(lineDist * minionPos) + (float)lineDist * dirDist) ? ((codable.velocity.X > 0f) ? 1f : -1f) : ((target.Center.X > codable.Center.X) ? 1f : -1f);
					targetCenter.X += ((minionPos == 0) ? 0f : ((minionPos % 5 == 0) ? ((float)lineDist / 4f) : ((minionPos % 4 == 0) ? ((float)lineDist / 2f) : ((minionPos % 3 == 0) ? ((float)lineDist / 3f) : 0f)))) * dir;
					targetCenter.X += (float)lineDist * 2f * dir;
					targetCenter.Y -= (float)hoverHeight / 4f * (float)minionPos;
					targetCenter.Y -= ((codable.velocity.X < 0f) ? ((float)lineDist * 0.25f) : ((float)(-(float)lineDist) * 0.25f)) * (float)((minionPos % 2 == 0) ? 1 : -1);
				}
				float targetDistX = Math.Abs(codable.Center.X - targetCenter.X);
				float targetDistY = Math.Abs(codable.Center.Y - targetCenter.Y);
				bool slowdownX = hover && owner.velocity.X < 0.025f && targetDistX < 8f * Math.Max(1f, maxSpeed / 4f);
				bool slowdownY = hover && owner.velocity.Y < 0.025f && targetDistY < 8f * Math.Max(1f, maxSpeed / 4f);
				Vector2 vel = BaseAI.AIVelocityLinear(codable, targetCenter, moveInterval, (ai[0] == 0f) ? maxSpeed : maxSpeedFlying, true);
				object obj2 = obj;
				if (obj2 == null && !slowdownX)
				{
					codable.velocity.X = codable.velocity.X + vel.X * 0.125f;
				}
				if (obj2 == null && !slowdownY)
				{
					codable.velocity.Y = codable.velocity.Y + vel.Y * 0.125f;
				}
				if ((obj2 | slowdownX) != null)
				{
					codable.velocity.X = codable.velocity.X * ((Math.Abs(codable.velocity.X) > 0.01f) ? 0.85f : 0f);
				}
				if ((vel.X > 0f && codable.velocity.X > vel.X) || (vel.X < 0f && codable.velocity.X < vel.X))
				{
					codable.velocity.X = vel.X;
				}
				if ((obj2 | slowdownY) != null)
				{
					codable.velocity.Y = codable.velocity.Y * ((Math.Abs(codable.velocity.Y) > 0.01f) ? 0.85f : 0f);
				}
				if ((vel.Y > 0f && codable.velocity.Y > vel.Y) || (vel.Y < 0f && codable.velocity.X < vel.Y))
				{
					codable.velocity.Y = vel.Y;
				}
			}
		}

		public static void AIMinionFighter(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, int jumpDistX = 4, int jumpDistY = 5, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = -1f, float maxSpeed = -1f, float maxSpeedFlying = -1f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			if (moveInterval == -1f)
			{
				moveInterval = 0.08f * Main.player[projectile.owner].moveSpeed;
			}
			if (maxSpeed == -1f)
			{
				maxSpeed = Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed);
			}
			if (maxSpeedFlying == -1f)
			{
				maxSpeedFlying = Math.Max(maxSpeed, Math.Max(Main.player[projectile.owner].maxRunSpeed, Main.player[projectile.owner].accRunSpeed));
			}
			projectile.timeLeft = 10;
			BaseAI.AIMinionFighter(projectile, ref ai, owner, ref projectile.tileCollide, ref projectile.netUpdate, ref projectile.gfxOffY, ref projectile.stepSpeed, pet ? 0 : projectile.minionPos, jumpDistX, jumpDistY, lineDist, returnDist, teleportDist, moveInterval, maxSpeed, maxSpeedFlying, GetTarget);
			projectile.spriteDirection = projectile.direction;
			Vector2.Distance(projectile.Center, owner.Center);
			if (ai[0] == 1f)
			{
				projectile.spriteDirection = ((owner.velocity.X == 0f) ? projectile.spriteDirection : ((owner.velocity.X > 0f) ? 1 : -1));
			}
			if ((GetTarget == null || GetTarget(projectile, owner) == null || GetTarget(projectile, owner) == owner) && (projectile.velocity.X >= -0.025f || projectile.velocity.X <= 0.025f) && projectile.velocity.Y == 0f)
			{
				projectile.spriteDirection = ((owner.Center.X > projectile.Center.X) ? 1 : -1);
			}
		}

		public static void AIMinionFighter(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, ref float gfxOffY, ref float stepSpeed, int minionPos, int jumpDistX = 4, int jumpDistY = 5, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float moveInterval = 0.2f, float maxSpeed = 4.5f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			float dist = Vector2.Distance(codable.Center, owner.Center);
			if (dist > (float)teleportDist)
			{
				codable.Center = owner.Center;
			}
			int tileX = (int)(codable.Center.X / 16f);
			int tileY = (int)(codable.Center.Y / 16f);
			Tile tile = Main.tile[tileX, tileY];
			bool inTile = tile != null && tile.nactive() && Main.tileSolid[(int)tile.type];
			float prevAI = ai[0];
			ai[0] = (float)(((ai[0] == 1f && (owner.velocity.Y != 0f || dist > Math.Max((float)lineDist, (float)returnDist / 10f))) || dist > (float)returnDist || inTile) ? 1 : 0);
			if (ai[0] != prevAI)
			{
				netUpdate = true;
			}
			if (ai[0] == 0f)
			{
				tileCollide = true;
				Entity target = (GetTarget == null) ? null : GetTarget(codable, owner);
				Vector2 targetCenter = (target == null) ? default(Vector2) : target.Center;
				bool isOwner = target == null || targetCenter == owner.Center;
				if (targetCenter == default(Vector2))
				{
					targetCenter = owner.Center;
					targetCenter.X += (float)((owner.width + 10 + lineDist * minionPos) * -(float)owner.direction);
				}
				float targetDistX = Math.Abs(codable.Center.X - targetCenter.X);
				float targetDistY = Math.Abs(codable.Center.Y - targetCenter.Y);
				int moveDirection = (targetCenter.X > codable.Center.X) ? 1 : -1;
				int moveDirectionY = (targetCenter.Y > codable.Center.Y) ? 1 : -1;
				if (isOwner && owner.velocity.X < 0.025f && codable.velocity.Y == 0f && targetDistX < 8f)
				{
					codable.velocity.X = codable.velocity.X * ((Math.Abs(codable.velocity.X) > 0.01f) ? 0.8f : 0f);
				}
				else if (codable.velocity.X < -maxSpeed || codable.velocity.X > maxSpeed)
				{
					if (codable.velocity.Y == 0f)
					{
						codable.velocity *= 0.85f;
					}
				}
				else if (codable.velocity.X < maxSpeed && moveDirection == 1)
				{
					if (codable.velocity.X < 0f)
					{
						codable.velocity.X = codable.velocity.X * 0.85f;
					}
					codable.velocity.X = codable.velocity.X + moveInterval * ((codable.velocity.X < 0f) ? 2f : 1f);
					if (codable.velocity.X > maxSpeed)
					{
						codable.velocity.X = maxSpeed;
					}
				}
				else if (codable.velocity.X > -maxSpeed && moveDirection == -1)
				{
					if (codable.velocity.X > 0f)
					{
						codable.velocity.X = codable.velocity.X * 0.8f;
					}
					codable.velocity.X = codable.velocity.X - moveInterval * ((codable.velocity.X > 0f) ? 2f : 1f);
					if (codable.velocity.X < -maxSpeed)
					{
						codable.velocity.X = -maxSpeed;
					}
				}
				BaseAI.WalkupHalfBricks(codable, ref gfxOffY, ref stepSpeed);
				if (!BaseAI.HitTileOnSide(codable, 3, true))
				{
					codable.velocity.Y = codable.velocity.Y + 0.35f;
					return;
				}
				if ((codable.velocity.X < 0f && moveDirection == -1) || (codable.velocity.X > 0f && moveDirection == 1))
				{
					bool test = target != null && !isOwner && targetDistX < 50f && targetDistY > (float)(codable.height + codable.height / 2) && targetDistY < 16f * (float)(jumpDistY + 1) && BaseUtility.CanHit(codable.Hitbox, target.Hitbox);
					Vector2 newVec = BaseAI.AttemptJump(codable.position, codable.velocity, codable.width, codable.height, moveDirection, (float)moveDirectionY, jumpDistX, jumpDistY, maxSpeed, true, target, test);
					if (tileCollide)
					{
						newVec = Collision.TileCollision(codable.position, newVec, codable.width, codable.height, false, false, 1);
						Vector4 slopeVec = Collision.SlopeCollision(codable.position, newVec, codable.width, codable.height, 0f, false);
						codable.position = new Vector2(slopeVec.X, slopeVec.Y);
						codable.velocity = new Vector2(slopeVec.Z, slopeVec.W);
					}
					if (codable.velocity != newVec)
					{
						codable.velocity = newVec;
						netUpdate = true;
						return;
					}
				}
			}
			else
			{
				tileCollide = false;
				Vector2 targetCenter2 = owner.Center;
				if (owner.velocity.Y != 0f && dist < 80f)
				{
					targetCenter2 = owner.Center + BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), BaseUtility.RotationTo(codable.Center, owner.Center));
				}
				Vector2 newVel = BaseUtility.RotateVector(default(Vector2), new Vector2(maxSpeedFlying, 0f), BaseUtility.RotationTo(codable.Center, targetCenter2));
				if (owner.velocity.Y != 0f && ((newVel.X > 0f && codable.velocity.X < 0f) || (newVel.X < 0f && codable.velocity.X > 0f)))
				{
					codable.velocity *= 0.98f;
					newVel *= 0.02f;
					codable.velocity += newVel;
				}
				else
				{
					codable.velocity = newVel;
				}
				codable.position += owner.velocity;
			}
		}

		public static void AIMinionSlime(Projectile projectile, ref float[] ai, Entity owner, bool pet = false, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float jumpVelX = -1f, float jumpVelY = 20f, float maxSpeedFlying = -1f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			if (jumpVelX == -1f)
			{
				jumpVelX = 2f + Main.player[projectile.owner].velocity.X;
			}
			if (maxSpeedFlying == -1f)
			{
				maxSpeedFlying = Math.Max(jumpVelX, jumpVelY);
			}
			projectile.timeLeft = 10;
			BaseAI.AIMinionSlime(projectile, ref ai, owner, ref projectile.tileCollide, ref projectile.netUpdate, pet ? 0 : projectile.minionPos, lineDist, returnDist, teleportDist, jumpVelX, jumpVelY, maxSpeedFlying, GetTarget);
			projectile.spriteDirection = projectile.direction;
			Vector2.Distance(projectile.Center, owner.Center);
			if (ai[0] == 1f)
			{
				projectile.spriteDirection = ((owner.velocity.X == 0f) ? projectile.spriteDirection : ((owner.velocity.X > 0f) ? 1 : -1));
			}
			if ((GetTarget == null || GetTarget(projectile, owner) == null || GetTarget(projectile, owner) == owner) && (projectile.velocity.X >= -0.025f || projectile.velocity.X <= 0.025f) && projectile.velocity.Y == 0f)
			{
				projectile.spriteDirection = ((owner.Center.X > projectile.Center.X) ? 1 : -1);
			}
		}

		public static void AIMinionSlime(Entity codable, ref float[] ai, Entity owner, ref bool tileCollide, ref bool netUpdate, int minionPos, int lineDist = 40, int returnDist = 400, int teleportDist = 800, float jumpVelX = 2f, float jumpVelY = 20f, float maxSpeedFlying = 4.5f, Func<Entity, Entity, Entity> GetTarget = null)
		{
			float dist = Vector2.Distance(codable.Center, owner.Center);
			if (dist > (float)teleportDist)
			{
				codable.Center = owner.Center;
			}
			int tileX = (int)(codable.Center.X / 16f);
			int tileY = (int)(codable.Center.Y / 16f);
			Tile tile = Main.tile[tileX, tileY];
			bool inTile = tile != null && tile.nactive() && Main.tileSolid[(int)tile.type];
			float prevAI = ai[0];
			ai[0] = (float)(((ai[0] == 1f && (owner.velocity.Y != 0f || dist > Math.Max((float)lineDist, (float)returnDist / 10f))) || dist > (float)returnDist || inTile) ? 1 : 0);
			if (ai[0] != prevAI)
			{
				netUpdate = true;
			}
			if (ai[0] == 0f)
			{
				tileCollide = true;
				Entity target = (GetTarget == null) ? null : GetTarget(codable, owner);
				Vector2 targetCenter = (target == null) ? default(Vector2) : target.Center;
				bool flag = target == null || targetCenter == owner.Center;
				if (targetCenter == default(Vector2))
				{
					targetCenter = owner.Center;
					targetCenter.X += (float)((lineDist + lineDist * minionPos) * -(float)owner.direction);
				}
				float targetDistX = Math.Abs(codable.Center.X - targetCenter.X);
				Math.Abs(codable.Center.Y - targetCenter.Y);
				int moveDirection = (targetCenter.X > codable.Center.X) ? 1 : -1;
				float y = targetCenter.Y;
				float y2 = codable.Center.Y;
				if (flag && owner.velocity.X < 0.025f && codable.velocity.Y == 0f && targetDistX < 8f)
				{
					codable.velocity.X = codable.velocity.X * ((Math.Abs(codable.velocity.X) > 0.01f) ? 0.8f : 0f);
				}
				else if (codable.velocity.Y == 0f)
				{
					codable.velocity.X = codable.velocity.X * 0.8f;
					if (codable.velocity.X > -0.1f && codable.velocity.X < 0.1f)
					{
						codable.velocity.X = 0f;
					}
					codable.velocity.Y = -jumpVelY;
					codable.velocity.X = codable.velocity.X + jumpVelX * (float)moveDirection;
					codable.position += codable.velocity;
				}
				if (!BaseAI.HitTileOnSide(codable, 3, true))
				{
					codable.velocity.Y = codable.velocity.Y + 0.35f;
					return;
				}
				if ((codable.velocity.X < 0f && moveDirection == -1) || (codable.velocity.X > 0f && moveDirection == 1))
				{
					Vector2 newVec = codable.velocity;
					if (tileCollide)
					{
						newVec = Collision.TileCollision(codable.position, newVec, codable.width, codable.height, false, false, 1);
						Vector4 slopeVec = Collision.SlopeCollision(codable.position, newVec, codable.width, codable.height, 0f, false);
						codable.position = new Vector2(slopeVec.X, slopeVec.Y);
						codable.velocity = new Vector2(slopeVec.Z, slopeVec.W);
					}
					if (codable.velocity != newVec)
					{
						codable.velocity = newVec;
						netUpdate = true;
						return;
					}
				}
			}
			else
			{
				tileCollide = false;
				Vector2 targetCenter2 = owner.Center;
				if (owner.velocity.Y != 0f && dist < 80f)
				{
					targetCenter2 = owner.Center + BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), BaseUtility.RotationTo(codable.Center, owner.Center));
				}
				Vector2 newVel = BaseUtility.RotateVector(default(Vector2), new Vector2(maxSpeedFlying, 0f), BaseUtility.RotationTo(codable.Center, targetCenter2));
				if (owner.velocity.Y != 0f && ((newVel.X > 0f && codable.velocity.X < 0f) || (newVel.X < 0f && codable.velocity.X > 0f)))
				{
					codable.velocity *= 0.98f;
					newVel *= 0.02f;
					codable.velocity += newVel;
				}
				else
				{
					codable.velocity = newVel;
				}
				codable.position += owner.velocity;
			}
		}

		public static void AIRotate(Entity codable, ref float rotation, ref float moveRot, Vector2 rotateCenter, bool absolute = false, float rotDistance = 50f, float rotThreshold = 20f, float rotAmount = 0.024f, bool moveTowards = true)
		{
			if (absolute)
			{
				moveRot += rotAmount;
				Vector2 rotVec = BaseUtility.RotateVector(default(Vector2), new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
				codable.Center = rotVec;
				rotVec.Normalize();
				rotation = BaseUtility.RotationTo(codable.Center, rotateCenter) - 1.57f;
				codable.velocity *= 0f;
				return;
			}
			float dist = Vector2.Distance(codable.Center, rotateCenter);
			if (dist < rotDistance)
			{
				if (rotDistance - dist > rotThreshold)
				{
					moveRot += rotAmount;
					Vector2 rotVec2 = BaseUtility.RotateVector(default(Vector2), new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
					float rot2 = BaseUtility.RotationTo(codable.Center, rotVec2);
					codable.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), rot2);
					rotation = BaseUtility.RotationTo(codable.Center, codable.Center + codable.velocity);
					return;
				}
				moveRot += rotAmount;
				Vector2 rotVec3 = BaseUtility.RotateVector(default(Vector2), new Vector2(rotDistance, 0f), moveRot) + rotateCenter;
				float rot3 = BaseUtility.RotationTo(codable.Center, rotVec3);
				codable.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(5f, 0f), rot3);
				rotation = BaseUtility.RotationTo(codable.Center, codable.Center + codable.velocity);
				return;
			}
			else
			{
				if (moveTowards)
				{
					codable.velocity = BaseAI.AIVelocityLinear(codable, rotateCenter, rotAmount, rotAmount, true);
					rotation = BaseUtility.RotationTo(codable.Center, rotateCenter) - 1.57f;
					return;
				}
				codable.velocity *= 0.95f;
				return;
			}
		}

		public static void AIPounce(Entity codable, Player player, float pounceScalar = 3f, float maxSpeed = 5f, float yBoost = -5.2f, float minDistance = 50f, float maxDistance = 60f)
		{
			if (player == null || !player.active || player.dead)
			{
				return;
			}
			BaseAI.AIPounce(codable, player.Center, pounceScalar, maxSpeed, yBoost, minDistance, maxDistance);
		}

		public static void AIPounce(Entity codable, Vector2 pounceCenter, float pounceScalar = 3.5f, float maxSpeed = 5f, float yBoost = -5.2f, float minDistance = 50f, float maxDistance = 60f)
		{
			int direction = (codable is NPC) ? ((NPC)codable).direction : ((codable is Projectile) ? ((Projectile)codable).direction : 0);
			float dist = Vector2.Distance(codable.Center, pounceCenter);
			if (pounceCenter.Y <= codable.Center.Y && dist > minDistance && dist < maxDistance)
			{
				bool onLeft = pounceCenter.X < codable.Center.X;
				if (codable.velocity.Y == 0f && ((onLeft && direction == -1) || (!onLeft && direction == 1)))
				{
					codable.velocity.X = codable.velocity.X * pounceScalar;
					if (codable.velocity.X > maxSpeed)
					{
						codable.velocity.X = maxSpeed;
					}
					if (codable.velocity.X < -maxSpeed)
					{
						codable.velocity.X = -maxSpeed;
					}
					codable.velocity.Y = yBoost;
					if (codable is NPC)
					{
						((NPC)codable).netUpdate = true;
					}
				}
			}
		}

		public static void AIPath(NPC npc, ref float[] ai, Vector2[] points, float moveInterval = 0.11f, float maxSpeed = 3f, bool direct = false)
		{
			Vector2 destVec = new Vector2(ai[0], ai[1]);
			if (Main.netMode != 1 && destVec != default(Vector2) && Vector2.Distance(npc.Center, destVec) <= Math.Max(5f, (float)(npc.width + npc.height) / 2f * 0.45f))
			{
				ai[0] = 0f;
				ai[1] = 0f;
				destVec = default(Vector2);
			}
			if (npc.ai[2] < (float)points.Length)
			{
				if (destVec == default(Vector2))
				{
					npc.velocity *= 0.95f;
					if (Main.netMode != 1)
					{
						destVec = points[(int)npc.ai[2]];
						ai[0] = destVec.X;
						ai[1] = destVec.Y;
						ai[2] += 1f;
						npc.netUpdate = true;
						return;
					}
				}
				else
				{
					npc.velocity = BaseAI.AIVelocityLinear(npc, destVec, moveInterval, maxSpeed, direct);
				}
			}
		}

		public static void AITackle(NPC npc, ref float[] ai, Vector2 point, float moveInterval = 0.11f, float maxSpeed = 3f, bool direct = false, int tackleDelay = 50, float drift = 0.95f)
		{
			Vector2 destVec = new Vector2(ai[0], ai[1]);
			if (destVec != default(Vector2) && Vector2.Distance(npc.Center, destVec) <= Math.Max(5f, (float)(npc.width + npc.height) / 2f * 0.45f))
			{
				ai[0] = 0f;
				ai[1] = 0f;
				destVec = default(Vector2);
			}
			if (destVec == default(Vector2))
			{
				npc.velocity *= drift;
				ai[2] -= 1f;
				if (ai[2] <= 0f)
				{
					ai[2] = (float)tackleDelay;
					destVec = point;
					ai[0] = destVec.X;
					ai[1] = destVec.Y;
				}
				if (Main.netMode == 2)
				{
					npc.netUpdate = true;
					return;
				}
			}
			else
			{
				npc.velocity = BaseAI.AIVelocityLinear(npc, destVec, moveInterval, maxSpeed, direct);
			}
		}

		public static Random GetSyncedRand(NPC npc)
		{
			return new Random(npc.whoAmI);
		}

		public static void AIGravitate(NPC npc, ref float[] ai, UnifiedRandom rand, Vector2 point, float moveInterval = 0.06f, float maxSpeed = 2f, bool canCrossCenter = true, bool direct = false, int minDistance = 50, int maxDistance = 200)
		{
			Vector2 destVec = new Vector2(ai[0], ai[1]);
			bool idleTooLong = false;
			if (Main.netMode != 1)
			{
				if (!idleTooLong && destVec != default(Vector2) && Vector2.Distance(npc.Center, destVec) <= Math.Max(12f, (float)(npc.width + npc.height) / 2f * 3f * (moveInterval / 0.06f)))
				{
					ai[2] += 1f;
					if (ai[2] > 100f)
					{
						ai[2] = 0f;
						idleTooLong = true;
					}
				}
				if (idleTooLong || (destVec != default(Vector2) && Vector2.Distance(npc.Center, destVec) <= Math.Max(5f, (float)(npc.width + npc.height) / 2f * 0.75f)))
				{
					ai[0] = 0f;
					ai[1] = 0f;
					destVec = default(Vector2);
				}
			}
			if (destVec == default(Vector2))
			{
				if (npc.velocity.X > 0.3f || npc.velocity.Y > 0.3f)
				{
					npc.velocity.X = npc.velocity.X * 0.95f;
				}
				if (canCrossCenter)
				{
					destVec = BaseUtility.GetRandomPosNear(point, rand, minDistance, maxDistance, false);
				}
				else
				{
					int distance = maxDistance - minDistance;
					Vector2 topLeft = new Vector2(point.X - (float)(minDistance + rand.Next(distance)), point.Y - (float)(minDistance + rand.Next(distance)));
					Vector2 topRight = new Vector2(point.X + (float)(minDistance + rand.Next(distance)), topLeft.Y);
					Vector2 bottomLeft = new Vector2(topLeft.X, point.Y + (float)(minDistance + rand.Next(distance)));
					Vector2 bottomRight = new Vector2(topRight.X, bottomLeft.Y);
					float tempDist = 9999999f;
					Vector2 closestPoint = default(Vector2);
					for (int i = 0; i < 4; i++)
					{
						Vector2 corner = (i == 0) ? topLeft : ((i == 1) ? topRight : ((i == 2) ? bottomLeft : bottomRight));
						if (Vector2.Distance(npc.Center, corner) < tempDist)
						{
							tempDist = Vector2.Distance(npc.Center, corner);
							closestPoint = corner;
						}
					}
					if (closestPoint == topLeft || closestPoint == bottomRight)
					{
						destVec = ((rand.Next(2) == 0) ? topRight : bottomLeft);
					}
					else if (closestPoint == topRight || closestPoint == bottomLeft)
					{
						destVec = ((rand.Next(2) == 0) ? topLeft : bottomRight);
					}
				}
				ai[0] = destVec.X;
				ai[1] = destVec.Y;
				if (Main.netMode == 2)
				{
					npc.netUpdate = true;
					return;
				}
			}
			else if (destVec != default(Vector2))
			{
				npc.velocity = BaseAI.AIVelocityLinear(npc, destVec, moveInterval, maxSpeed, direct);
			}
		}

		public static Vector2 AIVelocityLinear(Entity codable, Vector2 destVec, float moveInterval, float maxSpeed, bool direct = false)
		{
			Vector2 returnVelocity = codable.velocity;
			bool flag = (codable is NPC) ? (!((NPC)codable).noTileCollide) : (codable is Projectile && ((Projectile)codable).tileCollide);
			if (direct)
			{
				returnVelocity = BaseUtility.RotateVector(codable.Center, codable.Center + new Vector2(maxSpeed, 0f), BaseUtility.RotationTo(codable.Center, destVec)) - codable.Center;
			}
			else
			{
				if (codable.Center.X > destVec.X)
				{
					returnVelocity.X = Math.Max(-maxSpeed, returnVelocity.X - moveInterval);
				}
				else if (codable.Center.X < destVec.X)
				{
					returnVelocity.X = Math.Min(maxSpeed, returnVelocity.X + moveInterval);
				}
				if (codable.Center.Y > destVec.Y)
				{
					returnVelocity.Y = Math.Max(-maxSpeed, returnVelocity.Y - moveInterval);
				}
				else if (codable.Center.Y < destVec.Y)
				{
					returnVelocity.Y = Math.Min(maxSpeed, returnVelocity.Y + moveInterval);
				}
			}
			if (flag)
			{
				returnVelocity = Collision.TileCollision(codable.position, returnVelocity, codable.width, codable.height, false, false, 1);
			}
			return returnVelocity;
		}

		public static void AILightningBolt(Projectile projectile, ref float[] ai, float changeAngleAt = 40f)
		{
			int projFrameCounter = projectile.frameCounter;
			projectile.frameCounter = projFrameCounter + 1;
			if (projectile.velocity == Vector2.Zero)
			{
				if (projectile.frameCounter >= projectile.extraUpdates * 2)
				{
					projectile.frameCounter = 0;
					bool shouldKillProjectile = true;
					for (int i = 1; i < projectile.oldPos.Length; i = projFrameCounter + 1)
					{
						if (projectile.oldPos[i] != projectile.oldPos[0])
						{
							shouldKillProjectile = false;
						}
						projFrameCounter = i;
					}
					if (shouldKillProjectile)
					{
						projectile.Kill();
						return;
					}
				}
			}
			else if (projectile.frameCounter >= projectile.extraUpdates * 2)
			{
				projectile.frameCounter = 0;
				float velSpeed = projectile.velocity.Length();
				UnifiedRandom unifiedRandom = new UnifiedRandom((int)projectile.ai[1]);
				int newFrameCounter = 0;
				Vector2 projVelocity = -Vector2.UnitY;
				Vector2 angleVector;
				do
				{
					int percentile = unifiedRandom.Next();
					projectile.ai[1] = (float)percentile;
					percentile %= 100;
					angleVector = Utils.ToRotationVector2((float)percentile / 100f * 6.2831855f);
					if (angleVector.Y > 0f)
					{
						angleVector.Y *= -1f;
					}
					bool moreFrames = false;
					if (angleVector.Y > -0.02f)
					{
						moreFrames = true;
					}
					if (angleVector.X * (float)(projectile.extraUpdates + 1) * 2f * velSpeed + projectile.localAI[0] > changeAngleAt)
					{
						moreFrames = true;
					}
					if (angleVector.X * (float)(projectile.extraUpdates + 1) * 2f * velSpeed + projectile.localAI[0] < -changeAngleAt)
					{
						moreFrames = true;
					}
					if (!moreFrames)
					{
						goto IL_1A8;
					}
					projFrameCounter = newFrameCounter;
					newFrameCounter = projFrameCounter + 1;
				}
				while (projFrameCounter < 100);
				projectile.velocity = Vector2.Zero;
				projectile.localAI[1] = 1f;
				goto IL_1AC;
				IL_1A8:
				projVelocity = angleVector;
				IL_1AC:
				if (projectile.velocity != Vector2.Zero)
				{
					projectile.localAI[0] += projVelocity.X * (float)(projectile.extraUpdates + 1) * 2f * velSpeed;
					projectile.velocity = Utils.RotatedBy(projVelocity, (double)(projectile.ai[0] + 1.5707964f), default(Vector2)) * velSpeed;
					projectile.rotation = Utils.ToRotation(projectile.velocity) + 1.5707964f;
					return;
				}
			}
		}

		public static void AIProjWorm(Projectile p, ref float[] ai, int[] wormTypes, int wormLength, float velScalar = 1f, float velScalarIdle = 1f, float velocityMax = 30f, float velocityMaxIdle = 15f)
		{
			int[] wtypes = new int[(wormTypes.Length == 1) ? 1 : wormLength];
			wtypes[0] = wormTypes[0];
			if (wormTypes.Length > 1)
			{
				wtypes[wtypes.Length - 1] = wormTypes[2];
				for (int i = 1; i < wtypes.Length - 1; i++)
				{
					wtypes[i] = wormTypes[1];
				}
			}
			int dummyNPC = -1;
			BaseAI.AIProjWorm(p, ref ai, ref dummyNPC, wtypes, velScalar, velScalarIdle, velocityMax, velocityMaxIdle);
		}

		public static void AIProjWorm(Projectile p, ref float[] ai, ref int npcTargetToAttack, int[] wormTypes, float velScalar = 1f, float velScalarIdle = 1f, float velocityMax = 30f, float velocityMaxIdle = 15f)
		{
			Player plrOwner = Main.player[p.owner];
			if ((int)Main.time % 120 == 0)
			{
				p.netUpdate = true;
			}
			if (!plrOwner.active)
			{
				p.active = false;
				return;
			}
			bool flag = p.type == wormTypes[0];
			bool isWorm = BaseUtility.InArray(wormTypes, p.type);
			bool isTail = p.type == wormTypes[wormTypes.Length - 1];
			int wormWidthHeight = 10;
			if (isWorm)
			{
				p.timeLeft = 2;
				wormWidthHeight = 30;
			}
			if (flag)
			{
				Vector2 plrCenter = plrOwner.Center;
				float minAttackDist = 700f;
				float returnDist = 1000f;
				float teleportDist = 2000f;
				int target = -1;
				if (p.Distance(plrCenter) > teleportDist)
				{
					p.Center = plrCenter;
					p.netUpdate = true;
				}
				if (true)
				{
					NPC ownerMinionAttackTargetNPC5 = p.OwnerMinionAttackTargetNPC;
					if (ownerMinionAttackTargetNPC5 != null && ownerMinionAttackTargetNPC5.CanBeChasedBy(p, false) && p.Distance(ownerMinionAttackTargetNPC5.Center) < minAttackDist * 2f)
					{
						target = ownerMinionAttackTargetNPC5.whoAmI;
					}
					if (target < 0)
					{
						int dummy;
						for (int i = 0; i < 200; i = dummy + 1)
						{
							NPC npcTarget = Main.npc[i];
							if (npcTarget.CanBeChasedBy(p, false) && plrOwner.Distance(npcTarget.Center) < returnDist && p.Distance(npcTarget.Center) < minAttackDist)
							{
								target = i;
								bool boss = npcTarget.boss;
							}
							dummy = i;
						}
					}
				}
				npcTargetToAttack = target;
				if (target != -1)
				{
					NPC npcTarget2 = Main.npc[target];
					Vector2 npcDist = npcTarget2.Center - p.Center;
					Utils.ToDirectionInt(npcDist.X > 0f);
					Utils.ToDirectionInt(npcDist.Y > 0f);
					float velocityScalar = 0.4f;
					if (npcDist.Length() < 600f)
					{
						velocityScalar = 0.6f;
					}
					if (npcDist.Length() < 300f)
					{
						velocityScalar = 0.8f;
					}
					velocityScalar *= velScalar;
					if (npcDist.Length() > npcTarget2.Size.Length() * 0.75f)
					{
						p.velocity += Vector2.Normalize(npcDist) * velocityScalar * 1.5f;
						if (Vector2.Dot(p.velocity, npcDist) < 0.25f)
						{
							p.velocity *= 0.8f;
						}
					}
					if (p.velocity.Length() > velocityMax)
					{
						p.velocity = Vector2.Normalize(p.velocity) * velocityMax;
					}
				}
				else
				{
					float velocityScalarIdle = 0.2f;
					Vector2 newPointDist = plrCenter - p.Center;
					if (newPointDist.Length() < 200f)
					{
						velocityScalarIdle = 0.12f;
					}
					if (newPointDist.Length() < 140f)
					{
						velocityScalarIdle = 0.06f;
					}
					velocityScalarIdle *= velScalarIdle;
					if (newPointDist.Length() > 100f)
					{
						if (Math.Abs(plrCenter.X - p.Center.X) > 20f)
						{
							p.velocity.X = p.velocity.X + velocityScalarIdle * (float)Math.Sign(plrCenter.X - p.Center.X);
						}
						if (Math.Abs(plrCenter.Y - p.Center.Y) > 10f)
						{
							p.velocity.Y = p.velocity.Y + velocityScalarIdle * (float)Math.Sign(plrCenter.Y - p.Center.Y);
						}
					}
					else if (p.velocity.Length() > 2f)
					{
						p.velocity *= 0.96f;
					}
					if (Math.Abs(p.velocity.Y) < 1f)
					{
						p.velocity.Y = p.velocity.Y - 0.1f;
					}
					if (p.velocity.Length() > velocityMaxIdle)
					{
						p.velocity = Vector2.Normalize(p.velocity) * velocityMaxIdle;
					}
				}
				p.rotation = Utils.ToRotation(p.velocity) + 1.5707964f;
				int direction = p.direction;
				p.direction = (p.spriteDirection = ((p.velocity.X > 0f) ? 1 : -1));
				if (direction != p.direction)
				{
					p.netUpdate = true;
				}
				float scaleChange = MathHelper.Clamp(p.localAI[0], 0f, 50f);
				p.position = p.Center;
				p.scale = 1f + scaleChange * 0.01f;
				p.width = (p.height = (int)((float)wormWidthHeight * p.scale));
				p.Center = p.position;
				if (p.alpha > 0)
				{
					p.alpha -= 42;
					if (p.alpha < 0)
					{
						p.alpha = 0;
						return;
					}
				}
				return;
			}
			bool npcInFront = false;
			Vector2 projCenter = Vector2.Zero;
			float projRot = 0f;
			float tileScalar = 0f;
			float projectileScale = 1f;
			if (p.ai[1] == 1f)
			{
				p.ai[1] = 0f;
				p.netUpdate = true;
			}
			int byUUID = Projectile.GetByUUID(p.owner, (int)p.ai[0]);
			if (isWorm && byUUID >= 0 && Main.projectile[byUUID].active && !isTail)
			{
				npcInFront = true;
				projCenter = Main.projectile[byUUID].Center;
				projRot = Main.projectile[byUUID].rotation;
				projectileScale = MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
				tileScalar = 16f;
				int alpha = Main.projectile[byUUID].alpha;
				Main.projectile[byUUID].localAI[0] = p.localAI[0] + 1f;
				if (Main.projectile[byUUID].type != wormTypes[0])
				{
					Main.projectile[byUUID].localAI[1] = (float)p.whoAmI;
				}
				if (p.owner == Main.myPlayer && Main.projectile[byUUID].type == wormTypes[0] && p.type == wormTypes[wormTypes.Length - 1])
				{
					Main.projectile[byUUID].Kill();
					p.Kill();
					return;
				}
			}
			if (!npcInFront)
			{
				return;
			}
			p.alpha -= 42;
			if (p.alpha < 0)
			{
				p.alpha = 0;
			}
			p.velocity = Vector2.Zero;
			Vector2 centerDist = projCenter - p.Center;
			if (projRot != p.rotation)
			{
				float rotDist = MathHelper.WrapAngle(projRot - p.rotation);
				centerDist = Utils.RotatedBy(centerDist, (double)(rotDist * 0.1f), default(Vector2));
			}
			p.rotation = Utils.ToRotation(centerDist) + 1.5707964f;
			p.position = p.Center;
			p.scale = projectileScale;
			p.width = (p.height = (int)((float)wormWidthHeight * p.scale));
			p.Center = p.position;
			if (centerDist != Vector2.Zero)
			{
				p.Center = projCenter - Vector2.Normalize(centerDist) * tileScalar * projectileScale;
			}
			p.spriteDirection = ((centerDist.X > 0f) ? 1 : -1);
		}

		public static void AIProjSpaceOctopus(Projectile p, ref float[] ai, int parentNPCType, int fireProjType = -1, float shootVelocity = 16f, float hoverTime = 210f, float xMult = 0.15f, float yMult = 0.075f, Action<int, Projectile> SpawnDust = null)
		{
			BaseAI.AIProjSpaceOctopus(p, ref ai, parentNPCType, fireProjType, shootVelocity, hoverTime, xMult, yMult, -1, true, false, SpawnDust);
		}

		public static void AIProjSpaceOctopus(Projectile projectile, ref float[] ai, int parentNPCType, int fireProjType = -1, float shootVelocity = 16f, float hoverTime = 210f, float xMult = 0.15f, float yMult = 0.075f, int fireDmg = -1, bool useParentTarget = true, bool noParentHover = false, Action<int, Projectile> SpawnDust = null)
		{
			if (fireDmg == -1)
			{
				fireDmg = projectile.damage;
			}
			ai[0] += 1f;
			if (ai[0] < hoverTime)
			{
				bool parentAlive = true;
				int parentID = (int)ai[1];
				if (Main.npc[parentID].active && Main.npc[parentID].type == parentNPCType)
				{
					if (!noParentHover && Main.npc[parentID].oldPos[1] != Vector2.Zero)
					{
						projectile.position += Main.npc[parentID].position - Main.npc[parentID].oldPos[1];
					}
				}
				else
				{
					ai[0] = hoverTime;
					parentAlive = false;
				}
				if (parentAlive && !noParentHover)
				{
					projectile.velocity += new Vector2((float)Math.Sign(Main.npc[parentID].Center.X - projectile.Center.X), (float)Math.Sign(Main.npc[parentID].Center.Y - projectile.Center.Y)) * new Vector2(xMult, yMult);
					if (projectile.velocity.Length() > 6f)
					{
						projectile.velocity *= 6f / projectile.velocity.Length();
					}
				}
				if (SpawnDust != null)
				{
					SpawnDust(0, projectile);
				}
				projectile.rotation = projectile.velocity.X * 0.1f;
			}
			if (ai[0] == hoverTime)
			{
				bool hasParentTarget = true;
				int parentTarget = -1;
				if (!useParentTarget)
				{
					int parentID2 = (int)ai[1];
					if (Main.npc[parentID2].active && Main.npc[parentID2].type == parentNPCType)
					{
						parentTarget = Main.npc[parentID2].target;
					}
					else
					{
						hasParentTarget = false;
					}
				}
				else
				{
					hasParentTarget = false;
				}
				if (!hasParentTarget)
				{
					parentTarget = (int)Player.FindClosest(projectile.position, projectile.width, projectile.height);
				}
				Vector2 distanceVec = Main.player[parentTarget].Center - projectile.Center;
				distanceVec.X += (float)Main.rand.Next(-50, 51);
				distanceVec.Y += (float)Main.rand.Next(-50, 51);
				distanceVec.X *= (float)Main.rand.Next(80, 121) * 0.01f;
				distanceVec.Y *= (float)Main.rand.Next(80, 121) * 0.01f;
				Vector2 distVecNormal = Vector2.Normalize(distanceVec);
				if (Utils.HasNaNs(distVecNormal))
				{
					distVecNormal = Vector2.UnitY;
				}
				if (fireProjType == -1)
				{
					projectile.velocity = distVecNormal * shootVelocity;
					projectile.netUpdate = true;
				}
				else
				{
					if (Main.netMode != 1 && Collision.CanHitLine(projectile.Center, 0, 0, Main.player[parentTarget].Center, 0, 0))
					{
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, distVecNormal.X * shootVelocity, distVecNormal.Y * shootVelocity, fireProjType, fireDmg, 1f, Main.myPlayer, 0f, 0f);
					}
					ai[0] = 0f;
				}
			}
			if (ai[0] >= hoverTime)
			{
				projectile.rotation = Utils.AngleLerp(projectile.rotation, Utils.ToRotation(projectile.velocity) + 1.5707964f, 0.4f);
				if (SpawnDust != null)
				{
					SpawnDust(1, projectile);
				}
			}
		}

		public static void AIYoyo(Projectile p, ref float[] ai, ref float[] localAI, float yoyoTimeMax = -1f, float maxRange = -1f, float topSpeed = -1f, bool dontChannel = false, float rotAmount = 0.45f)
		{
			if (yoyoTimeMax == -1f)
			{
				yoyoTimeMax = ProjectileID.Sets.YoyosLifeTimeMultiplier[p.type];
			}
			if (maxRange == -1f)
			{
				maxRange = ProjectileID.Sets.YoyosMaximumRange[p.type];
			}
			if (topSpeed == -1f)
			{
				topSpeed = ProjectileID.Sets.YoyosTopSpeed[p.type];
			}
			BaseAI.AIYoyo(p, ref ai, ref localAI, Main.player[p.owner], Main.player[p.owner].channel, default(Vector2), yoyoTimeMax, maxRange, topSpeed, dontChannel, rotAmount);
		}

		public static void AIYoyo(Projectile p, ref float[] ai, ref float[] localAI, Entity owner, bool isChanneling, Vector2 targetPos = default(Vector2), float yoyoTimeMax = 120f, float maxRange = 150f, float topSpeed = 8f, bool dontChannel = false, float rotAmount = 0.45f)
		{
			bool playerYoyo = owner is Player;
			Player powner = playerYoyo ? ((Player)owner) : null;
			float meleeSpeed = playerYoyo ? powner.meleeSpeed : 1f;
			Vector2 targetP = targetPos;
			if (playerYoyo && Main.myPlayer == p.owner && targetPos == default(Vector2))
			{
				targetP = Main.ReverseGravitySupport(Main.MouseScreen, 0f) + Main.screenPosition;
			}
			bool yoyoFound = false;
			if (owner is Player)
			{
				for (int i = 0; i < p.whoAmI; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].owner == p.owner && Main.projectile[i].type == p.type)
					{
						yoyoFound = true;
					}
				}
			}
			if ((playerYoyo && p.owner == Main.myPlayer) || (!playerYoyo && Main.netMode != 1))
			{
				localAI[0] += 1f;
				if (yoyoFound)
				{
					localAI[0] += (float)Main.rand.Next(10, 31) * 0.1f;
				}
				float yoyoTimeLeft = localAI[0] / 60f;
				yoyoTimeLeft /= (1f + meleeSpeed) / 2f;
				if (yoyoTimeMax != -1f && yoyoTimeLeft > yoyoTimeMax)
				{
					ai[0] = -1f;
				}
			}
			if ((playerYoyo && powner.dead) || (!playerYoyo && !owner.active))
			{
				p.Kill();
				return;
			}
			if (playerYoyo && !dontChannel && !yoyoFound)
			{
				powner.heldProj = p.whoAmI;
				powner.itemAnimation = 2;
				powner.itemTime = 2;
				if (p.position.X + (float)(p.width / 2) > powner.position.X + (float)(powner.width / 2))
				{
					powner.ChangeDir(1);
					p.direction = 1;
				}
				else
				{
					powner.ChangeDir(-1);
					p.direction = -1;
				}
			}
			if (Utils.HasNaNs(p.velocity))
			{
				p.Kill();
			}
			p.timeLeft = 6;
			float pMaxRange = maxRange;
			if (playerYoyo && powner.yoyoString)
			{
				pMaxRange = pMaxRange * 1.25f + 30f;
			}
			pMaxRange /= (1f + meleeSpeed * 3f) / 4f;
			float pTopSpeed = topSpeed / ((1f + meleeSpeed * 3f) / 4f);
			float topSpeedX = 14f - pTopSpeed / 2f;
			float topSpeedY = 5f + pTopSpeed / 2f;
			if (yoyoFound)
			{
				topSpeedY += 20f;
			}
			if (ai[0] >= 0f)
			{
				if (p.velocity.Length() > pTopSpeed)
				{
					p.velocity *= 0.98f;
				}
				bool yoyoTooFar = false;
				bool yoyoWayTooFar = false;
				Vector2 centerDist = owner.Center - p.Center;
				if (centerDist.Length() > pMaxRange)
				{
					yoyoTooFar = true;
					if ((double)centerDist.Length() > (double)pMaxRange * 1.3)
					{
						yoyoWayTooFar = true;
					}
				}
				if ((playerYoyo && p.owner == Main.myPlayer) || (!playerYoyo && Main.netMode != 1))
				{
					if ((playerYoyo && (!isChanneling || powner.stoned || powner.frozen)) || (!playerYoyo && !isChanneling))
					{
						ai[0] = -1f;
						ai[1] = 0f;
						p.netUpdate = true;
					}
					else
					{
						Vector2 vector = targetP;
						float x = vector.X;
						float y = vector.Y;
						Vector2 mouseDist = new Vector2(x, y) - owner.Center;
						if (mouseDist.Length() > pMaxRange)
						{
							mouseDist.Normalize();
							mouseDist *= pMaxRange;
							mouseDist = owner.Center + mouseDist;
							x = mouseDist.X;
							y = mouseDist.Y;
						}
						if (ai[0] != x || ai[1] != y)
						{
							Vector2 coordDist = new Vector2(x, y) - owner.Center;
							if (coordDist.Length() > pMaxRange - 1f)
							{
								coordDist.Normalize();
								coordDist *= pMaxRange - 1f;
								Vector2 vector2 = owner.Center + coordDist;
								x = vector2.X;
								y = vector2.Y;
							}
							ai[0] = x;
							ai[1] = y;
							p.netUpdate = true;
						}
					}
				}
				if (yoyoWayTooFar && p.owner == Main.myPlayer)
				{
					ai[0] = -1f;
					p.netUpdate = true;
				}
				if (ai[0] >= 0f)
				{
					if (yoyoTooFar)
					{
						topSpeedX /= 2f;
						pTopSpeed *= 2f;
						if (p.Center.X > owner.Center.X && p.velocity.X > 0f)
						{
							p.velocity.X = p.velocity.X * 0.5f;
						}
						if (p.Center.Y > owner.Center.Y && p.velocity.Y > 0f)
						{
							p.velocity.Y = p.velocity.Y * 0.5f;
						}
						if (p.Center.X < owner.Center.X && p.velocity.X > 0f)
						{
							p.velocity.X = p.velocity.X * 0.5f;
						}
						if (p.Center.Y < owner.Center.Y && p.velocity.Y > 0f)
						{
							p.velocity.Y = p.velocity.Y * 0.5f;
						}
					}
					Vector2 coordDist2 = new Vector2(ai[0], ai[1]) - p.Center;
					p.velocity.Length();
					float coordLength = coordDist2.Length();
					if (coordLength > topSpeedY)
					{
						coordDist2.Normalize();
						float scaleFactor = (coordLength > pTopSpeed * 2f) ? pTopSpeed : (coordLength / 2f);
						coordDist2 *= scaleFactor;
						p.velocity = (p.velocity * (topSpeedX - 1f) + coordDist2) / topSpeedX;
					}
					else if (yoyoFound)
					{
						if ((double)p.velocity.Length() < (double)pTopSpeed * 0.6)
						{
							coordDist2 = p.velocity;
							coordDist2.Normalize();
							coordDist2 *= pTopSpeed * 0.6f;
							p.velocity = (p.velocity * (topSpeedX - 1f) + coordDist2) / topSpeedX;
						}
					}
					else
					{
						p.velocity *= 0.8f;
					}
					if (yoyoFound && !yoyoTooFar && (double)p.velocity.Length() < (double)pTopSpeed * 0.6)
					{
						p.velocity.Normalize();
						p.velocity *= pTopSpeed * 0.6f;
					}
				}
			}
			else
			{
				topSpeedX = (float)((int)((double)topSpeedX * 0.8));
				pTopSpeed *= 1.5f;
				p.tileCollide = false;
				Vector2 posDist = owner.position - p.Center;
				float posLength = posDist.Length();
				if (posLength < pTopSpeed + 10f || posLength == 0f)
				{
					p.Kill();
				}
				else
				{
					posDist.Normalize();
					posDist *= pTopSpeed;
					p.velocity = (p.velocity * (topSpeedX - 1f) + posDist) / topSpeedX;
				}
			}
			p.rotation += rotAmount;
		}

		public static void TileCollideYoyo(Projectile p, ref Vector2 velocity, Vector2 newVelocity)
		{
			bool normalizeVelocity = false;
			if (velocity.X != newVelocity.X)
			{
				normalizeVelocity = true;
				velocity.X = newVelocity.X * -1f;
			}
			if (velocity.Y != newVelocity.Y)
			{
				normalizeVelocity = true;
				velocity.Y = newVelocity.Y * -1f;
			}
			if (normalizeVelocity)
			{
				Vector2 centerDist = Main.player[p.owner].Center - p.Center;
				centerDist.Normalize();
				centerDist *= velocity.Length();
				centerDist *= 0.25f;
				velocity *= 0.75f;
				velocity += centerDist;
				if (velocity.Length() > 6f)
				{
					velocity *= 0.5f;
				}
			}
		}

		public static void EntityCollideYoyo(Projectile p, ref float[] ai, ref float[] localAI, Entity owner, Entity target, bool spawnCounterweight = true, float velMult = 1f)
		{
			if (owner is Player && spawnCounterweight)
			{
				((Player)owner).Counterweight(target.Center, p.damage, p.knockBack);
			}
			if (target.Center.X < owner.Center.X)
			{
				p.direction = -1;
			}
			else
			{
				p.direction = 1;
			}
			if (ai[0] >= 0f)
			{
				Vector2 value2 = p.Center - target.Center;
				value2.Normalize();
				float scaleFactor = 16f;
				p.velocity *= -0.5f;
				p.velocity += value2 * scaleFactor;
				p.velocity *= velMult;
				p.netUpdate = true;
				localAI[0] += 20f;
				if (!Collision.CanHit(p.position, p.width, p.height, owner.position, owner.width, owner.height))
				{
					localAI[0] += 40f;
				}
			}
		}

		public static void AIStar(Projectile p, ref float[] ai, float landingHorizon = -1f, bool fadein = true)
		{
			if (landingHorizon != -1f)
			{
				if (p.position.Y > landingHorizon)
				{
					p.tileCollide = true;
				}
			}
			else
			{
				if (ai[0] == 0f && !Collision.SolidCollision(p.position, p.width, p.height))
				{
					ai[0] = 1f;
					p.netUpdate = true;
				}
				if (ai[0] != 0f)
				{
					p.tileCollide = true;
				}
			}
			if (fadein)
			{
				p.alpha = Math.Max(0, p.alpha - 25);
			}
		}

		public static void AIExplosive(Projectile p, ref float[] ai, bool rocket = false, bool rotate = true, int beginGravity = 10, float slowdownX = 0.97f, float gravity = 0.2f)
		{
			if (rocket && Math.Abs(p.velocity.X) < 15f && Math.Abs(p.velocity.Y) < 15f)
			{
				p.velocity *= 1.1f;
			}
			ai[0] += 1f;
			if (rocket)
			{
				if (p.velocity.X < 0f)
				{
					p.spriteDirection = -1;
					p.rotation = (float)Math.Atan2((double)(-(double)p.velocity.Y), (double)(-(double)p.velocity.X)) - 1.57f;
				}
				else
				{
					p.spriteDirection = 1;
					p.rotation = (float)Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 1.57f;
				}
			}
			else if (ai[0] > (float)beginGravity)
			{
				ai[0] = (float)beginGravity;
				if (p.velocity.Y == 0f && p.velocity.X != 0f)
				{
					p.velocity.X = p.velocity.X * slowdownX;
					if (p.velocity.X > -0.01f && p.velocity.X < 0.01f)
					{
						p.velocity.X = 0f;
						p.netUpdate = true;
					}
				}
				p.velocity.Y = p.velocity.Y + gravity;
			}
			if (rotate)
			{
				p.rotation += p.velocity.X * 0.1f;
			}
		}

		public static void TileCollideExplosive(Projectile p, ref Vector2 velocity, bool bomb = false)
		{
			if (p.velocity.X != velocity.X)
			{
				p.velocity.X = velocity.X * -0.4f;
			}
			if (p.velocity.Y != velocity.Y && velocity.Y > 0.7f && !bomb)
			{
				p.velocity.Y = velocity.Y * -0.4f;
			}
		}

		public static void AIArrow(Entity codable, ref float[] ai, int gravApplyInterval = 50, float gravity = 0.1f, float maxSpeedY = 16f)
		{
			ai[0] += 1f;
			if (ai[0] >= (float)gravApplyInterval)
			{
				codable.velocity.Y = codable.velocity.Y + gravity;
			}
			if (codable.velocity.Y > maxSpeedY)
			{
				codable.velocity.Y = maxSpeedY;
			}
		}

		public static void AIDemonScythe(Entity codable, ref float[] ai, int startSpeedupInterval = 30, int stopSpeedupInterval = 100, float rotateScalar = 0.8f, float speedupScalar = 1.06f, float maxSpeed = 8f)
		{
			if (codable is Projectile)
			{
				((Projectile)codable).rotation += (float)codable.direction * rotateScalar;
			}
			if (codable is NPC)
			{
				((NPC)codable).rotation += (float)codable.direction * rotateScalar;
			}
			ai[0] += 1f;
			if (ai[0] >= (float)startSpeedupInterval)
			{
				if (ai[0] < (float)stopSpeedupInterval)
				{
					codable.velocity *= speedupScalar;
				}
				else
				{
					ai[0] = (float)stopSpeedupInterval;
				}
			}
			if ((Math.Abs(codable.velocity.X) + Math.Abs(codable.velocity.Y)) * 0.5f > maxSpeed)
			{
				codable.velocity.Normalize();
				codable.velocity *= maxSpeed;
			}
		}

		public static void AIVilethorn(Projectile p, int alphaInterval = 50, int alphaReduction = 4, int length = 8)
		{
			if (p.ai[0] == 0f)
			{
				p.rotation = (float)Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 1.57f;
				p.alpha -= alphaInterval;
				if (p.alpha <= 0)
				{
					p.alpha = 0;
					p.ai[0] = 1f;
					if (p.ai[1] == 0f)
					{
						p.ai[1] += 1f;
						p.position += p.velocity;
					}
					if (p.ai[1] < (float)length && Main.myPlayer == p.owner)
					{
						Vector2 rotVec = p.velocity;
						int id = Projectile.NewProjectile(p.Center.X + p.velocity.X, p.Center.Y + p.velocity.Y, rotVec.X, rotVec.Y, p.type, p.damage, p.knockBack, p.owner, 0f, 0f);
						Main.projectile[id].damage = p.damage;
						Main.projectile[id].ai[1] = p.ai[1] + 1f;
						NetMessage.SendData(27, -1, -1, NetworkText.FromLiteral(""), id, 0f, 0f, 0f, 0, 0, 0);
						p.position -= p.velocity;
						return;
					}
				}
			}
			else
			{
				p.alpha += alphaReduction;
				if (p.alpha >= 255)
				{
					p.Kill();
					return;
				}
			}
			p.position -= p.velocity;
		}

		public static void AIStream(Projectile p, float scaleReduce = 0.04f, float gravity = 0.075f, bool goldenShower = false, int start = 3, Func<Projectile, Vector2, int, int, int> SpawnDust = null)
		{
			if (goldenShower)
			{
				p.scale -= scaleReduce;
				if (p.scale <= 0f)
				{
					p.Kill();
				}
				if (p.ai[0] <= (float)start)
				{
					p.ai[0] += 1f;
					return;
				}
				p.velocity.Y = p.velocity.Y + gravity;
				if (Main.netMode != 2 && SpawnDust != null)
				{
					for (int i = 0; i < 3; i++)
					{
						float dustX = p.velocity.X / 3f * (float)i;
						float dustY = p.velocity.Y / 3f * (float)i;
						int offset = 1;
						Vector2 pos = new Vector2(p.position.X - (float)offset, p.position.Y - (float)offset);
						int width = p.width + offset * 2;
						int height = p.height + offset * 2;
						int dustID = SpawnDust(p, pos, width, height);
						if (dustID != -1)
						{
							Main.dust[dustID].noGravity = true;
							Main.dust[dustID].velocity *= 0.1f;
							Main.dust[dustID].velocity += p.velocity * 0.5f;
							Dust dust = Main.dust[dustID];
							dust.position.X = dust.position.X - dustX;
							Dust dust2 = Main.dust[dustID];
							dust2.position.Y = dust2.position.Y - dustY;
						}
					}
					if (Main.rand.Next(8) == 0)
					{
						int offset2 = 1;
						Vector2 pos2 = new Vector2(p.position.X - (float)offset2, p.position.Y - (float)offset2);
						int width2 = p.width + offset2 * 2;
						int height2 = p.height + offset2 * 2;
						int dustID2 = SpawnDust(p, pos2, width2, height2);
						if (dustID2 != -1)
						{
							Main.dust[dustID2].velocity *= 0.25f;
							Main.dust[dustID2].velocity += p.velocity * 0.5f;
							return;
						}
					}
				}
			}
			else
			{
				p.scale -= scaleReduce;
				if (p.scale <= 0f)
				{
					p.Kill();
				}
				p.velocity.Y = p.velocity.Y + gravity;
				if (Main.netMode != 2 && SpawnDust != null)
				{
					SpawnDust(p, p.position, p.width, p.height);
				}
			}
		}

		public static void AIThrownWeapon(Projectile p, ref float[] ai, bool spin = false, int timeUntilDrop = 10, float xScalar = 0.99f, float yIncrement = 0.25f, float maxSpeedY = 16f)
		{
			p.rotation += (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y)) * 0.03f * (float)p.direction;
			ai[0] += 1f;
			if (ai[0] >= (float)timeUntilDrop)
			{
				p.velocity.Y = p.velocity.Y + yIncrement;
				p.velocity.X = p.velocity.X * xScalar;
			}
			else if (!spin)
			{
				p.rotation = BaseUtility.RotationTo(p.Center, p.Center + p.velocity) + 1.57f;
			}
			if (p.velocity.Y > maxSpeedY)
			{
				p.velocity.Y = maxSpeedY;
			}
		}

		public static void AISpear(Projectile p, ref float[] ai, float initialSpeed = 3f, float moveOutward = 1.4f, float moveInward = 1.6f, bool overrideKill = false)
		{
			Player plr = Main.player[p.owner];
			Item item = plr.inventory[plr.selectedItem];
			if (Main.myPlayer == p.owner && item != null && item.autoReuse && plr.itemAnimation == 1)
			{
				p.Kill();
				return;
			}
			Main.player[p.owner].heldProj = p.whoAmI;
			Main.player[p.owner].itemTime = Main.player[p.owner].itemAnimation;
			Vector2 gfxOffset = new Vector2(0f, plr.gfxOffY);
			BaseAI.AISpear(p, ref ai, plr.Center + gfxOffset, plr.direction, plr.itemAnimation, plr.itemAnimationMax, initialSpeed, moveOutward, moveInward, overrideKill, plr.frozen);
		}

		public static void AISpear(Projectile p, ref float[] ai, Vector2 center, int ownerDirection, int itemAnimation, int itemAnimationMax, float initialSpeed = 3f, float moveOutward = 1.4f, float moveInward = 1.6f, bool overrideKill = false, bool frozen = false)
		{
			p.direction = ownerDirection;
			p.position.X = center.X - (float)p.width * 0.5f;
			p.position.Y = center.Y - (float)p.height * 0.5f;
			if (ai[0] == 0f)
			{
				ai[0] = initialSpeed;
				p.netUpdate = true;
			}
			if (!frozen)
			{
				if ((float)itemAnimation < (float)itemAnimationMax * 0.33f)
				{
					ai[0] -= moveInward;
				}
				else
				{
					ai[0] += moveOutward;
				}
			}
			p.position += p.velocity * ai[0];
			if (!overrideKill && Main.player[p.owner].itemAnimation == 0)
			{
				p.Kill();
			}
			p.rotation = (float)Math.Atan2((double)p.velocity.Y, (double)p.velocity.X) + 2.355f;
			if (p.direction == -1)
			{
				p.rotation -= 0f;
				return;
			}
			if (p.direction == 1)
			{
				p.rotation -= 1.57f;
			}
		}

		public static void AIBoomerang(Projectile p, ref float[] ai, Vector2 position = default(Vector2), int width = -1, int height = -1, bool playSound = true, float maxDistance = 9f, int returnDelay = 35, float speedInterval = 0.4f, float rotationInterval = 0.4f, bool direct = false)
		{
			if (position == default(Vector2))
			{
				position = Main.player[p.owner].position;
			}
			if (width == -1)
			{
				width = Main.player[p.owner].width;
			}
			if (height == -1)
			{
				height = Main.player[p.owner].height;
			}
			Vector2 center = position + new Vector2((float)width * 0.5f, (float)height * 0.5f);
			if (playSound && p.soundDelay == 0)
			{
				p.soundDelay = 8;
				Main.PlaySound(2, (int)p.position.X, (int)p.position.Y, 7, 1f, 0f);
			}
			if (ai[0] == 0f)
			{
				ai[1] += 1f;
				if (ai[1] >= (float)returnDelay)
				{
					ai[0] = 1f;
					ai[1] = 0f;
					p.netUpdate = true;
				}
			}
			else
			{
				p.tileCollide = false;
				float distPlayerX = center.X - p.Center.X;
				float distPlayerY = center.Y - p.Center.Y;
				float distPlayer = (float)Math.Sqrt((double)(distPlayerX * distPlayerX + distPlayerY * distPlayerY));
				if (distPlayer > 3000f)
				{
					p.Kill();
				}
				if (direct)
				{
					p.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(speedInterval, 0f), BaseUtility.RotationTo(p.Center, center));
				}
				else
				{
					distPlayer = maxDistance / distPlayer;
					distPlayerX *= distPlayer;
					distPlayerY *= distPlayer;
					if (p.velocity.X < distPlayerX)
					{
						p.velocity.X = p.velocity.X + speedInterval;
						if (p.velocity.X < 0f && distPlayerX > 0f)
						{
							p.velocity.X = p.velocity.X + speedInterval;
						}
					}
					else if (p.velocity.X > distPlayerX)
					{
						p.velocity.X = p.velocity.X - speedInterval;
						if (p.velocity.X > 0f && distPlayerX < 0f)
						{
							p.velocity.X = p.velocity.X - speedInterval;
						}
					}
					if (p.velocity.Y < distPlayerY)
					{
						p.velocity.Y = p.velocity.Y + speedInterval;
						if (p.velocity.Y < 0f && distPlayerY > 0f)
						{
							p.velocity.Y = p.velocity.Y + speedInterval;
						}
					}
					else if (p.velocity.Y > distPlayerY)
					{
						p.velocity.Y = p.velocity.Y - speedInterval;
						if (p.velocity.Y > 0f && distPlayerY < 0f)
						{
							p.velocity.Y = p.velocity.Y - speedInterval;
						}
					}
				}
				if (Main.myPlayer == p.owner)
				{
					Rectangle rectangle = p.Hitbox;
					Rectangle value = new Rectangle((int)position.X, (int)position.Y, width, height);
					if (rectangle.Intersects(value))
					{
						p.Kill();
					}
				}
			}
			p.rotation += rotationInterval * (float)p.direction;
		}

		public static void TileCollideBoomerang(Projectile p, ref Vector2 velocity, bool bounce = false)
		{
			if (bounce)
			{
				if (p.velocity.X != velocity.X)
				{
					p.velocity.X = -velocity.X;
				}
				if (p.velocity.Y != velocity.Y)
				{
					p.velocity.Y = -velocity.Y;
				}
			}
			else
			{
				p.ai[0] = 1f;
				p.velocity.X = -velocity.X;
				p.velocity.Y = -velocity.Y;
			}
			p.netUpdate = true;
		}

		public static void AIFlail(Projectile p, ref float[] ai, bool noKill = false, float chainDistance = 160f)
		{
			if (Main.player[p.owner] != null)
			{
				if (Main.player[p.owner].dead)
				{
					p.Kill();
					return;
				}
				Main.player[p.owner].itemAnimation = 10;
				Main.player[p.owner].itemTime = 10;
			}
			BaseAI.AIFlail(p, ref ai, Main.player[p.owner].Center, Main.player[p.owner].velocity, Main.player[p.owner].meleeSpeed, Main.player[p.owner].channel, noKill, chainDistance);
			Main.player[p.owner].direction = p.direction;
		}

		public static void AIFlail(Projectile p, ref float[] ai, Vector2 connectedPoint, Vector2 connectedPointVelocity, float meleeSpeed, bool channel, bool noKill = false, float chainDistance = 160f)
		{
			p.direction = ((p.Center.X > connectedPoint.X) ? 1 : -1);
			float pointX = connectedPoint.X - p.Center.X;
			float pointY = connectedPoint.Y - p.Center.Y;
			float pointDist = (float)Math.Sqrt((double)(pointX * pointX + pointY * pointY));
			if (ai[0] == 0f)
			{
				p.tileCollide = true;
				if (pointDist > chainDistance)
				{
					ai[0] = 1f;
					p.netUpdate = true;
				}
				else if (!channel)
				{
					if (p.velocity.Y < 0f)
					{
						p.velocity.Y = p.velocity.Y * 0.9f;
					}
					p.velocity.Y = p.velocity.Y + 1f;
					p.velocity.X = p.velocity.X * 0.9f;
				}
			}
			else if (ai[0] == 1f)
			{
				float meleeSpeed2 = 14f / meleeSpeed;
				float meleeSpeed3 = 0.9f / meleeSpeed;
				float maxBallDistance = chainDistance + 140f;
				Math.Abs(pointX);
				Math.Abs(pointY);
				if (ai[1] == 1f)
				{
					p.tileCollide = false;
				}
				if (!channel || pointDist > maxBallDistance || !p.tileCollide)
				{
					ai[1] = 1f;
					if (p.tileCollide)
					{
						p.netUpdate = true;
					}
					p.tileCollide = false;
					if (!noKill && pointDist < 20f)
					{
						p.Kill();
					}
				}
				if (!p.tileCollide)
				{
					meleeSpeed3 *= 2f;
				}
				if (pointDist > 60f || !p.tileCollide)
				{
					pointDist = meleeSpeed2 / pointDist;
					pointX *= pointDist;
					pointY *= pointDist;
					new Vector2(p.velocity.X, p.velocity.Y);
					float pointX2 = pointX - p.velocity.X;
					float pointY2 = pointY - p.velocity.Y;
					float pointDist2 = (float)Math.Sqrt((double)(pointX2 * pointX2 + pointY2 * pointY2));
					pointDist2 = meleeSpeed3 / pointDist2;
					pointX2 *= pointDist2;
					pointY2 *= pointDist2;
					p.velocity.X = p.velocity.X * 0.98f;
					p.velocity.Y = p.velocity.Y * 0.98f;
					p.velocity.X = p.velocity.X + pointX2;
					p.velocity.Y = p.velocity.Y + pointY2;
				}
				else
				{
					if (Math.Abs(p.velocity.X) + Math.Abs(p.velocity.Y) < 6f)
					{
						p.velocity.X = p.velocity.X * 0.96f;
						p.velocity.Y = p.velocity.Y + 0.2f;
					}
					if (connectedPointVelocity.X == 0f)
					{
						p.velocity.X = p.velocity.X * 0.96f;
					}
				}
			}
			p.rotation = (float)Math.Atan2((double)pointY, (double)pointX) - p.velocity.X * 0.1f;
		}

		public static void TileCollideFlail(Projectile p, ref Vector2 velocity, bool playSound = true)
		{
			if (velocity != p.velocity)
			{
				bool updateAndCollide = false;
				if (velocity.X != p.velocity.X)
				{
					if (Math.Abs(velocity.X) > 4f)
					{
						updateAndCollide = true;
					}
					p.position.X = p.position.X + p.velocity.X;
					p.velocity.X = -velocity.X * 0.2f;
				}
				if (velocity.Y != p.velocity.Y)
				{
					if (Math.Abs(velocity.Y) > 4f)
					{
						updateAndCollide = true;
					}
					p.position.Y = p.position.Y + p.velocity.Y;
					p.velocity.Y = -velocity.Y * 0.2f;
				}
				p.ai[0] = 1f;
				if (updateAndCollide)
				{
					p.netUpdate = true;
					Collision.HitTiles(p.position, p.velocity, p.width, p.height);
					if (playSound)
					{
						Main.PlaySound(0, (int)p.position.X, (int)p.position.Y, 1, 1f, 0f);
					}
				}
			}
		}

		public static bool StickToTiles(Vector2 position, ref Vector2 velocity, int width, int height, Func<int, int, bool> CanStick = null)
		{
			int tileLeftX = (int)(position.X / 16f) - 1;
			int tileRightX = (int)((position.X + (float)width) / 16f) + 2;
			int tileLeftY = (int)(position.Y / 16f) - 1;
			int tileRightY = (int)((position.Y + (float)height) / 16f) + 2;
			if (tileLeftX < 0)
			{
				tileLeftX = 0;
			}
			if (tileRightX > Main.maxTilesX)
			{
				tileRightX = Main.maxTilesX;
			}
			if (tileLeftY < 0)
			{
				tileLeftY = 0;
			}
			if (tileRightY > Main.maxTilesY)
			{
				tileRightY = Main.maxTilesY;
			}
			bool stick = false;
			for (int x = tileLeftX; x < tileRightX; x++)
			{
				for (int y = tileLeftY; y < tileRightY; y++)
				{
					if (Main.tile[x, y] != null && Main.tile[x, y].nactive() && ((CanStick != null) ? CanStick(x, y) : (Main.tileSolid[(int)Main.tile[x, y].type] || (Main.tileSolidTop[(int)Main.tile[x, y].type] && Main.tile[x, y].frameY == 0))))
					{
						Vector2 pos = new Vector2((float)(x * 16), (float)(y * 16));
						if (position.X + (float)width - 4f > pos.X && position.X + 4f < pos.X + 16f && position.Y + (float)height - 4f > pos.Y && position.Y + 4f < pos.Y + 16f)
						{
							stick = true;
							velocity *= 0f;
							break;
						}
					}
				}
				if (stick)
				{
					break;
				}
			}
			return stick;
		}

		public static void AIShadowflameGhost(NPC npc, ref float[] ai, bool speedupOverTime = false, float distanceBeforeTakeoff = 660f, float velIntervalX = 0.3f, float velMaxX = 7f, float velIntervalY = 0.2f, float velMaxY = 4f, float velScalarY = 4f, float velScalarYMax = 15f, float velIntervalXTurn = 0.4f, float velIntervalYTurn = 0.4f, float velIntervalScalar = 0.95f, float velIntervalMaxTurn = 5f)
		{
			int npcAvoidCollision;
			for (int i = 0; i < 200; i = npcAvoidCollision + 1)
			{
				if (i != npc.whoAmI && Main.npc[i].active && Main.npc[i].type == npc.type)
				{
					Vector2 dist = Main.npc[i].Center - npc.Center;
					if (dist.Length() < 50f)
					{
						dist.Normalize();
						if (dist.X == 0f && dist.Y == 0f)
						{
							if (i > npc.whoAmI)
							{
								dist.X = 1f;
							}
							else
							{
								dist.X = -1f;
							}
						}
						dist *= 0.4f;
						npc.velocity -= dist;
						Main.npc[i].velocity += dist;
					}
				}
				npcAvoidCollision = i;
			}
			if (speedupOverTime)
			{
				float timerMax = 120f;
				if (npc.localAI[0] < timerMax)
				{
					if (npc.localAI[0] == 0f)
					{
						Main.PlaySound(SoundID.Item8, npc.Center);
						npc.TargetClosest(true);
						if (npc.direction > 0)
						{
							npc.velocity.X = npc.velocity.X + 2f;
						}
						else
						{
							npc.velocity.X = npc.velocity.X - 2f;
						}
						for (int j = 0; j < 20; j = npcAvoidCollision + 1)
						{
							npcAvoidCollision = j;
						}
					}
					npc.localAI[0] += 1f;
					float timerPartialTimes20 = (1f - npc.localAI[0] / timerMax) * 20f;
					int nextNPC = 0;
					while ((float)nextNPC < timerPartialTimes20)
					{
						npcAvoidCollision = nextNPC;
						nextNPC = npcAvoidCollision + 1;
					}
				}
			}
			if (npc.ai[0] == 0f)
			{
				npc.TargetClosest(true);
				npc.ai[0] = 1f;
				npc.ai[1] = (float)npc.direction;
				return;
			}
			if (npc.ai[0] == 1f)
			{
				npc.TargetClosest(true);
				npc.velocity.X = npc.velocity.X + npc.ai[1] * velIntervalX;
				if (npc.velocity.X > velMaxX)
				{
					npc.velocity.X = velMaxX;
				}
				else if (npc.velocity.X < -velMaxX)
				{
					npc.velocity.X = -velMaxX;
				}
				float playerDistY = Main.player[npc.target].Center.Y - npc.Center.Y;
				if (Math.Abs(playerDistY) > velMaxY)
				{
					velScalarY = velScalarYMax;
				}
				if (playerDistY > velMaxY)
				{
					playerDistY = velMaxY;
				}
				else if (playerDistY < -velMaxY)
				{
					playerDistY = -velMaxY;
				}
				npc.velocity.Y = (npc.velocity.Y * (velScalarY - 1f) + playerDistY) / velScalarY;
				if ((npc.ai[1] > 0f && Main.player[npc.target].Center.X - npc.Center.X < -distanceBeforeTakeoff) || (npc.ai[1] < 0f && Main.player[npc.target].Center.X - npc.Center.X > distanceBeforeTakeoff))
				{
					npc.ai[0] = 2f;
					npc.ai[1] = 0f;
					if (npc.Center.Y + 20f > Main.player[npc.target].Center.Y)
					{
						npc.ai[1] = -1f;
						return;
					}
					npc.ai[1] = 1f;
					return;
				}
			}
			else if (npc.ai[0] == 2f)
			{
				npc.velocity.Y = npc.velocity.Y + npc.ai[1] * velIntervalYTurn;
				if (npc.velocity.Length() > velIntervalMaxTurn)
				{
					npc.velocity *= velIntervalScalar;
				}
				if (npc.velocity.X > -1f && npc.velocity.X < 1f)
				{
					npc.TargetClosest(true);
					npc.ai[0] = 3f;
					npc.ai[1] = (float)npc.direction;
					return;
				}
			}
			else if (npc.ai[0] == 3f)
			{
				npc.velocity.X = npc.velocity.X + npc.ai[1] * velIntervalXTurn;
				if (npc.Center.Y > Main.player[npc.target].Center.Y)
				{
					npc.velocity.Y = npc.velocity.Y - velIntervalY;
				}
				else
				{
					npc.velocity.Y = npc.velocity.Y + velIntervalY;
				}
				if (npc.velocity.Length() > velIntervalMaxTurn)
				{
					npc.velocity *= velIntervalScalar;
				}
				if (npc.velocity.Y > -1f && npc.velocity.Y < 1f)
				{
					npc.TargetClosest(true);
					npc.ai[0] = 0f;
					npc.ai[1] = (float)npc.direction;
				}
			}
		}

		public static void AISpaceOctopus(NPC npc, ref float[] ai, float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f, float shootProjInterval = 70f, Action<NPC, Vector2> FireProj = null)
		{
			npc.TargetClosest(true);
			BaseAI.AISpaceOctopus(npc, ref ai, Main.player[npc.target].Center, moveSpeed, velMax, hoverDistance, shootProjInterval, FireProj);
		}

		public static void AISpaceOctopus(NPC npc, ref float[] ai, Vector2 targetCenter = default(Vector2), float moveSpeed = 0.15f, float velMax = 5f, float hoverDistance = 250f, float shootProjInterval = 70f, Action<NPC, Vector2> FireProj = null)
		{
			Vector2 wantedVelocity = targetCenter - npc.Center + new Vector2(0f, -hoverDistance);
			float dist = wantedVelocity.Length();
			if (dist < 20f)
			{
				wantedVelocity = npc.velocity;
			}
			else if (dist < 40f)
			{
				wantedVelocity.Normalize();
				wantedVelocity *= velMax * 0.35f;
			}
			else if (dist < 80f)
			{
				wantedVelocity.Normalize();
				wantedVelocity *= velMax * 0.65f;
			}
			else
			{
				wantedVelocity.Normalize();
				wantedVelocity *= velMax;
			}
			npc.SimpleFlyMovement(wantedVelocity, moveSpeed);
			npc.rotation = npc.velocity.X * 0.1f;
			if (FireProj != null && shootProjInterval > -1f && (ai[0] += 1f) >= shootProjInterval)
			{
				ai[0] = 0f;
				if (Main.netMode != 1)
				{
					Vector2 projVelocity = Vector2.Zero;
					while (Math.Abs(projVelocity.X) < 1.5f)
					{
						projVelocity = Utils.RotatedByRandom(Vector2.UnitY, 1.5707963705062866) * new Vector2(5f, 3f);
					}
					FireProj(npc, projVelocity);
				}
			}
		}

		public static void AIElemental(NPC npc, ref float[] ai, bool? noDamageMode = null, int noDamageTimeMax = 120, bool gravityChange = true, bool tileCollideChange = true, float startPhaseDist = 800f, float stopPhaseDist = 600f, int idleTooLong = 180, float velSpeed = 2f)
		{
			int timerDummy = (int)npc.localAI[0];
			BaseAI.AIElemental(npc, ref ai, ref timerDummy, noDamageMode, noDamageTimeMax, gravityChange, tileCollideChange, startPhaseDist, stopPhaseDist, idleTooLong, velSpeed);
			npc.localAI[0] = (float)timerDummy;
		}

		public static void AIElemental(NPC npc, ref float[] ai, ref int idleTimer, bool? noDamageMode = null, int noDamageTimeMax = 120, bool gravityChange = true, bool tileCollideChange = true, float startPhaseDist = 800f, float stopPhaseDist = 600f, int idleTooLong = 180, float velSpeed = 2f)
		{
			bool noDmg = (noDamageMode == null) ? Main.expertMode : noDamageMode.Value;
			if (gravityChange)
			{
				npc.noGravity = true;
			}
			if (tileCollideChange)
			{
				npc.noTileCollide = false;
			}
			if (noDmg)
			{
				npc.dontTakeDamage = false;
			}
			Player targetPlayer = (npc.target < 0) ? null : Main.player[npc.target];
			Vector2 playerCenter = (targetPlayer == null) ? (npc.Center + new Vector2(0f, 5f)) : targetPlayer.Center;
			playerCenter - npc.Center;
			if (npc.justHit && Main.netMode != 1 && noDmg && Main.rand.Next(6) == 0)
			{
				npc.netUpdate = true;
				ai[0] = -1f;
				ai[1] = 0f;
			}
			if (ai[0] == -1f)
			{
				if (noDmg)
				{
					npc.dontTakeDamage = true;
				}
				if (gravityChange)
				{
					npc.noGravity = false;
				}
				npc.velocity.X = npc.velocity.X * 0.98f;
				ai[1] += 1f;
				if (ai[1] >= (float)noDamageTimeMax)
				{
					ai[0] = (ai[1] = (ai[2] = (ai[3] = 0f)));
					return;
				}
			}
			else if (ai[0] == 0f)
			{
				npc.TargetClosest(true);
				targetPlayer = Main.player[npc.target];
				playerCenter = targetPlayer.Center;
				playerCenter - npc.Center;
				if (Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
				{
					ai[0] = 1f;
					return;
				}
				Vector2 centerDiff = playerCenter - npc.Center;
				centerDiff.Y -= (float)(targetPlayer.height / 4);
				if (centerDiff.Length() > startPhaseDist)
				{
					ai[0] = 2f;
					return;
				}
				Vector2 npcCenter = npc.Center;
				npcCenter.X = playerCenter.X;
				Vector2 npcCentDiff = npcCenter - npc.Center;
				if (npcCentDiff.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1))
				{
					ai[0] = 3f;
					ai[1] = npcCenter.X;
					ai[2] = npcCenter.Y;
					Vector2 npcCenter2 = npc.Center;
					npcCenter2.Y = playerCenter.Y;
					if (npcCentDiff.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, npcCenter2, 1, 1) && Collision.CanHit(npcCenter2, 1, 1, targetPlayer.position, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = npcCenter2.X;
						ai[2] = npcCenter2.Y;
					}
				}
				else
				{
					npcCenter = npc.Center;
					npcCenter.Y = playerCenter.Y;
					if ((npcCenter - npc.Center).Length() > 8f && Collision.CanHit(npc.Center, 1, 1, npcCenter, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = npcCenter.X;
						ai[2] = npcCenter.Y;
					}
				}
				if (ai[0] == 0f)
				{
					npc.localAI[0] = 0f;
					centerDiff.Normalize();
					centerDiff *= 0.5f;
					npc.velocity += centerDiff;
					ai[0] = 4f;
					ai[1] = 0f;
					return;
				}
			}
			else if (ai[0] == 1f)
			{
				Vector2 distDiff = playerCenter - npc.Center;
				float distLength = distDiff.Length();
				float velSpeed2 = velSpeed + distLength / 200f;
				float speedAdjuster = 50f;
				distDiff.Normalize();
				distDiff *= velSpeed2;
				npc.velocity = (npc.velocity * (speedAdjuster - 1f) + distDiff) / speedAdjuster;
				if (!Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
				{
					ai[0] = 0f;
					ai[1] = 0f;
					return;
				}
			}
			else if (ai[0] == 2f)
			{
				npc.noTileCollide = true;
				Vector2 distDiff2 = playerCenter - npc.Center;
				float num = distDiff2.Length();
				float speedAdjusterPhase = 4f;
				distDiff2.Normalize();
				distDiff2 *= velSpeed;
				npc.velocity = (npc.velocity * (speedAdjusterPhase - 1f) + distDiff2) / speedAdjusterPhase;
				if (num < stopPhaseDist && !Collision.SolidCollision(npc.position, npc.width, npc.height))
				{
					ai[0] = 0f;
					return;
				}
			}
			else if (ai[0] == 3f)
			{
				Vector2 targetDiff = new Vector2(ai[1], ai[2]) - npc.Center;
				float targetLength = targetDiff.Length();
				float velSpeedHorizontal = (velSpeed < 1f) ? (velSpeed * 0.5f) : Math.Max(0.1f, velSpeed - 1f);
				float speedAdjusterHorizontal = 3f;
				targetDiff.Normalize();
				targetDiff *= velSpeedHorizontal;
				npc.velocity = (npc.velocity * (speedAdjusterHorizontal - 1f) + targetDiff) / speedAdjusterHorizontal;
				if (npc.collideX || npc.collideY)
				{
					ai[0] = 4f;
					ai[1] = 0f;
				}
				if (targetLength < velSpeedHorizontal || targetLength > startPhaseDist || Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
				{
					ai[0] = 0f;
					return;
				}
			}
			else if (ai[0] == 4f)
			{
				if (npc.collideX)
				{
					npc.velocity.X = npc.velocity.X * -0.8f;
				}
				if (npc.collideY)
				{
					npc.velocity.Y = npc.velocity.Y * -0.8f;
				}
				Vector2 velVec;
				if (npc.velocity.X == 0f && npc.velocity.Y == 0f)
				{
					velVec = playerCenter - npc.Center;
					velVec.Y -= (float)(targetPlayer.height / 4);
					velVec.Normalize();
					npc.velocity = velVec * 0.1f;
				}
				float velSpeedIdle = (velSpeed < 1f) ? (velSpeed * 0.75f) : Math.Max(0.1f, velSpeed - 0.5f);
				float speedAdjusterIdle = 20f;
				velVec = npc.velocity;
				velVec.Normalize();
				velVec *= velSpeedIdle;
				npc.velocity = (npc.velocity * (speedAdjusterIdle - 1f) + velVec) / speedAdjusterIdle;
				ai[1] += 1f;
				if (ai[1] > (float)idleTooLong)
				{
					ai[0] = 0f;
					ai[1] = 0f;
				}
				if (Collision.CanHit(npc.Center, 1, 1, playerCenter, 1, 1))
				{
					ai[0] = 0f;
				}
				idleTimer++;
				if (idleTimer >= 5 && !Collision.SolidCollision(npc.position - new Vector2(10f, 10f), npc.width + 20, npc.height + 20))
				{
					idleTimer = 0;
					Vector2 npcCenter3 = npc.Center;
					npcCenter3.X = playerCenter.X;
					if (Collision.CanHit(npc.Center, 1, 1, npcCenter3, 1, 1) && Collision.CanHit(npc.Center, 1, 1, npcCenter3, 1, 1) && Collision.CanHit(playerCenter, 1, 1, npcCenter3, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = npcCenter3.X;
						ai[2] = npcCenter3.Y;
						return;
					}
					npcCenter3 = npc.Center;
					npcCenter3.Y = playerCenter.Y;
					if (Collision.CanHit(npc.Center, 1, 1, npcCenter3, 1, 1) && Collision.CanHit(playerCenter, 1, 1, npcCenter3, 1, 1))
					{
						ai[0] = 3f;
						ai[1] = npcCenter3.X;
						ai[2] = npcCenter3.Y;
						return;
					}
				}
			}
		}

		public static void AIWeapon(NPC npc, ref float[] ai, int rotTime = 120, int moveTime = 100, float maxSpeed = 6f, float movementScalar = 1f, float rotScalar = 1f)
		{
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
			{
				npc.TargetClosest(true);
			}
			BaseAI.AIWeapon(npc, ref ai, ref npc.rotation, Main.player[npc.target].Center, npc.justHit, rotTime, moveTime, maxSpeed, movementScalar, rotScalar);
		}

		public static void AIWeapon(Entity codable, ref float[] ai, ref float rotation, Vector2 targetPos, bool justHit = false, int rotTime = 120, int moveTime = 100, float maxSpeed = 6f, float movementScalar = 1f, float rotScalar = 1f)
		{
			if (ai[0] == 0f)
			{
				Vector2 vector2 = codable.Center;
				float distX = targetPos.X - vector2.X;
				float distY = targetPos.Y - vector2.Y;
				float dist = (float)Math.Sqrt((double)(distX * distX + distY * distY));
				float distMult = 9f / dist;
				codable.velocity.X = distX * distMult * movementScalar;
				codable.velocity.Y = distY * distMult * movementScalar;
				if (codable.velocity.X > maxSpeed)
				{
					codable.velocity.X = maxSpeed;
				}
				if (codable.velocity.X < -maxSpeed)
				{
					codable.velocity.X = -maxSpeed;
				}
				if (codable.velocity.Y > maxSpeed)
				{
					codable.velocity.Y = maxSpeed;
				}
				if (codable.velocity.Y < -maxSpeed)
				{
					codable.velocity.Y = -maxSpeed;
				}
				rotation = (float)Math.Atan2((double)codable.velocity.Y, (double)codable.velocity.X);
				ai[0] = 1f;
				ai[1] = 0f;
				return;
			}
			if (ai[0] == 1f)
			{
				if (justHit)
				{
					ai[0] = 2f;
					ai[1] = 0f;
				}
				codable.velocity *= 0.99f;
				ai[1] += 1f;
				if (ai[1] < (float)moveTime)
				{
					return;
				}
				ai[0] = 2f;
				ai[1] = 0f;
				codable.velocity.X = 0f;
				codable.velocity.Y = 0f;
				return;
			}
			else
			{
				if (justHit)
				{
					ai[0] = 2f;
					ai[1] = 0f;
				}
				codable.velocity *= 0.96f;
				ai[1] += 1f;
				rotation += (float)(0.1 + (double)(ai[1] / (float)rotTime) * 0.4000000059604645) * (float)codable.direction * rotScalar;
				if (ai[1] < (float)rotTime)
				{
					return;
				}
				if (codable is NPC)
				{
					((NPC)codable).netUpdate = true;
				}
				else if (codable is Projectile)
				{
					((Projectile)codable).netUpdate = true;
				}
				ai[0] = 0f;
				ai[1] = 0f;
				return;
			}
		}

		public static void AISnail(NPC npc, ref float[] ai, ref int snailStatus, float moveInterval = 0.3f, float rotAmt = 0.1f)
		{
			if (ai[0] == 0f)
			{
				npc.TargetClosest(true);
				npc.directionY = 1;
				ai[0] = 1f;
				if (npc.direction > 0)
				{
					npc.spriteDirection = 1;
				}
			}
			bool collisonOnX = false;
			if (Main.netMode != 1)
			{
				if (ai[2] == 0f && Main.rand.Next(7200) == 0)
				{
					ai[2] = 2f;
					npc.netUpdate = true;
				}
				if (!npc.collideX && !npc.collideY)
				{
					npc.localAI[3] += 1f;
					if (npc.localAI[3] > 5f)
					{
						ai[2] = 2f;
						npc.netUpdate = true;
					}
				}
				else
				{
					npc.localAI[3] = 0f;
				}
			}
			if (ai[2] > 0f)
			{
				ai[1] = 0f;
				ai[0] = 1f;
				npc.directionY = 1;
				if (npc.velocity.Y > moveInterval)
				{
					npc.rotation += (float)npc.direction * 0.1f;
				}
				else
				{
					npc.rotation = 0f;
				}
				npc.spriteDirection = npc.direction;
				npc.velocity.X = moveInterval * (float)npc.direction;
				npc.noGravity = false;
				snailStatus = 0;
				if (npc.collideX && npc.velocity.Y == 0f)
				{
					collisonOnX = true;
					ai[2] = 0f;
					ai[1] = 1f;
					npc.directionY = -1;
				}
				if (npc.velocity.Y == 0f)
				{
					if (npc.localAI[1] == npc.position.X)
					{
						npc.localAI[2] += 1f;
						if (npc.localAI[2] > 10f)
						{
							npc.direction = 1;
							npc.velocity.X = (float)npc.direction * moveInterval;
							npc.localAI[2] = 0f;
						}
					}
					else
					{
						npc.localAI[2] = 0f;
						npc.localAI[1] = npc.position.X;
					}
				}
			}
			if (ai[2] == 0f)
			{
				npc.noGravity = true;
				if (ai[1] == 0f)
				{
					if (npc.collideY)
					{
						ai[0] = 2f;
					}
					if (!npc.collideY && ai[0] == 2f)
					{
						npc.direction = -npc.direction;
						ai[1] = 1f;
						ai[0] = 1f;
					}
					if (npc.collideX)
					{
						npc.directionY = -npc.directionY;
						ai[1] = 1f;
					}
				}
				else
				{
					if (npc.collideX)
					{
						ai[0] = 2f;
					}
					if (!npc.collideX && ai[0] == 2f)
					{
						npc.directionY = -npc.directionY;
						ai[1] = 0f;
						ai[0] = 1f;
					}
					if (npc.collideY)
					{
						npc.direction = -npc.direction;
						ai[1] = 0f;
					}
				}
				if (!collisonOnX)
				{
					float prevRot = npc.rotation;
					if (npc.directionY < 0)
					{
						if (npc.direction < 0)
						{
							if (npc.collideX)
							{
								npc.rotation = 1.57f;
								npc.spriteDirection = -1;
							}
							else if (npc.collideY)
							{
								npc.rotation = 3.14f;
								npc.spriteDirection = 1;
							}
						}
						else if (npc.collideY)
						{
							npc.rotation = 3.14f;
							npc.spriteDirection = -1;
						}
						else if (npc.collideX)
						{
							npc.rotation = 4.71f;
							npc.spriteDirection = 1;
						}
					}
					else if (npc.direction < 0)
					{
						if (npc.collideY)
						{
							npc.rotation = 0f;
							npc.spriteDirection = -1;
						}
						else if (npc.collideX)
						{
							npc.rotation = 1.57f;
							npc.spriteDirection = 1;
						}
					}
					else if (npc.collideX)
					{
						npc.rotation = 4.71f;
						npc.spriteDirection = -1;
					}
					else if (npc.collideY)
					{
						npc.rotation = 0f;
						npc.spriteDirection = 1;
					}
					float prevRot2 = npc.rotation;
					npc.rotation = prevRot;
					if ((double)npc.rotation > 6.28)
					{
						npc.rotation -= 6.28f;
					}
					if (npc.rotation < 0f)
					{
						npc.rotation += 6.28f;
					}
					float rotDiffAbs = Math.Abs(npc.rotation - prevRot2);
					if (npc.rotation > prevRot2)
					{
						if ((double)rotDiffAbs > 3.14)
						{
							npc.rotation += rotAmt;
						}
						else
						{
							npc.rotation -= rotAmt;
							if (npc.rotation < prevRot2)
							{
								npc.rotation = prevRot2;
							}
						}
					}
					if (npc.rotation < prevRot2)
					{
						if ((double)rotDiffAbs > 3.14)
						{
							npc.rotation -= rotAmt;
						}
						else
						{
							npc.rotation += rotAmt;
							if (npc.rotation > prevRot2)
							{
								npc.rotation = prevRot2;
							}
						}
					}
				}
				if (npc.directionY == -1 && !npc.collideX)
				{
					snailStatus = 1;
				}
				else if (npc.directionY == 1 && !npc.collideX)
				{
					snailStatus = 0;
				}
				else if (npc.direction == 1 && !npc.collideY)
				{
					snailStatus = 3;
				}
				else
				{
					snailStatus = 2;
				}
				npc.velocity.X = moveInterval * (float)npc.direction;
				npc.velocity.Y = moveInterval * (float)npc.directionY;
			}
		}

		public static void CollisionTest(NPC npc, ref bool left, ref bool right, ref bool up, ref bool down)
		{
			up = (down = (left = (right = false)));
			int lengthX = npc.width / 16 + ((npc.width % 16 > 0) ? 1 : 0);
			int lengthY = npc.height / 16 + ((npc.height % 16 > 0) ? 1 : 0);
			int xLeft = Math.Max(0, Math.Min(Main.maxTilesX - 1, (int)(npc.position.X / 16f) - 1));
			int xRight = Math.Max(0, Math.Min(Main.maxTilesX - 1, xLeft + lengthX + 1));
			int yUp = Math.Max(0, Math.Min(Main.maxTilesY - 1, (int)(npc.position.Y / 16f) - 1));
			int yDown = Math.Max(0, Math.Min(Main.maxTilesY - 1, yUp + lengthY + 1));
			for (int x2 = xLeft; x2 < xRight; x2++)
			{
				Tile tileUp = Main.tile[x2, yUp];
				Tile tileDown = Main.tile[x2, yDown];
				if (tileUp != null && tileUp.nactive() && Main.tileSolid[(int)tileUp.type] && !Main.tileSolidTop[(int)tileUp.type])
				{
					up = true;
				}
				if (tileDown != null && tileDown.nactive() && Main.tileSolid[(int)tileDown.type])
				{
					down = true;
				}
				if (up & down)
				{
					break;
				}
			}
			for (int y2 = yUp; y2 < yDown; y2++)
			{
				Tile tileLeft = Main.tile[xLeft, y2];
				Tile tileRight = Main.tile[xRight, y2];
				if (tileLeft != null && tileLeft.nactive() && Main.tileSolid[(int)tileLeft.type] && !Main.tileSolidTop[(int)tileLeft.type])
				{
					left = true;
				}
				if (tileRight != null && tileRight.nactive() && Main.tileSolid[(int)tileRight.type] && !Main.tileSolidTop[(int)tileRight.type])
				{
					right = true;
				}
				if (left & right)
				{
					break;
				}
			}
		}

		public static void AISpore(NPC npc, ref float[] ai, float moveIntervalX = 0.1f, float moveIntervalY = 0.02f, float maxSpeedX = 5f, float maxSpeedY = 1f)
		{
			npc.TargetClosest(true);
			BaseAI.AISpore(npc, ref ai, Main.player[npc.target].Center, Main.player[npc.target].width, moveIntervalX, moveIntervalY, maxSpeedX, maxSpeedY);
		}

		public static void AISpore(Entity codable, ref float[] ai, Vector2 target, int targetWidth = 16, float moveIntervalX = 0.1f, float moveIntervalY = 0.02f, float maxSpeedX = 5f, float maxSpeedY = 1f)
		{
			codable.velocity.Y = codable.velocity.Y + moveIntervalY;
			if (codable.velocity.Y < 0f)
			{
				codable.velocity.Y = codable.velocity.Y * 0.99f;
			}
			if (codable.velocity.Y > 1f)
			{
				codable.velocity.Y = 1f;
			}
			int widthHalf = targetWidth / 2;
			if (codable.position.X + (float)codable.width < target.X - (float)widthHalf)
			{
				if (codable.velocity.X < 0f)
				{
					codable.velocity.X = codable.velocity.X * 0.98f;
				}
				codable.velocity.X = codable.velocity.X + moveIntervalX;
			}
			else if (codable.position.X > target.X + (float)widthHalf)
			{
				if (codable.velocity.X > 0f)
				{
					codable.velocity.X = codable.velocity.X * 0.98f;
				}
				codable.velocity.X = codable.velocity.X - moveIntervalX;
			}
			if (codable.velocity.X > maxSpeedX || codable.velocity.X < -maxSpeedX)
			{
				codable.velocity.X = codable.velocity.X * 0.97f;
			}
		}

		public static void AICharger(NPC npc, ref float[] ai, float moveInterval = 0.07f, float maxSpeed = 6f, bool allowBoredom = true, int ticksUntilBoredom = 30)
		{
			bool isMoving = false;
			if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
			{
				isMoving = true;
				ai[3] += 1f;
			}
			if (npc.position.X == npc.oldPosition.X || ai[3] >= (float)ticksUntilBoredom || isMoving)
			{
				ai[3] += 1f;
			}
			else if (ai[3] > 0f)
			{
				ai[3] -= 1f;
			}
			if (ai[3] > (float)(ticksUntilBoredom * 10))
			{
				ai[3] = 0f;
			}
			if (npc.justHit)
			{
				ai[3] = 0f;
			}
			if (ai[3] == (float)ticksUntilBoredom)
			{
				npc.netUpdate = true;
			}
			Vector2 npcCenter = npc.Center;
			float num = Main.player[npc.target].Center.X - npcCenter.X;
			float distY = Main.player[npc.target].Center.Y - npcCenter.Y;
			if ((float)Math.Sqrt((double)(num * num + distY * distY)) < 200f)
			{
				ai[3] = 0f;
			}
			if (!allowBoredom || ai[3] < (float)ticksUntilBoredom)
			{
				npc.TargetClosest(true);
			}
			else
			{
				if (npc.velocity.X == 0f)
				{
					if (npc.velocity.Y == 0f)
					{
						ai[0] += 1f;
						if ((double)ai[0] >= 2.0)
						{
							npc.direction *= -1;
							npc.spriteDirection = npc.direction;
							ai[0] = 0f;
						}
					}
				}
				else
				{
					ai[0] = 0f;
				}
				npc.directionY = -1;
				if (npc.direction == 0)
				{
					npc.direction = 1;
				}
			}
			if (npc.velocity.Y == 0f || npc.wet || (npc.velocity.X <= 0f && npc.direction < 0) || (npc.velocity.X >= 0f && npc.direction > 0))
			{
				if (npc.velocity.X < -maxSpeed || npc.velocity.X > maxSpeed)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
						return;
					}
				}
				else if (npc.velocity.X < maxSpeed && npc.direction == 1)
				{
					npc.velocity.X = npc.velocity.X + moveInterval;
					if (npc.velocity.X > maxSpeed)
					{
						npc.velocity.X = maxSpeed;
						return;
					}
				}
				else if (npc.velocity.X > -maxSpeed && npc.direction == -1)
				{
					npc.velocity.X = npc.velocity.X - moveInterval;
					if (npc.velocity.X < -maxSpeed)
					{
						npc.velocity.X = -maxSpeed;
					}
				}
			}
		}

		public static void AIFriendly(NPC npc, ref float[] ai, float moveInterval = 0.07f, float maxSpeed = 1f, bool critter = false, bool? seekHome = null, bool canTeleportHome = true, bool canFindHouse = true, bool canOpenDoors = true)
		{
			bool seekHouse = Main.raining;
			if (!Main.dayTime || Main.eclipse)
			{
				seekHouse = true;
			}
			if (seekHome != null)
			{
				seekHouse = seekHome.Value;
			}
			int npcTileX = (int)((double)npc.position.X + (double)(npc.width / 2)) / 16;
			int npcTileY = (int)((double)npc.position.Y + (double)npc.height + 1.0) / 16;
			if (critter && npc.target == 255)
			{
				npc.TargetClosest(true);
				npc.direction = ((npc.Center.X < Main.player[npc.target].Center.X) ? 1 : -1);
				npc.spriteDirection = npc.direction;
				if (npc.homeTileX == -1)
				{
					npc.homeTileX = (int)(npc.Center.X / 16f);
				}
			}
			bool isTalking = false;
			npc.directionY = -1;
			if (npc.direction == 0)
			{
				npc.direction = 1;
			}
			for (int m = 0; m < 255; m++)
			{
				if (Main.player[m].active && Main.player[m].talkNPC == npc.whoAmI)
				{
					isTalking = true;
					if (ai[0] != 0f)
					{
						npc.netUpdate = true;
					}
					ai[0] = 0f;
					ai[1] = 300f;
					ai[2] = 100f;
					npc.direction = ((Main.player[m].Center.X >= npc.Center.X) ? 1 : -1);
				}
			}
			if (ai[3] > 0f)
			{
				npc.life = -1;
				npc.HitEffect(0, 10.0);
				npc.active = false;
				if (npc.type == 37)
				{
					Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
				}
			}
			if ((npc.type >= 580 && npc.homeTileX == -1 && npc.homeTileY == -1) || (npc.homeTileX == 65535 && npc.homeTileY == 65535))
			{
				npc.homeTileX = (int)npc.Center.X / 16;
				npc.homeTileY = (int)npc.Center.Y / 16;
			}
			int homeTileY = npc.homeTileY;
			if (Main.netMode != 1 && npc.homeTileY > 0)
			{
				while (!WorldGen.SolidTile(npc.homeTileX, homeTileY) && homeTileY < Main.maxTilesY - 20)
				{
					homeTileY++;
				}
			}
			if (Main.netMode != 1 && canTeleportHome && seekHouse && (npcTileX != npc.homeTileX || npcTileY != homeTileY) && !npc.homeless)
			{
				bool moveToHome = true;
				for (int m2 = 0; m2 < 2; m2++)
				{
					Rectangle checkRect = new Rectangle((int)(npc.Center.X - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(npc.Center.Y - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
					if (m2 == 1)
					{
						checkRect = new Rectangle(npc.homeTileX * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, homeTileY * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
					}
					for (int m3 = 0; m3 < 255; m3++)
					{
						if (Main.player[m3].active && Main.player[m3].Hitbox.Intersects(checkRect))
						{
							moveToHome = false;
							break;
						}
						if (!moveToHome)
						{
							break;
						}
					}
				}
				if (moveToHome)
				{
					if (!Collision.SolidTiles(npc.homeTileX - 1, npc.homeTileX + 1, homeTileY - 3, homeTileY - 1))
					{
						npc.velocity.X = 0f;
						npc.velocity.Y = 0f;
						npc.position.X = (float)(npc.homeTileX * 16 + 8 - npc.width / 2);
						npc.position.Y = (float)(homeTileY * 16 - npc.height) - 0.1f;
						npc.netUpdate = true;
					}
					else if (canFindHouse)
					{
						npc.homeless = true;
						WorldGen.QuickFindHome(npc.whoAmI);
					}
				}
			}
			if (ai[0] == 0f)
			{
				if (ai[2] > 0f)
				{
					ai[2] -= 1f;
				}
				if (seekHouse && !isTalking && !critter)
				{
					if (Main.netMode != 1)
					{
						if (npcTileX == npc.homeTileX && npcTileY == homeTileY)
						{
							if (npc.velocity.X != 0f)
							{
								npc.netUpdate = true;
							}
							if (npc.velocity.X > 0.1f)
							{
								npc.velocity.X = npc.velocity.X - 0.1f;
							}
							else if (npc.velocity.X < -0.1f)
							{
								npc.velocity.X = npc.velocity.X + 0.1f;
							}
							else
							{
								npc.velocity.X = 0f;
							}
						}
						else
						{
							npc.direction = ((npcTileX <= npc.homeTileX) ? 1 : -1);
							ai[0] = 1f;
							ai[1] = (float)(200 + Main.rand.Next(200));
							ai[2] = 0f;
							npc.netUpdate = true;
						}
					}
				}
				else
				{
					if (npc.velocity.X > 0.1f)
					{
						npc.velocity.X = npc.velocity.X - 0.1f;
					}
					else if (npc.velocity.X < -0.1f)
					{
						npc.velocity.X = npc.velocity.X + 0.1f;
					}
					else
					{
						npc.velocity.X = 0f;
					}
					if (Main.netMode != 1)
					{
						if (ai[1] > 0f)
						{
							ai[1] -= 1f;
						}
						if (ai[1] <= 0f)
						{
							ai[0] = 1f;
							ai[1] = (float)(200 + Main.rand.Next(200));
							if (critter)
							{
								ai[1] += (float)Main.rand.Next(200, 400);
							}
							ai[2] = 0f;
							npc.netUpdate = true;
						}
					}
				}
				if (Main.netMode == 1 || (seekHouse && (npcTileX != npc.homeTileX || npcTileY != homeTileY)))
				{
					return;
				}
				if (npcTileX < npc.homeTileX - 25 || npcTileX > npc.homeTileX + 25)
				{
					if (ai[2] != 0f)
					{
						return;
					}
					if (npcTileX < npc.homeTileX - 50 && npc.direction == -1)
					{
						npc.direction = 1;
						npc.netUpdate = true;
						return;
					}
					if (npcTileX <= npc.homeTileX + 50 || npc.direction != 1)
					{
						return;
					}
					npc.direction = -1;
					npc.netUpdate = true;
					return;
				}
				else
				{
					if (Main.rand.Next(80) != 0 || (double)ai[2] != 0.0)
					{
						return;
					}
					ai[2] = 200f;
					npc.direction *= -1;
					npc.netUpdate = true;
					return;
				}
			}
			else
			{
				if (ai[0] != 1f)
				{
					return;
				}
				if (Main.netMode != 1 && !critter && seekHouse && npcTileX == npc.homeTileX && npcTileY == npc.homeTileY)
				{
					ai[0] = 0f;
					ai[1] = (float)(200 + Main.rand.Next(200));
					ai[2] = 60f;
					npc.netUpdate = true;
					return;
				}
				if (Main.netMode != 1 && !npc.homeless && !Main.tileDungeon[(int)Main.tile[npcTileX, npcTileY].type] && (npcTileX < npc.homeTileX - 35 || npcTileX > npc.homeTileX + 35))
				{
					if (npc.Center.X < (float)(npc.homeTileX * 16) && npc.direction == -1)
					{
						ai[1] -= 5f;
					}
					else if (npc.Center.X > (float)(npc.homeTileX * 16) && npc.direction == 1)
					{
						ai[1] -= 5f;
					}
				}
				ai[1] -= 1f;
				if (ai[1] <= 0f)
				{
					ai[0] = 0f;
					ai[1] = (float)(300 + Main.rand.Next(300));
					if (critter)
					{
						ai[1] -= (float)Main.rand.Next(100);
					}
					ai[2] = 60f;
					npc.netUpdate = true;
				}
				if (npc.closeDoor && (npc.Center.X / 16f > (float)(npc.doorX + 2) || npc.Center.X / 16f < (float)(npc.doorX - 2)))
				{
					if (WorldGen.CloseDoor(npc.doorX, npc.doorY, false))
					{
						npc.closeDoor = false;
						NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 1, (float)npc.doorX, (float)npc.doorY, (float)npc.direction, 0, 0, 0);
					}
					if (npc.Center.X / 16f > (float)(npc.doorX + 4) || npc.Center.X / 16f < (float)(npc.doorX - 4) || npc.Center.Y / 16f > (float)(npc.doorY + 4) || npc.Center.Y / 16f < (float)(npc.doorY - 4))
					{
						npc.closeDoor = false;
					}
				}
				if (npc.Center.X < -maxSpeed || npc.velocity.X > maxSpeed)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < maxSpeed && npc.direction == 1)
				{
					npc.velocity.X = npc.velocity.X + moveInterval;
					if (npc.velocity.X > maxSpeed)
					{
						npc.velocity.X = maxSpeed;
					}
				}
				else if (npc.velocity.X > -maxSpeed && npc.direction == -1)
				{
					npc.velocity.X = npc.velocity.X - moveInterval;
					if (npc.velocity.X > maxSpeed)
					{
						npc.velocity.X = maxSpeed;
					}
				}
				BaseAI.WalkupHalfBricks(npc);
				if (npc.velocity.Y != 0f)
				{
					return;
				}
				if (npc.position.X == ai[2])
				{
					npc.direction *= -1;
				}
				ai[2] = -1f;
				int tileX2 = (int)((npc.Center.X + (float)(15 * npc.direction)) / 16f);
				int tileY2 = (int)((npc.position.Y + (float)npc.height - 16f) / 16f);
				if (Main.tile[tileX2, tileY2] == null)
				{
					Main.tile[tileX2, tileY2] = new Tile();
				}
				if (Main.tile[tileX2, tileY2 - 1] == null)
				{
					Main.tile[tileX2, tileY2 - 1] = new Tile();
				}
				if (Main.tile[tileX2, tileY2 - 2] == null)
				{
					Main.tile[tileX2, tileY2 - 2] = new Tile();
				}
				if (Main.tile[tileX2, tileY2 - 3] == null)
				{
					Main.tile[tileX2, tileY2 - 3] = new Tile();
				}
				if (Main.tile[tileX2, tileY2 + 1] == null)
				{
					Main.tile[tileX2, tileY2 + 1] = new Tile();
				}
				if (Main.tile[tileX2 - npc.direction, tileY2 + 1] == null)
				{
					Main.tile[tileX2 - npc.direction, tileY2 + 1] = new Tile();
				}
				if (Main.tile[tileX2 + npc.direction, tileY2 - 1] == null)
				{
					Main.tile[tileX2 + npc.direction, tileY2 - 1] = new Tile();
				}
				if (Main.tile[tileX2 + npc.direction, tileY2 + 1] == null)
				{
					Main.tile[tileX2 + npc.direction, tileY2 + 1] = new Tile();
				}
				if (!canOpenDoors || !Main.tile[tileX2, tileY2 - 2].nactive() || Main.tile[tileX2, tileY2 - 2].type != 10 || (Main.rand.Next(10) != 0 && !seekHouse))
				{
					if ((npc.velocity.X < 0f && npc.spriteDirection == -1) || (npc.velocity.X > 0f && npc.spriteDirection == 1))
					{
						if (Main.tile[tileX2, tileY2 - 2].nactive() && Main.tileSolid[(int)Main.tile[tileX2, tileY2 - 2].type] && !Main.tileSolidTop[(int)Main.tile[tileX2, tileY2 - 2].type])
						{
							if ((npc.direction == 1 && !Collision.SolidTiles(tileX2 - 2, tileX2 - 1, tileY2 - 5, tileY2 - 1)) || (npc.direction == -1 && !Collision.SolidTiles(tileX2 + 1, tileX2 + 2, tileY2 - 5, tileY2 - 1)))
							{
								if (!Collision.SolidTiles(tileX2, tileX2, tileY2 - 5, tileY2 - 3))
								{
									npc.velocity.Y = -6f;
									npc.netUpdate = true;
								}
								else
								{
									npc.direction *= -1;
									npc.netUpdate = true;
								}
							}
							else
							{
								npc.direction *= -1;
								npc.netUpdate = true;
							}
						}
						else if (Main.tile[tileX2, tileY2 - 1].nactive() && Main.tileSolid[(int)Main.tile[tileX2, tileY2 - 1].type] && !Main.tileSolidTop[(int)Main.tile[tileX2, tileY2 - 1].type])
						{
							if ((npc.direction == 1 && !Collision.SolidTiles(tileX2 - 2, tileX2 - 1, tileY2 - 4, tileY2 - 1)) || (npc.direction == -1 && !Collision.SolidTiles(tileX2 + 1, tileX2 + 2, tileY2 - 4, tileY2 - 1)))
							{
								if (!Collision.SolidTiles(tileX2, tileX2, tileY2 - 4, tileY2 - 2))
								{
									npc.velocity.Y = -5f;
									npc.netUpdate = true;
								}
								else
								{
									npc.direction *= -1;
									npc.netUpdate = true;
								}
							}
							else
							{
								npc.direction *= -1;
								npc.netUpdate = true;
							}
						}
						else if (npc.position.Y + (float)npc.height - (float)tileY2 * 16f > 20f && Main.tile[tileX2, tileY2].nactive() && Main.tileSolid[(int)Main.tile[tileX2, tileY2].type] && Main.tile[tileX2, tileY2].slope() == 0)
						{
							if ((npc.direction == 1 && !Collision.SolidTiles(tileX2 - 2, tileX2, tileY2 - 3, tileY2 - 1)) || (npc.direction == -1 && !Collision.SolidTiles(tileX2, tileX2 + 2, tileY2 - 3, tileY2 - 1)))
							{
								npc.velocity.Y = -4.4f;
								npc.netUpdate = true;
							}
							else
							{
								npc.direction *= -1;
								npc.netUpdate = true;
							}
						}
						try
						{
							if (Main.tile[tileX2, tileY2 + 1] == null)
							{
								Main.tile[tileX2, tileY2 + 1] = new Tile();
							}
							if (Main.tile[tileX2 - npc.direction, tileY2 + 1] == null)
							{
								Main.tile[tileX2 - npc.direction, tileY2 + 1] = new Tile();
							}
							if (Main.tile[tileX2, tileY2 + 2] == null)
							{
								Main.tile[tileX2, tileY2 + 2] = new Tile();
							}
							if (Main.tile[tileX2 - npc.direction, tileY2 + 2] == null)
							{
								Main.tile[tileX2 - npc.direction, tileY2 + 2] = new Tile();
							}
							if (Main.tile[tileX2, tileY2 + 3] == null)
							{
								Main.tile[tileX2, tileY2 + 3] = new Tile();
							}
							if (Main.tile[tileX2 - npc.direction, tileY2 + 3] == null)
							{
								Main.tile[tileX2 - npc.direction, tileY2 + 3] = new Tile();
							}
							if (Main.tile[tileX2, tileY2 + 4] == null)
							{
								Main.tile[tileX2, tileY2 + 4] = new Tile();
							}
							if (Main.tile[tileX2 - npc.direction, tileY2 + 4] == null)
							{
								Main.tile[tileX2 - npc.direction, tileY2 + 4] = new Tile();
							}
							if (!critter && npcTileX >= npc.homeTileX - 35 && npcTileX <= npc.homeTileX + 35 && (!Main.tile[tileX2, tileY2 + 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX2, tileY2 + 1].type]) && (!Main.tile[tileX2 - npc.direction, tileY2 + 1].active() || !Main.tileSolid[(int)Main.tile[tileX2 - npc.direction, tileY2 + 1].type]) && (!Main.tile[tileX2, tileY2 + 2].nactive() || !Main.tileSolid[(int)Main.tile[tileX2, tileY2 + 2].type]) && (!Main.tile[tileX2 - npc.direction, tileY2 + 2].active() || !Main.tileSolid[(int)Main.tile[tileX2 - npc.direction, tileY2 + 2].type]) && (!Main.tile[tileX2, tileY2 + 3].nactive() || !Main.tileSolid[(int)Main.tile[tileX2, tileY2 + 3].type]) && (!Main.tile[tileX2 - npc.direction, tileY2 + 3].active() || !Main.tileSolid[(int)Main.tile[tileX2 - npc.direction, tileY2 + 3].type]) && (!Main.tile[tileX2, tileY2 + 4].nactive() || !Main.tileSolid[(int)Main.tile[tileX2, tileY2 + 4].type]) && (!Main.tile[tileX2 - npc.direction, tileY2 + 4].nactive() || !Main.tileSolid[(int)Main.tile[tileX2 - npc.direction, tileY2 + 4].type]))
							{
								npc.direction *= -1;
								npc.velocity.X = npc.velocity.X * -1f;
								npc.netUpdate = true;
							}
						}
						catch
						{
						}
						if ((double)npc.velocity.Y < 0.0)
						{
							ai[2] = npc.position.X;
						}
					}
					if (npc.velocity.Y < 0f && npc.wet)
					{
						npc.velocity.Y = npc.velocity.Y * 1.2f;
					}
					npc.velocity.Y = npc.velocity.Y * 1.2f;
					return;
				}
				if (Main.netMode == 1)
				{
					return;
				}
				if (WorldGen.OpenDoor(tileX2, tileY2 - 2, npc.direction))
				{
					npc.closeDoor = true;
					npc.doorX = tileX2;
					npc.doorY = tileY2 - 2;
					NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 0, (float)tileX2, (float)(tileY2 - 2), (float)npc.direction, 0, 0, 0);
					npc.netUpdate = true;
					ai[1] += 80f;
					return;
				}
				if (WorldGen.OpenDoor(tileX2, tileY2 - 2, -npc.direction))
				{
					npc.closeDoor = true;
					npc.doorX = tileX2;
					npc.doorY = tileY2 - 2;
					NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 0, (float)tileX2, (float)(tileY2 - 2), (float)(-(float)npc.direction), 0, 0, 0);
					npc.netUpdate = true;
					ai[1] += 80f;
					return;
				}
				npc.direction *= -1;
				npc.netUpdate = true;
				return;
			}
		}

		public static void AIEater(NPC npc, ref float[] ai, float moveInterval = 0.022f, float distanceDivider = 4.2f, float bounceScalar = 0.7f, bool fleeAtDay = false, bool ignoreWet = false)
		{
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
			{
				npc.TargetClosest(true);
			}
			float x = Main.player[npc.target].Center.X;
			float distY = Main.player[npc.target].Center.Y;
			Vector2 npcCenter = npc.Center;
			float num = (float)((int)(x / 8f)) * 8f;
			float distDY = (float)((int)(distY / 8f)) * 8f;
			npcCenter.X = (float)((int)(npcCenter.X / 8f)) * 8f;
			npcCenter.Y = (float)((int)(npcCenter.Y / 8f)) * 8f;
			float distX2 = num - npcCenter.X;
			float distY2 = distDY - npcCenter.Y;
			float dist = (float)Math.Sqrt((double)(distX2 * distX2 + distY2 * distY2));
			float SpeedX;
			float SpeedY;
			if (dist == 0f)
			{
				SpeedX = npc.velocity.X;
				SpeedY = npc.velocity.Y;
			}
			else
			{
				float distScalar = distanceDivider / dist;
				SpeedX = distX2 * distScalar;
				SpeedY = distY2 * distScalar;
			}
			ai[0] += 1f;
			if (ai[0] > 0f)
			{
				npc.velocity.Y = npc.velocity.Y + 0.023f;
			}
			else
			{
				npc.velocity.Y = npc.velocity.Y - 0.023f;
			}
			if (ai[0] < -100f || (double)ai[0] > 100.0)
			{
				npc.velocity.X = npc.velocity.X + 0.023f;
			}
			else
			{
				npc.velocity.X = npc.velocity.X - 0.023f;
			}
			if (ai[0] > 200f)
			{
				ai[0] = -200f;
			}
			if (dist < 150f)
			{
				npc.velocity.X = npc.velocity.X + SpeedX * 0.007f;
				npc.velocity.Y = npc.velocity.Y + SpeedY * 0.007f;
			}
			if (Main.player[npc.target].dead)
			{
				SpeedX = (float)npc.direction * distanceDivider / 2f;
				SpeedY = -distanceDivider / 2f;
			}
			if (npc.velocity.X < SpeedX)
			{
				npc.velocity.X = npc.velocity.X + moveInterval;
			}
			else if (npc.velocity.X > SpeedX)
			{
				npc.velocity.X = npc.velocity.X - moveInterval;
			}
			if (npc.velocity.Y < SpeedY)
			{
				npc.velocity.Y = npc.velocity.Y + moveInterval;
			}
			else if (npc.velocity.Y > SpeedY)
			{
				npc.velocity.Y = npc.velocity.Y - moveInterval;
			}
			npc.rotation = (float)Math.Atan2((double)SpeedY, (double)SpeedX) - 1.57f;
			if (npc.collideX)
			{
				npc.netUpdate = true;
				npc.velocity.X = npc.oldVelocity.X * -bounceScalar;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
				{
					npc.velocity.X = 2f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
				{
					npc.velocity.X = -2f;
				}
			}
			if (npc.collideY)
			{
				npc.netUpdate = true;
				npc.velocity.Y = npc.oldVelocity.Y * -bounceScalar;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1.5f)
				{
					npc.velocity.Y = 2f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1.5f)
				{
					npc.velocity.Y = -2f;
				}
			}
			if (!ignoreWet && npc.wet)
			{
				if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y * 0.95f;
				}
				npc.velocity.Y = npc.velocity.Y - 0.3f;
				if (npc.velocity.Y < -2f)
				{
					npc.velocity.Y = -2f;
				}
			}
			if ((fleeAtDay && Main.dayTime) || Main.player[npc.target].dead)
			{
				npc.velocity.Y = npc.velocity.Y - moveInterval * 2f;
				if (npc.timeLeft > 10)
				{
					npc.timeLeft = 10;
				}
			}
			if (((npc.velocity.X <= 0f || npc.oldVelocity.X >= 0f) && (npc.velocity.X >= 0f || npc.oldVelocity.X <= 0f) && (npc.velocity.Y <= 0f || npc.oldVelocity.Y >= 0f) && ((double)npc.velocity.Y >= 0.0 || npc.oldVelocity.Y <= 0f)) || npc.justHit)
			{
				return;
			}
			npc.netUpdate = true;
		}

		public static void AIWheel(NPC npc, ref float[] ai, float moveInterval = 6f, bool rotate = false)
		{
			if (ai[0] == 0f)
			{
				npc.TargetClosest(true);
				npc.directionY = 1;
				ai[0] = 1f;
			}
			if (ai[1] == 0f)
			{
				if (rotate)
				{
					npc.rotation += (float)(npc.direction * npc.directionY) * 0.13f;
				}
				if (npc.collideY)
				{
					ai[0] = 2f;
				}
				if (!npc.collideY && ai[0] == 2f)
				{
					npc.direction = -npc.direction;
					ai[1] = 1f;
					ai[0] = 1f;
				}
				if (npc.collideX)
				{
					npc.directionY = -npc.directionY;
					ai[1] = 1f;
				}
			}
			else
			{
				if (rotate)
				{
					npc.rotation -= (float)(npc.direction * npc.directionY) * 0.13f;
				}
				if (npc.collideX)
				{
					ai[0] = 2f;
				}
				if (!npc.collideX && ai[0] == 2f)
				{
					npc.directionY = -npc.directionY;
					ai[1] = 0f;
					ai[0] = 1f;
				}
				if (npc.collideY)
				{
					npc.direction = -npc.direction;
					ai[1] = 0f;
				}
			}
			npc.velocity.X = moveInterval * (float)npc.direction;
			npc.velocity.Y = moveInterval * (float)npc.directionY;
		}

		public static void AISpider(NPC npc, ref float[] ai, bool ignoreSight = false, float moveInterval = 0.08f, float slowSpeed = 1.5f, float maxSpeed = 3f, float distanceDivider = 2f, float bounceScalar = 0.5f, int transformType = -1)
		{
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
			{
				npc.TargetClosest(true);
			}
			Vector2 npcCenter = npc.Center;
			float x2 = Main.player[npc.target].Center.X;
			float distY = Main.player[npc.target].Center.Y;
			float num = (float)((int)(x2 / 8f)) * 8f;
			float distDY = (float)((int)(distY / 8f)) * 8f;
			npcCenter.X = (float)((int)(npcCenter.X / 8f)) * 8f;
			npcCenter.Y = (float)((int)(npcCenter.Y / 8f)) * 8f;
			float distX2 = num - npcCenter.X;
			float distY2 = distDY - npcCenter.Y;
			float dist = (float)Math.Sqrt((double)(distX2 * distX2 + distY2 * distY2));
			float velX;
			float velY;
			if (dist == 0f)
			{
				velX = npc.velocity.X;
				velY = npc.velocity.Y;
			}
			else
			{
				float speedMult = distanceDivider / dist;
				velX = distX2 * speedMult;
				velY = distY2 * speedMult;
			}
			if (Main.player[npc.target].dead)
			{
				velX = (float)npc.direction * distanceDivider / 2f;
				velY = -distanceDivider / 2f;
			}
			npc.spriteDirection = -1;
			if (!ignoreSight && !Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
			{
				ai[0] += 1f;
				if (ai[0] > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + 0.023f;
				}
				else
				{
					npc.velocity.Y = npc.velocity.Y - 0.023f;
				}
				if (ai[0] < -100f || (double)ai[0] > 100.0)
				{
					npc.velocity.X = npc.velocity.X + 0.023f;
				}
				else
				{
					npc.velocity.X = npc.velocity.X - 0.023f;
				}
				if (ai[0] > 200f)
				{
					ai[0] = -200f;
				}
				npc.velocity.X = npc.velocity.X + velX * 0.007f;
				npc.velocity.Y = npc.velocity.Y + velY * 0.007f;
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
				if (npc.velocity.X > slowSpeed || npc.velocity.X < -slowSpeed)
				{
					npc.velocity.X = npc.velocity.X * 0.9f;
				}
				if (npc.velocity.Y > slowSpeed || npc.velocity.Y < -slowSpeed)
				{
					npc.velocity.Y = npc.velocity.Y * 0.9f;
				}
				if (npc.velocity.X > maxSpeed)
				{
					npc.velocity.X = maxSpeed;
				}
				else if (npc.velocity.X < -maxSpeed)
				{
					npc.velocity.X = -maxSpeed;
				}
				if (npc.velocity.Y > maxSpeed)
				{
					npc.velocity.Y = maxSpeed;
				}
				else if (npc.velocity.Y < -maxSpeed)
				{
					npc.velocity.Y = -maxSpeed;
				}
			}
			else
			{
				if ((double)npc.velocity.X < (double)velX)
				{
					npc.velocity.X = npc.velocity.X + moveInterval;
					if ((double)npc.velocity.X < 0.0 && (double)velX > 0.0)
					{
						npc.velocity.X = npc.velocity.X + moveInterval;
					}
				}
				else if ((double)npc.velocity.X > (double)velX)
				{
					npc.velocity.X = npc.velocity.X - moveInterval;
					if ((double)npc.velocity.X > 0.0 && (double)velX < 0.0)
					{
						npc.velocity.X = npc.velocity.X - moveInterval;
					}
				}
				if ((double)npc.velocity.Y < (double)velY)
				{
					npc.velocity.Y = npc.velocity.Y + moveInterval;
					if ((double)npc.velocity.Y < 0.0 && (double)velY > 0.0)
					{
						npc.velocity.Y = npc.velocity.Y + moveInterval;
					}
				}
				else if ((double)npc.velocity.Y > (double)velY)
				{
					npc.velocity.Y = npc.velocity.Y - moveInterval;
					if ((double)npc.velocity.Y > 0.0 && (double)velY < 0.0)
					{
						npc.velocity.Y = npc.velocity.Y - moveInterval;
					}
				}
				npc.rotation = (float)Math.Atan2((double)velY, (double)velX);
			}
			if (npc.collideX)
			{
				npc.netUpdate = true;
				npc.velocity.X = npc.oldVelocity.X * -bounceScalar;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
				{
					npc.velocity.X = 2f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
				{
					npc.velocity.X = -2f;
				}
			}
			if (npc.collideY)
			{
				npc.netUpdate = true;
				npc.velocity.Y = npc.oldVelocity.Y * -bounceScalar;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1.5f)
				{
					npc.velocity.Y = 2f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1.5f)
				{
					npc.velocity.Y = -2f;
				}
			}
			if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
			{
				npc.netUpdate = true;
			}
			if (Main.netMode != 1 && transformType != -1)
			{
				int centerTileX = (int)npc.Center.X / 16;
				int centerTileY = (int)npc.Center.Y / 16;
				bool wallExists = false;
				for (int x = centerTileX - 1; x <= centerTileX + 1; x++)
				{
					for (int y = centerTileY - 1; y <= centerTileY + 1; y++)
					{
						if (Main.tile[x, y].wall > 0)
						{
							wallExists = true;
							break;
						}
					}
				}
				if (!wallExists)
				{
					npc.Transform(transformType);
				}
			}
		}

		public static void AISkull(NPC npc, ref float[] ai, bool tacklePlayer = true, float maxDistanceAmt = 4f, float maxDistance = 350f, float increment = 0.011f, float closeIncrement = 0.019f)
		{
			float distanceAmt = 1f;
			npc.TargetClosest(true);
			float distX = Main.player[npc.target].Center.X - npc.Center.X;
			float distY = Main.player[npc.target].Center.Y - npc.Center.Y;
			float dist = (float)Math.Sqrt((double)(distX * distX + distY * distY));
			ai[1] += 1f;
			if (ai[1] > 600f)
			{
				if (tacklePlayer)
				{
					increment *= 8f;
					distanceAmt = 4f;
					if (ai[1] > 650f)
					{
						ai[1] = 0f;
					}
				}
				else
				{
					ai[1] = 0f;
				}
			}
			else if (dist < 250f)
			{
				ai[0] += 0.9f;
				if (ai[0] > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + closeIncrement;
				}
				else
				{
					npc.velocity.Y = npc.velocity.Y - closeIncrement;
				}
				if (ai[0] < -100f || ai[0] > 100f)
				{
					npc.velocity.X = npc.velocity.X + closeIncrement;
				}
				else
				{
					npc.velocity.X = npc.velocity.X - closeIncrement;
				}
				if (ai[0] > 200f)
				{
					ai[0] = -200f;
				}
			}
			if (dist > maxDistance)
			{
				distanceAmt = maxDistanceAmt + maxDistanceAmt / 4f;
				increment = 0.3f;
			}
			else if (dist > maxDistance - maxDistance / 7f)
			{
				distanceAmt = maxDistanceAmt - maxDistanceAmt / 4f;
				increment = 0.2f;
			}
			else if (dist > maxDistance - 2f * (maxDistance / 7f))
			{
				distanceAmt = maxDistanceAmt / 2.66f;
				increment = 0.1f;
			}
			dist = distanceAmt / dist;
			distX *= dist;
			distY *= dist;
			if (Main.player[npc.target].dead)
			{
				distX = (float)npc.direction * distanceAmt / 2f;
				distY = -distanceAmt / 2f;
			}
			if (npc.velocity.X < distX)
			{
				npc.velocity.X = npc.velocity.X + increment;
			}
			else if (npc.velocity.X > distX)
			{
				npc.velocity.X = npc.velocity.X - increment;
			}
			if (npc.velocity.Y < distY)
			{
				npc.velocity.Y = npc.velocity.Y + increment;
				return;
			}
			if (npc.velocity.Y > distY)
			{
				npc.velocity.Y = npc.velocity.Y - increment;
			}
		}

		public static void AIFloater(NPC npc, ref float[] ai, bool ignoreWet = false, float moveInterval = 0.2f, float maxSpeedX = 2f, float maxSpeedY = 1.5f, float hoverInterval = 0.04f, float hoverMaxSpeed = 1.5f, int hoverHeight = 3)
		{
			bool flyUpward = false;
			if (npc.justHit)
			{
				ai[2] = 0f;
			}
			if (ai[2] >= 0f)
			{
				int tileDist = 16;
				bool inRangeX = false;
				bool inRangeY = false;
				if (npc.position.X > ai[0] - (float)tileDist && npc.position.X < ai[0] + (float)tileDist)
				{
					inRangeX = true;
				}
				else if ((npc.velocity.X < 0f && npc.direction > 0) || (npc.velocity.X > 0f && npc.direction < 0))
				{
					inRangeX = true;
				}
				tileDist += 24;
				if (npc.position.Y > ai[1] - (float)tileDist && npc.position.Y < ai[1] + (float)tileDist)
				{
					inRangeY = true;
				}
				if (inRangeX && inRangeY)
				{
					ai[2] += 1f;
					if (ai[2] >= 30f && tileDist == 16)
					{
						flyUpward = true;
					}
					if (ai[2] >= 60f)
					{
						ai[2] = -200f;
						npc.direction *= -1;
						npc.velocity.X = npc.velocity.X * -1f;
						npc.collideX = false;
					}
				}
				else
				{
					ai[0] = npc.position.X;
					ai[1] = npc.position.Y;
					ai[2] = 0f;
				}
				npc.TargetClosest(true);
			}
			else
			{
				ai[2] += 1f;
				if (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) > npc.position.X + (float)(npc.width / 2))
				{
					npc.direction = -1;
				}
				else
				{
					npc.direction = 1;
				}
			}
			int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
			int tileY = (int)((npc.position.Y + (float)npc.height) / 16f);
			bool tileBelowEmpty = true;
			for (int tY = tileY; tY < tileY + hoverHeight; tY++)
			{
				if (Main.tile[tileX, tY] == null)
				{
					Main.tile[tileX, tY] = new Tile();
				}
				if ((Main.tile[tileX, tY].nactive() && Main.tileSolid[(int)Main.tile[tileX, tY].type]) || Main.tile[tileX, tY].liquid > 0)
				{
					tileBelowEmpty = false;
					break;
				}
			}
			if (flyUpward)
			{
				tileBelowEmpty = true;
			}
			if (tileBelowEmpty)
			{
				npc.velocity.Y = npc.velocity.Y + moveInterval;
				if (npc.velocity.Y > maxSpeedY)
				{
					npc.velocity.Y = maxSpeedY;
				}
			}
			else
			{
				if (npc.directionY < 0 && npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y - moveInterval;
				}
				if (npc.velocity.Y < -maxSpeedY)
				{
					npc.velocity.Y = -maxSpeedY;
				}
			}
			if (!ignoreWet && npc.wet)
			{
				npc.velocity.Y = npc.velocity.Y - moveInterval;
				if (npc.velocity.Y < -maxSpeedY * 0.75f)
				{
					npc.velocity.Y = -maxSpeedY * 0.75f;
				}
			}
			if (npc.collideX)
			{
				npc.velocity.X = npc.oldVelocity.X * -0.4f;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 1f)
				{
					npc.velocity.X = 1f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -1f)
				{
					npc.velocity.X = -1f;
				}
			}
			if (npc.collideY)
			{
				npc.velocity.Y = npc.oldVelocity.Y * -0.25f;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
				{
					npc.velocity.Y = 1f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
				{
					npc.velocity.Y = -1f;
				}
			}
			if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
			{
				npc.velocity.X = npc.velocity.X - moveInterval * 0.5f;
				if (npc.velocity.X > maxSpeedX)
				{
					npc.velocity.X = npc.velocity.X - 0.1f;
				}
				else if (npc.velocity.X > 0f)
				{
					npc.velocity.X = npc.velocity.X + 0.05f;
				}
				if (npc.velocity.X < -maxSpeedX)
				{
					npc.velocity.X = -maxSpeedX;
				}
			}
			else if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
			{
				npc.velocity.X = npc.velocity.X + moveInterval * 0.5f;
				if (npc.velocity.X < -maxSpeedX)
				{
					npc.velocity.X = npc.velocity.X + 0.1f;
				}
				else if (npc.velocity.X < 0f)
				{
					npc.velocity.X = npc.velocity.X - 0.05f;
				}
				if (npc.velocity.X > maxSpeedX)
				{
					npc.velocity.X = maxSpeedX;
				}
			}
			if (npc.directionY == -1 && (double)npc.velocity.Y > (double)(-(double)hoverMaxSpeed))
			{
				npc.velocity.Y = npc.velocity.Y - hoverInterval;
				if ((double)npc.velocity.Y > (double)hoverMaxSpeed)
				{
					npc.velocity.Y = npc.velocity.Y - 0.05f;
				}
				else if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + (hoverInterval - 0.01f);
				}
				if ((double)npc.velocity.Y < (double)(-(double)hoverMaxSpeed))
				{
					npc.velocity.Y = -hoverMaxSpeed;
					return;
				}
			}
			else if (npc.directionY == 1 && (double)npc.velocity.Y < (double)hoverMaxSpeed)
			{
				npc.velocity.Y = npc.velocity.Y + hoverInterval;
				if ((double)npc.velocity.Y < (double)(-(double)hoverMaxSpeed))
				{
					npc.velocity.Y = npc.velocity.Y + 0.05f;
				}
				else if (npc.velocity.Y < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - (hoverInterval - 0.01f);
				}
				if ((double)npc.velocity.Y > (double)hoverMaxSpeed)
				{
					npc.velocity.Y = hoverMaxSpeed;
				}
			}
		}

		public static void AIFlier(NPC npc, ref float[] ai, bool sporadic = true, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float maxSpeedX = 4f, float maxSpeedY = 1.5f, bool canBeBored = true, int timeUntilBoredom = 300)
		{
			if (npc.collideX)
			{
				npc.velocity.X = npc.oldVelocity.X * -0.5f;
				float max = maxSpeedX * 0.5f;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < max)
				{
					npc.velocity.X = max;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -max)
				{
					npc.velocity.X = -max;
				}
			}
			if (npc.collideY)
			{
				npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
				float max2 = maxSpeedY * 0.66f;
				if (npc.velocity.Y > 0f && npc.velocity.Y < max2)
				{
					npc.velocity.Y = max2;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -max2)
				{
					npc.velocity.Y = -max2;
				}
			}
			npc.TargetClosest(true);
			Action move = delegate()
			{
				if (npc.direction == -1 && npc.velocity.X > -maxSpeedX)
				{
					NPC npc4 = npc;
					npc4.velocity.X = npc4.velocity.X - moveIntervalX;
					if (npc.velocity.X > maxSpeedX)
					{
						NPC npc5 = npc;
						npc5.velocity.X = npc5.velocity.X - moveIntervalX;
					}
					else if (npc.velocity.X > 0f)
					{
						NPC npc6 = npc;
						npc6.velocity.X = npc6.velocity.X + moveIntervalX * 0.5f;
					}
					if (npc.velocity.X < -maxSpeedX)
					{
						npc.velocity.X = -maxSpeedX;
					}
				}
				else if (npc.direction == 1 && npc.velocity.X < maxSpeedX)
				{
					NPC npc7 = npc;
					npc7.velocity.X = npc7.velocity.X + moveIntervalX;
					if (npc.velocity.X < -maxSpeedX)
					{
						NPC npc8 = npc;
						npc8.velocity.X = npc8.velocity.X + moveIntervalX;
					}
					else if (npc.velocity.X < 0f)
					{
						NPC npc9 = npc;
						npc9.velocity.X = npc9.velocity.X - moveIntervalX * 0.5f;
					}
					if (npc.velocity.X > maxSpeedX)
					{
						npc.velocity.X = maxSpeedX;
					}
				}
				if (npc.directionY == -1 && (double)npc.velocity.Y > (double)(-(double)maxSpeedY))
				{
					NPC npc10 = npc;
					npc10.velocity.Y = npc10.velocity.Y - moveIntervalY;
					if ((double)npc.velocity.Y > (double)maxSpeedY)
					{
						NPC npc11 = npc;
						npc11.velocity.Y = npc11.velocity.Y - moveIntervalY;
					}
					else if (npc.velocity.Y > 0f)
					{
						NPC npc12 = npc;
						npc12.velocity.Y = npc12.velocity.Y + moveIntervalY * 0.5f;
					}
					if ((double)npc.velocity.Y < (double)(-(double)maxSpeedY))
					{
						npc.velocity.Y = -maxSpeedY;
						return;
					}
				}
				else if (npc.directionY == 1 && (double)npc.velocity.Y < (double)maxSpeedY)
				{
					NPC npc13 = npc;
					npc13.velocity.Y = npc13.velocity.Y + moveIntervalY;
					if ((double)npc.velocity.Y < (double)(-(double)maxSpeedY))
					{
						NPC npc14 = npc;
						npc14.velocity.Y = npc14.velocity.Y + moveIntervalY;
					}
					else if (npc.velocity.Y < 0f)
					{
						NPC npc15 = npc;
						npc15.velocity.Y = npc15.velocity.Y - moveIntervalY * 0.5f;
					}
					if ((double)npc.velocity.Y > (double)maxSpeedY)
					{
						npc.velocity.Y = maxSpeedY;
					}
				}
			};
			if (canBeBored)
			{
				ai[0] += 1f;
			}
			if (canBeBored && ai[0] > (float)timeUntilBoredom)
			{
				if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
				{
					ai[0] = 0f;
				}
				if (ai[0] > (float)(timeUntilBoredom * 2))
				{
					ai[0] = 0f;
				}
				npc.direction = ((Main.player[npc.target].Center.X < npc.Center.X) ? 1 : -1);
				npc.directionY = ((Main.player[npc.target].Center.Y < npc.Center.Y) ? 1 : -1);
				move();
				return;
			}
			move();
			if (sporadic)
			{
				if (npc.wet)
				{
					if (npc.velocity.Y > 0f)
					{
						NPC npc2 = npc;
						npc2.velocity.Y = npc2.velocity.Y * 0.95f;
					}
					NPC npc3 = npc;
					npc3.velocity.Y = npc3.velocity.Y - 0.5f;
					if (npc.velocity.Y < -maxSpeedX)
					{
						npc.velocity.Y = -maxSpeedX;
					}
					npc.TargetClosest(true);
				}
				move();
			}
		}

		public static void AIPlant(NPC npc, ref float[] ai, bool checkTilePoint = true, Vector2 endPoint = default(Vector2), bool isTilePos = true, float vineLength = 150f, float vineLengthLong = 200f, int vineTimeExtend = 300, int vineTimeMax = 450, float moveInterval = 0.035f, float speedMax = 2f, Vector2 targetOffset = default(Vector2))
		{
			if (endPoint != default(Vector2))
			{
				ai[0] = endPoint.X;
				ai[1] = endPoint.Y;
			}
			if (checkTilePoint)
			{
				Vector2 vector = isTilePos ? new Vector2(ai[0], ai[1]) : new Vector2(ai[0] / 16f, ai[1] / 16f);
				int tx = (int)vector.X;
				int ty = (int)vector.Y;
				if (Main.tile[tx, ty] == null)
				{
					Main.tile[tx, ty] = new Tile();
				}
				if (!Main.tile[tx, ty].nactive() || !Main.tileSolid[(int)Main.tile[tx, ty].type] || (Main.tileSolid[(int)Main.tile[tx, ty].type] && Main.tileSolidTop[(int)Main.tile[tx, ty].type]))
				{
					if (npc.DeathSound != null)
					{
						Main.PlaySound(npc.DeathSound, (int)npc.Center.X, (int)npc.Center.Y);
					}
					npc.life = -1;
					npc.HitEffect(0, 10.0);
					npc.active = false;
					return;
				}
			}
			npc.TargetClosest(true);
			ai[2] += 1f;
			if (ai[2] > (float)vineTimeExtend)
			{
				vineLength = vineLengthLong;
				if (ai[2] > (float)vineTimeMax)
				{
					ai[2] = 0f;
				}
			}
			Vector2 endPointCenter = isTilePos ? new Vector2(ai[0] * 16f + 8f, ai[1] * 16f + 8f) : new Vector2(ai[0], ai[1]);
			Vector2 vector2 = Main.player[npc.target].Center + targetOffset;
			float distPlayerX = vector2.X - (float)(npc.width / 2) - endPointCenter.X;
			float distPlayerY = vector2.Y - (float)(npc.height / 2) - endPointCenter.Y;
			float distPlayer = (float)Math.Sqrt((double)(distPlayerX * distPlayerX + distPlayerY * distPlayerY));
			if (distPlayer > vineLength)
			{
				distPlayer = vineLength / distPlayer;
				distPlayerX *= distPlayer;
				distPlayerY *= distPlayer;
			}
			if (npc.position.X < endPointCenter.X + distPlayerX)
			{
				npc.velocity.X = npc.velocity.X + moveInterval;
				if (npc.velocity.X < 0f && distPlayerX > 0f)
				{
					npc.velocity.X = npc.velocity.X + moveInterval * 1.5f;
				}
			}
			else if (npc.position.X > endPointCenter.X + distPlayerX)
			{
				npc.velocity.X = npc.velocity.X - moveInterval;
				if (npc.velocity.X > 0f && distPlayerX < 0f)
				{
					npc.velocity.X = npc.velocity.X - moveInterval * 1.5f;
				}
			}
			if (npc.position.Y < endPointCenter.Y + distPlayerY)
			{
				npc.velocity.Y = npc.velocity.Y + moveInterval;
				if (npc.velocity.Y < 0f && distPlayerY > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + moveInterval * 1.5f;
				}
			}
			else if (npc.position.Y > endPointCenter.Y + distPlayerY)
			{
				npc.velocity.Y = npc.velocity.Y - moveInterval;
				if (npc.velocity.Y > 0f && distPlayerY < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - moveInterval * 1.5f;
				}
			}
			npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -speedMax, speedMax);
			npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -speedMax, speedMax);
			if (distPlayerX > 0f)
			{
				npc.spriteDirection = 1;
				npc.rotation = (float)Math.Atan2((double)distPlayerY, (double)distPlayerX);
			}
			else if (distPlayerX < 0f)
			{
				npc.spriteDirection = -1;
				npc.rotation = (float)Math.Atan2((double)distPlayerY, (double)distPlayerX) + 3.14f;
			}
			if (npc.collideX)
			{
				npc.netUpdate = true;
				npc.velocity.X = npc.oldVelocity.X * -0.7f;
				npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -speedMax, speedMax);
			}
			if (npc.collideY)
			{
				npc.netUpdate = true;
				npc.velocity.Y = npc.oldVelocity.Y * -0.7f;
				npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -speedMax, speedMax);
			}
		}

		public static void AIWorm(NPC npc, int[] wormTypes, int wormLength = 3, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
		{
			bool diggingDummy = false;
			BaseAI.AIWorm(npc, ref diggingDummy, wormTypes, wormLength, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}

		public static void AIWorm(NPC npc, ref bool isDigging, int[] wormTypes, int wormLength = 3, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
		{
			int[] wtypes = new int[(wormTypes.Length == 1) ? 1 : wormLength];
			wtypes[0] = wormTypes[0];
			if (wormTypes.Length > 1)
			{
				wtypes[wtypes.Length - 1] = wormTypes[2];
				for (int i = 1; i < wtypes.Length - 1; i++)
				{
					wtypes[i] = wormTypes[1];
				}
			}
			BaseAI.AIWorm(npc, ref isDigging, wtypes, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}

		public static void AIWorm(NPC npc, int[] wormTypes, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
		{
			bool diggingDummy = false;
			BaseAI.AIWorm(npc, ref diggingDummy, wormTypes, partDistanceAddon, maxSpeed, gravityResist, fly, split, ignoreTiles, spawnTileDust, soundEffects, rotateAverage, onChangeType);
		}

		public static void AIWorm(NPC npc, ref bool isDigging, int[] wormTypes, float partDistanceAddon = 0f, float maxSpeed = 8f, float gravityResist = 0.07f, bool fly = false, bool split = false, bool ignoreTiles = false, bool spawnTileDust = true, bool soundEffects = true, bool rotateAverage = false, Action<NPC, int, bool> onChangeType = null)
		{
			bool singlePiece = wormTypes.Length == 1;
			bool isHead = npc.type == wormTypes[0];
			bool isTail = npc.type == wormTypes[wormTypes.Length - 1];
			bool isBody = !isHead && !isTail;
			int wormLength = wormTypes.Length;
			if (split)
			{
				npc.realLife = -1;
			}
			else if (npc.ai[3] > 0f)
			{
				npc.realLife = (int)npc.ai[3];
			}
			if (npc.ai[0] == -1f)
			{
				npc.ai[0] = (float)npc.whoAmI;
			}
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
			{
				npc.TargetClosest(true);
			}
			if (isHead)
			{
				if ((npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead) && npc.timeLeft > 300)
				{
					npc.timeLeft = 300;
				}
			}
			else
			{
				npc.timeLeft = 50;
			}
			if (Main.netMode != 1)
			{
				if (!singlePiece)
				{
					if (fly && isHead && npc.ai[0] == 0f)
					{
						npc.ai[3] = (float)npc.whoAmI;
						npc.realLife = npc.whoAmI;
						int npcID = npc.whoAmI;
						for (int i = 1; i < wormLength - 1; i++)
						{
							int npcType = wormTypes[i];
							float ai0 = 0f;
							float ai = (float)npcID;
							float ai2 = 0f;
							float ai3 = npc.ai[3];
							int newnpcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, npcType, npc.whoAmI, ai0, ai, ai2, ai3, 255);
							Main.npc[npcID].ai[0] = (float)newnpcID;
							Main.npc[npcID].netUpdate = true;
							npcID = newnpcID;
						}
						npc.netUpdate = true;
					}
					else if ((isHead || isBody) && npc.ai[0] == 0f)
					{
						if (isHead)
						{
							if (!split)
							{
								npc.ai[3] = (float)npc.whoAmI;
								npc.realLife = npc.whoAmI;
							}
							npc.ai[2] = (float)(wormLength - 1);
						}
						float ai4 = 0f;
						float ai5 = (float)npc.whoAmI;
						float ai6 = npc.ai[2] - 1f;
						float ai7 = npc.ai[3];
						if (split)
						{
							npc.ai[3] = 0f;
						}
						if (isHead)
						{
							npc.ai[0] = (float)NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, wormTypes[1], npc.whoAmI, ai4, ai5, ai6, ai7, 255);
						}
						else if (isBody && npc.ai[2] > 0f)
						{
							npc.ai[0] = (float)NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, wormTypes[wormLength - (int)npc.ai[2]], npc.whoAmI, ai4, ai5, ai6, ai7, 255);
						}
						else
						{
							npc.ai[0] = (float)NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, wormTypes[wormTypes.Length - 1], npc.whoAmI, ai4, ai5, ai6, ai7, 255);
						}
						Main.npc[(int)npc.ai[0]].netUpdate = true;
						npc.netUpdate = true;
					}
				}
				if (!singlePiece && split)
				{
					if (!Main.npc[(int)npc.ai[1]].active && !Main.npc[(int)npc.ai[0]].active)
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
					if (npc.type == wormTypes[0] && !Main.npc[(int)npc.ai[0]].active)
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
					if (npc.type == wormTypes[wormTypes.Length - 1] && !Main.npc[(int)npc.ai[1]].active)
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
					if (isBody && !Main.npc[(int)npc.ai[1]].active)
					{
						int oldType = npc.type;
						npc.type = wormTypes[0];
						int npcID2 = npc.whoAmI;
						float lifePercent = (float)npc.life / (float)npc.lifeMax;
						float lastPiece = npc.ai[0];
						npc.SetDefaults(npc.type, -1f);
						npc.life = (int)((float)npc.lifeMax * lifePercent);
						npc.ai[0] = lastPiece;
						npc.TargetClosest(true);
						npc.netUpdate = true;
						npc.whoAmI = npcID2;
						if (onChangeType != null)
						{
							onChangeType(npc, oldType, true);
						}
					}
					else if (isBody && !Main.npc[(int)npc.ai[0]].active)
					{
						int oldType2 = npc.type;
						npc.type = wormTypes[wormTypes.Length - 1];
						int npcID3 = npc.whoAmI;
						float lifePercent2 = (float)npc.life / (float)npc.lifeMax;
						float lastPiece2 = npc.ai[1];
						npc.SetDefaults(npc.type, -1f);
						npc.life = (int)((float)npc.lifeMax * lifePercent2);
						npc.ai[1] = lastPiece2;
						npc.TargetClosest(true);
						npc.netUpdate = true;
						npc.whoAmI = npcID3;
						if (onChangeType != null)
						{
							onChangeType(npc, oldType2, false);
						}
					}
				}
				else if (!singlePiece)
				{
					if (npc.type != wormTypes[0] && (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].aiStyle != npc.aiStyle))
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
					if (npc.type != wormTypes[wormTypes.Length - 1] && (!Main.npc[(int)npc.ai[0]].active || Main.npc[(int)npc.ai[0]].aiStyle != npc.aiStyle))
					{
						npc.life = 0;
						npc.HitEffect(0, 10.0);
						npc.active = false;
					}
				}
				if (!npc.active && Main.netMode == 2)
				{
					NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1f, 0f, 0f, -1, 0, 0);
				}
			}
			int tileX = (int)(npc.position.X / 16f) - 1;
			int tileCenterX = (int)(npc.Center.X / 16f) + 2;
			int tileY = (int)(npc.position.Y / 16f) - 1;
			int tileCenterY = (int)(npc.Center.Y / 16f) + 2;
			if (tileX < 0)
			{
				tileX = 0;
			}
			if (tileCenterX > Main.maxTilesX)
			{
				tileCenterX = Main.maxTilesX;
			}
			if (tileY < 0)
			{
				tileY = 0;
			}
			if (tileCenterY > Main.maxTilesY)
			{
				tileCenterY = Main.maxTilesY;
			}
			bool canMove = false;
			if (fly || ignoreTiles)
			{
				canMove = true;
			}
			if (!canMove || spawnTileDust)
			{
				for (int tX = tileX; tX < tileCenterX; tX++)
				{
					for (int tY = tileY; tY < tileCenterY; tY++)
					{
						Tile checkTile = BaseWorldGen.GetTileSafely(tX, tY);
						if (checkTile != null && ((checkTile.nactive() && (Main.tileSolid[(int)checkTile.type] || (Main.tileSolidTop[(int)checkTile.type] && checkTile.frameY == 0))) || checkTile.liquid > 64))
						{
							Vector2 tPos;
							tPos.X = (float)(tX * 16);
							tPos.Y = (float)(tY * 16);
							if (npc.position.X + (float)npc.width > tPos.X && npc.position.X < tPos.X + 16f && npc.position.Y + (float)npc.height > tPos.Y && npc.position.Y < tPos.Y + 16f)
							{
								canMove = true;
								if (spawnTileDust && Main.rand.Next(100) == 0 && checkTile.nactive())
								{
									WorldGen.KillTile(tX, tY, true, true, false);
								}
							}
						}
					}
				}
			}
			if (!canMove && npc.type == wormTypes[0])
			{
				Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
				int playerCheckDistance = 1000;
				bool canMove2 = true;
				for (int m3 = 0; m3 < 255; m3++)
				{
					if (Main.player[m3].active)
					{
						Rectangle rectangle2 = new Rectangle((int)Main.player[m3].position.X - playerCheckDistance, (int)Main.player[m3].position.Y - playerCheckDistance, playerCheckDistance * 2, playerCheckDistance * 2);
						if (rectangle.Intersects(rectangle2))
						{
							canMove2 = false;
							break;
						}
					}
				}
				if (canMove2)
				{
					canMove = true;
				}
			}
			if (fly)
			{
				if (npc.velocity.X < 0f)
				{
					npc.spriteDirection = 1;
				}
				else if (npc.velocity.X > 0f)
				{
					npc.spriteDirection = -1;
				}
			}
			Vector2 npcCenter = npc.Center;
			float playerCenterX = Main.player[npc.target].Center.X;
			float playerCenterY = Main.player[npc.target].Center.Y;
			playerCenterX = (float)((int)(playerCenterX / 16f) * 16);
			playerCenterY = (float)((int)(playerCenterY / 16f) * 16);
			npcCenter.X = (float)((int)(npcCenter.X / 16f) * 16);
			npcCenter.Y = (float)((int)(npcCenter.Y / 16f) * 16);
			playerCenterX -= npcCenter.X;
			playerCenterY -= npcCenter.Y;
			float dist = (float)Math.Sqrt((double)(playerCenterX * playerCenterX + playerCenterY * playerCenterY));
			isDigging = canMove;
			if (npc.ai[1] > 0f && npc.ai[1] < 200f)
			{
				try
				{
					npcCenter = npc.Center;
					playerCenterX = Main.npc[(int)npc.ai[1]].Center.X - npcCenter.X;
					playerCenterY = Main.npc[(int)npc.ai[1]].Center.Y - npcCenter.Y;
				}
				catch
				{
				}
				if (!rotateAverage || npc.type == wormTypes[0])
				{
					npc.rotation = (float)Math.Atan2((double)playerCenterY, (double)playerCenterX) + 1.57f;
				}
				else
				{
					NPC frontNPC = Main.npc[(int)npc.ai[1]];
					Vector2 rotVec = BaseUtility.RotateVector(frontNPC.Center, frontNPC.Center + new Vector2(0f, 30f), frontNPC.rotation);
					npc.rotation = BaseUtility.RotationTo(npc.Center, rotVec) + 1.57f;
				}
				dist = (float)Math.Sqrt((double)(playerCenterX * playerCenterX + playerCenterY * playerCenterY));
				dist = (dist - (float)npc.width - partDistanceAddon) / dist;
				playerCenterX *= dist;
				playerCenterY *= dist;
				npc.velocity = default(Vector2);
				npc.position.X = npc.position.X + playerCenterX;
				npc.position.Y = npc.position.Y + playerCenterY;
				if (fly)
				{
					if (playerCenterX < 0f)
					{
						npc.spriteDirection = 1;
						return;
					}
					if (playerCenterX > 0f)
					{
						npc.spriteDirection = -1;
						return;
					}
				}
			}
			else
			{
				if (!canMove)
				{
					npc.TargetClosest(true);
					npc.velocity.Y = npc.velocity.Y + 0.11f;
					if (npc.velocity.Y > maxSpeed)
					{
						npc.velocity.Y = maxSpeed;
					}
					if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)maxSpeed * 0.4)
					{
						if (npc.velocity.X < 0f)
						{
							npc.velocity.X = npc.velocity.X - gravityResist * 1.1f;
						}
						else
						{
							npc.velocity.X = npc.velocity.X + gravityResist * 1.1f;
						}
					}
					else if (npc.velocity.Y == maxSpeed)
					{
						if (npc.velocity.X < playerCenterX)
						{
							npc.velocity.X = npc.velocity.X + gravityResist;
						}
						else if (npc.velocity.X > playerCenterX)
						{
							npc.velocity.X = npc.velocity.X - gravityResist;
						}
					}
					else if (npc.velocity.Y > 4f)
					{
						if (npc.velocity.X < 0f)
						{
							npc.velocity.X = npc.velocity.X + gravityResist * 0.9f;
						}
						else
						{
							npc.velocity.X = npc.velocity.X - gravityResist * 0.9f;
						}
					}
				}
				else
				{
					if (soundEffects && npc.soundDelay == 0)
					{
						float distSoundDelay = dist / 40f;
						if (distSoundDelay < 10f)
						{
							distSoundDelay = 10f;
						}
						if (distSoundDelay > 20f)
						{
							distSoundDelay = 20f;
						}
						npc.soundDelay = (int)distSoundDelay;
						Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1, 1f, 0f);
					}
					dist = (float)Math.Sqrt((double)(playerCenterX * playerCenterX + playerCenterY * playerCenterY));
					float absPlayerCenterX = Math.Abs(playerCenterX);
					float absPlayerCenterY = Math.Abs(playerCenterY);
					float newSpeed = maxSpeed / dist;
					playerCenterX *= newSpeed;
					playerCenterY *= newSpeed;
					bool dontFall = false;
					if (fly)
					{
						if (((npc.velocity.X > 0f && playerCenterX < 0f) || (npc.velocity.X < 0f && playerCenterX > 0f) || (npc.velocity.Y > 0f && playerCenterY < 0f) || (npc.velocity.Y < 0f && playerCenterY > 0f)) && Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) > gravityResist / 2f && dist < 300f)
						{
							dontFall = true;
							if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < maxSpeed)
							{
								npc.velocity *= 1.1f;
							}
						}
						if (npc.position.Y > Main.player[npc.target].position.Y || (double)(Main.player[npc.target].position.Y / 16f) > Main.worldSurface || Main.player[npc.target].dead)
						{
							dontFall = true;
							if (Math.Abs(npc.velocity.X) < maxSpeed / 2f)
							{
								if (npc.velocity.X == 0f)
								{
									npc.velocity.X = npc.velocity.X - (float)npc.direction;
								}
								npc.velocity.X = npc.velocity.X * 1.1f;
							}
							else if (npc.velocity.Y > -maxSpeed)
							{
								npc.velocity.Y = npc.velocity.Y - gravityResist;
							}
						}
					}
					if (!dontFall)
					{
						if ((npc.velocity.X > 0f && playerCenterX > 0f) || (npc.velocity.X < 0f && playerCenterX < 0f) || (npc.velocity.Y > 0f && playerCenterY > 0f) || (npc.velocity.Y < 0f && playerCenterY < 0f))
						{
							if (npc.velocity.X < playerCenterX)
							{
								npc.velocity.X = npc.velocity.X + gravityResist;
							}
							else if (npc.velocity.X > playerCenterX)
							{
								npc.velocity.X = npc.velocity.X - gravityResist;
							}
							if (npc.velocity.Y < playerCenterY)
							{
								npc.velocity.Y = npc.velocity.Y + gravityResist;
							}
							else if (npc.velocity.Y > playerCenterY)
							{
								npc.velocity.Y = npc.velocity.Y - gravityResist;
							}
							if ((double)Math.Abs(playerCenterY) < (double)maxSpeed * 0.2 && ((npc.velocity.X > 0f && playerCenterX < 0f) || (npc.velocity.X < 0f && playerCenterX > 0f)))
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y = npc.velocity.Y + gravityResist * 2f;
								}
								else
								{
									npc.velocity.Y = npc.velocity.Y - gravityResist * 2f;
								}
							}
							if ((double)Math.Abs(playerCenterX) < (double)maxSpeed * 0.2 && ((npc.velocity.Y > 0f && playerCenterY < 0f) || (npc.velocity.Y < 0f && playerCenterY > 0f)))
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X = npc.velocity.X + gravityResist * 2f;
								}
								else
								{
									npc.velocity.X = npc.velocity.X - gravityResist * 2f;
								}
							}
						}
						else if (absPlayerCenterX > absPlayerCenterY)
						{
							if (npc.velocity.X < playerCenterX)
							{
								npc.velocity.X = npc.velocity.X + gravityResist * 1.1f;
							}
							else if (npc.velocity.X > playerCenterX)
							{
								npc.velocity.X = npc.velocity.X - gravityResist * 1.1f;
							}
							if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)maxSpeed * 0.5)
							{
								if (npc.velocity.Y > 0f)
								{
									npc.velocity.Y = npc.velocity.Y + gravityResist;
								}
								else
								{
									npc.velocity.Y = npc.velocity.Y - gravityResist;
								}
							}
						}
						else
						{
							if (npc.velocity.Y < playerCenterY)
							{
								npc.velocity.Y = npc.velocity.Y + gravityResist * 1.1f;
							}
							else if (npc.velocity.Y > playerCenterY)
							{
								npc.velocity.Y = npc.velocity.Y - gravityResist * 1.1f;
							}
							if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)maxSpeed * 0.5)
							{
								if (npc.velocity.X > 0f)
								{
									npc.velocity.X = npc.velocity.X + gravityResist;
								}
								else
								{
									npc.velocity.X = npc.velocity.X - gravityResist;
								}
							}
						}
					}
				}
				if (!rotateAverage || npc.type == wormTypes[0])
				{
					npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
				}
				else
				{
					NPC frontNPC2 = Main.npc[(int)npc.ai[1]];
					Vector2 rotVec2 = BaseUtility.RotateVector(frontNPC2.Center, frontNPC2.Center + new Vector2(0f, 30f), frontNPC2.rotation);
					npc.rotation = BaseUtility.RotationTo(npc.Center, rotVec2) + 1.57f;
				}
				if (npc.type == wormTypes[0])
				{
					if (canMove)
					{
						if (npc.localAI[0] != 1f)
						{
							npc.netUpdate = true;
						}
						npc.localAI[0] = 1f;
					}
					else
					{
						if (npc.localAI[0] != 0f)
						{
							npc.netUpdate = true;
						}
						npc.localAI[0] = 0f;
					}
					if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
					{
						npc.netUpdate = true;
						return;
					}
				}
			}
		}

		public static void AITeleporter(NPC npc, ref float[] ai, bool checkGround = true, bool immobile = true, int distFromPlayer = 20, int teleportInterval = 650, int attackInterval = 100, int stopAttackInterval = 500, bool delayOnHit = true, Action<bool> TeleportEffects = null, Func<int, int, bool> CanTeleportTo = null, Action Attack = null)
		{
			npc.TargetClosest(true);
			if (immobile)
			{
				npc.velocity.X = npc.velocity.X * 0.93f;
				if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
				{
					npc.velocity.X = 0f;
				}
			}
			if (ai[0] == 0f)
			{
				ai[0] = (float)Math.Max(0, Math.Max(teleportInterval, teleportInterval - 150));
			}
			if (ai[2] != 0f && ai[3] != 0f)
			{
				if (TeleportEffects != null)
				{
					TeleportEffects(true);
				}
				npc.position.X = ai[2] * 16f - (float)(npc.width / 2) + 8f;
				npc.position.Y = ai[3] * 16f - (float)npc.height;
				npc.velocity.X = 0f;
				npc.velocity.Y = 0f;
				ai[2] = 0f;
				ai[3] = 0f;
				if (TeleportEffects != null)
				{
					TeleportEffects(false);
				}
			}
			if (npc.justHit)
			{
				ai[0] = 0f;
			}
			ai[0] += 1f;
			if (attackInterval != -1 && ai[0] < (float)stopAttackInterval && ai[0] % (float)attackInterval == 0f)
			{
				ai[1] = 30f;
				npc.netUpdate = true;
			}
			else if (ai[0] >= (float)teleportInterval && Main.netMode != 1)
			{
				ai[0] = 1f;
				int playerTileX = (int)Main.player[npc.target].position.X / 16;
				int playerTileY = (int)Main.player[npc.target].position.Y / 16;
				int tileX = (int)npc.position.X / 16;
				int tileY = (int)npc.position.Y / 16;
				int teleportCheckCount = 0;
				bool hasTeleportPoint = false;
				if (Vector2.Distance(npc.Center, Main.player[npc.target].Center) > 2000f)
				{
					teleportCheckCount = 100;
					hasTeleportPoint = true;
				}
				while (!hasTeleportPoint && teleportCheckCount < 100)
				{
					teleportCheckCount++;
					int tpTileX = Main.rand.Next(playerTileX - distFromPlayer, playerTileX + distFromPlayer);
					for (int tpY = Main.rand.Next(playerTileY - distFromPlayer, playerTileY + distFromPlayer); tpY < playerTileY + distFromPlayer; tpY++)
					{
						if ((tpY < playerTileY - 4 || tpY > playerTileY + 4 || tpTileX < playerTileX - 4 || tpTileX > playerTileX + 4) && (tpY < tileY - 1 || tpY > tileY + 1 || tpTileX < tileX - 1 || tpTileX > tileX + 1) && (!checkGround || Main.tile[tpTileX, tpY].nactive()) && ((CanTeleportTo != null && CanTeleportTo(tpTileX, tpY)) || (!Main.tile[tpTileX, tpY - 1].lava() && (!checkGround || Main.tileSolid[(int)Main.tile[tpTileX, tpY].type]) && !Collision.SolidTiles(tpTileX - 1, tpTileX + 1, tpY - 4, tpY - 1))))
						{
							if (attackInterval != -1)
							{
								ai[1] = 20f;
							}
							ai[2] = (float)tpTileX;
							ai[3] = (float)tpY;
							hasTeleportPoint = true;
							break;
						}
					}
				}
				npc.netUpdate = true;
			}
			if (Attack != null && attackInterval != -1 && ai[1] > 0f)
			{
				ai[1] -= 1f;
				if (ai[1] == 25f)
				{
					Attack();
				}
			}
		}

		public static void AIFish(NPC npc, ref float[] ai, bool hostile = false, bool ignoreNonWetPlayer = true, bool ignoreWater = false, float velMaxX = 3f, float velMaxY = 2f)
		{
			if (hostile && npc.direction == 0)
			{
				npc.TargetClosest(true);
			}
			if (ignoreWater || npc.wet)
			{
				bool hasTarget = false;
				if (hostile)
				{
					npc.TargetClosest(false);
					if ((!ignoreNonWetPlayer || Main.player[npc.target].wet) && !Main.player[npc.target].dead)
					{
						hasTarget = true;
					}
				}
				if (!hasTarget)
				{
					if (npc.collideX)
					{
						npc.velocity.X = npc.velocity.X * -1f;
						npc.direction *= -1;
						npc.netUpdate = true;
					}
					if (npc.collideY)
					{
						npc.netUpdate = true;
						int mult = (npc.velocity.Y > 0f) ? -1 : 1;
						npc.velocity.Y = Math.Abs(npc.velocity.Y) * (float)mult;
						npc.directionY = mult;
						ai[0] = 1f * (float)mult;
					}
				}
				if (hasTarget)
				{
					npc.TargetClosest(true);
					npc.velocity.X = npc.velocity.X + (float)npc.direction * 0.1f;
					npc.velocity.Y = npc.velocity.Y + (float)npc.directionY * 0.1f;
					if (npc.velocity.X > velMaxX)
					{
						npc.velocity.X = velMaxX;
					}
					if (npc.velocity.X < -velMaxX)
					{
						npc.velocity.X = -velMaxX;
					}
					if (npc.velocity.Y > velMaxY)
					{
						npc.velocity.Y = velMaxY;
					}
					if (npc.velocity.Y < -velMaxY)
					{
						npc.velocity.Y = -velMaxY;
					}
				}
				else
				{
					npc.velocity.X = npc.velocity.X + (float)npc.direction * 0.1f;
					if (npc.velocity.X < -1f || npc.velocity.X > 1f)
					{
						npc.velocity.X = npc.velocity.X * 0.95f;
					}
					if (ai[0] == -1f)
					{
						npc.velocity.Y = npc.velocity.Y - 0.01f;
						if ((double)npc.velocity.Y < -0.3)
						{
							ai[0] = 1f;
						}
					}
					else
					{
						npc.velocity.Y = npc.velocity.Y + 0.01f;
						if ((double)npc.velocity.Y > 0.3)
						{
							ai[0] = -1f;
						}
					}
					int tileX = (int)(npc.Center.X / 16f);
					int tileY = (int)(npc.Center.Y / 16f);
					for (int i = -1; i < 3; i++)
					{
						if (Main.tile[tileX, tileY + i] == null)
						{
							Main.tile[tileX, tileY + i] = new Tile();
						}
					}
					if (Main.tile[tileX, tileY - 1].liquid > 128 && (Main.tile[tileX, tileY + 1].nactive() || Main.tile[tileX, tileY + 2].nactive()))
					{
						ai[0] = -1f;
					}
					if (npc.velocity.Y > velMaxY || npc.velocity.Y < -velMaxY)
					{
						npc.velocity.Y = npc.velocity.Y * 0.95f;
					}
				}
			}
			else
			{
				if (Main.netMode != 1 && npc.velocity.Y == 0f)
				{
					npc.velocity.Y = (float)Main.rand.Next(-50, -20) * 0.1f;
					npc.velocity.X = (float)Main.rand.Next(-20, 20) * 0.1f;
					npc.netUpdate = true;
				}
				npc.velocity.Y = npc.velocity.Y + 0.3f;
				if (npc.velocity.Y > 10f)
				{
					npc.velocity.Y = 10f;
				}
				ai[0] = 1f;
			}
			npc.rotation = npc.velocity.Y * (float)npc.direction * 0.1f;
			if ((double)npc.rotation < -0.2)
			{
				npc.rotation = -0.2f;
			}
			if ((double)npc.rotation > 0.2)
			{
				npc.rotation = 0.2f;
			}
		}

		public static void AIZombie(NPC npc, ref float[] ai, bool fleeWhenDay = true, bool allowBoredom = true, int openDoors = 1, float moveInterval = 0.07f, float velMax = 1f, int maxJumpTilesX = 3, int maxJumpTilesY = 4, int ticksUntilBoredom = 60, bool targetPlayers = true, int doorBeatCounterMax = 10, int doorCounterMax = 60, bool jumpUpPlatforms = false, Action<bool, bool, Vector2, Vector2> onTileCollide = null, bool ignoreJumpTiles = false)
		{
			bool xVelocityChanged = false;
			if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
			{
				xVelocityChanged = true;
			}
			if (npc.position.X == npc.oldPosition.X || ai[3] >= (float)ticksUntilBoredom || xVelocityChanged)
			{
				ai[3] += 1f;
			}
			else if ((double)Math.Abs(npc.velocity.X) > 0.9 && ai[3] > 0f)
			{
				ai[3] -= 1f;
			}
			if (ai[3] > (float)(ticksUntilBoredom * 10))
			{
				ai[3] = 0f;
			}
			if (npc.justHit)
			{
				ai[3] = 0f;
			}
			if (ai[3] == (float)ticksUntilBoredom)
			{
				npc.netUpdate = true;
			}
			bool notBored = ai[3] < (float)ticksUntilBoredom;
			if (targetPlayers && (!fleeWhenDay || !Main.dayTime || (double)npc.position.Y > Main.worldSurface * 16.0) && ((fleeWhenDay && Main.dayTime) ? notBored : (!allowBoredom || notBored)))
			{
				npc.TargetClosest(true);
			}
			else if (ai[2] <= 0f)
			{
				if (fleeWhenDay && Main.dayTime && (double)(npc.position.Y / 16f) < Main.worldSurface && npc.timeLeft > 10)
				{
					npc.timeLeft = 10;
				}
				if (npc.velocity.X == 0f)
				{
					if (npc.velocity.Y == 0f)
					{
						ai[0] += 1f;
						if (ai[0] >= 2f)
						{
							npc.direction *= -1;
							npc.spriteDirection = npc.direction;
							ai[0] = 0f;
						}
					}
				}
				else
				{
					ai[0] = 0f;
				}
				if (npc.direction == 0)
				{
					npc.direction = 1;
				}
			}
			if (npc.velocity.X < -velMax || npc.velocity.X > velMax)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.velocity *= 0.8f;
				}
			}
			else if (npc.velocity.X < velMax && npc.direction == 1)
			{
				npc.velocity.X = npc.velocity.X + moveInterval;
				if (npc.velocity.X > velMax)
				{
					npc.velocity.X = velMax;
				}
			}
			else if (npc.velocity.X > -velMax && npc.direction == -1)
			{
				npc.velocity.X = npc.velocity.X - moveInterval;
				if (npc.velocity.X < -velMax)
				{
					npc.velocity.X = -velMax;
				}
			}
			BaseAI.WalkupHalfBricks(npc);
			if (openDoors != -1 && BaseAI.AttemptOpenDoor(npc, ref ai[1], ref ai[2], ref ai[3], (float)ticksUntilBoredom, doorBeatCounterMax, doorCounterMax, openDoors))
			{
				npc.velocity.X = 0f;
			}
			else if (openDoors != -1)
			{
				ai[1] = 0f;
				ai[2] = 0f;
			}
			if (BaseAI.HitTileOnSide(npc, 3, true) && ((npc.velocity.X < 0f && npc.direction == -1) || (npc.velocity.X > 0f && npc.direction == 1)))
			{
				Vector2 newVec = BaseAI.AttemptJump(npc.position, npc.velocity, npc.width, npc.height, npc.direction, (float)npc.directionY, maxJumpTilesX, maxJumpTilesY, velMax, jumpUpPlatforms, (jumpUpPlatforms && notBored) ? Main.player[npc.target] : null, ignoreJumpTiles);
				if (!npc.noTileCollide)
				{
					newVec = Collision.TileCollision(npc.position, newVec, npc.width, npc.height, false, false, 1);
					Vector4 slopeVec = Collision.SlopeCollision(npc.position, newVec, npc.width, npc.height, 0f, false);
					Vector2 slopeVel = new Vector2(slopeVec.Z, slopeVec.W);
					if (onTileCollide != null && npc.velocity != slopeVel)
					{
						onTileCollide(npc.velocity.X != slopeVel.X, npc.velocity.Y != slopeVel.Y, npc.velocity, slopeVel);
					}
					npc.position = new Vector2(slopeVec.X, slopeVec.Y);
					npc.velocity = slopeVel;
				}
				if (npc.velocity != newVec)
				{
					npc.velocity = newVec;
					npc.netUpdate = true;
				}
			}
		}

		public static void AIEye(NPC npc, ref float[] ai, bool fleeWhenDay = true, bool ignoreWet = false, float moveIntervalX = 0.1f, float moveIntervalY = 0.04f, float velMaxX = 4f, float velMaxY = 1.5f, float bounceScalarX = 1f, float bounceScalarY = 1f)
		{
			if (npc.collideX)
			{
				npc.velocity.X = npc.oldVelocity.X * -0.5f;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
				{
					npc.velocity.X = 2f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
				{
					npc.velocity.X = -2f;
				}
				npc.velocity.X = npc.velocity.X * bounceScalarX;
			}
			if (npc.collideY)
			{
				npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
				{
					npc.velocity.Y = 1f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
				{
					npc.velocity.Y = -1f;
				}
				npc.velocity.Y = npc.velocity.Y * bounceScalarY;
			}
			if (fleeWhenDay && Main.dayTime && (double)npc.position.Y <= Main.worldSurface * 16.0)
			{
				if (npc.timeLeft > 10)
				{
					npc.timeLeft = 10;
				}
				npc.directionY = -1;
				if (npc.velocity.Y > 0f)
				{
					npc.direction = 1;
				}
				npc.direction = -1;
				if (npc.velocity.X > 0f)
				{
					npc.direction = 1;
				}
			}
			else
			{
				npc.TargetClosest(true);
				if (Main.player[npc.target].dead)
				{
					if (npc.timeLeft > 10)
					{
						npc.timeLeft = 10;
					}
					npc.directionY = -1;
					if (npc.velocity.Y > 0f)
					{
						npc.direction = 1;
					}
					npc.direction = -1;
					if (npc.velocity.X > 0f)
					{
						npc.direction = 1;
					}
				}
			}
			if (npc.direction == -1 && npc.velocity.X > -velMaxX)
			{
				npc.velocity.X = npc.velocity.X - moveIntervalX;
				if (npc.velocity.X > 4f)
				{
					npc.velocity.X = npc.velocity.X - 0.1f;
				}
				else if (npc.velocity.X > 0f)
				{
					npc.velocity.X = npc.velocity.X + 0.05f;
				}
				if (npc.velocity.X < -4f)
				{
					npc.velocity.X = -velMaxX;
				}
			}
			else if (npc.direction == 1 && npc.velocity.X < velMaxX)
			{
				npc.velocity.X = npc.velocity.X + moveIntervalX;
				if (npc.velocity.X < -velMaxX)
				{
					npc.velocity.X = npc.velocity.X + 0.1f;
				}
				else if (npc.velocity.X < 0f)
				{
					npc.velocity.X = npc.velocity.X - 0.05f;
				}
				if (npc.velocity.X > velMaxX)
				{
					npc.velocity.X = velMaxX;
				}
			}
			if (npc.directionY == -1 && (double)npc.velocity.Y > (double)(-(double)velMaxY))
			{
				npc.velocity.Y = npc.velocity.Y - moveIntervalY;
				if ((double)npc.velocity.Y > (double)velMaxY)
				{
					npc.velocity.Y = npc.velocity.Y - 0.05f;
				}
				else if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + 0.03f;
				}
				if ((double)npc.velocity.Y < (double)(-(double)velMaxY))
				{
					npc.velocity.Y = -velMaxY;
				}
			}
			else if (npc.directionY == 1 && (double)npc.velocity.Y < (double)velMaxY)
			{
				npc.velocity.Y = npc.velocity.Y + moveIntervalY;
				if ((double)npc.velocity.Y < (double)(-(double)velMaxY))
				{
					npc.velocity.Y = npc.velocity.Y + 0.05f;
				}
				else if (npc.velocity.Y < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - 0.03f;
				}
				if ((double)npc.velocity.Y > (double)velMaxY)
				{
					npc.velocity.Y = velMaxY;
				}
			}
			if (!ignoreWet && npc.wet)
			{
				if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y * 0.95f;
				}
				npc.velocity.Y = npc.velocity.Y - 0.5f;
				if (npc.velocity.Y < -velMaxY * 1.5f)
				{
					npc.velocity.Y = -velMaxY * 1.5f;
				}
				npc.TargetClosest(true);
				return;
			}
		}

		public static void AISlime(NPC npc, ref float[] ai, bool fleeWhenDay = false, int jumpTime = 200, float jumpVelX = 2f, float jumpVelY = -6f, float jumpVelHighX = 3f, float jumpVelHighY = -8f)
		{
			bool getNewTarget = false;
			if ((fleeWhenDay && !Main.dayTime) || npc.life != npc.lifeMax || (double)npc.position.Y > Main.worldSurface * 16.0)
			{
				getNewTarget = true;
			}
			if (ai[2] > 1f)
			{
				ai[2] -= 1f;
			}
			if (npc.wet)
			{
				if (npc.collideY)
				{
					npc.velocity.Y = -2f;
				}
				if (npc.velocity.Y < 0f && ai[3] == npc.position.X)
				{
					npc.direction *= -1;
					ai[2] = 200f;
				}
				if (npc.velocity.Y > 0f)
				{
					ai[3] = npc.position.X;
				}
				if (npc.velocity.Y > 2f)
				{
					npc.velocity.Y = npc.velocity.Y * 0.9f;
				}
				npc.velocity.Y = npc.velocity.Y - 0.5f;
				if (npc.velocity.Y < -4f)
				{
					npc.velocity.Y = -4f;
				}
				if (ai[2] == 1f && getNewTarget)
				{
					npc.TargetClosest(true);
				}
			}
			npc.aiAction = 0;
			if (ai[2] == 0f)
			{
				ai[0] = -100f;
				ai[2] = 1f;
				npc.TargetClosest(true);
			}
			if (npc.velocity.Y == 0f)
			{
				if (ai[3] == npc.position.X)
				{
					npc.direction *= -1;
					ai[2] = 200f;
				}
				ai[3] = 0f;
				npc.velocity.X = npc.velocity.X * 0.8f;
				if (npc.velocity.X > -0.1f && npc.velocity.X < 0.1f)
				{
					npc.velocity.X = 0f;
				}
				if (getNewTarget)
				{
					ai[0] += 1f;
				}
				ai[0] += 1f;
				if (ai[0] >= 0f)
				{
					npc.netUpdate = true;
					if (ai[2] == 1f && getNewTarget)
					{
						npc.TargetClosest(true);
					}
					if (ai[1] == 2f)
					{
						npc.velocity.Y = jumpVelHighY;
						npc.velocity.X = npc.velocity.X + jumpVelHighX * (float)npc.direction;
						ai[0] = (float)(-(float)jumpTime);
						ai[1] = 0f;
						ai[3] = npc.position.X;
						return;
					}
					npc.velocity.Y = jumpVelY;
					npc.velocity.X = npc.velocity.X + jumpVelX * (float)npc.direction;
					ai[0] = (float)(-(float)jumpTime) - 80f;
					ai[1] += 1f;
					return;
				}
				else if (ai[0] >= -30f)
				{
					npc.aiAction = 1;
					return;
				}
			}
			else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
			{
				if ((npc.direction == -1 && (double)npc.velocity.X < 0.1) || (npc.direction == 1 && (double)npc.velocity.X > -0.1))
				{
					npc.velocity.X = npc.velocity.X + 0.2f * (float)npc.direction;
					return;
				}
				npc.velocity.X = npc.velocity.X * 0.93f;
				return;
			}
		}

		public static void WalkupHalfBricks(NPC npc)
		{
			BaseAI.WalkupHalfBricks(npc, ref npc.gfxOffY, ref npc.stepSpeed);
		}

		public static void WalkupHalfBricks(Entity codable, ref float gfxOffY, ref float stepSpeed)
		{
			if (codable == null)
			{
				return;
			}
			if (codable.velocity.Y >= 0f)
			{
				int offset = 0;
				if (codable.velocity.X < 0f)
				{
					offset = -1;
				}
				if (codable.velocity.X > 0f)
				{
					offset = 1;
				}
				Vector2 pos = codable.position;
				pos.X += codable.velocity.X;
				int tileX = (int)(((double)pos.X + (double)(codable.width / 2) + (double)((codable.width / 2 + 1) * offset)) / 16.0);
				int tileY = (int)(((double)pos.Y + (double)codable.height - 1.0) / 16.0);
				if (Main.tile[tileX, tileY] == null)
				{
					Main.tile[tileX, tileY] = new Tile();
				}
				if (Main.tile[tileX, tileY - 1] == null)
				{
					Main.tile[tileX, tileY - 1] = new Tile();
				}
				if (Main.tile[tileX, tileY - 2] == null)
				{
					Main.tile[tileX, tileY - 2] = new Tile();
				}
				if (Main.tile[tileX, tileY - 3] == null)
				{
					Main.tile[tileX, tileY - 3] = new Tile();
				}
				if (Main.tile[tileX, tileY + 1] == null)
				{
					Main.tile[tileX, tileY + 1] = new Tile();
				}
				if (Main.tile[tileX - offset, tileY - 3] == null)
				{
					Main.tile[tileX - offset, tileY - 3] = new Tile();
				}
				if ((double)(tileX * 16) >= (double)pos.X + (double)codable.width || (double)(tileX * 16 + 16) <= (double)pos.X || ((!Main.tile[tileX, tileY].nactive() || Main.tile[tileX, tileY].slope() != 0 || Main.tile[tileX, tileY - 1].slope() != 0 || !Main.tileSolid[(int)Main.tile[tileX, tileY].type] || Main.tileSolidTop[(int)Main.tile[tileX, tileY].type]) && (!Main.tile[tileX, tileY - 1].halfBrick() || !Main.tile[tileX, tileY - 1].nactive())) || (Main.tile[tileX, tileY - 1].nactive() && Main.tileSolid[(int)Main.tile[tileX, tileY - 1].type] && !Main.tileSolidTop[(int)Main.tile[tileX, tileY - 1].type] && (!Main.tile[tileX, tileY - 1].halfBrick() || (Main.tile[tileX, tileY - 4].nactive() && Main.tileSolid[(int)Main.tile[tileX, tileY - 4].type] && !Main.tileSolidTop[(int)Main.tile[tileX, tileY - 4].type]))) || (Main.tile[tileX, tileY - 2].nactive() && Main.tileSolid[(int)Main.tile[tileX, tileY - 2].type] && !Main.tileSolidTop[(int)Main.tile[tileX, tileY - 2].type]) || (Main.tile[tileX, tileY - 3].nactive() && Main.tileSolid[(int)Main.tile[tileX, tileY - 3].type] && !Main.tileSolidTop[(int)Main.tile[tileX, tileY - 3].type]) || (Main.tile[tileX - offset, tileY - 3].nactive() && Main.tileSolid[(int)Main.tile[tileX - offset, tileY - 3].type]))
				{
					gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
					return;
				}
				float tileWorldY = (float)(tileY * 16);
				if (Main.tile[tileX, tileY].halfBrick())
				{
					tileWorldY += 8f;
				}
				if (Main.tile[tileX, tileY - 1].halfBrick())
				{
					tileWorldY -= 8f;
				}
				if ((double)tileWorldY >= (double)pos.Y + (double)codable.height)
				{
					gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
					return;
				}
				float tileWorldYHeight = pos.Y + (float)codable.height - tileWorldY;
				float heightNeeded = 16.1f;
				if ((double)tileWorldYHeight <= (double)heightNeeded)
				{
					gfxOffY += codable.position.Y + (float)codable.height - tileWorldY;
					codable.position.Y = tileWorldY - (float)codable.height;
					stepSpeed = (((double)tileWorldYHeight >= 9.0) ? 2f : 1f);
					return;
				}
			}
			else
			{
				gfxOffY = Math.Max(0f, gfxOffY - stepSpeed);
			}
		}

		public static Vector2 AttemptJump(Vector2 position, Vector2 velocity, int width, int height, int direction, float directionY = 0f, int tileDistX = 3, int tileDistY = 4, float maxSpeedX = 1f, bool jumpUpPlatforms = false, Entity target = null, bool ignoreTiles = false)
		{
			Vector2 result;
			try
			{
				tileDistX -= 2;
				Vector2 newVelocity = velocity;
				int tileX = Math.Max(10, Math.Min(Main.maxTilesX - 10, (int)((position.X + (float)width * 0.5f + ((float)width * 0.5f + 8f) * (float)direction) / 16f)));
				int tileY = Math.Max(10, Math.Min(Main.maxTilesY - 10, (int)((position.Y + (float)height - 15f) / 16f)));
				int tileItX = Math.Max(10, Math.Min(Main.maxTilesX - 10, tileX + direction * tileDistX));
				int tileItY = Math.Max(10, Math.Min(Main.maxTilesY - 10, tileY - tileDistY));
				int lastY = tileY;
				int tileHeight = (int)((float)height / 16f);
				if (height > tileHeight * 16)
				{
					tileHeight++;
				}
				Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, width, height);
				if (ignoreTiles && target != null && Math.Abs(position.X + (float)width * 0.5f - target.Center.X) < (float)(width + 120))
				{
					float dist = (float)((int)Math.Abs(position.Y + (float)height * 0.5f - target.Center.Y) / 16);
					if (dist < (float)(tileDistY + 2))
					{
						newVelocity.Y = -8f + dist * -0.5f;
					}
				}
				if (newVelocity.Y == velocity.Y)
				{
					for (int y = tileY; y >= tileItY; y--)
					{
						Tile tile = Main.tile[tileX, y];
						Tile tileNear = Main.tile[Math.Min(Main.maxTilesX, tileX - direction), y];
						if (tile == null)
						{
							tile = (Main.tile[tileX, y] = new Tile());
						}
						if (tileNear == null)
						{
							tileNear = (Main.tile[Math.Min(Main.maxTilesX, tileX - direction), y] = new Tile());
						}
						if (tile.nactive() && (y != tileY || (!tile.halfBrick() && tile.slope() == 0)) && Main.tileSolid[(int)tile.type] && (jumpUpPlatforms || !Main.tileSolidTop[(int)tile.type]))
						{
							if (!Main.tileSolidTop[(int)tile.type] && new Rectangle(tileX * 16, y * 16, 16, 16)
							{
								Y = hitbox.Y
							}.Intersects(hitbox))
							{
								newVelocity = velocity;
								break;
							}
							if (tileNear.nactive() && Main.tileSolid[(int)tileNear.type] && !Main.tileSolidTop[(int)tileNear.type])
							{
								newVelocity = velocity;
								break;
							}
							if (target == null || (float)(y * 16) >= target.Center.Y)
							{
								lastY = y;
								newVelocity.Y = -(5f + (float)(tileY - y) * ((tileY - y > 3) ? (1f - (float)(tileY - y - 2) * 0.0525f) : 1f));
							}
						}
						else if (lastY - y >= tileHeight)
						{
							break;
						}
					}
				}
				if (newVelocity.Y == velocity.Y)
				{
					if (Main.tile[tileX, tileY + 1] == null)
					{
						Main.tile[tileX, tileY + 1] = new Tile();
					}
					if (Main.tile[tileX + direction, tileY + 1] == null)
					{
						Main.tile[tileX, tileY + 1] = new Tile();
					}
					if (Main.tile[tileX + direction, tileY + 2] == null)
					{
						Main.tile[tileX, tileY + 2] = new Tile();
					}
					if (directionY < 0f && (!Main.tile[tileX, tileY + 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY + 1].type]) && (!Main.tile[tileX + direction, tileY + 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX + direction, tileY + 1].type]) && (!Main.tile[tileX + direction, tileY + 2].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY + 2].type] || target == null || target.Center.Y + (float)target.height * 0.25f < (float)tileY * 16f))
					{
						newVelocity.Y = -8f;
						newVelocity.X *= 1.5f * (1f / maxSpeedX);
						if (tileX <= tileItX)
						{
							for (int x = tileX; x < tileItX; x++)
							{
								Tile tile2 = Main.tile[x, tileY + 1];
								if (tile2 == null)
								{
									tile2 = (Main.tile[x, tileY + 1] = new Tile());
								}
								if (x != tileX && !tile2.nactive())
								{
									newVelocity.Y -= 0.0325f;
									newVelocity.X += (float)direction * 0.255f;
								}
							}
						}
						else if (tileX > tileItX)
						{
							for (int x2 = tileItX; x2 < tileX; x2++)
							{
								Tile tile3 = Main.tile[x2, tileY + 1];
								if (tile3 == null)
								{
									tile3 = (Main.tile[x2, tileY + 1] = new Tile());
								}
								if (x2 != tileItX && !tile3.nactive())
								{
									newVelocity.Y -= 0.0325f;
									newVelocity.X += (float)direction * 0.255f;
								}
							}
						}
					}
				}
				result = newVelocity;
			}
			catch (Exception e)
			{
				BaseUtility.LogFancy("Redemption~ ATTEMPT JUMP ERROR:", e);
				result = velocity;
			}
			return result;
		}

		public static Vector2 AttemptJump(Vector2 position, Vector2 velocity, int width, int height, int direction, Vector2 vector, float directionY = 0f, int tileDistX = 3, int tileDistY = 4, float maxSpeedX = 1f, bool jumpUpPlatforms = false, bool ignoreTiles = false)
		{
			Vector2 result;
			try
			{
				tileDistX -= 2;
				Vector2 newVelocity = velocity;
				int tileX = Math.Max(10, Math.Min(Main.maxTilesX - 10, (int)((position.X + (float)width * 0.5f + ((float)width * 0.5f + 8f) * (float)direction) / 16f)));
				int tileY = Math.Max(10, Math.Min(Main.maxTilesY - 10, (int)((position.Y + (float)height - 15f) / 16f)));
				int tileItX = Math.Max(10, Math.Min(Main.maxTilesX - 10, tileX + direction * tileDistX));
				int tileItY = Math.Max(10, Math.Min(Main.maxTilesY - 10, tileY - tileDistY));
				int lastY = tileY;
				int tileHeight = (int)((float)height / 16f);
				if (height > tileHeight * 16)
				{
					tileHeight++;
				}
				Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, width, height);
				if (ignoreTiles && Math.Abs(position.X + (float)width * 0.5f - vector.X) < (float)(width + 120))
				{
					float dist = (float)((int)Math.Abs(position.Y + (float)height * 0.5f - vector.Y) / 16);
					if (dist < (float)(tileDistY + 2))
					{
						newVelocity.Y = -8f + dist * -0.5f;
					}
				}
				if (newVelocity.Y == velocity.Y)
				{
					for (int y = tileY; y >= tileItY; y--)
					{
						Tile tile = Main.tile[tileX, y];
						Tile tileNear = Main.tile[Math.Min(Main.maxTilesX, tileX - direction), y];
						if (tile == null)
						{
							tile = (Main.tile[tileX, y] = new Tile());
						}
						if (tileNear == null)
						{
							tileNear = (Main.tile[Math.Min(Main.maxTilesX, tileX - direction), y] = new Tile());
						}
						if (tile.nactive() && (y != tileY || (!tile.halfBrick() && tile.slope() == 0)) && Main.tileSolid[(int)tile.type] && (jumpUpPlatforms || !Main.tileSolidTop[(int)tile.type]))
						{
							if (!Main.tileSolidTop[(int)tile.type] && new Rectangle(tileX * 16, y * 16, 16, 16)
							{
								Y = hitbox.Y
							}.Intersects(hitbox))
							{
								newVelocity = velocity;
								break;
							}
							if (tileNear.nactive() && Main.tileSolid[(int)tileNear.type] && !Main.tileSolidTop[(int)tileNear.type])
							{
								newVelocity = velocity;
								break;
							}
							if ((float)(y * 16) >= vector.Y)
							{
								lastY = y;
								newVelocity.Y = -(5f + (float)(tileY - y) * ((tileY - y > 3) ? (1f - (float)(tileY - y - 2) * 0.0525f) : 1f));
							}
						}
						else if (lastY - y >= tileHeight)
						{
							break;
						}
					}
				}
				if (newVelocity.Y == velocity.Y)
				{
					if (Main.tile[tileX, tileY + 1] == null)
					{
						Main.tile[tileX, tileY + 1] = new Tile();
					}
					if (Main.tile[tileX + direction, tileY + 1] == null)
					{
						Main.tile[tileX, tileY + 1] = new Tile();
					}
					if (Main.tile[tileX + direction, tileY + 2] == null)
					{
						Main.tile[tileX, tileY + 2] = new Tile();
					}
					if (directionY < 0f && (!Main.tile[tileX, tileY + 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY + 1].type]) && (!Main.tile[tileX + direction, tileY + 1].nactive() || !Main.tileSolid[(int)Main.tile[tileX + direction, tileY + 1].type]) && (!Main.tile[tileX + direction, tileY + 2].nactive() || !Main.tileSolid[(int)Main.tile[tileX, tileY + 2].type] || vector.Y < (float)tileY * 16f))
					{
						newVelocity.Y = -8f;
						newVelocity.X *= 1.5f * (1f / maxSpeedX);
						if (tileX <= tileItX)
						{
							for (int x = tileX; x < tileItX; x++)
							{
								Tile tile2 = Main.tile[x, tileY + 1];
								if (tile2 == null)
								{
									tile2 = (Main.tile[x, tileY + 1] = new Tile());
								}
								if (x != tileX && !tile2.nactive())
								{
									newVelocity.Y -= 0.0325f;
									newVelocity.X += (float)direction * 0.255f;
								}
							}
						}
						else if (tileX > tileItX)
						{
							for (int x2 = tileItX; x2 < tileX; x2++)
							{
								Tile tile3 = Main.tile[x2, tileY + 1];
								if (tile3 == null)
								{
									tile3 = (Main.tile[x2, tileY + 1] = new Tile());
								}
								if (x2 != tileItX && !tile3.nactive())
								{
									newVelocity.Y -= 0.0325f;
									newVelocity.X += (float)direction * 0.255f;
								}
							}
						}
					}
				}
				result = newVelocity;
			}
			catch (Exception e)
			{
				BaseUtility.LogFancy("Redemption~ ATTEMPT JUMP ERROR:", e);
				result = velocity;
			}
			return result;
		}

		public static bool AttemptOpenDoor(NPC npc, ref float doorBeatCounter, ref float doorCounter, ref float tickUpdater, float ticksUntilBoredom, int doorBeatCounterMax = 10, int doorCounterMax = 60, int interactDoorStyle = 0)
		{
			if (BaseAI.HitTileOnSide(npc, 3, true))
			{
				int tileX = (int)((npc.Center.X + ((float)(npc.width / 2) + 8f) * (float)npc.direction) / 16f);
				int tileY = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
				for (int i = 1; i >= -3; i--)
				{
					if (i == 1 && Main.tile[tileX + npc.direction, tileY + i] == null)
					{
						Main.tile[tileX + npc.direction, tileY + i] = new Tile();
					}
					else if (i == -1 && Main.tile[tileX + npc.direction, tileY + i] == null)
					{
						Main.tile[tileX + npc.direction, tileY + i] = new Tile();
					}
					if (Main.tile[tileX, tileY + i] == null)
					{
						Main.tile[tileX, tileY + i] = new Tile();
					}
				}
				if (Main.tile[tileX, tileY - 1].nactive() && Main.tile[tileX, tileY - 1].type == 10)
				{
					doorCounter += 1f;
					tickUpdater = 0f;
					if (doorCounter >= (float)doorCounterMax)
					{
						npc.velocity.X = 0.5f * (float)(-(float)npc.direction);
						doorBeatCounter += 1f;
						doorCounter = 0f;
						bool attemptOpenDoor = false;
						if (doorBeatCounter >= (float)doorBeatCounterMax)
						{
							attemptOpenDoor = true;
							doorBeatCounter = 10f;
						}
						WorldGen.KillTile(tileX, tileY - 1, true, false, false);
						if (attemptOpenDoor && Main.netMode != 1)
						{
							bool openedDoor = false;
							if (interactDoorStyle != 0)
							{
								if (interactDoorStyle == 1)
								{
									WorldGen.KillTile(tileX, tileY, false, false, false);
									openedDoor = !Main.tile[tileX, tileY].nactive();
								}
								else
								{
									openedDoor = WorldGen.OpenDoor(tileX, tileY, npc.direction);
								}
							}
							if (!openedDoor)
							{
								tickUpdater = ticksUntilBoredom;
								npc.netUpdate = true;
							}
							if (Main.netMode == 2 && openedDoor)
							{
								NetMessage.SendData(19, -1, -1, NetworkText.FromLiteral(""), 0, (float)tileX, (float)tileY, (float)npc.direction, 0, 0, 0);
							}
						}
					}
					return true;
				}
			}
			return false;
		}

		public static bool EmptyTiles(Rectangle rect)
		{
			int topX = rect.X / 16;
			int topY = rect.Y / 16;
			for (int x = topX; x < topX + rect.Width; x++)
			{
				int y = topY;
				while (x < topY + rect.Height)
				{
					Tile tile = Main.tile[x, y];
					if (tile != null && tile.nactive() && Main.tileSolid[(int)tile.type])
					{
						return false;
					}
					y++;
				}
			}
			return true;
		}

		public static bool HitTileOnSide(Entity codable, int dir, bool noYMovement = true)
		{
			if (!noYMovement || codable.velocity.Y == 0f)
			{
				Vector2 dummyVec = default(Vector2);
				return BaseAI.HitTileOnSide(codable.position, codable.width, codable.height, dir, ref dummyVec);
			}
			return false;
		}

		public static bool HitTileOnSide(Vector2 position, int width, int height, int dir, ref Vector2 hitTilePos)
		{
			int tilePosX = 0;
			int tilePosY = 0;
			int tilePosWidth = 0;
			int tilePosHeight = 0;
			if (dir == 0)
			{
				tilePosX = (int)(position.X - 8f) / 16;
				tilePosY = (int)position.Y / 16;
				tilePosWidth = tilePosX + 1;
				tilePosHeight = (int)(position.Y + (float)height) / 16;
			}
			else if (dir == 1)
			{
				tilePosX = (int)(position.X + (float)width + 8f) / 16;
				tilePosY = (int)position.Y / 16;
				tilePosWidth = tilePosX + 1;
				tilePosHeight = (int)(position.Y + (float)height) / 16;
			}
			else if (dir == 2)
			{
				tilePosX = (int)position.X / 16;
				tilePosY = (int)(position.Y - 8f) / 16;
				tilePosWidth = (int)(position.X + (float)width) / 16;
				tilePosHeight = tilePosY + 1;
			}
			else if (dir == 3)
			{
				tilePosX = (int)position.X / 16;
				tilePosY = (int)(position.Y + (float)height + 8f) / 16;
				tilePosWidth = (int)(position.X + (float)width) / 16;
				tilePosHeight = tilePosY + 1;
			}
			for (int x2 = tilePosX; x2 < tilePosWidth; x2++)
			{
				for (int y2 = tilePosY; y2 < tilePosHeight; y2++)
				{
					if (Main.tile[x2, y2] == null)
					{
						return false;
					}
					if (Main.tile[x2, y2].nactive() && Main.tileSolid[(int)Main.tile[x2, y2].type])
					{
						hitTilePos = new Vector2((float)x2, (float)y2);
						return true;
					}
				}
			}
			return false;
		}

		public static int DropItemBossBag(Player player, Entity codable, int type, int amt, int maxStack, float chance, bool clusterItem = false)
		{
			if (player != null)
			{
				if ((float)Main.rand.NextDouble() <= chance)
				{
					player.QuickSpawnItem(type, amt);
				}
				return -2;
			}
			return BaseAI.DropItem(codable, type, amt, maxStack, chance, clusterItem, false);
		}

		public static int DropItem(Entity codable, int type, int amt, int maxStack, int chance, bool clusterItem = false)
		{
			return BaseAI.DropItem(codable, type, amt, maxStack, (float)chance / 100f, clusterItem, false);
		}

		public static int DropItem(Entity codable, int type, int amt, int maxStack, float chance, bool clusterItem = false, bool sync = false)
		{
			int itemID = -1;
			if ((sync || Main.netMode != 1) && (float)Main.rand.NextDouble() <= chance)
			{
				if (clusterItem)
				{
					int stackCount = 0;
					int stackCount2 = 0;
					while (stackCount != amt)
					{
						stackCount++;
						stackCount2++;
						if (stackCount == amt || stackCount2 == maxStack)
						{
							itemID = Item.NewItem((int)codable.position.X, (int)codable.position.Y, codable.width, codable.height, type, stackCount2, false, 0, false, false);
							if (sync)
							{
								NetMessage.SendData(21, -1, -1, null, itemID, 0f, 0f, 0f, 0, 0, 0);
							}
							stackCount2 = 0;
						}
					}
				}
				else
				{
					int count = 0;
					while (count < amt)
					{
						count++;
						itemID = Item.NewItem((int)codable.position.X, (int)codable.position.Y, codable.width, codable.height, type, 1, false, 0, false, false);
						if (sync)
						{
							NetMessage.SendData(21, -1, -1, null, itemID, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
			return itemID;
		}

		public static void DamagePlayer(Player player, int dmgAmt, float knockback, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false)
		{
			int hitDirection = (damager == null) ? 0 : damager.direction;
			BaseAI.DamagePlayer(player, dmgAmt, knockback, hitDirection, damager, dmgVariation, hitThroughDefense, 0, 1f);
		}

		public static void DamagePlayer(Player player, int dmgAmt, float knockback, int hitDirection, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false, int critChance = 0, float critMult = 1f)
		{
			if (hitThroughDefense)
			{
				dmgAmt += (int)((float)player.statDefense * 0.5f);
			}
			if (damager == null)
			{
				int parsedDamage = dmgAmt;
				if (dmgVariation)
				{
					parsedDamage = Main.DamageVar((float)dmgAmt);
				}
				player.Hurt(PlayerDeathReason.ByOther(-1), parsedDamage, hitDirection, false, false, false, 0);
				return;
			}
			Player subPlayer;
			if ((subPlayer = (damager as Player)) != null)
			{
				int parsedDamage2 = dmgAmt;
				if (dmgVariation)
				{
					parsedDamage2 = Main.DamageVar((float)dmgAmt);
				}
				player.Hurt(PlayerDeathReason.ByPlayer(subPlayer.whoAmI), parsedDamage2, hitDirection, true, false, false, 0);
				subPlayer.attackCD = (int)((float)subPlayer.itemAnimationMax * 0.33f);
				return;
			}
			if (damager is Projectile)
			{
				Projectile p = (Projectile)damager;
				if (p.friendly)
				{
					int parsedDamage3 = dmgAmt;
					if (dmgVariation)
					{
						parsedDamage3 = Main.DamageVar((float)dmgAmt);
					}
					player.Hurt(PlayerDeathReason.ByProjectile(p.owner, p.whoAmI), parsedDamage3, hitDirection, true, false, false, 0);
					p.playerImmune[player.whoAmI] = 40;
					return;
				}
				if (p.hostile)
				{
					int parsedDamage4 = dmgAmt;
					if (dmgVariation)
					{
						parsedDamage4 = Main.DamageVar((float)dmgAmt);
					}
					player.Hurt(PlayerDeathReason.ByProjectile(-1, p.whoAmI), parsedDamage4, hitDirection, false, false, false, 0);
					return;
				}
			}
			else if (damager is NPC)
			{
				NPC npc = (NPC)damager;
				int parsedDamage5 = dmgAmt;
				if (dmgVariation)
				{
					parsedDamage5 = Main.DamageVar((float)dmgAmt);
				}
				player.Hurt(PlayerDeathReason.ByNPC(npc.whoAmI), parsedDamage5, hitDirection, false, false, false, 0);
			}
		}

		public static void DamageNPC(NPC npc, int dmgAmt, float knockback, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false)
		{
			int hitDirection = (damager == null) ? 0 : damager.direction;
			BaseAI.DamageNPC(npc, dmgAmt, knockback, hitDirection, damager, dmgVariation, hitThroughDefense);
		}

		public static void DamageNPC(NPC npc, int dmgAmt, float knockback, int hitDirection, Entity damager, bool dmgVariation = true, bool hitThroughDefense = false)
		{
			if (hitThroughDefense)
			{
				dmgAmt += (int)((float)npc.defense * 0.5f);
			}
			if (damager == null)
			{
				int parsedDamage = dmgAmt;
				if (dmgVariation)
				{
					parsedDamage = Main.DamageVar((float)dmgAmt);
				}
				npc.StrikeNPC(parsedDamage, knockback, hitDirection, false, false, false);
				if (Main.netMode != 0)
				{
					NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1f, knockback, (float)hitDirection, parsedDamage, 0, 0);
					return;
				}
			}
			else if (damager is Projectile)
			{
				Projectile p = (Projectile)damager;
				if (p.owner == Main.myPlayer)
				{
					int parsedDamage2 = dmgAmt;
					if (dmgVariation)
					{
						parsedDamage2 = Main.DamageVar((float)dmgAmt);
					}
					npc.StrikeNPC(parsedDamage2, knockback, hitDirection, false, false, false);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1f, knockback, (float)hitDirection, parsedDamage2, 0, 0);
					}
					if (p.penetrate != 1)
					{
						npc.immune[p.owner] = 10;
						return;
					}
				}
			}
			else if (damager is Player)
			{
				Player player = (Player)damager;
				if (player.whoAmI == Main.myPlayer)
				{
					int parsedDamage3 = dmgAmt;
					if (dmgVariation)
					{
						parsedDamage3 = Main.DamageVar((float)dmgAmt);
					}
					npc.StrikeNPC(parsedDamage3, knockback, hitDirection, false, false, false);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(28, -1, -1, NetworkText.FromLiteral(""), npc.whoAmI, 1f, knockback, (float)hitDirection, parsedDamage3, 0, 0);
					}
					npc.immune[player.whoAmI] = player.itemAnimation;
				}
			}
		}

		public static void KillNPCWithLoot(NPC npc)
		{
			BaseAI.DamageNPC(npc, npc.lifeMax + npc.defense + 1, 0f, 0, null, false, true);
		}

		public static void KillNPC(NPC npc)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			npc.active = false;
			int npcID = npc.whoAmI;
			Main.npc[npcID] = new NPC();
			if (Main.netMode == 2)
			{
				NetMessage.SendData(23, -1, -1, null, npcID, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public static void KillProjectile(Projectile p)
		{
			if (p.owner == Main.myPlayer)
			{
				NetMessage.SendData(29, -1, -1, NetworkText.FromLiteral(""), p.identity, (float)p.owner, 0f, 0f, 0, 0, 0);
			}
			p.active = false;
		}

		public static void SpawnSmoke(Vector2 start, float width, float height, int loopAmount = 2, float scale = 1f)
		{
			Vector2 center = start + new Vector2(width * 0.5f, height * 0.5f);
			UnifiedRandom rand = Main.rand;
			for (int i = 0; i < loopAmount; i++)
			{
				Vector2 vector = new Vector2(center.X - 24f, center.Y - 24f);
				Vector2 velocityDefault = default(Vector2);
				int goreID = Gore.NewGore(vector, velocityDefault, Main.rand.Next(61, 64), 1f);
				Gore gore = Main.gore[goreID];
				gore.scale = scale * 1.5f;
				gore.velocity.X = ((rand.Next(2) == 0) ? (-(gore.velocity.X + 1.5f)) : (gore.velocity.X + 1.5f));
				Gore gore2 = gore;
				gore2.velocity.Y = gore2.velocity.Y + 1.5f;
				goreID = Gore.NewGore(vector, velocityDefault, Main.rand.Next(61, 64), 1f);
				gore = Main.gore[goreID];
				gore.scale = scale * 1.5f;
				gore.velocity.X = ((rand.Next(2) == 0) ? (-(gore.velocity.X + 1.5f)) : (gore.velocity.X + 1.5f));
				Gore gore3 = gore;
				gore3.velocity.Y = gore3.velocity.Y + 1.5f;
				goreID = Gore.NewGore(vector, velocityDefault, Main.rand.Next(61, 64), 1f);
				gore = Main.gore[goreID];
				gore.scale = scale * 1.5f;
				gore.velocity.X = ((rand.Next(2) == 0) ? (-(gore.velocity.X + 1.5f)) : (gore.velocity.X + 1.5f));
				Gore gore4 = gore;
				gore4.velocity.Y = gore4.velocity.Y + 1.5f;
				goreID = Gore.NewGore(vector, velocityDefault, Main.rand.Next(61, 64), 1f);
				gore = Main.gore[goreID];
				gore.scale = scale * 1.5f;
				gore.velocity.X = ((rand.Next(2) == 0) ? (-(gore.velocity.X + 1.5f)) : (gore.velocity.X + 1.5f));
				Gore gore5 = gore;
				gore5.velocity.Y = gore5.velocity.Y + 1.5f;
			}
		}

		public static int GetProjectile(Vector2 center, int projType = -1, int owner = -1, float distance = -1f, Func<Projectile, bool> CanAdd = null)
		{
			return BaseAI.GetProjectile(center, projType, owner, null, distance, CanAdd);
		}

		public static int GetProjectile(Vector2 center, int projType = -1, int owner = -1, int[] projsToExclude = null, float distance = -1f, Func<Projectile, bool> CanAdd = null)
		{
			int currentProj = -1;
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj != null && proj.active && (projType == -1 || proj.type == projType) && ((float)owner == -1f || proj.owner == owner) && (distance == -1f || proj.Distance(center) < distance))
				{
					bool add = true;
					if (projsToExclude != null)
					{
						for (int j = 0; j < projsToExclude.Length; j++)
						{
							if (projsToExclude[j] == proj.whoAmI)
							{
								add = false;
								break;
							}
						}
					}
					if ((!add || CanAdd == null || CanAdd(proj)) && add)
					{
						distance = proj.Distance(center);
						currentProj = i;
					}
				}
			}
			return currentProj;
		}

		public static int[] GetProjectiles(Vector2 center, int projType = -1, int owner = -1, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			return BaseAI.GetProjectiles(center, projType, owner, null, distance, CanAdd);
		}

		public static int[] GetProjectiles(Vector2 center, int projType = -1, int owner = -1, int[] projsToExclude = null, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			List<int> allProjs = new List<int>();
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj != null && proj.active && (projType == -1 || proj.type == projType) && (owner == -1 || proj.owner == owner) && (distance == -1f || proj.Distance(center) < distance))
				{
					bool add = true;
					if (projsToExclude != null)
					{
						for (int j = 0; j < projsToExclude.Length; j++)
						{
							if (projsToExclude[j] == proj.whoAmI)
							{
								add = false;
								break;
							}
						}
					}
					if ((!add || CanAdd == null || CanAdd(proj)) && add)
					{
						allProjs.Add(i);
					}
				}
			}
			return allProjs.ToArray();
		}

		public static int[] GetProjectiles(Vector2 center, int[] projTypes, int owner = -1, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			return BaseAI.GetProjectiles(center, projTypes, owner, null, distance, CanAdd);
		}

		public static int[] GetProjectiles(Vector2 center, int[] projTypes, int owner = -1, int[] projsToExclude = null, float distance = 500f, Func<Projectile, bool> CanAdd = null)
		{
			List<int> allProjs = new List<int>();
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				Projectile proj = Main.projectile[i];
				if (proj != null && proj.active && (owner == -1 || proj.owner == owner) && (distance == -1f || proj.Distance(center) < distance))
				{
					bool isType = false;
					foreach (int type in projTypes)
					{
						if (proj.type == type)
						{
							isType = true;
							break;
						}
					}
					if (isType)
					{
						bool add = true;
						if (projsToExclude != null)
						{
							for (int j = 0; j < projsToExclude.Length; j++)
							{
								if (projsToExclude[j] == proj.whoAmI)
								{
									add = false;
									break;
								}
							}
						}
						if ((!add || CanAdd == null || CanAdd(proj)) && add)
						{
							allProjs.Add(i);
						}
					}
				}
			}
			return allProjs.ToArray();
		}

		public static int[] GetNPCsInBox(Rectangle rect, int npcType = -1, int[] npcsToExclude = null, Func<NPC, bool> CanAdd = null)
		{
			List<int> allNPCs = new List<int>();
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != 488 && rect.Intersects(npc.Hitbox))
				{
					bool add = true;
					if (npcsToExclude != null)
					{
						for (int j = 0; j < npcsToExclude.Length; j++)
						{
							if (npcsToExclude[j] == npc.whoAmI)
							{
								add = false;
								break;
							}
						}
					}
					if ((!add || CanAdd == null || CanAdd(npc)) && add)
					{
						allNPCs.Add(i);
					}
				}
			}
			return allNPCs.ToArray();
		}

		public static int GetNPC(Vector2 center, int npcType = -1, float distance = -1f, Func<NPC, bool> CanAdd = null)
		{
			return BaseAI.GetNPC(center, npcType, null, distance, CanAdd);
		}

		public static int GetNPC(Vector2 center, int npcType = -1, int[] npcsToExclude = null, float distance = -1f, Func<NPC, bool> CanAdd = null)
		{
			int currentNPC = -1;
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != 488 && (distance == -1f || npc.Distance(center) < distance))
				{
					bool add = true;
					if (npcsToExclude != null)
					{
						for (int j = 0; j < npcsToExclude.Length; j++)
						{
							if (npcsToExclude[j] == npc.whoAmI)
							{
								add = false;
								break;
							}
						}
					}
					if ((!add || CanAdd == null || CanAdd(npc)) && add)
					{
						distance = npc.Distance(center);
						currentNPC = i;
					}
				}
			}
			return currentNPC;
		}

		public static int[] GetNPCs(Vector2 center, int npcType = -1, float distance = 500f, Func<NPC, bool> CanAdd = null)
		{
			return BaseAI.GetNPCs(center, npcType, new int[0], distance, CanAdd);
		}

		public static int[] GetNPCs(Vector2 center, int npcType = -1, int[] npcsToExclude = null, float distance = 500f, Func<NPC, bool> CanAdd = null)
		{
			List<int> allNPCs = new List<int>();
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (npc != null && npc.active && npc.life > 0 && (npcType == -1 || npc.type == npcType) && npc.type != 488 && (distance == -1f || npc.Distance(center) < distance))
				{
					bool add = true;
					if (npcsToExclude != null)
					{
						for (int j = 0; j < npcsToExclude.Length; j++)
						{
							if (npcsToExclude[j] == npc.whoAmI)
							{
								add = false;
								break;
							}
						}
					}
					if ((!add || CanAdd == null || CanAdd(npc)) && add)
					{
						allNPCs.Add(i);
					}
				}
			}
			return allNPCs.ToArray();
		}

		public static int[] GetPlayersInBox(Rectangle rect, int[] playersToExclude = null, Func<Player, bool> CanAdd = null)
		{
			List<int> allPlayers = new List<int>();
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player plr = Main.player[i];
				if (plr != null && plr.active && !plr.dead && rect.Intersects(plr.Hitbox))
				{
					bool add = true;
					if (playersToExclude != null)
					{
						for (int j = 0; j < playersToExclude.Length; j++)
						{
							if (playersToExclude[j] == plr.whoAmI)
							{
								add = false;
								break;
							}
						}
					}
					if ((!add || CanAdd == null || CanAdd(plr)) && add)
					{
						allPlayers.Add(i);
					}
				}
			}
			return allPlayers.ToArray();
		}

		public static int GetPlayerByName(string name, bool aliveOnly = true)
		{
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player player = Main.player[i];
				if (player != null && player.active && (!aliveOnly || !player.dead) && player.name == name)
				{
					return i;
				}
			}
			return -1;
		}

		public static int GetPlayer(Vector2 center, float distance = -1f, Func<Player, bool> CanAdd = null)
		{
			return BaseAI.GetPlayer(center, null, true, distance, CanAdd);
		}

		public static int GetPlayer(Vector2 center, int[] playersToExclude = null, bool activeOnly = true, float distance = -1f, Func<Player, bool> CanAdd = null)
		{
			int currentPlayer = -1;
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player player = Main.player[i];
				if (player != null && (!activeOnly || (player.active && !player.dead)) && (distance == -1f || player.Distance(center) < distance))
				{
					bool add = true;
					if (playersToExclude != null)
					{
						for (int j = 0; j < playersToExclude.Length; j++)
						{
							if (playersToExclude[j] == player.whoAmI)
							{
								add = false;
								break;
							}
						}
					}
					if ((!add || CanAdd == null || CanAdd(player)) && add)
					{
						distance = player.Distance(center);
						currentPlayer = i;
					}
				}
			}
			return currentPlayer;
		}

		public static int[] GetPlayers(Vector2 center, float distance = 500f, Func<Player, bool> CanAdd = null)
		{
			return BaseAI.GetPlayers(center, null, true, distance, CanAdd);
		}

		public static int[] GetPlayers(Vector2 center, int[] playersToExclude = null, bool aliveOnly = true, float distance = 500f, Func<Player, bool> CanAdd = null)
		{
			List<int> allPlayers = new List<int>();
			for (int i = 0; i < Main.player.Length; i++)
			{
				Player player = Main.player[i];
				if (player != null && player.active && (!aliveOnly || !player.dead) && player.Distance(center) < distance)
				{
					bool add = true;
					if (playersToExclude != null)
					{
						for (int j = 0; j < playersToExclude.Length; j++)
						{
							if (playersToExclude[j] == player.whoAmI)
							{
								add = false;
								break;
							}
						}
					}
					if ((!add || CanAdd == null || CanAdd(player)) && add)
					{
						allPlayers.Add(i);
					}
				}
			}
			return allPlayers.ToArray();
		}

		public static bool CanTarget(Player player, Entity codable)
		{
			if (codable is NPC)
			{
				NPC npc = (NPC)codable;
				return npc.life > 0 && (!npc.friendly || (npc.type == 22 && player.killGuide)) && !npc.dontTakeDamage;
			}
			if (codable is Player)
			{
				Player player2 = (Player)codable;
				return player2.statLife > 0 && !player2.immune && player2.hostile && (player.team == 0 || player2.team == 0 || player.team != player2.team);
			}
			return false;
		}

		public static void SetTarget(NPC npc, int target)
		{
			npc.target = target;
			if (npc.target < 0 || npc.target >= 255)
			{
				npc.target = 0;
			}
			npc.targetRect = Main.player[npc.target].Hitbox;
			if (npc.target != npc.oldTarget && !npc.collideX && !npc.collideY)
			{
				npc.netUpdate = true;
			}
		}

		public static int ShootPeriodic(Entity codable, Vector2 position, int width, int height, int projType, ref float delayTimer, float delayTimerMax = 100f, int damage = -1, float speed = 10f, bool checkCanHit = true, Vector2 offset = default(Vector2))
		{
			int pID = -1;
			if (damage == -1)
			{
				Projectile projectile = new Projectile();
				projectile.SetDefaults(projType);
				damage = projectile.damage;
			}
			if ((codable is NPC) ? (Main.netMode != 1) : (!(codable is Projectile) || ((Projectile)codable).owner == Main.myPlayer))
			{
				Vector2 targetCenter = position + new Vector2((float)width * 0.5f, (float)height * 0.5f);
				delayTimer -= 1f;
				if (delayTimer <= 0f)
				{
					if (!checkCanHit || Collision.CanHit(codable.position, codable.width, codable.height, position, width, height))
					{
						Vector2 fireTarget = codable.Center + offset;
						float rot = BaseUtility.RotationTo(codable.Center, targetCenter);
						fireTarget = BaseUtility.RotateVector(codable.Center, fireTarget, rot);
						pID = BaseAI.FireProjectile(targetCenter, fireTarget, projType, damage, 0f, speed, 0, -1, default(Vector2));
					}
					delayTimer = delayTimerMax;
					if (codable is NPC)
					{
						((NPC)codable).netUpdate = true;
					}
				}
			}
			return pID;
		}

		public static int FireProjectile(Vector2 fireTarget, NPC npc, int projectileType, int damage, float knockback, float speedScalar = 1f, int soundGroup = 0, int sound = -1, int hostility = 0, int owner = -1)
		{
			if (Main.netMode != 2 && soundGroup != -1 && sound != -1)
			{
				Main.PlaySound(soundGroup, (int)npc.Center.X, (int)npc.Center.Y, sound, 1f, 0f);
			}
			if (Main.netMode != 1)
			{
				int result = BaseAI.FireProjectile(fireTarget, npc.Center, projectileType, damage, knockback, speedScalar, hostility, owner, default(Vector2));
				npc.netUpdate = true;
				return result;
			}
			return -1;
		}

		public static int FireProjectile(Vector2 fireTarget, Projectile p, int projectileType, int damage, float knockback, float speedScalar = 1f, int soundGroup = 0, int sound = -1, int hostility = 0, int owner = -1)
		{
			if (Main.netMode != 2 && soundGroup != -1 && sound != -1)
			{
				Main.PlaySound(soundGroup, (int)p.Center.X, (int)p.Center.Y, sound, 1f, 0f);
			}
			if (p.owner == Main.myPlayer)
			{
				return BaseAI.FireProjectile(fireTarget, p.Center, projectileType, damage, knockback, speedScalar, hostility, owner, default(Vector2));
			}
			return -1;
		}

		public static int FireProjectile(Vector2 fireTarget, Vector2 position, int projectileType, int damage, float knockback, float speedScalar = 1f, int hostility = 0, int owner = -1, Vector2 targetOffset = default(Vector2))
		{
			Vector2 rotVec = BaseUtility.RotateVector(position, position + new Vector2(speedScalar, 0f), BaseUtility.RotationTo(position, fireTarget));
			rotVec -= position;
			int projectileID = Projectile.NewProjectile(position.X, position.Y, rotVec.X, rotVec.Y, projectileType, damage, knockback, (owner != -1) ? owner : Main.myPlayer, 0f, 0f);
			Projectile proj = Main.projectile[projectileID];
			proj.velocity = rotVec;
			if (hostility != 0)
			{
				proj.friendly = (hostility == 1 || hostility == 2);
				proj.hostile = (hostility == -1 || hostility == 2);
				if (Main.netMode != 0)
				{
					MNet.SendBaseNetMessage(0, new object[]
					{
						proj.owner,
						proj.identity,
						proj.friendly,
						proj.hostile
					});
				}
			}
			proj.netUpdate2 = true;
			Main.projectile[projectileID] = proj;
			return projectileID;
		}

		public static void Look(Projectile p, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
		{
			BaseAI.Look(p, ref p.rotation, ref p.spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
		}

		public static void Look(NPC npc, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
		{
			BaseAI.Look(npc, ref npc.rotation, ref npc.spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
		}

		public static void Look(Entity c, ref float rotation, ref int spriteDirection, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
		{
			BaseAI.LookAt(c.position + c.velocity, c.position, ref rotation, ref spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
		}

		public static void LookAt(Vector2 lookTarget, Entity c, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
		{
			int spriteDirection = (c is NPC) ? ((NPC)c).spriteDirection : ((c is Projectile) ? ((Projectile)c).spriteDirection : 0);
			float rotation = (c is NPC) ? ((NPC)c).rotation : ((c is Projectile) ? ((Projectile)c).rotation : 0f);
			BaseAI.LookAt(lookTarget, c.Center, ref rotation, ref spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
			if (c is NPC)
			{
				((NPC)c).spriteDirection = spriteDirection;
				((NPC)c).rotation = rotation;
				return;
			}
			if (c is Projectile)
			{
				((Projectile)c).spriteDirection = spriteDirection;
				((Projectile)c).rotation = rotation;
			}
		}

		public static void LookAt(Vector2 lookTarget, Vector2 center, ref float rotation, ref int spriteDirection, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.075f, bool flipSpriteDir = false)
		{
			if (lookType == 0)
			{
				if (lookTarget.X > center.X)
				{
					spriteDirection = -1;
				}
				else
				{
					spriteDirection = 1;
				}
				if (flipSpriteDir)
				{
					spriteDirection *= -1;
				}
				float rotX = lookTarget.X - center.X;
				float rotY = lookTarget.Y - center.Y;
				rotation = -((float)Math.Atan2((double)rotX, (double)rotY) - 1.57f + rotAddon);
				if (spriteDirection == 1)
				{
					rotation -= 3.1415927f;
					return;
				}
			}
			else if (lookType == 1)
			{
				if (lookTarget.X > center.X)
				{
					spriteDirection = -1;
				}
				else
				{
					spriteDirection = 1;
				}
				if (flipSpriteDir)
				{
					spriteDirection *= -1;
					return;
				}
			}
			else
			{
				if (lookType == 2)
				{
					float rotX2 = lookTarget.X - center.X;
					float rotY2 = lookTarget.Y - center.Y;
					rotation = -((float)Math.Atan2((double)rotX2, (double)rotY2) - 1.57f + rotAddon);
					return;
				}
				if (lookType == 3 || lookType == 4)
				{
					int num = spriteDirection;
					if (lookType == 3 && lookTarget.X > center.X)
					{
						spriteDirection = -1;
					}
					else
					{
						spriteDirection = 1;
					}
					if (lookType == 3 && flipSpriteDir)
					{
						spriteDirection *= -1;
					}
					if (num != spriteDirection)
					{
						rotation += 3.1415927f * (float)spriteDirection;
					}
					float pi2 = 6.2831855f;
					float rotX3 = lookTarget.X - center.X;
					float rot = (float)Math.Atan2((double)(lookTarget.Y - center.Y), (double)rotX3) + rotAddon;
					if (spriteDirection == 1)
					{
						rot += 3.1415927f;
					}
					if (rot > pi2)
					{
						rot -= pi2;
					}
					else if (rot < 0f)
					{
						rot += pi2;
					}
					if (rotation > pi2)
					{
						rotation -= pi2;
					}
					else if (rotation < 0f)
					{
						rotation += pi2;
					}
					if (rotation < rot)
					{
						if ((double)(rot - rotation) > 3.1415927410125732)
						{
							rotation -= rotAmount;
						}
						else
						{
							rotation += rotAmount;
						}
					}
					else if (rotation > rot)
					{
						if ((double)(rotation - rot) > 3.1415927410125732)
						{
							rotation += rotAmount;
						}
						else
						{
							rotation -= rotAmount;
						}
					}
					if (rotation > rot - rotAmount && rotation < rot + rotAmount)
					{
						rotation = rot;
					}
				}
			}
		}

		public static void RotateTo(ref float rotation, float rotDestination, float rotAmount = 0.075f)
		{
			float pi2 = 6.2831855f;
			float rot = rotDestination;
			if (rot > pi2)
			{
				rot -= pi2;
			}
			else if (rot < 0f)
			{
				rot += pi2;
			}
			if (rotation > pi2)
			{
				rotation -= pi2;
			}
			else if (rotation < 0f)
			{
				rotation += pi2;
			}
			if (rotation < rot)
			{
				if ((double)(rot - rotation) > 3.1415927410125732)
				{
					rotation -= rotAmount;
				}
				else
				{
					rotation += rotAmount;
				}
			}
			else if (rotation > rot)
			{
				if ((double)(rotation - rot) > 3.1415927410125732)
				{
					rotation += rotAmount;
				}
				else
				{
					rotation -= rotAmount;
				}
			}
			if (rotation > rot - rotAmount && rotation < rot + rotAmount)
			{
				rotation = rot;
			}
		}

		public static Vector2 TraceTile(Vector2 start, float distance, float rotation, Vector2 ignoreTile, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
		{
			Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
			return BaseAI.Trace(start, end, ignoreTile, 1, npcCheck, tileCheck, playerCheck, 1f, ignorePlatforms);
		}

		public static Vector2 TracePlayer(Vector2 start, float distance, float rotation, int ignorePlayer, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
		{
			Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
			return BaseAI.Trace(start, end, ignorePlayer, 0, npcCheck, tileCheck, playerCheck, 1f, ignorePlatforms);
		}

		public static Vector2 TraceNPC(Vector2 start, float distance, float rotation, int ignoreNPC, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool ignorePlatforms = true)
		{
			Vector2 end = BaseUtility.RotateVector(start, start + new Vector2(distance, 0f), rotation);
			return BaseAI.Trace(start, end, ignoreNPC, 2, npcCheck, tileCheck, playerCheck, 1f, ignorePlatforms);
		}

		public static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, float Jump = 1f, bool ignorePlatforms = true)
		{
			return BaseAI.Trace(start, end, ignore, ignoreType, null, npcCheck, tileCheck, playerCheck, false, Jump, ignorePlatforms);
		}

		public static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, object dim, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool returnCenter = false, float Jump = 1f, bool ignorePlatforms = true)
		{
			object tileTypesToIgnore;
			if (!ignorePlatforms)
			{
				tileTypesToIgnore = null;
			}
			else
			{
				(tileTypesToIgnore = new int[1])[0] = 19;
			}
			return BaseAI.Trace(start, end, ignore, ignoreType, dim, npcCheck, tileCheck, playerCheck, returnCenter, tileTypesToIgnore, Jump);
		}

		public static Vector2 Trace(Vector2 start, Vector2 end, object ignore, int ignoreType, object dim, bool npcCheck = true, bool tileCheck = true, bool playerCheck = true, bool returnCenter = false, int[] tileTypesToIgnore = null, float Jump = 1f)
		{
			try
			{
				if (ignore == null)
				{
					return start;
				}
				if (dim == null)
				{
					dim = new Rectangle(0, 0, 1, 1);
				}
				if (start.X < 0f)
				{
					start.X = 0f;
				}
				if (start.X > (float)(Main.maxTilesX * 16))
				{
					start.X = (float)(Main.maxTilesX * 16);
				}
				if (start.Y < 0f)
				{
					start.Y = 0f;
				}
				if (start.Y > (float)(Main.maxTilesY * 16))
				{
					start.Y = (float)(Main.maxTilesY * 16);
				}
				if (end.X < 0f)
				{
					end.X = 0f;
				}
				if (end.X > (float)(Main.maxTilesX * 16))
				{
					end.X = (float)(Main.maxTilesX * 16);
				}
				if (end.Y < 0f)
				{
					end.Y = 0f;
				}
				if (end.Y > (float)(Main.maxTilesY * 16))
				{
					end.Y = (float)(Main.maxTilesY * 16);
				}
				Vector2 TC = new Vector2(1f, 1f);
				Vector2 Pstart = start;
				Vector2 Pend = end;
				Vector2 dir = Pend - Pstart;
				dir.Normalize();
				float length = Vector2.Distance(Pstart, Pend);
				for (float Way = 0f; Way < length; Way += Jump)
				{
					Vector2 v = Pstart + dir * Way + TC;
					Rectangle dimensions = (Rectangle)dim;
					Rectangle posRect = new Rectangle((int)v.X - ((dimensions.Width == 1) ? 0 : (dimensions.Width / 2)), (int)v.Y - ((dimensions.Height == 1) ? 0 : (dimensions.Height / 2)), dimensions.Width, dimensions.Height);
					if (tileCheck)
					{
						int vecX = (int)v.X / 16;
						int vecY = (int)v.Y / 16;
						Rectangle rect = new Rectangle((int)v.X, (int)v.Y, 16, 16);
						if (posRect.Intersects(rect))
						{
							Vector2 vec = (ignoreType == 1) ? ((Vector2)ignore) : new Vector2(-1f, -1f);
							if ((int)vec.X != vecX && (int)vec.Y != vecY)
							{
								Tile tile = Main.tile[vecX, vecY];
								if (tile != null && tile.nactive() && (tileTypesToIgnore == null || tileTypesToIgnore.Length == 0 || !BaseUtility.InArray(tileTypesToIgnore, (int)tile.type)) && Main.tileSolid[(int)tile.type])
								{
									return returnCenter ? new Vector2((float)(vecX * 16 + 8), (float)(vecY * 16 + 8)) : v;
								}
							}
						}
					}
					if (npcCheck)
					{
						int[] npcs = BaseAI.GetNPCs(v, -1, 5f, null);
						for (int i = 0; i < npcs.Length; i++)
						{
							NPC npc = Main.npc[npcs[i]];
							if (npc.active && npc.life > 0 && (ignoreType != 2 || npc.whoAmI != (int)ignore))
							{
								Rectangle npcRect = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
								if (posRect.Intersects(npcRect))
								{
									return returnCenter ? npc.Center : v;
								}
							}
						}
					}
					if (playerCheck)
					{
						int[] players = BaseAI.GetPlayers(v, 5f, null);
						for (int j = 0; j < players.Length; j++)
						{
							Player player = Main.player[players[j]];
							if (!player.dead && player.active && (ignoreType != 0 || player.whoAmI != (int)ignore))
							{
								Rectangle playerRect = new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height);
								if (posRect.Intersects(playerRect))
								{
									return returnCenter ? player.Center : v;
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				BaseUtility.LogFancy("Redemption~ TRACE ERROR:", e);
			}
			return end;
		}

		public static Vector2[] GetLinePoints(Vector2 start, Vector2 end, float jump = 1f)
		{
			Vector2 dir = end - start;
			dir.Normalize();
			float length = Vector2.Distance(start, end);
			float way = 0f;
			BaseUtility.RotationTo(start, end);
			List<Vector2> vList = new List<Vector2>();
			while (way < length)
			{
				vList.Add(start + dir * way);
				way += jump;
			}
			return vList.ToArray();
		}
	}
}
