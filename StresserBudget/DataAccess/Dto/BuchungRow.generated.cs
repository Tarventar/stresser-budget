using System;

namespace DataAccess.Dto
{
    /// <summary>
    /// Data transfer object for the table [Buchung]. Each instance represents one row on the database
    /// </summary>
    public partial class BuchungRow
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
        /// Gets or sets the value of field IDDauerauftrag / [IDDauerauftrag].
        /// </summary>
        public int IDDauerauftrag  { get; set; }
        /// <summary>
        /// Gets or sets the value of field Bezeichnung / [Bezeichnung].
        /// </summary>
        public string Bezeichnung  { get; set; }
        /// <summary>
        /// Gets or sets the value of field Datum / [Datum].
        /// </summary>
        public DateTime Datum  { get; set; }
        /// <summary>
        /// Gets or sets the value of field Betrag / [Betrag].
        /// </summary>
        public int Betrag  { get; set; }
        /// <summary>
        /// Gets or sets the value of field Bemerkung / [Bemerkung]. Null is allowed.
        /// </summary>
        public string Bemerkung { get; set; }
        /// <summary>
        /// Gets or sets the flag if the row represents a valid row or not.
        /// </summary>
        public bool IsNull { get; set; }
    }
}
