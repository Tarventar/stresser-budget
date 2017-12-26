using System;
using DataAccess.Wrapper;

namespace DataAccess
{
    /// <summary>
    /// An instance of this class allows access to the tables and views.
    /// </summary>
    public partial class DbFactory
    {
        
        private string mConString;
        
        private WrapperKonto mKonto = null;
        private WrapperDauerauftrag mDauerauftrag = null;
        private WrapperBudget mBudget = null;
        private WrapperBuchung mBuchung = null;
        
        /// <summary>
        /// Constructor of this class.
        /// </summary>
        /// <param name="conString">The connection string to use</param>
        public DbFactory(string conString)
        {
            mConString = conString;
        }
        
        /// <summary>
        /// Gives access to the table [Konto].
        /// </summary>
        public WrapperKonto Konto
        {
            get
            {
                if (mKonto == null)
                    mKonto = new WrapperKonto(mConString);
                return mKonto;
            }
        }
        
        /// <summary>
        /// Gives access to the table [Dauerauftrag].
        /// </summary>
        public WrapperDauerauftrag Dauerauftrag
        {
            get
            {
                if (mDauerauftrag == null)
                    mDauerauftrag = new WrapperDauerauftrag(mConString);
                return mDauerauftrag;
            }
        }
        
        /// <summary>
        /// Gives access to the table [Budget].
        /// </summary>
        public WrapperBudget Budget
        {
            get
            {
                if (mBudget == null)
                    mBudget = new WrapperBudget(mConString);
                return mBudget;
            }
        }
        
        /// <summary>
        /// Gives access to the table [Buchung].
        /// </summary>
        public WrapperBuchung Buchung
        {
            get
            {
                if (mBuchung == null)
                    mBuchung = new WrapperBuchung(mConString);
                return mBuchung;
            }
        }
        
    }
}
