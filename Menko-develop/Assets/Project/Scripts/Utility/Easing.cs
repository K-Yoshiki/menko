using UnityEngine;

public static class Easing
{
	#region Linear

	/// <summary>
	/// Simple lenear. No easing.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float Linear(float a, float b, float duration, float time)
	{
		return b * time / duration + a;
	}

	#endregion

	#region Exponential

	/// <summary>
	/// Exponential easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float ExpoOut(float a, float b, float duration, float time)
	{
		return (time == duration) ? a + b : b * (-Mathf.Pow(2, -10 * time / duration) + 1) + a;
	}

	/// <summary>
	/// Exponential easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float ExpoIn(float a, float b, float duration, float time)
	{
		return (time == 0) ? a : b * Mathf.Pow(2, 10 * (time / duration - 1)) + a;
	}

	/// <summary>
	/// Exponential easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float ExpoInOut(float a, float b, float duration, float time)
	{
		if (time == 0) return a;
		if (time == duration) return a + b;
		if ((time /= duration / 2) < 1) return b / 2 * Mathf.Pow(2, 10 * (time - 1)) + a;
		return b / 2 * (-Mathf.Pow(2, -10 * --time) + 2) + a;
	}

	/// <summary>
	/// Exponential easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float ExpoOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2) return ExpoOut(time * 2, a, b / 2, duration);
		return ExpoIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion

	#region Circular

	/// <summary>
	/// Circular (sqrt(1-t^2)) easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float CircOut(float a, float b, float duration, float time)
	{
		return b * Mathf.Sqrt(1 - (time = time / duration - 1) * time) + a;
	}

	/// <summary>
	/// Circular (sqrt(1-t^2)) easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float CircIn(float a, float b, float duration, float time)
	{
		return -b * (Mathf.Sqrt(1 - (time /= duration) * time) - 1) + a;
	}

	/// <summary>
	/// Circular (sqrt(1-t^2)) easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float CircInOut(float a, float b, float duration, float time)
	{
		if ((time /= duration / 2) < 1)
			return -b / 2 * (Mathf.Sqrt(1 - time * time) - 1) + a;

		return b / 2 * (Mathf.Sqrt(1 - (time -= 2) * time) + 1) + a;
	}

	/// <summary>
	/// Circular (sqrt(1-t^2)) easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float CircOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2) return CircOut(time * 2, a, b / 2, duration);
		return CircIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion

	#region Quad

	/// <summary>
	/// Quadratic (t^2) easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuadOut(float a, float b, float duration, float time)
	{
		return -b * (time /= duration) * (time - 2) + a;
	}

	/// <summary>
	/// Quadratic (t^2) easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuadIn(float a, float b, float duration, float time)
	{
		return b * (time /= duration) * time + a;
	}

	/// <summary>
	/// Quadratic (t^2) easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuadInOut(float a, float b, float duration, float time)
	{
		if ((time /= duration / 2) < 1)
			return b / 2 * time * time + a;

		return -b / 2 * ((--time) * (time - 2) - 1) + a;
	}

	/// <summary>
	/// Quadratic (t^2) easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuadOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2)
			return QuadOut(time * 2, a, b / 2, duration);

		return QuadIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion

	#region Sine

	/// <summary>
	/// Sinusoidal (sin(t)) easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float SineEaseOut(float a, float b, float duration, float time)
	{
		return b * Mathf.Sin(time / duration * (Mathf.PI / 2)) + a;
	}

	/// <summary>
	/// Sinusoidal (sin(t)) easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float SineEaseIn(float a, float b, float duration, float time)
	{
		return -b * Mathf.Cos(time / duration * (Mathf.PI / 2)) + b + a;
	}

	/// <summary>
	/// Sinusoidal (sin(t)) easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float SineEaseInOut(float a, float b, float duration, float time)
	{
		if ((time /= duration / 2) < 1)
			return b / 2 * (Mathf.Sin(Mathf.PI * time / 2)) + a;

		return -b / 2 * (Mathf.Cos(Mathf.PI * --time / 2) - 2) + a;
	}

	/// <summary>
	/// Sinusoidal (sin(t)) easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float SineEaseOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2)
			return SineEaseOut(time * 2, a, b / 2, duration);

		return SineEaseIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion

	#region Cubic

	/// <summary>
	/// Cubic (t^3) easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float CubicOut(float a, float b, float duration, float time)
	{
		return b * ((time = time / duration - 1) * time * time + 1) + a;
	}

	/// <summary>
	/// Cubic (t^3) easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float CubicIn(float a, float b, float duration, float time)
	{
		return b * (time /= duration) * time * time + a;
	}

	/// <summary>
	/// Cubic (t^3) easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float CubicInOut(float a, float b, float duration, float time)
	{
		if ((time /= duration / 2) < 1)
			return b / 2 * time * time * time + a;

		return b / 2 * ((time -= 2) * time * time + 2) + a;
	}

	/// <summary>
	/// Cubic (t^3) easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float CubicOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2)
			return CubicOut(time * 2, a, b / 2, duration);

		return CubicIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion

	#region Quartic

	/// <summary>
	/// Quartic (t^4) easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuartOut(float a, float b, float duration, float time)
	{
		return -b * ((time = time / duration - 1) * time * time * time - 1) + a;
	}

	/// <summary>
	/// Quartic (t^4) easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuartIn(float a, float b, float duration, float time)
	{
		return b * (time /= duration) * time * time * time + a;
	}

	/// <summary>
	/// Quartic (t^4) easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuartInOut(float a, float b, float duration, float time)
	{
		if ((time /= duration / 2) < 1)
			return b / 2 * time * time * time * time + a;

		return -b / 2 * ((time -= 2) * time * time * time - 2) + a;
	}

	/// <summary>
	/// Quartic (t^4) easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuartOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2)
			return QuartOut(time * 2, a, b / 2, duration);

		return QuartIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion

	#region Quintic

	/// <summary>
	/// Quintic (t^5) easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuintOut(float a, float b, float duration, float time)
	{
		return b * ((time = time / duration - 1) * time * time * time * time + 1) + a;
	}

	/// <summary>
	/// Quintic (t^5) easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuintIn(float a, float b, float duration, float time)
	{
		return b * (time /= duration) * time * time * time * time + a;
	}

	/// <summary>
	/// Quintic (t^5) easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuintInOut(float a, float b, float duration, float time)
	{
		if ((time /= duration / 2) < 1)
			return b / 2 * time * time * time * time * time + a;
		return b / 2 * ((time -= 2) * time * time * time * time + 2) + a;
	}

	/// <summary>
	/// Quintic (t^5) easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float QuintOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2)
			return QuintOut(time * 2, a, b / 2, duration);
		return QuintIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion

	#region Elastic

	/// <summary>
	/// Elastic (exponentially decaying sine wave) easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float ElasticOut(float a, float b, float duration, float time)
	{
		if ((time /= duration) == 1)
			return a + b;

		float p = duration * 0.3f;
		float s = p / 4;

		return (b * Mathf.Pow(2, -10 * time) * Mathf.Sin((time * duration - s) * (2 * Mathf.PI) / p) + b + a);
	}

	/// <summary>
	/// Elastic (exponentially decaying sine wave) easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float ElasticIn(float a, float b, float duration, float time)
	{
		if ((time /= duration) == 1)
			return a + b;

		float p = duration * 0.3f;
		float s = p / 4;

		return -(b * Mathf.Pow(2, 10 * (time -= 1)) * Mathf.Sin((time * duration - s) * (2 * Mathf.PI) / p)) + a;
	}

	/// <summary>
	/// Elastic (exponentially decaying sine wave) easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float ElasticInOut(float a, float b, float duration, float time)
	{
		if ((time /= duration / 2) == 2)
			return a + b;

		float p = duration * (0.3f * 1.5f);
		float s = p / 4;

		if (time < 1)
			return -0.5f * (b * Mathf.Pow(2, 10 * (time -= 1)) * Mathf.Sin((time * duration - s) * (2 * Mathf.PI) / p)) + a;
		else
			return b * Mathf.Pow(2, -10 * (time -= 1)) * Mathf.Sin((time * duration - s) * (2 * Mathf.PI) / p) * 0.5f + b + a;
	}

	/// <summary>
	/// Elastic (exponentially decaying sine wave) easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float ElasticOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2)
			return ElasticOut(time * 2, a, b / 2, duration);
		else
			return ElasticIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion

	#region Bounce

	/// <summary>
	/// Bounce (exponentially decaying parabolic bounce) easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float BounceOut(float a, float b, float duration, float time)
	{
		if ((time /= duration) < (1.0f / 2.75f))
			return b * (7.5625f * time * time) + a;
		else if (time < (2.0f / 2.75f))
			return b * (7.5625f * (time -= (1.5f / 2.75f)) * time + 0.75f) + a;
		else if (time < (2.5f / 2.75f))
			return b * (7.5625f * (time -= (2.25f / 2.75f)) * time + 0.9375f) + a;
		else
			return b * (7.5625f * (time -= (2.625f / 2.75f)) * time + 0.984375f) + a;
	}

	/// <summary>
	/// Bounce (exponentially decaying parabolic bounce) easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float BounceIn(float a, float b, float duration, float time)
	{
		return b - BounceOut(duration - time, 0, b, duration) + a;
	}

	/// <summary>
	/// Bounce (exponentially decaying parabolic bounce) easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float BounceInOut(float a, float b, float duration, float time)
	{
		if (time < duration / 2)
			return BounceIn(time * 2, 0, b, duration) * 0.5f + a;
		else
			return BounceOut(time * 2 - duration, 0, b, duration) * 0.5f + b * 0.5f + a;
	}

	/// <summary>
	/// Bounce (exponentially decaying parabolic bounce) easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float BounceOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2)
			return BounceOut(time * 2, a, b / 2, duration);
		else
			return BounceIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion

	#region Back

	/// <summary>
	/// Back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float BackEaseOut(float a, float b, float duration, float time)
	{
		return b * ((time = time / duration - 1) * time * ((1.70158f + 1) * time + 1.70158f) + 1) + a;
	}

	/// <summary>
	/// Back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float BackEaseIn(float a, float b, float duration, float time)
	{
		return b * (time /= duration) * time * ((1.70158f + 1) * time - 1.70158f) + a;
	}

	/// <summary>
	/// Back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float BackEaseInOut(float a, float b, float duration, float time)
	{
		float s = 1.70158f;
		if ((time /= duration / 2) < 1)
			return b / 2 * (time * time * (((s *= (1.525f)) + 1) * time - s)) + a;
		else
			return b / 2 * ((time -= 2) * time * (((s *= (1.525f)) + 1) * time + s) + 2) + a;
	}

	/// <summary>
	/// Back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in.
	/// </summary>
	/// <param name="a">Start value.</param>
	/// <param name="b">End value.</param>
	/// <param name="duration">Duration.</param>
	/// <param name="time">Current time in sec.</param>
	/// <returns>The correct value.</returns>
	public static float BackEaseOutIn(float a, float b, float duration, float time)
	{
		if (time < duration / 2)
			return BackEaseOut(time * 2, a, b / 2, duration);
		else
			return BackEaseIn((time * 2) - duration, a + b / 2, b / 2, duration);
	}

	#endregion
}