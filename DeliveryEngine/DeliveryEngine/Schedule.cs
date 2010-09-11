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
                
                ToHandleRegularCustomers();

                // please handle the new customer after regular customers
                // may have tiny issue for dealid in scanhistory table
                ToHandleNewCustomers();

                Console.WriteLine("### schedule");
                Thread.Sleep(5000);
                
            }
                    
        }

        // find all new came deals from last
        private List<Deals> GrabDeals(bool newDealsOnly)
        {
            //sp_getnewdeals
            return DBQuerys.GrabLatestDeals(newDealsOnly);
        }

        // for new customers, or keywords change
        //need grab history deals to match
        private void ToHandleNewCustomers()
        { 
            List<Customers> newCustomers = new List<Customers>();
            newCustomers = DBQuerys.GetAllCustomers(true);
         
            if (newCustomers.Count > 0)
            {
                List<Deals> historydeals = new List<Deals>();
                historydeals = GrabDeals(false);
                Process(historydeals, newCustomers,true);                    
            }
        }

        // for existing users,
        // only grab latest deal to delivery
        private void ToHandleRegularCustomers()
        {
            List<Deals> newdeals = new List<Deals>();
            newdeals = GrabDeals(true);

            if (newdeals.Count > 0)
            {
                List<Customers> activecustomers = new List<Customers>();
                activecustomers = DBQuerys.GetAllCustomers(false);
                Process(newdeals, activecustomers,false);
            }
        }

        private void Process(List<Deals> deals, List<Customers> users,bool newCust)
        {
            foreach (Deals deal in deals)
            {
                foreach (Customers tmpcust in users)
                {
                    // if deal. desc matchs tmpcust.keywords
                    if (DoMatch(deal.Title, tmpcust.KeyWords))
                    {
                        UpdateSchedule(tmpcust.CustomerID, deal.dealsID,newCust);
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
                if (desc.ToLower().Contains(word.ToLower()))
                {
                    return true;
                }            
            }

            return false;
        }

        private void UpdateSchedule(int customerID, int dealID, bool newcust)
        {
            Console.WriteLine("### update customer: "+ customerID.ToString()+" deal: "+dealID);
            DBQuerys.UpdateScheduleTable(customerID, dealID.ToString(),newcust);
            //TODO
            //if customer in dealsdeliveryschedule already
            // update dealids field with new dealID
            // if custoemr not in table yet
            // insert new one, need figure out next delivey time for this user.
            // realtime flag is useful
            // all logic in database

            // if new customer, need reset isnew flag in custoemr table
            // 
        
        }


    }
}
