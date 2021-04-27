using System.Text.Json;
using UnityEngine;

namespace CloudStructures.Converters
{
    /// <summary>
    /// Provides value converter using System.Text.Json.
    /// </summary>
    public sealed class SystemTextJsonConverter : IValueConverter
    {
        public static readonly JsonSerializerOptions OPTIONS
            = new JsonSerializerOptions() {
                IncludeFields = true
            };

        /// <summary>
        /// Serialize value to binary.
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] Serialize<T>(T value)
            => JsonSerializer.SerializeToUtf8Bytes(value, OPTIONS);


        /// <summary>
        /// Deserialize value from binary.
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] value) 
            => JsonSerializer.Deserialize<T>(value, OPTIONS);
    }
}
