namespace ApiCore.Common.Extensions;

public static class ObjectExtensions {
	public static T NotNull<T>(this T obj, string name) {
		if (obj == null) {
			throw new ArgumentException(name);
		}
		return obj;
	}
}
