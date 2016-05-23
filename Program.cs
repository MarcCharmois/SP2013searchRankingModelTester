using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using System.Diagnostics;
using Microsoft.Office.Server.Search.Query;
using System.Configuration;
using System.Globalization;

namespace SP2013SearchRankingModelTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string SPSiteAdress = ConfigurationManager.AppSettings["SPSiteAddress"].ToString();
            Console.WindowWidth = 150;
            Console.WindowHeight = 50;
            Console.WriteLine(SPSiteAdress);
     
            SPSite objSite = new SPSite(SPSiteAdress); //Replace with yours 
            SPWeb objTargetWeb = objSite.OpenWeb(SPSiteAdress);
            ResultType resultType = ResultType.RelevantResults;
          

            Console.WriteLine("type your query....");
            string strQuery = Console.ReadLine();

            Console.WriteLine("type your ranking model ID....");
            string rankikgModelId = Console.ReadLine();

            KeywordQuery keywordQuery = new KeywordQuery(objSite);
            SearchExecutor searchExecutor = new SearchExecutor();
            keywordQuery.QueryText = strQuery;
            keywordQuery.ResultTypes = resultType;
            //This is where we specify the custom ranking model to use. 
            keywordQuery.RankingModelId = rankikgModelId; 
            ResultTableCollection resultTableCollection = searchExecutor.ExecuteQuery(keywordQuery);
            ResultTable resultTable = resultTableCollection[resultType];
            int index = 0;
            string separator = "  ";
            while (resultTable.Read())
            {
                index++;
                if (index > 9)
                {
                    separator = " ";
                }
                Console.WriteLine(index + separator + "Rank: " + String.Format("{0:0.00000}", resultTable["RANK"]) + " Title: " + resultTable["TITLE"].ToString());
            }
             
            Console.ReadLine();
        }
    }
}
