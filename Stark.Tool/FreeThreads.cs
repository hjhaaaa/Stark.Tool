using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Stark.Tool
{   
    /// <summary>
    /// 自由线程类
    /// </summary>
    public class FreeThreads
    {
        public FreeThreads(int Count)
        {
            manual = new ManualResetEvent[Count];
            for (int i = 0; i < manual.Length; i++) {
                manual[i] = new ManualResetEvent(true);
            }
        }
        ManualResetEvent[] manual;
        public void Do<T>(Action<T> action,T Parm)
        {
            int i = WaitHandle.WaitAny(manual);
            manual[i].Reset();
            Thread t = new Thread(() => {
                try {
                    action(Parm);
                } catch (Exception ex) {

                } finally {
                    manual[i].Set();
                }

            });
        }
    }
}
