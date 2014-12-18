#region File and License Information
/*
<File>
	<License Type="BSD">
		Copyright © 2009 - 2012, Daniel Vaughan. All rights reserved.
	
		This file is part of Calcium (http://calciumsdk.net).

		Redistribution and use in source and binary forms, with or without
		modification, are permitted provided that the following conditions are met:
			* Redistributions of source code must retain the above copyright
			  notice, this list of conditions and the following disclaimer.
			* Redistributions in binary form must reproduce the above copyright
			  notice, this list of conditions and the following disclaimer in the
			  documentation and/or other materials provided with the distribution.
			* Neither the name of the <organization> nor the
			  names of its contributors may be used to endorse or promote products
			  derived from this software without specific prior written permission.

		THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
		ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
		WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
		DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
		DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
		(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
		LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
		ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
		(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
		SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
	</License>
	<Owner Name="Daniel Vaughan" Email="danielvaughan@outcoder.com" />
	<CreationDate>2009-03-22 14:02:43Z</CreationDate>
</File>
*/
#endregion

using System;

namespace Work.UndoManager
{
	public static class ArgumentValidator
	{
		/// <summary>
		/// Utility class for validating method parameters.
		/// </summary>
		/// 		/// <summary>
		/// Ensures the specified value is not null.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to test.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <returns>The specified value.</returns>
		/// <exception cref="ArgumentNullException">Occurs if the specified value 
		/// is <code>null</code>.</exception>
		/// <example>
		/// public UIElementAdapter(UIElement uiElement)
		/// {
		/// 	this.uiElement = ArgumentValidator.AssertNotNull(uiElement, "uiElement");	
		/// }
		/// </example>
		public static T AssertNotNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}

			return value;
		}

		/// <summary>
		/// Ensures the specified value is not <code>null</code> or empty (a zero length string).
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <returns>The specified value.</returns>
		/// <exception cref="ArgumentNullException">Occurs if the specified value 
		/// is <code>null</code> or empty (a zero length string).</exception>
		/// <example>
		/// public DoSomething(string message)
		/// {
		/// 	this.message = ArgumentValidator.AssertNotNullOrEmpty(message, "message");	
		/// }
		/// </example>
		public static string AssertNotNullOrEmpty(string value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}

			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(
					"Argument should not be an empty string.", parameterName); /* TODO: Make localizable resource. */
			}

			return value;
		}

		/// <summary>
		/// Ensures the specified value is not <code>null</code> 
		/// or white space.
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <returns>The specified value.</returns>
		/// <exception cref="ArgumentNullException">Occurs if the specified value 
		/// is <code>null</code> or consists of only white space.</exception>
		public static string AssertNotNullOrWhiteSpace(string value, string parameterName)
		{
#if WINDOWS_PHONE71
			if (string.IsNullOrWhiteSpace(value))
			{
				/* TODO: Make localizable resource. */
				throw new ArgumentException(
					"Parameter should not be null or white space.", parameterName);
			}
#else
			if (value == null || value.Trim().Length == 0)
			{
				/* TODO: Make localizable resource. */
				throw new ArgumentException(
					"Parameter should not be null or white space.", parameterName);
			}
#endif
			return value;
		}

		/// <summary>
		/// Asserts that the specified value is not an empty guid.
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <param name="parameterName">The name of the member.</param>
		/// <returns>The specified value.</returns>
		/// <exception cref="ArgumentNullException">Occurs if the specified value 
		/// is an empty guid. That is, if <c>value</c> equals <c>Guid.Empty</c>.</exception>
		public static Guid AssertNotEmpty(Guid value, string parameterName)
		{
			if (value == Guid.Empty)
			{
				/* TODO: Make localizable resource. */
				throw new ArgumentException(
					"Parameter should not be an empty Guid.", parameterName);
			}

			return value;
		}

		/// <summary>
		/// Asserts that the specified value is greater 
		/// than the specified expected value.
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <param name="expected">The number which must be greater than the value.</param>
		/// <param name="parameterName">The name of the member.</param>
		/// <returns>The specified value.</returns>
		/// <exception cref="ArgumentNullException">Occurs if the specified value 
		/// is not greater than the expected value.</exception>
		public static int AssertGreaterThan(int value, int expected, string parameterName)
		{
			if (value > expected)
			{
				return value;
			}

			throw new ArgumentOutOfRangeException(
				"Argument should be greater than " + expected, parameterName); /* TODO: Make localizable resource. */
		}

		public static double AssertGreaterThan(double value, double expected, string parameterName)
		{
			if (value > expected)
			{
				return value;
			}

			throw new ArgumentOutOfRangeException(
				"Argument should be greater than " + expected, parameterName); /* TODO: Make localizable resource. */
		}

		public static int AssertGreaterThanOrEqual(int value, int expected, string parameterName)
		{
			if (value >= expected)
			{
				return value;
			}

			throw new ArgumentOutOfRangeException(
				"Argument should be greater than or equal to " + expected, parameterName); /* TODO: Make localizable resource. */
		}

		public static double AssertGreaterThanOrEqual(double value, double expected, string parameterName)
		{
			if (value >= expected)
			{
				return value;
			}

			throw new ArgumentOutOfRangeException(
				"Argument should be greater than or equal to " + expected, parameterName); /* TODO: Make localizable resource. */
		}

		public static int AssertLessThan(int value, int expected, string parameterName)
		{
			if (value >= expected)
			{
				throw new ArgumentOutOfRangeException(
					"Argument should be less than " + expected, parameterName); /* TODO: Make localizable resource. */
			}

			return value;
		}

		public static double AssertLessThan(double value, double expected, string parameterName)
		{
			if (value >= expected)
			{
				throw new ArgumentOutOfRangeException(
					"Argument should be less than " + expected, parameterName); /* TODO: Make localizable resource. */
			}

			return value;
		}

		public static int AssertLessThanOrEqual(int value, int expected, string parameterName)
		{
			if (value > expected)
			{
				throw new ArgumentOutOfRangeException(
					"Argument should be less than or equal to " + expected, parameterName); /* TODO: Make localizable resource. */
			}

			return value;
		}

		public static double AssertLessThanOrEqual(double value, double expected, string parameterName)
		{
			if (value > expected)
			{
				throw new ArgumentOutOfRangeException(
					"Argument should be less than or equal to " + expected, parameterName); /* TODO: Make localizable resource. */
			}

			return value;
		}

		/// <summary>
		/// Ensures the specified value is not <code>null</code> 
		/// and that it is of the specified type.
		/// </summary>
		/// <param name="value">The value to test.</param> 
		/// <param name="parameterName">The name of the parameter.</param>
		/// <returns>The value to test.</returns>
		/// <exception cref="ArgumentNullException">Occurs if the specified value 
		/// is <code>null</code> or of type not assignable from the specified type.</exception>
		/// <example>
		/// public DoSomething(object message)
		/// {
		/// 	this.message = ArgumentValidator.AssertNotNullAndOfType&lt;string&gt;(message, "message");	
		/// }
		/// </example>
		public static T AssertNotNullAndOfType<T>(object value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}

			var result = value as T;
			if (result == null)
			{
				throw new ArgumentException(string.Format(
					"Expected argument of type " + typeof(T) + ", but was " + value.GetType(), typeof(T), value.GetType()),
					parameterName);
			}
			return result;
		}

		//		/// <summary>
		//		/// Ensures the specified value is not null.
		//		/// </summary>
		//		/// <typeparam name="T">The type of the value.</typeparam>
		//		/// <param name="value">The value to test.</param>
		//		/// <param name="expression">The expression containing 
		//		/// from which the member name is derived.</param>
		//		/// <returns>The specified value.</returns>
		//		/// <exception cref="ArgumentNullException">Occurs if the specified value 
		//		/// is <code>null</code>.</exception>
		//		/// <example>
		//		/// public UIElementAdapter(UIElement uiElement)
		//		/// {
		//		/// 	this.uiElement = ArgumentValidator.AssertNotNull(uiElement, () => uiElement);	
		//		/// }
		//		/// </example>
		//		public static T AssertNotNull<T>(T value, Expression<Func<T>> expression) where T : class
		//		{
		//			return AssertNotNull(value, ReflectionUtility.GetMember(expression).Name);
		//		}

		//		/// <summary>
		//		/// Ensures the specified value is not <code>null</code> or empty (a zero length string).
		//		/// </summary>
		//		/// <param name="value">The value to test.</param>
		//		/// <param name="expression">The expression containing 
		//		/// from which the member name is derived.</param>
		//		/// <returns>The specified value.</returns>
		//		/// <exception cref="ArgumentNullException">Occurs if the specified value 
		//		/// is <code>null</code> or empty (a zero length string).</exception>
		//		/// <example>
		//		/// public DoSomething(string message)
		//		/// {
		//		/// 	this.message = ArgumentValidator.AssertNotNullOrEmpty(message, "message");	
		//		/// }
		//		/// </example>
		//		public static string AssertNotNullOrEmpty<T>(string value, Expression<Func<T>> expression)
		//		{
		//			return AssertNotNullOrEmpty(value, ReflectionUtility.GetMember(expression).Name);
		//		}

		//		public static int AssertLessThan(int value, int expected, Expression<Func<int>> expression)
		//		{
		//			return AssertLessThan(value, expected, ReflectionUtility.GetMember(expression).Name);
		//		}
		//
		//		public static double AssertLessThan(double value, double expected, Expression<Func<double>> expression)
		//		{
		//			return AssertLessThan(value, expected, ReflectionUtility.GetMember(expression).Name);
		//		}
		//
		//		public static int AssertLessThanOrEqual(int value, int expected, Expression<Func<int>> expression)
		//		{
		//			return AssertLessThanOrEqual(value, expected, ReflectionUtility.GetMember(expression).Name);
		//		}
		//
		//		public static double AssertLessThanOrEqual(double value, double expected, Expression<Func<double>> expression)
		//		{
		//			return AssertLessThanOrEqual(value, expected, ReflectionUtility.GetMember(expression).Name);
		//		}

		//		/// <summary>
		//		/// Asserts that the specified value is greater 
		//		/// than the specified expected value.
		//		/// </summary>
		//		/// <param name="value">The value to test.</param>
		//		/// <param name="expected">The number which must be greater than the value.</param>
		//		/// <param name="expression">The member expression containing
		//		/// the name of the parameter derived.</param>
		//		/// <returns>The specified value.</returns>
		//		/// <exception cref="ArgumentNullException">Occurs if the specified value 
		//		/// is not greater than the expected value.</exception>
		//		public static int AssertGreaterThan(int value, int expected, Expression<Func<int>> expression)
		//		{
		//			return AssertGreaterThan(value, expected, ReflectionUtility.GetMember(expression).Name);
		//		}
		//
		//		public static double AssertGreaterThan(double value, double expected, Expression<Func<double>> expression)
		//		{
		//			return AssertGreaterThan(value, expected, ReflectionUtility.GetMember(expression).Name);
		//		}
		//
		//		public static int AssertGreaterThanOrEqual(int value, int expected, Expression<Func<int>> expression)
		//		{
		//			return AssertGreaterThanOrEqual(value, expected, ReflectionUtility.GetMember(expression).Name);
		//		}
		//
		//		public static double AssertGreaterThanOrEqual(double value, double expected, Expression<Func<double>> expression)
		//		{
		//			return AssertGreaterThanOrEqual(value, expected, ReflectionUtility.GetMember(expression).Name);
		//		}

		//		public static int AssertTrue(int value, int mustBeGreaterThan, 
		//			Expression<Func<int, int, bool>> operation, Expression<Func<int>> memberNameExpression)
		//		{
		//			operation.Compile().Invoke(value, mustBeGreaterThan);
		////			if (!operation(value, mustBeGreaterThan))
		////			{
		////				
		////			}
		//		}
	}
}