// Copyright (c) 2008, Andre Loker <mail@andreloker.de> 
// Permission to use, copy, modify, and/or distribute this software for any 
// purpose with or without fee is hereby granted, provided that the above 
// copyright notice and this permission notice appear in all copies. 
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES 
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR 
// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES 
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN 
// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF 
// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE. 
namespace System.Data
{
    /// <summary> 
    /// Extension methods for <see cref="IDataRecord"/> 
    /// </summary> 
    [System.Diagnostics.DebuggerStepThrough()]
    public static class DataRecordExtensions
    {
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Boolean"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Boolean"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static bool GetBoolean(this IDataRecord dataRecord, String column)
        {
            return Convert.ToBoolean(dataRecord[column]);
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Byte"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Byte"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static byte GetByte(this IDataRecord dataRecord, String column)
        {
            return (byte)dataRecord[column];
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Char"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Char"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static char GetChar(this IDataRecord dataRecord, String column)
        {
            return (char)dataRecord[column];
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="DateTime"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="DateTime"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static DateTime GetDateTime(this IDataRecord dataRecord, String column)
        {
            return (DateTime)dataRecord[column];
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Decimal"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Decimal"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static decimal GetDecimal(this IDataRecord dataRecord, String column)
        {
            return (decimal)dataRecord[column];
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Double"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Double"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static double GetDouble(this IDataRecord dataRecord, String column)
        {
            return Convert.ToDouble(dataRecord[column]);
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Single"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Single"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static float GetFloat(this IDataRecord dataRecord, String column)
        {
            return (float)dataRecord[column];
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Guid"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Guid"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static Guid GetGuid(this IDataRecord dataRecord, String column)
        {
            return (Guid)dataRecord[column];
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Int16"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Int16"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static short GetInt16(this IDataRecord dataRecord, String column)
        {
            return Convert.ToInt16(dataRecord[column]);
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Int32"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Int32"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static int GetInt32(this IDataRecord dataRecord, String column)
        {
            return Convert.ToInt32(dataRecord[column]);
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="Int64"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Int64"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static long GetInt64(this IDataRecord dataRecord, String column)
        {
            return Convert.ToInt64(dataRecord[column]);
        }
        /// <summary> 
        /// Casts the value of the given <paramref name="column"/> to a <see cref="String"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value of the <paramref name="column"/> cast to <see cref="Boolean"/> 
        /// </returns> 
        /// <remarks> 
        /// No null check or type cast is performed. If the value in the column does not have the correct 
        /// type or is <c>null</c>; a <see cref="InvalidCastException"/> will be thrown. 
        /// </remarks> 
        public static string GetString(this IDataRecord dataRecord, String column)
        {
            return Convert.ToString(dataRecord[column]);
        }
        /// <summary> 
        /// Gets the value in the given <paramref name="column"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">Name of the column.</param> 
        /// <returns> 
        /// The value found in the given <paramref name="column"/> of the current row. 
        /// </returns> 
        /// <remarks> 
        /// This method is equivalent to <see cref="IDataRecord.Item(string)("/> and only exists for 
        /// consistency reasons. 
        /// </remarks> 
        public static object GetValue(this IDataRecord dataRecord, String column)
        {
            return dataRecord[column];
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Boolean}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Boolean}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="Boolean"/> if possible. 
        /// </remarks> 
        public static bool? GetSafeBoolean(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (bool?)null : Convert.ToBoolean(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Byte}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Byte}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="Byte"/> if possible. 
        /// </remarks> 
        public static byte? GetSafeByte(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (byte?)null : Convert.ToByte(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Char}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Char}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="Char"/> if possible. 
        /// </remarks> 
        public static char? GetSafeChar(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (char?)null : Convert.ToChar(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{DateTime}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{DateTime}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="DateTime"/> if possible. 
        /// </remarks> 
        public static DateTime? GetSafeDateTime(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (DateTime?)null : Convert.ToDateTime(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Decimal}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Decimal}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="Decimal"/> if possible. 
        /// </remarks> 
        public static decimal? GetSafeDecimal(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (decimal?)null : Convert.ToDecimal(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Double}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Double}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="Double"/> if possible. 
        /// </remarks> 
        public static double? GetSafeDouble(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (double?)null : Convert.ToDouble(val);
        }
        public static double? GetSafeDouble(this IDataRecord dataRecord, int column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (double?)null : Convert.ToDouble(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Single}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Single}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="Single"/> if possible. 
        /// </remarks> 
        public static float? GetSafeFloat(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (float?)null : Convert.ToSingle(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Guid}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Guid}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// cast to a <see cref="Guid"/>. 
        /// </remarks> 
        public static Guid? GetSafeGuid(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (Guid?)null : (Guid)val;
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Int16}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Int16}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="Int16"/> if possible. 
        /// </remarks> 
        public static short? GetSafeInt16(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (short?)null : Convert.ToInt16(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Int32}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Int32}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="Int32"/> if possible. 
        /// </remarks> 
        public static int? GetSafeInt32(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (int?)null : Convert.ToInt32(val);
        }
        public static int? GetSafeInt32(this IDataRecord dataRecord, int column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (int?)null : Convert.ToInt32(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="Nullable{Int64}"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="Nullable{Int64}"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="Int64"/> if possible. 
        /// </remarks> 
        public static long? GetSafeInt64(this IDataRecord dataRecord, String column)
        {
            var val = dataRecord[column];
            return val is DBNull ? (long?)null : Convert.ToInt64(val);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> converted to <see cref="String"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns>The value in the given <paramref name="column"/> converted to <see cref="String"/> </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// converted to a <see cref="String"/> if possible. 
        /// </remarks> 
        public static string GetSafeString(this IDataRecord dataRecord, String column)
        {
            var value = dataRecord[column];
            return value is DBNull ? null : Convert.ToString(value);
        }
        public static string GetSafeString(this IDataRecord dataRecord, int column)
        {
            var value = dataRecord[column];
            return value is DBNull ? null : Convert.ToString(value);
        }
        /// <summary> 
        /// Returns the value in the given <paramref name="column"/> 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns> 
        /// The value in the given <paramref name="column"/> 
        /// </returns> 
        /// <remarks> 
        /// If the value in the column is <see cref="DBNull"/>, <c>null</c> is returned. Otherwise the value is 
        /// returned as it is found in the <see cref="IDataRecord"/>. 
        /// </remarks> 
        public static object GetSafeValue(this IDataRecord dataRecord, String column)
        {
            var value = dataRecord[column];
            return value is DBNull ? null : value;
        }
        /// <summary> 
        /// Return whether the specified field is set to null. 
        /// </summary> 
        /// <param name="dataRecord">The data record.</param> 
        /// <param name="column">The column.</param> 
        /// <returns> 
        /// <c>true</c> if the specified field is set to null. Otherwise, <c>false</c>. 
        /// </returns> 
        public static bool IsDBNull(this IDataRecord dataRecord, String column)
        {
            return dataRecord[column] is DBNull;
        }
    }
}
