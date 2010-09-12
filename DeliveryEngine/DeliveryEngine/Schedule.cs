using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace DeliveryEngine
{
    class Schedule
    {
        // 
        public void Start()
        {
            
            while (true)
            {
                
                List<Deals> newdeals = new List<Deals>();
                newdeals = GrabNewDeals();

                if( newdeals.Count > 0)
                    ToSchedule(newdeals);
                
                Console.WriteLine("### schedule");
                Thread.Sleep(5000);
                
            }
                    
        }

        // find all new came deals from last
        private List<Deals> GrabNewDeals()
        {
            //sp_getnewdeals
            return DBQuerys.GrabLatestDeals();
        }


        private void ToSchedule(List<Deals> newdeals)
        {
            // please considering put this in memory, so don't need to query db everytime.

            //TODO
            List<Customers> activecustomers = new List<Customers>();
            activecustomers = DBQuerys.GetAllCustomers();
                        
            foreach (Deals deal in newdeals)
            {
                foreach (Customers tmpcust in activecustomers)
                { 
                    // if deal. desc matchs tmpcust.keywords
                    if (DoMatch(deal.Title, tmpcust.KeyWords))
                    {
                        UpdateSchedule(tmpcust.CustomerID, deal.dealsID);                        
                    }                    
                }            
            }    
        }

        private bool DoMatch(string desc, string keywords)
        {
            string[] keywordsarray = keywords.Split(',');

            // may use match rate control 
            // if user has 4 keywords, will regard it as a match if two keywords matches?

            foreach (string word in keywordsarray)
            {
                if (desc.Contains(word))
                {
                    return true;
                }            
            }

            return false;
        }

        private void UpdateSchedule(int customerID, int dealID)
        {
            Console.WriteLine("### update customer: "+ customerID.ToString()+" deal: "+dealID);
            DBQuerys.UpdateScheduleTable(customerID, dealID.ToString());
            //TODO
            //if customer in dealsdeliveryschedule already
            // update dealids field with new dealID
            // if custoemr not in table yet
            // insert new one, need figure out next delivey time for this user.
            // realtime flag is useful
            // all logic in database
        
        }


    }
}
