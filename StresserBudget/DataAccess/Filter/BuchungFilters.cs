using System;
using DataAccess;

namespace DataAccess.Filter
{
    /// <summary>
    /// An instance of this class is a filter to access rows from [Buchung].
    /// </summary>
    public class BuchungFilter
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
        
        // IDDauerauftrag
        /// <summary>
        /// IDDauerauftrag equals this (data = this). Null to ignore.
        /// </summary>
        public int? IDDauerauftrag { get; set; }
        /// <summary>
        /// Minimum without value for IDDauerauftrag (data &gt; this). Null to ignore.
        /// </summary>
        public int? IDDauerauftragMinWo { get; set; }
        
        // Bezeichnung
        /// <summary>
        /// Bezeichnung equals this (data = this). Null to ignore.
        /// </summary>
        public string Bezeichnung { get; set; }
        /// <summary>
        /// Bezeichnung is like this (data like this). Null to ignore.
        /// </summary>
        public string BezeichnungPattern { get; set; }
        
        // Datum
        /// <summary>
        /// Datum equals this (data = this). Null to ignore.
        /// </summary>
        public DateTime? Datum { get; set; }
        /// <summary>
        /// Maximum without value for Datum (data &lt; this). Null to ignore.
        /// </summary>
        public DateTime? DatumMaxWo { get; set; }
        /// <summary>
        /// Minimum without value for Datum (data &gt; this). Null to ignore.
        /// </summary>
        public DateTime? DatumMinWo { get; set; }
        /// <summary>
        /// Maximum with value included for Datum (data &lt;= this). Null to ignore.
        /// </summary>
        public DateTime? DatumMax { get; set; }
        /// <summary>
        /// Minimum with value included for Datum (data &gt;= this). Null to ignore.
        /// </summary>
        public DateTime? DatumMin { get; set; }
        
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
        
        // Bemerkung
        /// <summary>
        /// Bemerkung equals this (data = this). Null to ignore.
        /// </summary>
        public string Bemerkung { get; set; }
        /// <summary>
        /// Bemerkung is like this (data like this). Null to ignore.
        /// </summary>
        public string BemerkungPattern { get; set; }
        /// <summary>
        /// True: Bemerkung IS NULL; False: Bemerkung IS NOT NULL. Null to ignore.
        /// </summary>
        public bool? BemerkungIsNull { get; set; }
        
        /// <summary>
        /// Constructor. Initializes the filter to pass all data (no filter criteria is active).
        /// </summary>
        public BuchungFilter()
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
            IDDauerauftrag = null;
            IDDauerauftragMinWo = null;
            Bezeichnung = null;
            BezeichnungPattern = null;
            Datum = null;
            DatumMaxWo = null;
            DatumMinWo = null;
            DatumMax = null;
            DatumMin = null;
            Betrag = null;
            BetragMaxWo = null;
            BetragMinWo = null;
            BetragMax = null;
            BetragMin = null;
            Bemerkung = null;
            BemerkungPattern = null;
            BemerkungIsNull = null;
        }
    }
}
