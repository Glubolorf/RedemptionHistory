using System;
using Microsoft.Xna.Framework;

namespace Redemption.StructureHelper.ChestHelper.GUI
{
	internal class PoolRuleElement : ChestRuleElement
	{
		public PoolRuleElement() : base(new ChestRulePool())
		{
			this.color = Color.Purple;
			base.Append(this.countSetter);
		}

		public PoolRuleElement(ChestRule rule) : base(rule)
		{
			this.color = Color.Purple;
			this.countSetter.Value = (rule as ChestRulePool).itemsToGenerate;
			base.Append(this.countSetter);
		}

		public override void Update(GameTime gameTime)
		{
			if (this.countSetter.Value > this.rule.pool.Count)
			{
				this.countSetter.Value = this.rule.pool.Count;
			}
			(this.rule as ChestRulePool).itemsToGenerate = this.countSetter.Value;
			base.Update(gameTime);
		}

		private NumberSetter countSetter = new NumberSetter(1, "Amount to Pick", 100, "");
	}
}
