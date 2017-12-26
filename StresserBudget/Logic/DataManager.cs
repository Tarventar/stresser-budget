using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Logic.DataManagers;

namespace Logic
{
    public static class DataManager
    {
        private static DbFactory mDb;
        private static Lazy<BuchungDataManager> mBuchungDataManager = new Lazy<BuchungDataManager>(() => new BuchungDataManager(mDb));
        private static Lazy<BudgetDataManager> mBudgetDataManager = new Lazy<BudgetDataManager>(() => new BudgetDataManager(mDb));
        private static Lazy<DauerauftragDataManager> mDauerauftragDataManager = new Lazy<DauerauftragDataManager>(() => new DauerauftragDataManager(mDb));
        private static Lazy<KontoDataManager> mKontoDataManager = new Lazy<KontoDataManager>(() => new KontoDataManager(mDb));

        public static void Initialize(string aConnectionString)
        {
            mDb = new DbFactory(aConnectionString);
        }

        public static BuchungDataManager Buchung
        {
            get { return mBuchungDataManager.Value; }
        }

        public static BudgetDataManager Budget
        {
            get { return mBudgetDataManager.Value; }
        }

        public static DauerauftragDataManager Dauerauftrag
        {
            get { return mDauerauftragDataManager.Value; }
        }

        public static KontoDataManager Konto
        {
            get { return mKontoDataManager.Value; }
        }
    }
}
