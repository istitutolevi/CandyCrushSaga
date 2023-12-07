using System;
using System.Threading;

namespace CandyCrushSaga.Utilities
{
    internal static class ThreadManager
    {
        internal static void RunAsync(Action action)
        {
            (new Thread(() => { action(); })).Start();
        }

        internal static bool RunUnsafeCode(Action tryaction, Action<Exception> catcherror = null, Action finallyaction = null)
        {
            if(tryaction != null)
            {
                var done = false;
                try
                {
                    tryaction();
                    done = true;
                }
                catch (Exception e)
                {
                    if (catcherror != null)
                        catcherror(e);

                    Logger.LogDebugMessage(e.Message);
                }
                finally
                {
                    if (finallyaction != null)
                        done = RunUnsafeCode(finallyaction);
                }
                return done;
            }
            return false;
        }
    }
}
