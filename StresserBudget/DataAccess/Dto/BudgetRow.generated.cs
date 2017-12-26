using System;

namespace DataAccess.Dto
{
    /// <summary>
    /// Data transfer object for the table [Budget]. Each instance represents one row on the database
    /// </summary>
    public partial class BudgetRow
    {
        /// <summary>
        /// Gets or sets the value of field ID / [ID].
        /// </summary>
        public int ID  { get; set; }
        /// <summary>
        /// Gets or sets the value of field IDKonto / [IDKonto].
        /// </summary>
        public int IDKonto  { get; set; }
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
