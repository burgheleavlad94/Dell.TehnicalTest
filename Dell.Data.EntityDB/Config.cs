using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dell.Data
{
    public class Config
    {
        /// <summary>
        /// Gets or sets information about currently used and stored <see cref="ConnectionInfo"/> from configuration file.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Connections Connections { get; set; }
    }

    /// <summary>
    /// Holds configuration information about current used and all <see cref="ConnectionInfo"/> collection stored in configuration file.
    /// </summary>
    public class Connections
    {
        /// <summary>
        /// Gets or sets default <see cref="ConnectionInfo"/> name used when none is specified by client.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Required = Required.DisallowNull)]
        public string Default { get; set; }

        /// <summary>
        /// A collection of <see cref="ConnectionInfo"/> from configuration file. 
        /// </summary>
        [JsonProperty(PropertyName = null)]
        public HashSet<ConnectionInfo> Items { get; set; } = new HashSet<ConnectionInfo>();

        /// <summary>
        /// Gets <see cref="ConnectionInfo"/> regarding <paramref name="key"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ConnectionInfo this[string key]
        {
            get => Items.FirstOrDefault(e => e.Name == key);
            //set => Items[key] = value;  
        }

        /// <summary>
        /// Gets default <see cref="ConnectionInfo"/>.
        /// </summary>
        /// <returns></returns>
        public ConnectionInfo GetDefaultConnection() => this[this.Default];
    }

    /// <summary>
    /// Defines a connection string.
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// Connection string name used to identify an item in collection
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Data repository privider.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Required = Required.AllowNull)]
        public string Provider { get; set; }

        /// <summary>
        /// String used to connect to data repository.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, Required = Required.Always)]
        public string ConnectionString { get; set; }
    }
}
