using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
namespace DeliveryEngine
{
    // 9.12.2010 Li
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
                Console.ForegroundColor = ConsoleColor.Blue;
                
                Console.WriteLine("### schedule     "+DateTime.Now.ToLongTimeString());
                Thread.Sleep(30000);
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
                string tmpids = "";
                foreach (Customers tmpcust in users) 
                {
                    foreach (Deals deal in deals)
                    {
                        // if deal. desc matchs tmpcust.keywords
                        if (DoMatch(deal.Title, tmpcust.KeyWords))         
                        {
                            tmpids += deal.dealsID+",";
                        }
                        
                    }
                    // need check.
                    if (tmpids.Length > 0)
                    {
                        UpdateSchedule(tmpcust.CustomerID, tmpids.Trim(','), newCust, tmpcust.KeyWords);
                        tmpids = "";
                    }
                }
        }
        

        private bool DoMatch(string desc, string keywords)
        {
            string andsplit = "~^~";
            string[] andsplitarray = { andsplit };

            string exlcudesplit = "!^!";
            string[] exlcudesplitarray = { exlcudesplit };

            string[] keywordsarray = keywords.Split(',');
            // todo, need do further split ....

            // may use match rate control 
            // if user has 4 keywords, will regard it as a match if two keywords matches?
            
            foreach (string word in keywordsarray)
            {
                // if combine keywords, need all keywords show up there.
                //ex: HP && laptop
                if (word.Contains(andsplit) && !word.Contains(exlcudesplit))
                {                   
                    string[] subkeysarray =  word.Split(andsplitarray,StringSplitOptions.RemoveEmptyEntries);
                    
                    foreach(string subword in subkeysarray){
                        if (!desc.ToLower().Contains(subword.ToLower()))
                        {
                            return false;
                        }
                    }
                    return true;
                } // some must be in, but if another word is in, exclude.
                    //ex: free !! shipping
                else if (word.Contains(exlcudesplit) && !word.Contains(andsplit))
                {
                    string[] subkeysarray = word.Split(exlcudesplitarray, StringSplitOptions.RemoveEmptyEntries);

                    if (!desc.ToLower().Contains(subkeysarray[0].ToLower())) // the first one is keyword wanted
                    {
                        return false;
                    }
                    for (int i = 1; i < subkeysarray.Length; i++ ) // others are keywords don't want
                    {
                        if (desc.ToLower().Contains(subkeysarray[i].ToLower()))
                        {
                            return false;
                        }                        
                    }
                    return true;

                } //ex: free && gift !! shipping !! ship
                else if (word.Contains(exlcudesplit) && word.Contains(andsplit))
                {
                    string[] subkeysarray = word.Split(exlcudesplitarray, StringSplitOptions.RemoveEmptyEntries);

                    string[] subsubkeyarray =  subkeysarray[0].Split(andsplitarray,StringSplitOptions.RemoveEmptyEntries);// only the first one in the array is "&& keywords"
                    foreach (string subword in subsubkeyarray)   
                    {
                        if (!desc.ToLower().Contains(subword.ToLower()))  // if one needed keyword missing, not match
                        {
                            return false;
                        }
                    }

                    for (int i = 1; i < subkeysarray.Length; i++) // others are keywords don't want
                    {
                        if (desc.ToLower().Contains(subkeysarray[i].ToLower()))  // if one don't need exist, not match
                        {
                            return false;
                        }
                    }

                    return true;

                }
                    // without these two spliter, just regular single keyword...
                else if (desc.ToLower().Contains(word.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateSchedule(int customerID, string dealIDs, bool newcust,string keywords)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("### update customer: "+ customerID.ToString()+" deal: "+dealIDs);
            DBQuerys.UpdateScheduleTable(customerID, dealIDs, newcust, keywords);        
        }

    }
}
