using System;
using UnityEngine;

[Serializable]
public struct BigNumber
{
	public double Mantissa => mantissa;
	public long Exponent => exponent;

	[SerializeField] private double mantissa;
	[SerializeField] private long exponent;

	public static BigNumber One => new BigNumber(1, 0);
	public static BigNumber Zero => new BigNumber(0, 0);

	public BigNumber(double mantissa, long exponent)
	{
		this.mantissa = mantissa;
		this.exponent = exponent;
	}

	public override string ToString()
	{
		return $"{mantissa:0.###}e{exponent}";
	}

	public static BigNumber operator +(BigNumber a, BigNumber b)
	{
		if (a.mantissa == 0)
			return b;

		if (b.mantissa == 0)
			return a;
		// 더 큰 수를 a로 만든다.
		if (a < b)
			(a, b) = (b, a);

		long exponentDifference = a.exponent - b.exponent;

		double adjustedMantissa =
			b.mantissa * Math.Pow(10, -exponentDifference);

		return new BigNumber(a.mantissa + adjustedMantissa, a.exponent);
	}
	public static BigNumber operator -(BigNumber a, BigNumber b)
	{
		return a + new BigNumber(-b.mantissa, b.exponent);
	}


	public static bool operator >(BigNumber a, BigNumber b)
	{
		if (a.mantissa >= 0 && b.mantissa < 0)
			return true;

		if (a.mantissa < 0 && b.mantissa >= 0)
			return false;

		// 둘 다 양수
		if (a.mantissa >= 0)
		{
			if (a.exponent != b.exponent)
				return a.exponent > b.exponent;

			return a.mantissa > b.mantissa;
		}

		// 둘 다 음수
		if (a.exponent != b.exponent)
			return a.exponent < b.exponent;

		return a.mantissa > b.mantissa;
	}

	public static BigNumber operator *(BigNumber a, BigNumber b)
	{
		if (a.mantissa == 0 || b.mantissa == 0)
			return Zero;

		return new BigNumber(
			a.mantissa * b.mantissa,
			a.exponent + b.exponent
		);
	}
	public static bool operator <(BigNumber a, BigNumber b)
	{
		return b > a;
	}

	public static BigNumber DoubleToBigNumber(double value)
	{
		if (value == 0)
		{
			return new BigNumber(0, 0);
		}

		long exponent = (long)Math.Floor(Math.Log10(Math.Abs(value)));
		double mantissa = value / Math.Pow(10, exponent);

		return new BigNumber(mantissa, exponent);
	}
}