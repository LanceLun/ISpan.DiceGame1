using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISpan.DiceGame
{
	public class Dice:IComparable<Dice>
	{
		/// <summary>
		/// 點數,只能1~6
		/// </summary>
		public int Value { get; private set; }
		public override string ToString()
		{
			return this.Value.ToString();
		}
		public static Dice Roll(IRandomProvider provider = null)
		{
			provider = provider ?? new DefaultRandomProvider();

			return new Dice { Value = provider.Next(1, 7) };
		}

		public int CompareTo(Dice other)
		{
			return this.Value.CompareTo(other.Value);
		}

		public interface IRandomProvider
		{
			int Next(int min, int max);
		}
		public class DefaultRandomProvider : IRandomProvider
		{
			public int Next(int min, int max)
			{
				int seed=Guid.NewGuid().GetHashCode();
				return new Random(seed).Next(min, max);
			}
		}


	}
}
