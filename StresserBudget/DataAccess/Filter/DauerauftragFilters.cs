using System;
using DataAccess;

namespace DataAccess.Filter
{
    /// <summary>
    /// An instance of this class is a filter to access rows from [Dauerauftrag].
    /// </summary>
    public class DauerauftragFilter
    {
        /// <summary>
        /// Gets or sets the flag which controls how the criterias will be combined.
        /// </summary>
        public OryxFilterCombinationEnum ComboMode { get; set; }
        
        // IDBudget
        /// <summary>
        /// IDBudget equals this (data = this). Null to ignore.
        /// </summary>
        public int? IDBudget { get; set; }
        
        // Bezeichnung
        /// <summary>
        /// Bezeichnung equals this (data = this). Null to ignore.
        /// </summary>
        public string Bezeichnung { get; set; }
        /// <summary>
        /// Bezeichnung is like this (data like this). Null to ignore.
        /// </summary>
        public string BezeichnungPattern { get; set; }
        
        // GueltigAb
        /// <summary>
        /// GueltigAb equals this (data = this). Null to ignore.
        /// </summary>
        public DateTime? GueltigAb { get; set; }
        /// <summary>
        /// Maximum without value for GueltigAb (data &lt; this). Null to ignore.
        /// </summary>
        public DateTime? GueltigAbMaxWo { get; set; }
        /// <summary>
        /// Minimum without value for GueltigAb (data &gt; this). Null to ignore.
        /// </summary>
        public DateTime? GueltigAbMinWo { get; set; }
        /// <summary>
        /// Maximum with value included for GueltigAb (data &lt;= this). Null to ignore.
        /// </summary>
        public DateTime? GueltigAbMax { get; set; }
        /// <summary>
        /// Minimum with value included for GueltigAb (data &gt;= this). Null to ignore.
        /// </summary>
        public DateTime? GueltigAbMin { get; set; }
        
        // GueltigBis
        /// <summary>
        /// GueltigBis equals this (data = this). Null to ignore.
        /// </summary>
        public DateTime? GueltigBis { get; set; }
        /// <summary>
        /// Maximum without value for GueltigBis (data &lt; this). Null to ignore.
        /// </summary>
        public DateTime? GueltigBisMaxWo { get; set; }
        /// <summary>
        /// Minimum without value for GueltigBis (data &gt; this). Null to ignore.
        /// </summary>
        public DateTime? GueltigBisMinWo { get; set; }
        /// <summary>
        /// Maximum with value included for GueltigBis (data &lt;= this). Null to ignore.
        /// </summary>
        public DateTime? GueltigBisMax { get; set; }
        /// <summary>
        /// Minimum with value included for GueltigBis (data &gt;= this). Null to ignore.
        /// </summary>
        public DateTime? GueltigBisMin { get; set; }
        
        // Lauftag
        /// <summary>
        /// Lauftag equals this (data = this). Null to ignore.
        /// </summary>
        public int? Lauftag { get; set; }
        /// <summary>
        /// Maximum without value for Lauftag (data &lt; this). Null to ignore.
        /// </summary>
        public int? LauftagMaxWo { get; set; }
        /// <summary>
        /// Minimum without value for Lauftag (data &gt; this). Null to ignore.
        /// </summary>
        public int? LauftagMinWo { get; set; }
        /// <summary>
        /// Maximum with value included for Lauftag (data &lt;= this). Null to ignore.
        /// </summary>
        public int? LauftagMax { get; set; }
        /// <summary>
        /// Minimum with value included for Lauftag (data &gt;= this). Null to ignore.
        /// </summary>
        public int? LauftagMin { get; set; }
        
        // Betrag
        /// <summary>
        /// Betrag equals this (data = this). Null to ignore.
        /// </summary>
        public int? Betrag { get; set; }
        /// <summary>
        /// Maximum without value for Betrag (data &lt; this). Null to ignore.
        /// </summary>
        public int? BetragMaxWo { get; set; }
        /// <summary>
        /// Minimum without value for Betrag (data &gt; this). Null to ignore.
        /// </summary>
        public int? BetragMinWo { get; set; }
        /// <summary>
        /// Maximum with value included for Betrag (data &lt;= this). Null to ignore.
        /// </summary>
        public int? BetragMax { get; set; }
        /// <summary>
        /// Minimum with value included for Betrag (data &gt;= this). Null to ignore.
        /// </summary>
        public int? BetragMin { get; set; }
        
        /// <summary>
        /// Constructor. Initializes the filter to pass all data (no filter criteria is active).
        /// </summary>
        public DauerauftragFilter()
        {
            this.Reset();
        }
        
        /// <summary>
        /// Resets the filter to filter out no rows.
        /// </summary>
        public void Reset()
        {
            ComboMode = OryxFilterCombinationEnum.And;
            IDBudget = null;
            Bezeichnung = null;
            BezeichnungPattern = null;
            GueltigAb = null;
            GueltigAbMaxWo = null;
            GueltigAbMinWo = null;
            GueltigAbMax = null;
            GueltigAbMin = null;
            GueltigBis = null;
            GueltigBisMaxWo = null;
            GueltigBisMinWo = null;
            GueltigBisMax = null;
            GueltigBisMin = null;
            Lauftag = null;
            LauftagMaxWo = null;
            LauftagMinWo = null;
            LauftagMax = null;
            LauftagMin = null;
            Betrag = null;
            BetragMaxWo = null;
            BetragMinWo = null;
            BetragMax = null;
            BetragMin = null;
        }
    }
}
