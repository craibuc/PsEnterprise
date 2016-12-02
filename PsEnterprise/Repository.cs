using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CrystalDecisions.Enterprise;
using CrystalDecisions.Sdk.Uri;
using System.Collections;
using System.Diagnostics;

namespace PsEnterprise
{
    public class Repository
    {

        /// <summary>
        /// Generate a logon token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="server"></param>
        /// <param name="authentication"></param>
        /// <returns></returns>
        public static string GetToken(string username, string password, string server, string authentication)
        {

            EnterpriseSession session = null;

            try
            {

                SessionMgr sessionMgr = new SessionMgr();
                session = sessionMgr.Logon(username, password, server, authentication);
                LogonTokenMgr logonTokenMgr = session.LogonTokenMgr;

                //The token generated below is good on any client for an unlimited number of logins for 24 hours
                return logonTokenMgr.CreateLogonTokenEx("", 1440, -1);

            }
            catch (Exception e)
            {
                throw e;
            }
        
        } // method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static EnterpriseSession GetSession(string token)
        {
            try
            {
                SessionMgr sessionMgr = new SessionMgr();
                return sessionMgr.LogonWithToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }

        } // method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static InfoObjects GetInfoObjects(EnterpriseSession session, string query)
        {

            try
            {
                InfoStore infoStore = (InfoStore)session.GetService("InfoStore");
                return infoStore.Query(query);
            }
            catch (Exception e)
            {
                throw e;
            }

        } // method

        /// <summary>
        /// Convert a URI query (e.g. page://InfoObjects/Root Folder/) into one or more SQL queries (e.g SELECT * FROM ci_infoobjects).
        /// http://www.forumtopics.com/busobj/viewtopic.php?p=801889&sid=d75db3b6ce7d5bf42ba76fa04ae4f8e8
        /// http://bukhantsov.org/2013/06/cms-uri-queries/
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query">A query that begins with cuid://,path://,query://,search://</param>
        /// <param name="pageSize">A value < 1 specifies no paging</param>
        /// <returns>string array of SQL queries</returns>
        public static string[] ResolveUriQuery(EnterpriseSession session, string query, int pageSize = 0)
        {
            List<string> queries = new List<string>();

            try
            {
                //string query = "query://{select SI_ID from ci_infoobjects where si_kind ='FullClient'}";

                PagingQueryOptions options = new PagingQueryOptions();
                options.IsIncremental = (false);

                //A page size less than 1 specifies no paging.
                //options.PageSize = pageSize;
                Debug.WriteLine(options.PageSize);

                InfoStore infoStore = (InfoStore)session.GetService("InfoStore");

                IPageResult pageResult = PageFactoryFacade.PageResult(infoStore, query, options);

                IEnumerator pageResultIter = pageResult.Enumerator;

                while (pageResultIter.MoveNext())
                {
                    String pagequery = (String)pageResultIter.Current;
                    IStatelessPageInfo pageInfo = PageFactoryFacade.FetchPage(infoStore, pagequery, options);

                    queries.Add(pageInfo.PageSQL);

                    Debug.WriteLine("SQL: " + pageInfo.PageSQL);
                }

                return queries.ToArray();

            }
            catch (Exception e)
            {
                throw e;
            }

        } // method

    } // class

} // namespace
