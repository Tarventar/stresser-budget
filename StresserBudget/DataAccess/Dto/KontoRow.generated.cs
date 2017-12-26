using System;

namespace DataAccess.Dto
{
    /// <summary>
    /// Data transfer object for the table [Konto]. Each instance represents one row on the database
    /// </summary>
    public partial class KontoRow
    {
        /// <summary>
        /// Gets or sets the value of field ID / [ID].
        /// </summary>
        public int ID  { get; set; }
        /// <summary>
        /// Gets or sets the value of field Bezeichnung / [Bezeichnung].
        /// </summary>
        public string Bezeichnung  { get; set; }
        /// <summary>
        /// Gets or sets the flag if the row represents a valid row or not.
        /// </summary>
        public bool IsNull { get; set; }
    }
}
