//using MLaunch.Core;
//using System.Collections.Generic;

//namespace MLaunch.Exp
//{
//    public interface IPlugin
//    {
//        string DisplayName { get; }
//    }

//    // "1 + 1" = [ 2 ]
//    // ">cmd" = prompt
//    // "d term" = ddg
//    // "time" = [ t1, t2, t3, ... ]
//    public interface ICommand : IPlugin
//    {
//        bool CanHandle(string term);

//        void Execute(string term);
//    }

//    // Image previews
//    // Audio player (custom QueryResult)
//    public interface IQueryResultPostProcessor : IPlugin
//    {
//        IList<QueryResult> PostProcess(IList<QueryResult> results);
//    }

//    public interface IQueryResultSource : IPlugin
//    {
//        IList<QueryResult> Query(string term);
//    }

//    public class SysMon : ICommand, IQueryResultSource
//    {
//        public SysMon()
//        {
//        }

//        public string DisplayName => "System Monitor";

//        public bool CanHandle(string term)
//        {
//            // "sysmon"
//            throw new System.NotImplementedException();
//        }

//        public void Execute(string term)
//        {
//            // Do nothing
//            throw new System.NotImplementedException();
//        }

//        public IList<QueryResult> Query(string term)
//        {
//            // CPU graph
//            // Mem graph
//            // Network graph

//            return null;
//        }
//    }

//    //public class SysMonGraphQueryResult : QueryResult
//    //{
//    //    public float[] GraphData { get; set; }

//    //    public override void Draw()
//    //    {
//    //        // Draw icon
//    //        // Draw name/description
//    //        // Draw graph
//    //    }
//    //}
//}