using System;

namespace DataAccess.Dto
{
    /// <summary>
    /// Data transfer object for the table [Dauerauftrag]. Each instance represents one row on the database
    /// </summary>
    public partial class DauerauftragRow
    {
        /// <summary>
        /// Gets or sets the value of field ID / [ID].
        /// </summary>
        public int ID  { get; set; }
        /// <summary>
        /// Gets or sets the value of field IDBudget / [IDBudget].
        /// </summary>
        public int IDBudget  { get; set; }
        /// <summary>
        /// Gets or sets the value of field Bezeichnung / [Bezeichnung].
        /// </summary>
        public string Bezeichnung  { get; set; }
        /// <summary>
        /// Gets or sets the value of field GueltigAb / [GueltigAb].
        /// </summary>
        public DateTime GueltigAb  { get; set; }
        /// <summary>
        /// Gets or sets the value of field GueltigBis / [GueltigBis].
        /// </summary>
        public DateTime GueltigBis  { get; set; }
        /// <summary>
        /// Gets or sets the value of field Lauftag / [Lauftag].
        /// </summary>
        public int Lauftag  { get; set; }
        /// <summary>
        /// Gets or sets the value of field Betrag / [Betrag].
        /// </summary>
        public int Betrag  { get; set; }
        /// <summary>
        /// Gets or sets the flag if the row represents a valid row or not.
        /// </summary>
        public bool IsNull { get; set; }
    }
}
